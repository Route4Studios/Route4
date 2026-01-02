using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Services.Discord;

/// <summary>
/// Real Discord.NET implementation of IDiscordBotService
/// Currently minimal implementation - can be expanded when ready to connect to real Discord
/// </summary>
public class RealDiscordBotService : IDiscordBotService
{
    private readonly DiscordSettings _settings;
    private readonly ILogger<RealDiscordBotService> _logger;
    private DiscordSocketClient? _client;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);
    private bool _isConnected = false;

    public RealDiscordBotService(DiscordSettings settings, ILogger<RealDiscordBotService> logger)
    {
        _settings = settings;
        _logger = logger;
        
        _logger.LogInformation("RealDiscordBotService initialized with token ending in ...{TokenSuffix}", 
            _settings.BotToken.Length > 6 ? _settings.BotToken[^6..] : "***");
    }

    private async Task<DiscordSocketClient> GetConnectedClientAsync(string botToken)
    {
        await _connectionLock.WaitAsync();
        try
        {
            // Reuse existing client if already connected
            if (_client != null && _isConnected && _client.ConnectionState == ConnectionState.Connected)
            {
                return _client;
            }

            // Dispose old client if exists
            if (_client != null)
            {
                await _client.StopAsync();
                await _client.DisposeAsync();
            }

            // Create new client with gateway intents
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds // Only need Guilds intent for channels/roles
            };
            _client = new DiscordSocketClient(config);
            
            // Set up ready event
            var tcs = new TaskCompletionSource<bool>();
            _client.Ready += () =>
            {
                _isConnected = true;
                tcs.TrySetResult(true);
                return Task.CompletedTask;
            };

            _client.Log += LogAsync;

            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            
            // Wait for ready with timeout
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(15));
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);
            
            if (completedTask == timeoutTask)
            {
                _logger.LogWarning("Discord client connection timed out after 15 seconds");
                _isConnected = false;
                throw new TimeoutException("Discord connection timed out");
            }
            
            _logger.LogInformation("Discord client connected successfully");
            return _client;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Discord client connection");
            _isConnected = false;
            throw;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private Task LogAsync(LogMessage log)
    {
        var severity = log.Severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Trace,
            LogSeverity.Debug => LogLevel.Debug,
            _ => LogLevel.Information
        };
        
        _logger.Log(severity, log.Exception, "[Discord.NET] {Message}", log.Message);
        return Task.CompletedTask;
    }

    public async Task<bool> ValidateServerAccessAsync(string guildId, string botToken)
    {
        _logger.LogInformation("Validating access to Discord server {GuildId} with real bot", guildId);
        
        try
        {
            var client = await GetConnectedClientAsync(botToken);
            
            try
            {
                var guild = client.GetGuild(ulong.Parse(guildId));
                bool hasAccess = guild != null;
                
                if (!hasAccess)
                {
                    _logger.LogWarning("Bot does not have access to guild {GuildId}", guildId);
                }
                else
                {
                    _logger.LogInformation("Successfully validated access to guild {GuildName} ({GuildId})", guild.Name, guildId);
                }
                
                return hasAccess;
            }
            finally
            {
                await client.StopAsync();
                await client.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate Discord server access for guild {GuildId}", guildId);
            return false;
        }
    }

    public async Task<string> CreateServerAsync(string serverName, string? iconUrl = null)
    {
        _logger.LogError("Discord API Limitation: Bots cannot create servers. Server '{ServerName}' must be created manually.", serverName);
        
        throw new InvalidOperationException(
            "Discord bots cannot create servers via API. " +
            "Please create the server manually on discord.com and invite the bot, " +
            "then use the 'ExistingGuildId' parameter in your provisioning request.");
    }

    public async Task<DiscordChannelResult> ProvisionChannelTemplatesAsync(string guildId, DiscordChannelTemplateSet templates)
    {
        _logger.LogInformation("Provisioning channel templates for guild {GuildId} using real Discord API", guildId);
        
        try
        {
            var client = await GetConnectedClientAsync(_settings.BotToken);
            
            try
            {
                await Task.Delay(2000);
                
                var guild = client.GetGuild(ulong.Parse(guildId));
                if (guild == null)
                {
                    return new DiscordChannelResult { Success = false, ErrorMessage = "Guild not found" };
                }
                
                var createdChannels = new List<DiscordChannelMapping>();
                var allTemplates = templates.Orientation.Concat(templates.Releases).Concat(templates.Reflection)
                    .Concat(templates.Residue).Concat(templates.Invitations).Concat(templates.Process).Concat(templates.Private).ToList();
                
                foreach (var template in allTemplates)
                {
                    var existing = guild.Channels.FirstOrDefault(c => c.Name.Equals(template.Name, StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        createdChannels.Add(new DiscordChannelMapping { Purpose = template.Purpose, ChannelId = existing.Id.ToString(), Name = existing.Name });
                        continue;
                    }
                    
                    var channel = await guild.CreateTextChannelAsync(template.Name, props => { props.Topic = template.Description ?? template.Purpose; });
                    createdChannels.Add(new DiscordChannelMapping { Purpose = template.Purpose, ChannelId = channel.Id.ToString(), Name = channel.Name });
                    await Task.Delay(500);
                }
                
                return new DiscordChannelResult { Success = true, Channels = createdChannels };
            }
            finally
            {
                await client.StopAsync();
                await client.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to provision channels");
            return new DiscordChannelResult { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<bool> LockChannelAsync(string guildId, string channelId, string botToken)
    {
        _logger.LogInformation("Locking channel {ChannelId} in guild {GuildId} using Discord API", channelId, guildId);
        // TODO: Implement real channel locking using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> UnlockChannelAsync(string guildId, string channelId, string botToken)
    {
        _logger.LogInformation("Unlocking channel {ChannelId} in guild {GuildId} using Discord API", channelId, guildId);
        // TODO: Implement real channel unlocking using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> SetSlowModeAsync(string guildId, string channelId, int seconds, string botToken)
    {
        _logger.LogInformation("Setting slow mode {Seconds}s on channel {ChannelId} using Discord API", seconds, channelId);
        // TODO: Implement real slow mode using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> SendMessageAsync(string guildId, string channelId, string message, string botToken)
    {
        _logger.LogInformation("Sending message to channel {ChannelId} using Discord API", channelId);
        // TODO: Implement real message sending using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> SetChannelReadOnlyAsync(string guildId, string channelId, bool readOnly)
    {
        _logger.LogInformation("Setting channel {ChannelId} read-only: {ReadOnly} using Discord API", channelId, readOnly);
        // TODO: Implement real channel read-only setting using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<DiscordRoleResult> CreateRolesAsync(string guildId, DiscordRoleTemplateSet roleTemplateSet)
    {
        _logger.LogInformation("Creating roles for guild {GuildId} using Discord API", guildId);
        
        try
        {
            var client = await GetConnectedClientAsync(_settings.BotToken);
            
            try
            {
                await Task.Delay(2000);
                
                var guild = client.GetGuild(ulong.Parse(guildId));
                if (guild == null)
                {
                    return new DiscordRoleResult { Success = false, ErrorMessage = "Guild not found" };
                }
                
                var createdRoles = new List<DiscordRoleMapping>();
                
                foreach (var template in new[] { roleTemplateSet.CoreTeam, roleTemplateSet.Witness, roleTemplateSet.Member })
                {
                    var existing = guild.Roles.FirstOrDefault(r => r.Name.Equals(template.Name, StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        createdRoles.Add(new DiscordRoleMapping { Type = template.Type, RoleId = existing.Id.ToString(), Name = existing.Name });
                        continue;
                    }
                    
                    var role = await guild.CreateRoleAsync(template.Name, null, template.Color.HasValue ? new Color(template.Color.Value) : null, template.IsHoisted, template.IsMentionable);
                    createdRoles.Add(new DiscordRoleMapping { Type = template.Type, RoleId = role.Id.ToString(), Name = role.Name });
                    await Task.Delay(500);
                }
                
                return new DiscordRoleResult { Success = true, Roles = createdRoles };
            }
            finally
            {
                await client.StopAsync();
                await client.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create roles");
            return new DiscordRoleResult { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<bool> AssignRoleAsync(string guildId, string userId, string roleId)
    {
        _logger.LogInformation("Assigning role {RoleId} to user {UserId} using Discord API", roleId, userId);
        // TODO: Implement real role assignment using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> RemoveRoleAsync(string guildId, string userId, string roleId)
    {
        _logger.LogInformation("Removing role {RoleId} from user {UserId} using Discord API", roleId, userId);
        // TODO: Implement real role removal using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> OpenProcessRoomAsync(string guildId, string channelId, string[] allowedRoleIds)
    {
        _logger.LogInformation("Opening process room {ChannelId} for {RoleCount} roles using Discord API", channelId, allowedRoleIds.Length);
        // TODO: Implement real process room opening using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> CloseProcessRoomAsync(string guildId, string channelId)
    {
        _logger.LogInformation("Closing process room {ChannelId} using Discord API", channelId);
        // TODO: Implement real process room closure using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> StartReleaseWindowAsync(string guildId, ReleaseWindowConfig config)
    {
        _logger.LogInformation("Starting release window in guild {GuildId} using Discord API", guildId);
        // TODO: Implement real release window start using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> EndReleaseWindowAsync(string guildId, string channelId)
    {
        _logger.LogInformation("Ending release window {ChannelId} using Discord API", channelId);
        // TODO: Implement real release window end using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async Task<string> CreateTimedInviteAsync(string guildId, TimeSpan duration, int? maxUses = null)
    {
        _logger.LogInformation("Creating timed invite for guild {GuildId}, duration: {Duration} using Discord API", guildId, duration);
        // TODO: Implement real invite creation using Discord.NET
        await Task.Delay(100);
        return "https://discord.gg/real-invite-placeholder";
    }

    public async Task<bool> RevokeInviteAsync(string inviteCode)
    {
        _logger.LogInformation("Revoking invite {InviteCode} using Discord API", inviteCode);
        // TODO: Implement real invite revocation using Discord.NET
        await Task.Delay(100);
        return true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_client != null)
        {
            await _client.StopAsync();
            await _client.DisposeAsync();
            _client = null;
        }
        _connectionLock.Dispose();
    }
}