namespace Route4MoviePlug.Api.Models;

// Route4 Discord Admin DTOs
// Following the architecture: Client never needs to "learn Discord"

public class ClientDiscordIntakeDto
{
    public Guid ClientId { get; set; }
    public required string ClientName { get; set; }
    public DiscordConfigurationDto? CurrentConfiguration { get; set; }
}

public class DiscordConfigurationDto
{
    public string? GuildId { get; set; }
    public bool IsActive { get; set; }
    public string? LanguagePack { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class UpdateClientIntakeRequest
{
    public List<string>? CoreTeamMembers { get; set; }
    public string? LanguagePack { get; set; } = "default"; // default, mary, custom
    public string? ReleaseCadence { get; set; } = "episodic"; // episodic, seasonal, one-off
    public List<string>? EnabledRituals { get; set; }
    public string? ConfidentialitySensitivity { get; set; } = "medium"; // low, medium, high
}

public class ProvisionDiscordServerRequest
{
    public string? ExistingGuildId { get; set; } // null = create new server
    public required string BotToken { get; set; }
    public string? LanguagePack { get; set; } = "default";
}

public class ConfigureExistingServerRequest
{
    public required string GuildId { get; set; }
    public required string BotToken { get; set; }
    public string? LanguagePack { get; set; } = "default";
}

public class DiscordBotInvitationDto
{
    public required string ClientSlug { get; set; }
    public required string InvitationUrl { get; set; }
    public required string[] Instructions { get; set; }
}

public class DiscordProvisioningResult
{
    public bool Success { get; set; }
    public bool ServerCreated { get; set; }
    public string? GuildId { get; set; }
    public int ChannelsCreated { get; set; }
    public int RolesCreated { get; set; }
    public string? ErrorMessage { get; set; }
}

public class DiscordClientReviewDto
{
    public required string GuildId { get; set; }
    public List<DiscordChannelReviewItem> Channels { get; set; } = new();
    public List<DiscordRoleReviewItem> Roles { get; set; } = new();
}

public class DiscordChannelReviewItem
{
    public required string Purpose { get; set; }
    public required string CurrentName { get; set; }
    public required string VisibilityLevel { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsProcessRoom { get; set; }
}

public class DiscordRoleReviewItem
{
    public required string Type { get; set; }
    public required string CurrentName { get; set; }
}

public class ApplyDiscordReviewRequest
{
    public List<ChannelUpdateRequest> ChannelUpdates { get; set; } = new();
    public List<RoleUpdateRequest> RoleUpdates { get; set; } = new();
}

public class ChannelUpdateRequest
{
    public required string Purpose { get; set; }
    public string? NewName { get; set; }
}

public class RoleUpdateRequest
{
    public required string Type { get; set; }
    public string? NewName { get; set; }
}

public class DiscordGoLiveResult
{
    public bool Success { get; set; }
    public string? InviteCode { get; set; }
    public int ProcessChannelsLocked { get; set; }
    public string? ErrorMessage { get; set; }
}

// Release Cycle Management DTOs
public class ReleaseCycleTemplateDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int StageCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateReleaseCycleTemplateRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class ReleaseInstanceDto
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string CurrentStage { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateReleaseInstanceRequest
{
    public Guid TemplateId { get; set; }
    public required string Key { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}

// Extension methods for DTO conversion
public static class DiscordModelExtensions
{
    public static DiscordConfigurationDto? ToDtoIfExists(this DiscordConfiguration? config)
    {
        if (config == null) return null;
        
        return new DiscordConfigurationDto
        {
            GuildId = config.GuildId,
            IsActive = config.IsActive,
            LanguagePack = config.LanguagePack,
            CreatedAt = config.CreatedAt,
            UpdatedAt = config.UpdatedAt
        };
    }
}