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
    public string? ApplicationImageUrl { get; set; } // Bot/Application avatar image URL
    
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

/// <summary>
/// Ritual Mapping - Phase 4: Maps rituals to channels, visibility, and automation
/// Defines the behavior, automation, and Discord state for each ritual stage
/// </summary>
public class RitualMapping
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public Guid ReleaseCycleTemplateId { get; set; }
    public ReleaseCycleTemplate? ReleaseCycleTemplate { get; set; }
    
    public required string RitualName { get; set; } // "Signal", "Hold", "Drop", "Echo", "Fragments", etc.
    public required string StageType { get; set; } // Maps to ReleaseStageTemplate.Type
    public string? Description { get; set; }
    public int ExecutionOrder { get; set; }
    
    // Channel Mapping
    public required string TargetChannelPurpose { get; set; } // "signal", "releases", "reflection", "process", etc.
    public string? TargetChannelId { get; set; } // Populated from DiscordChannel after provisioning
    
    // Visibility & Access
    public required string VisibilityLevel { get; set; } // L0, L1, L2, L3
    public string[]? RequiredRoles { get; set; } // Roles that can access during this ritual
    public bool IsReadOnly { get; set; }
    
    // Timing & Duration
    public int? DefaultDurationHours { get; set; } // How long channel stays open
    public string? OpenTrigger { get; set; } // Manual, ScheduledTime, PreviousStageCompleted
    public string? CloseTrigger { get; set; } // Manual, DurationExpired, NextStageStart
    
    // Discord Automation
    public bool AutomaticallyUnlockChannel { get; set; } // Unlock on stage entry
    public bool AutomaticallyLockChannel { get; set; } // Lock on stage exit
    public string? SlowModeSeconds { get; set; } // Discord slow mode during reflection
    public bool DisableFileUploads { get; set; }
    public bool DisableExternalEmojis { get; set; }
    
    // Narrative/UX
    public string? AnnouncementMessage { get; set; } // Pinned message when channel opens
    public string? ClosingMessage { get; set; } // Message when channel closes
    public bool IsAnonymous { get; set; } // For Signal: hide poster identity
    
    // Sequencing
    public bool CanBeSkipped { get; set; }
    public string? NotesForAdmin { get; set; }
    
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Release Stage State Transition - Logs state transitions for audit and automation
/// </summary>
public class ReleaseStateTransition
{
    public Guid Id { get; set; }
    public Guid ReleaseInstanceId { get; set; }
    public ReleaseInstance? ReleaseInstance { get; set; }
    
    public required string FromStage { get; set; }
    public required string ToStage { get; set; }
    public required string TransitionReason { get; set; } // "Manual", "AutoAdvance", "SkipStage"
    
    public string? DiscordChannelsOpened { get; set; } // JSON array of channel IDs
    public string? DiscordChannelsLocked { get; set; } // JSON array of channel IDs
    public string? AnnouncementSent { get; set; } // Message sent to Discord
    
    public Guid? TriggeredBy { get; set; } // Admin user ID
    public DateTime OccurredAt { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Release State Machine - Defines allowed transitions and validates state progression
/// </summary>
public static class ReleaseStateMachine
{
    // Valid state transitions for Mary (and default for all clients)
    public static readonly Dictionary<string, HashSet<string>> ValidTransitions = new()
    {
        { "Draft", new() { "Scheduled", "Archived" } },
        { "Scheduled", new() { "Signal", "Archived" } },
        { "Signal", new() { "Process", "Archived" } },
        { "Process", new() { "Hold", "Archived" } },
        { "Hold", new() { "Drop", "Archived" } },
        { "Drop", new() { "Echo", "Archived" } },
        { "Echo", new() { "Fragments", "Archived" } },
        { "Fragments", new() { "Interval", "Archived" } },
        { "Interval", new() { "PrivateViewing", "Archived" } },
        { "PrivateViewing", new() { "Archive", "Archived" } },
        { "Archive", new() { "Archived" } },
        { "Archived", new() { } }
    };
    
    public static bool IsValidTransition(string fromStage, string toStage)
    {
        return ValidTransitions.TryGetValue(fromStage, out var allowedNextStates) &&
               allowedNextStates.Contains(toStage);
    }
    
    // Ritual stages (Mary-specific implementation)
    public static readonly List<string> MaryRitualSequence = new()
    {
        "Signal",       // Pre-Artifact (Anonymous song drop)
        "Process",      // Writing Table, Shot Council, etc.
        "Hold",         // Intentional silence
        "Drop",         // Primary Release
        "Echo",         // Reflection
        "Fragments",    // BTS residue
        "Interval",     // Podcast
        "PrivateViewing", // Witness-only
        "Archive"       // Permanent record
    };
}