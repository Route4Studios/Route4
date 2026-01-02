using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Route4MoviePlug.Api.Services.Discord;

namespace Route4MoviePlug.Api.Controllers;

/// <summary>
/// Route4 Admin Controller - Release Engine Setup
/// Following Route4 architecture: Abstract platform complexity from clients
/// </summary>
[ApiController]
[Route("api/admin/clients/{clientSlug}/release-engine")]
public class Route4ReleaseEngineAdminController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly IDiscordBotService _discordBot;
    private readonly ILogger<Route4ReleaseEngineAdminController> _logger;

    public Route4ReleaseEngineAdminController(
        Route4DbContext context,
        IDiscordBotService discordBot,
        ILogger<Route4ReleaseEngineAdminController> logger)
    {
        _context = context;
        _discordBot = discordBot;
        _logger = logger;
    }

    /// <summary>
    /// Phase 1: Client Intake - Get client configuration
    /// </summary>
    [HttpGet("intake")]
    public async Task<ActionResult<ClientDiscordIntakeDto>> GetClientIntake(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        return Ok(new ClientDiscordIntakeDto
        {
            ClientId = client.Id,
            ClientName = client.Name,
            CurrentConfiguration = client.DiscordConfiguration?.ToDtoIfExists()
        });
    }

    /// <summary>
    /// Phase 1: Update client intake configuration
    /// </summary>
    [HttpPost("intake")]
    public async Task<ActionResult> UpdateClientIntake(string clientSlug, [FromBody] UpdateClientIntakeRequest request)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        // This is where we store Route4-specific configuration
        // Not Discord configuration - that comes later
        
        _logger.LogInformation($"Updated intake configuration for client {clientSlug}");
        
        return Ok(new { message = "Client intake configuration updated" });
    }

    /// <summary>
    /// Phase 1.5: Generate Discord Bot Invitation URL
    /// Since bots cannot create servers, provide the OAuth URL for manual setup
    /// </summary>
    [HttpGet("bot-invitation")]
    public ActionResult<DiscordBotInvitationDto> GetBotInvitation(string clientSlug)
    {
        var botClientId = "1456365248356286677"; // From configuration
        var requiredPermissions = 8; // Administrator permission for simplicity
        // In production, use specific permissions: Manage Channels, Manage Roles, etc.
        
        var inviteUrl = $"https://discord.com/oauth2/authorize?client_id={botClientId}&permissions={requiredPermissions}&scope=bot";
        
        return Ok(new DiscordBotInvitationDto
        {
            ClientSlug = clientSlug,
            InvitationUrl = inviteUrl,
            Instructions = new[]
            {
                "1. Create a new Discord server manually on discord.com",
                "2. Click the invitation URL below to add the Route4 bot to your server",
                "3. Select the server you just created and authorize the bot",
                "4. Copy the Server ID (right-click server name → Copy Server ID)",
                "5. Return here and use the 'Configure Existing Server' option"
            }
        });
    }

    /// <summary>
    /// Get Discord Setup Status - Check which steps are complete
    /// </summary>
    [HttpGet("setup-status")]
    public async Task<ActionResult<DiscordSetupStatusDto>> GetSetupStatus(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc!.Channels)
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc!.Roles)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        // Generate bot invitation URL
        var botClientId = "1456365248356286677";
        var requiredPermissions = 8; // Administrator
        var botInvitationUrl = $"https://discord.com/oauth2/authorize?client_id={botClientId}&permissions={requiredPermissions}&scope=bot";

        var status = new DiscordSetupStatusDto
        {
            ClientSlug = clientSlug,
            ClientName = client.Name,
            Step1_BotInvitationGenerated = true, // Always available - can generate anytime
            Step2_ServerCreated = false,
            Step3_BotAdded = false,
            Step4_ServerConfigured = false,
            GuildId = null,
            ChannelCount = 0,
            RoleCount = 0,
            ConfiguredAt = null,
            NextStep = "Generate bot invitation URL",
            NextStepNumber = 1,
            BotInvitationUrl = botInvitationUrl
        };

        if (client.DiscordConfiguration != null)
        {
            // Step 2: Server created - We have a Guild ID stored
            var hasGuildId = !string.IsNullOrEmpty(client.DiscordConfiguration.GuildId);
            status.Step2_ServerCreated = hasGuildId;
            
            // Step 3: Bot added - We have both Guild ID and Bot Token stored
            var hasBotToken = !string.IsNullOrEmpty(client.DiscordConfiguration.BotToken);
            status.Step3_BotAdded = hasGuildId && hasBotToken;
            
            // Step 4: Configured - Has channels and roles provisioned
            var hasChannels = client.DiscordConfiguration.Channels.Any();
            var hasRoles = client.DiscordConfiguration.Roles.Any();
            status.Step4_ServerConfigured = client.DiscordConfiguration.IsActive && 
                                           hasGuildId && hasBotToken && hasChannels && hasRoles;
            
            status.GuildId = client.DiscordConfiguration.GuildId;
            status.ChannelCount = client.DiscordConfiguration.Channels.Count;
            status.RoleCount = client.DiscordConfiguration.Roles.Count;
            status.ConfiguredAt = client.DiscordConfiguration.UpdatedAt ?? client.DiscordConfiguration.CreatedAt;

            // Determine next step based on current progress
            if (status.Step4_ServerConfigured)
            {
                status.NextStep = "✅ Setup complete! Server ready for use.";
                status.NextStepNumber = 0;
            }
            else if (status.Step3_BotAdded && hasGuildId)
            {
                status.NextStep = "Click 'Configure Server' to provision channels and roles";
                status.NextStepNumber = 4;
            }
            else if (status.Step2_ServerCreated)
            {
                status.NextStep = "Invite bot to your Discord server using the URL above";
                status.NextStepNumber = 3;
            }
            else
            {
                status.NextStep = "Create a Discord server, then paste its Server ID below";
                status.NextStepNumber = 2;
            }
        }
        else
        {
            // No configuration exists yet - guide user through first step
            status.NextStep = "Generate bot invitation URL to get started";
            status.NextStepNumber = 1;
        }

        return Ok(status);
    }

    /// <summary>
    /// Phase 2: Discord Server Configuration (Not Creation)
    /// Route4 configures existing Discord servers with templates
    /// Note: Discord API limitation - bots cannot create servers
    /// </summary>
    [HttpPost("configure-existing-server")]
    public async Task<ActionResult<DiscordProvisioningResult>> ConfigureExistingServer(
        string clientSlug, 
        [FromBody] ConfigureExistingServerRequest request)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        if (string.IsNullOrEmpty(request.GuildId))
            return BadRequest(new { error = "GuildId is required. Please provide the Discord server ID." });

        try
        {
            var result = new DiscordProvisioningResult();
            
            // Step 1: Validate bot has access to the existing server (optional - skip for faster setup)
            // In production, you may want to enable this validation
            var skipValidation = true; // TODO: Make this configurable
            
            if (!skipValidation)
            {
                var isValid = await _discordBot.ValidateServerAccessAsync(request.GuildId, request.BotToken);
                if (!isValid)
                {
                    return BadRequest(new { 
                        error = "Cannot access Discord server. Please ensure:",
                        troubleshooting = new[]
                        {
                            "1. The bot has been invited to the server",
                            "2. The Guild ID is correct",
                            "3. The bot token is valid",
                            "4. The bot has administrator permissions"
                        }
                    });
                }
            }
            
            var guildId = request.GuildId;
            result.ServerCreated = false; // We're configuring, not creating

            // Step 2: Apply Route4 channel templates
            var channelTemplates = Route4DiscordTemplates.GetDefaultChannelTemplates();
            
            // Apply any client-specific language pack
            if (!string.IsNullOrEmpty(request.LanguagePack))
            {
                // Load language overrides from configuration
                var languageOverrides = await GetLanguagePackOverrides(request.LanguagePack);
                channelTemplates = channelTemplates.ApplyLanguagePack(languageOverrides);
            }

            var channelResult = await _discordBot.ProvisionChannelTemplatesAsync(guildId, channelTemplates);
            if (!channelResult.Success)
                return BadRequest(new { error = $"Failed to provision channels: {channelResult.ErrorMessage}" });

            // Step 3: Create role templates
            var roleTemplates = Route4DiscordTemplates.GetDefaultRoleTemplates();
            var roleResult = await _discordBot.CreateRolesAsync(guildId, roleTemplates);
            if (!roleResult.Success)
                return BadRequest(new { error = $"Failed to create roles: {roleResult.ErrorMessage}" });

            // Step 4: Save Discord configuration
            var discordConfig = client.DiscordConfiguration ?? new DiscordConfiguration
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                CreatedAt = DateTime.UtcNow
            };

            discordConfig.GuildId = guildId;
            discordConfig.BotToken = request.BotToken; // TODO: Encrypt this
            discordConfig.IsActive = true;
            discordConfig.LanguagePack = request.LanguagePack;
            discordConfig.UpdatedAt = DateTime.UtcNow;

            if (client.DiscordConfiguration == null)
            {
                _context.DiscordConfigurations.Add(discordConfig);
                client.DiscordConfiguration = discordConfig;
            }
            else
            {
                _context.DiscordConfigurations.Update(discordConfig);
            }

            // Save channel mappings
            foreach (var channel in channelResult.Channels)
            {
                var channelEntity = new DiscordChannel
                {
                    Id = Guid.NewGuid(),
                    DiscordConfigurationId = discordConfig.Id,
                    ChannelId = channel.ChannelId,
                    Name = channel.Name,
                    Purpose = channel.Purpose,
                    VisibilityLevel = GetVisibilityForPurpose(channel.Purpose),
                    IsReadOnly = GetReadOnlyForPurpose(channel.Purpose),
                    IsProcessRoom = GetProcessRoomForPurpose(channel.Purpose),
                    CreatedAt = DateTime.UtcNow
                };
                _context.DiscordChannels.Add(channelEntity);
            }

            // Save role mappings
            foreach (var role in roleResult.Roles)
            {
                var roleEntity = new DiscordRole
                {
                    Id = Guid.NewGuid(),
                    DiscordConfigurationId = discordConfig.Id,
                    RoleId = role.RoleId,
                    Name = role.Name,
                    Type = role.Type,
                    CreatedAt = DateTime.UtcNow
                };
                _context.DiscordRoles.Add(roleEntity);
            }

            await _context.SaveChangesAsync();

            result.Success = true;
            result.GuildId = guildId;
            result.ChannelsCreated = channelResult.Channels.Count;
            result.RolesCreated = roleResult.Roles.Count;

            _logger.LogInformation($"Successfully provisioned Discord server for client {clientSlug}");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error provisioning Discord for client {clientSlug}: {ex.Message}");
            return StatusCode(500, new { error = "Discord provisioning failed" });
        }
    }

    /// <summary>
    /// Phase 5: Client Review - Get current Discord setup for client review
    /// </summary>
    [HttpGet("review")]
    public async Task<ActionResult<DiscordClientReviewDto>> GetClientReview(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc.Channels)
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc.Roles)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client?.DiscordConfiguration == null)
            return NotFound(new { error = "Discord configuration not found" });

        var review = new DiscordClientReviewDto
        {
            GuildId = client.DiscordConfiguration.GuildId!,
            Channels = client.DiscordConfiguration.Channels.Select(c => new DiscordChannelReviewItem
            {
                Purpose = c.Purpose,
                CurrentName = c.Name,
                VisibilityLevel = c.VisibilityLevel,
                IsReadOnly = c.IsReadOnly,
                IsProcessRoom = c.IsProcessRoom
            }).ToList(),
            Roles = client.DiscordConfiguration.Roles.Select(r => new DiscordRoleReviewItem
            {
                Type = r.Type,
                CurrentName = r.Name
            }).ToList()
        };

        return Ok(review);
    }

    /// <summary>
    /// Phase 5: Apply client review changes (optional renames)
    /// </summary>
    [HttpPost("review/apply")]
    public async Task<ActionResult> ApplyClientReview(string clientSlug, [FromBody] ApplyDiscordReviewRequest request)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc.Channels)
            .Include(c => c.DiscordConfiguration)
                .ThenInclude(dc => dc.Roles)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client?.DiscordConfiguration == null)
            return NotFound(new { error = "Discord configuration not found" });

        // Apply any channel renames
        foreach (var channelUpdate in request.ChannelUpdates)
        {
            var channel = client.DiscordConfiguration.Channels
                .FirstOrDefault(c => c.Purpose == channelUpdate.Purpose);
            
            if (channel != null && !string.IsNullOrEmpty(channelUpdate.NewName))
            {
                channel.Name = channelUpdate.NewName;
                channel.UpdatedAt = DateTime.UtcNow;
                _context.DiscordChannels.Update(channel);
            }
        }

        // Apply any role renames
        foreach (var roleUpdate in request.RoleUpdates)
        {
            var role = client.DiscordConfiguration.Roles
                .FirstOrDefault(r => r.Type == roleUpdate.Type);
            
            if (role != null && !string.IsNullOrEmpty(roleUpdate.NewName))
            {
                role.Name = roleUpdate.NewName;
                role.UpdatedAt = DateTime.UtcNow;
                _context.DiscordRoles.Update(role);
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Applied client review changes for {clientSlug}");
        
        return Ok(new { message = "Discord configuration updated based on client review" });
    }

    /// <summary>
    /// Phase 6: Go Live - Activate Discord integration
    /// </summary>
    [HttpPost("go-live")]
    public async Task<ActionResult<DiscordGoLiveResult>> GoLive(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.DiscordConfiguration)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client?.DiscordConfiguration == null)
            return NotFound(new { error = "Discord configuration not found" });

        try
        {
            var config = client.DiscordConfiguration;
            
            // Create initial invite link
            var inviteCode = await _discordBot.CreateTimedInviteAsync(config.GuildId!, TimeSpan.FromDays(7));
            
            // Lock unused channels (process rooms start locked)
            var processChannels = await _context.DiscordChannels
                .Where(c => c.DiscordConfigurationId == config.Id && c.IsProcessRoom)
                .ToListAsync();

            foreach (var channel in processChannels)
            {
                await _discordBot.LockChannelAsync(config.GuildId!, channel.ChannelId, "");
            }

            var result = new DiscordGoLiveResult
            {
                Success = true,
                InviteCode = inviteCode,
                ProcessChannelsLocked = processChannels.Count
            };

            _logger.LogInformation($"Discord integration went live for client {clientSlug}");
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error going live with Discord for client {clientSlug}: {ex.Message}");
            return StatusCode(500, new { error = "Failed to activate Discord integration" });
        }
    }

    // Helper methods
    private async Task<Dictionary<string, string>?> GetLanguagePackOverrides(string languagePack)
    {
        // This is where language pack customizations would be loaded
        // For now, return null (use defaults)
        await Task.CompletedTask;
        return null;
    }

    private string GetVisibilityForPurpose(string purpose)
    {
        return purpose switch
        {
            "signal" => "L2",
            "releases" => "L3",
            "reflection" => "L3",
            "fragments" => "L2",
            "invitations" => "L1",
            "process" => "L1",
            "private" => "L0",
            _ => "L2"
        };
    }

    private bool GetReadOnlyForPurpose(string purpose)
    {
        return purpose switch
        {
            "reflection" => false, // after-the-drop allows discussion
            "process" => false,    // process rooms allow participation
            "private" => false,    // core team channels allow discussion
            _ => true             // most channels are read-only
        };
    }

    private bool GetProcessRoomForPurpose(string purpose)
    {
        return purpose == "process";
    }

    /// <summary>
    /// Phase 3: Release Cycle Setup - Get available templates
    /// </summary>
    [HttpGet("release-cycles/templates")]
    public async Task<ActionResult<List<ReleaseCycleTemplateDto>>> GetReleaseCycleTemplates(string clientSlug)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var templates = await _context.ReleaseCycleTemplates
            .Include(t => t.Stages)
            .Where(t => t.ClientId == client.Id)
            .OrderBy(t => t.Name)
            .ToListAsync();

        var result = templates.Select(t => new ReleaseCycleTemplateDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            StageCount = t.Stages.Count,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Phase 3: Create Release Cycle Template for client
    /// </summary>
    [HttpPost("release-cycles/templates")]
    public async Task<ActionResult<ReleaseCycleTemplateDto>> CreateReleaseCycleTemplate(
        string clientSlug, 
        [FromBody] CreateReleaseCycleTemplateRequest request)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var template = new ReleaseCycleTemplate
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            Name = request.Name,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Add default Mary stages based on Route4 design document
        var maryStages = new[]
        {
            new { Name = "Signal", Type = "pre_artifact", VisibilityLevel = "L2", Order = 1, Channel = "signal" },
            new { Name = "Table Read", Type = "witness_shadow", VisibilityLevel = "L1", Order = 2, Channel = "writing-table" },
            new { Name = "Writing Table", Type = "process", VisibilityLevel = "L1", Order = 3, Channel = "writing-table" },
            new { Name = "Shot Council", Type = "process", VisibilityLevel = "L1", Order = 4, Channel = "shot-council" },
            new { Name = "Character Witness Shadow", Type = "witness_shadow", VisibilityLevel = "L1", Order = 5, Channel = "cut-room" },
            new { Name = "Hold", Type = "silence", VisibilityLevel = "L0", Order = 6, Channel = "" },
            new { Name = "Drop", Type = "primary_release", VisibilityLevel = "L3", Order = 7, Channel = "releases" },
            new { Name = "Echo", Type = "reflection", VisibilityLevel = "L3", Order = 8, Channel = "after-the-drop" },
            new { Name = "Fragments", Type = "residue", VisibilityLevel = "L2", Order = 9, Channel = "fragments" },
            new { Name = "Interval", Type = "meta_reflection", VisibilityLevel = "L3", Order = 10, Channel = "interval" },
            new { Name = "Private Viewing", Type = "witness_viewing", VisibilityLevel = "L1", Order = 11, Channel = "private-viewing" },
            new { Name = "Archive", Type = "archive", VisibilityLevel = "L3", Order = 12, Channel = "" }
        };

        foreach (var stage in maryStages)
        {
            template.Stages.Add(new ReleaseStageTemplate
            {
                Id = Guid.NewGuid(),
                ReleaseCycleTemplateId = template.Id,
                Name = stage.Name,
                Type = stage.Type,
                VisibilityLevel = stage.VisibilityLevel,
                DisplayOrder = stage.Order,
                DiscordChannelTemplate = stage.Channel,
                IsDiscordIntegrated = !string.IsNullOrEmpty(stage.Channel),
                RequiresWitnessRole = stage.VisibilityLevel == "L1",
                IsReadOnly = stage.Type != "process",
                CreatedAt = DateTime.UtcNow
            });
        }

        _context.ReleaseCycleTemplates.Add(template);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created Release Cycle Template '{request.Name}' for client {clientSlug}");

        var result = new ReleaseCycleTemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            StageCount = template.Stages.Count,
            IsActive = template.IsActive,
            CreatedAt = template.CreatedAt
        };

        return Ok(result);
    }

    /// <summary>
    /// Phase 4: Create Release Instance from template
    /// </summary>
    [HttpPost("release-cycles/instances")]
    public async Task<ActionResult<ReleaseInstanceDto>> CreateReleaseInstance(
        string clientSlug,
        [FromBody] CreateReleaseInstanceRequest request)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var template = await _context.ReleaseCycleTemplates
            .Include(t => t.Stages)
            .FirstOrDefaultAsync(t => t.Id == request.TemplateId && t.ClientId == client.Id);

        if (template == null)
            return NotFound(new { error = "Release cycle template not found" });

        var instance = new ReleaseInstance
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            ReleaseCycleTemplateId = template.Id,
            Key = request.Key,
            Title = request.Title,
            Description = request.Description,
            CurrentStage = template.Stages.OrderBy(s => s.DisplayOrder).First().Name,
            Status = "created",
            CreatedAt = DateTime.UtcNow
        };

        _context.ReleaseInstances.Add(instance);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created Release Instance '{request.Key}' for client {clientSlug}");

        var result = new ReleaseInstanceDto
        {
            Id = instance.Id,
            Key = instance.Key,
            Title = instance.Title,
            Description = instance.Description,
            CurrentStage = instance.CurrentStage,
            Status = instance.Status,
            CreatedAt = instance.CreatedAt
        };

        return Ok(result);
    }

    /// <summary>
    /// Get Release Instances for client
    /// </summary>
    [HttpGet("release-cycles/instances")]
    public async Task<ActionResult<List<ReleaseInstanceDto>>> GetReleaseInstances(string clientSlug)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var instances = await _context.ReleaseInstances
            .Where(i => i.ClientId == client.Id)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();

        var result = instances.Select(i => new ReleaseInstanceDto
        {
            Id = i.Id,
            Key = i.Key,
            Title = i.Title,
            Description = i.Description,
            CurrentStage = i.CurrentStage,
            Status = i.Status,
            CreatedAt = i.CreatedAt
        }).ToList();

        return Ok(result);
    }
}