namespace Route4MoviePlug.Api.Services.Discord;

/// <summary>
/// Discord Bot Service - Core Discord integration
/// Following Route4 architecture: Discord as execution layer, not product feature
/// </summary>
public interface IDiscordBotService
{
    // Server Management
    Task<bool> ValidateServerAccessAsync(string guildId, string botToken);
    Task<string> CreateServerAsync(string serverName, string? iconUrl = null);
    Task<bool> SetApplicationImageAsync(string botToken, string imageUrl);
    
    // Channel Management - Template-based provisioning
    Task<DiscordChannelResult> ProvisionChannelTemplatesAsync(string guildId, DiscordChannelTemplateSet templates);
    Task<bool> LockChannelAsync(string guildId, string channelId, string botToken);
    Task<bool> UnlockChannelAsync(string guildId, string channelId, string botToken);
    Task<bool> SetChannelReadOnlyAsync(string guildId, string channelId, bool readOnly);
    Task<bool> SetSlowModeAsync(string guildId, string channelId, int seconds, string botToken);
    Task<bool> SendMessageAsync(string guildId, string channelId, string message, string botToken);
    
    // Role Management
    Task<DiscordRoleResult> CreateRolesAsync(string guildId, DiscordRoleTemplateSet roles);
    Task<bool> AssignRoleAsync(string guildId, string userId, string roleId);
    Task<bool> RemoveRoleAsync(string guildId, string userId, string roleId);
    
    // Ritual Automation
    Task<bool> OpenProcessRoomAsync(string guildId, string channelId, string[] allowedRoleIds);
    Task<bool> CloseProcessRoomAsync(string guildId, string channelId);
    Task<bool> StartReleaseWindowAsync(string guildId, ReleaseWindowConfig config);
    Task<bool> EndReleaseWindowAsync(string guildId, string channelId);
    
    // Invitation Management
    Task<string> CreateTimedInviteAsync(string guildId, TimeSpan duration, int? maxUses = null);
    Task<bool> RevokeInviteAsync(string inviteCode);
}

public class DiscordChannelTemplateSet
{
    public required List<DiscordChannelTemplate> Orientation { get; set; }
    public required List<DiscordChannelTemplate> Releases { get; set; }
    public required List<DiscordChannelTemplate> Reflection { get; set; }
    public required List<DiscordChannelTemplate> Residue { get; set; }
    public required List<DiscordChannelTemplate> Invitations { get; set; }
    public required List<DiscordChannelTemplate> Process { get; set; }
    public required List<DiscordChannelTemplate> Private { get; set; }
}

public class DiscordChannelTemplate
{
    public required string Name { get; set; }
    public required string Purpose { get; set; } // signal, releases, reflection, etc.
    public required string VisibilityLevel { get; set; } // L0, L1, L2, L3
    public bool IsReadOnly { get; set; } = true;
    public bool IsProcessRoom { get; set; } = false;
    public string[]? AllowedRoleTypes { get; set; }
    public string? Description { get; set; }
}

public class DiscordRoleTemplateSet
{
    public required DiscordRoleTemplate CoreTeam { get; set; }
    public required DiscordRoleTemplate Witness { get; set; }
    public required DiscordRoleTemplate Member { get; set; }
}

public class DiscordRoleTemplate
{
    public required string Name { get; set; }
    public required string Type { get; set; } // CoreTeam, Witness, Member
    public string? Description { get; set; }
    public uint? Color { get; set; }
    public bool IsHoisted { get; set; } = false;
    public bool IsMentionable { get; set; } = false;
}

public class ReleaseWindowConfig
{
    public required string ChannelId { get; set; }
    public TimeSpan ReadOnlyDuration { get; set; } = TimeSpan.FromHours(24);
    public string[]? AllowedRoleIds { get; set; }
}

public class DiscordChannelResult
{
    public bool Success { get; set; }
    public List<DiscordChannelMapping> Channels { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

public class DiscordRoleResult
{
    public bool Success { get; set; }
    public List<DiscordRoleMapping> Roles { get; set; } = new();
    public string? ErrorMessage { get; set; }
}

public class DiscordChannelMapping
{
    public required string Purpose { get; set; }
    public required string ChannelId { get; set; }
    public required string Name { get; set; }
}

public class DiscordRoleMapping
{
    public required string Type { get; set; }
    public required string RoleId { get; set; }
    public required string Name { get; set; }
}