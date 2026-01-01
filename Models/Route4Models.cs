namespace Route4MoviePlug.Api.Models;

// Core Route4 Models - Aligned with Architecture Document

/// <summary>
/// Release Cycle Template - Defines the repeatable sequence of stages for a client
/// </summary>
public class ReleaseCycleTemplate
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<ReleaseStageTemplate> Stages { get; set; } = new List<ReleaseStageTemplate>();
}

/// <summary>
/// Release Stage Template - Individual stage definition in a release cycle
/// </summary>
public class ReleaseStageTemplate
{
    public Guid Id { get; set; }
    public Guid ReleaseCycleTemplateId { get; set; }
    public ReleaseCycleTemplate? ReleaseCycleTemplate { get; set; }
    
    public required string Name { get; set; } // e.g., "Signal", "Hold", "Drop", "Echo"
    public required string Type { get; set; } // e.g., "Pre-Artifact", "Process", "Primary Release", "Reflection"
    public required string VisibilityLevel { get; set; } // L0, L1, L2, L3
    public int DisplayOrder { get; set; }
    public bool IsDiscordIntegrated { get; set; }
    public string? DiscordChannelTemplate { get; set; }
    public bool RequiresWitnessRole { get; set; }
    public int? DurationHours { get; set; }
    public bool IsReadOnly { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Release Instance - Actual execution of a release cycle
/// </summary>
public class ReleaseInstance
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public Guid ReleaseCycleTemplateId { get; set; }
    public ReleaseCycleTemplate? ReleaseCycleTemplate { get; set; }
    
    public required string Key { get; set; } // e.g., "S1E1", "PILOT"
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string CurrentStage { get; set; } // Current stage name
    public string Status { get; set; } = "Draft"; // Draft, Scheduled, Active, Completed, Archived
    
    public DateTime? ScheduledStartAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<ReleaseStageExecution> StageExecutions { get; set; } = new List<ReleaseStageExecution>();
    public ICollection<ReleaseArtifact> Artifacts { get; set; } = new List<ReleaseArtifact>();
    public ICollection<WitnessEvent> WitnessEvents { get; set; } = new List<WitnessEvent>();
}

/// <summary>
/// Release Stage Execution - Tracking of individual stage execution
/// </summary>
public class ReleaseStageExecution
{
    public Guid Id { get; set; }
    public Guid ReleaseInstanceId { get; set; }
    public ReleaseInstance? ReleaseInstance { get; set; }
    
    public required string StageName { get; set; }
    public required string Status { get; set; } // Pending, Active, Completed, Skipped
    public DateTime? ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Release Artifact - Media and content artifacts for releases
/// </summary>
public class ReleaseArtifact
{
    public Guid Id { get; set; }
    public Guid ReleaseInstanceId { get; set; }
    public ReleaseInstance? ReleaseInstance { get; set; }
    
    public required string Kind { get; set; } // raw, proxy, fragment, process_preview, release_public, etc.
    public required string VisibilityLevel { get; set; } // L0, L1, L2, L3
    public required string Stage { get; set; } // Associated stage name
    
    public required string FileName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long? FileSizeBytes { get; set; }
    public string? ContentType { get; set; }
    public string? StorageProvider { get; set; } // frameio, local, etc.
    public string? ProviderAssetId { get; set; }
    public string? PlaybackEmbedUrl { get; set; }
    public string? DownloadUrl { get; set; }
    public bool IsAnonymous { get; set; } // For Signal artifacts
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Witness Event - Tracking of witness participation and presence
/// </summary>
public class WitnessEvent
{
    public Guid Id { get; set; }
    public Guid ReleaseInstanceId { get; set; }
    public ReleaseInstance? ReleaseInstance { get; set; }
    public Guid? UserId { get; set; } // Nullable for anonymous witnessing
    
    public required string EventType { get; set; } // "Presence", "Invitation", "Participation"
    public required string Stage { get; set; }
    public string? DiscordUserId { get; set; }
    public string? DiscordChannelId { get; set; }
    
    public DateTime OccurredAt { get; set; }
    public string? Metadata { get; set; } // JSON for additional context
}

/// <summary>
/// Discord Configuration - Client's Discord integration settings
/// </summary>
public class DiscordConfiguration
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    
    public string? GuildId { get; set; } // Discord Server ID
    public string? BotToken { get; set; } // Encrypted
    public bool IsActive { get; set; }
    public string? LanguagePack { get; set; } // Custom terminology
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<DiscordChannel> Channels { get; set; } = new List<DiscordChannel>();
    public ICollection<DiscordRole> Roles { get; set; } = new List<DiscordRole>();
}

/// <summary>
/// Discord Channel - Channel configuration and mapping
/// </summary>
public class DiscordChannel
{
    public Guid Id { get; set; }
    public Guid DiscordConfigurationId { get; set; }
    public DiscordConfiguration? DiscordConfiguration { get; set; }
    
    public required string ChannelId { get; set; } // Discord Channel ID
    public required string Name { get; set; }
    public required string Purpose { get; set; } // signal, releases, reflection, process, etc.
    public required string VisibilityLevel { get; set; } // L0, L1, L2, L3
    public bool IsReadOnly { get; set; }
    public bool IsProcessRoom { get; set; } // Locked except during sessions
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Discord Role - Role configuration and mapping
/// </summary>
public class DiscordRole
{
    public Guid Id { get; set; }
    public Guid DiscordConfigurationId { get; set; }
    public DiscordConfiguration? DiscordConfiguration { get; set; }
    
    public required string RoleId { get; set; } // Discord Role ID
    public required string Name { get; set; }
    public required string Type { get; set; } // CoreTeam, Witness, Member
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}