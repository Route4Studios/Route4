using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Route4MoviePlug.Api.Services.Discord;

namespace Route4MoviePlug.Api.Services;

/// <summary>
/// Release Management Service - Orchestrates release state transitions and Discord automation
/// Implements Phase 4: Ritual Mapping and state machine logic
/// </summary>
public interface IReleaseManagementService
{
    Task<Result<ReleaseInstance>> CreateReleaseAsync(string clientSlug, CreateReleaseRequest request);
    Task<Result<ReleaseInstance>> AdvanceReleaseStageAsync(Guid releaseId, string targetStage, string? notes = null);
    Task<Result<RitualMapping>> GetRitualMappingAsync(Guid clientId, string ritualName);
    Task<Result<List<ReleaseStateTransition>>> GetReleaseHistoryAsync(Guid releaseId);
    Task<bool> IsValidTransitionAsync(Guid releaseId, string targetStage);
}

/// <summary>
/// Generic result wrapper for service operations
/// </summary>
public class Result<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public static Result<T> Ok(T data) => new() { Success = true, Data = data };
    public static Result<T> Error(string message) => new() { Success = false, ErrorMessage = message };
}

/// <summary>
/// DTOs for Release Management
/// </summary>
public class CreateReleaseRequest
{
    public required string ReleaseKey { get; set; } // e.g., "S1E1"
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ScheduledStartAt { get; set; }
}

public class AdvanceReleaseStageRequest
{
    public required string TargetStage { get; set; }
    public string? AnnouncementMessage { get; set; }
    public string? Notes { get; set; }
}

public class ReleaseStateDto
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Title { get; set; }
    public required string CurrentStage { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ScheduledStartAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public List<RitualMappingDto> AvailableNextStages { get; set; } = new();
}

public class RitualMappingDto
{
    public Guid Id { get; set; }
    public required string RitualName { get; set; }
    public required string StageType { get; set; }
    public required string TargetChannelPurpose { get; set; }
    public required string VisibilityLevel { get; set; }
    public int? DefaultDurationHours { get; set; }
    public string? AnnouncementMessage { get; set; }
    public bool IsReadOnly { get; set; }
}

public class ReleaseHistoryDto
{
    public Guid TransitionId { get; set; }
    public required string FromStage { get; set; }
    public required string ToStage { get; set; }
    public required string Reason { get; set; }
    public DateTime OccurredAt { get; set; }
    public string? Notes { get; set; }
    public int? DiscordChannelsAffected { get; set; }
}

public class ReleaseManagementService : IReleaseManagementService
{
    private readonly Route4DbContext _context;
    private readonly IDiscordBotService _discordBot;
    private readonly ILogger<ReleaseManagementService> _logger;

    public ReleaseManagementService(
        Route4DbContext context,
        IDiscordBotService discordBot,
        ILogger<ReleaseManagementService> logger)
    {
        _context = context;
        _discordBot = discordBot;
        _logger = logger;
    }

    /// <summary>
    /// Create a new release instance from a release cycle template
    /// </summary>
    public async Task<Result<ReleaseInstance>> CreateReleaseAsync(string clientSlug, CreateReleaseRequest request)
    {
        try
        {
            var client = await _context.Clients
                .Include(c => c.DiscordConfiguration)
                .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

            if (client == null)
                return Result<ReleaseInstance>.Error($"Client '{clientSlug}' not found");

            // Get the default/only release cycle template for the client
            var cycleTpl = await _context.ReleaseCycleTemplates
                .Include(t => t.Stages)
                .FirstOrDefaultAsync(t => t.ClientId == client.Id && t.IsActive);

            if (cycleTpl == null)
                return Result<ReleaseInstance>.Error($"No active release cycle template found for client");

            var release = new ReleaseInstance
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                ReleaseCycleTemplateId = cycleTpl.Id,
                Key = request.ReleaseKey,
                Title = request.Title,
                Description = request.Description,
                CurrentStage = "Draft",
                Status = "Draft",
                ScheduledStartAt = request.ScheduledStartAt,
                CreatedAt = DateTime.UtcNow
            };

            // Create stage execution records
            foreach (var stage in cycleTpl.Stages.OrderBy(s => s.DisplayOrder))
            {
                release.StageExecutions.Add(new ReleaseStageExecution
                {
                    Id = Guid.NewGuid(),
                    ReleaseInstanceId = release.Id,
                    StageName = stage.Name,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                });
            }

            _context.ReleaseInstances.Add(release);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created release {request.ReleaseKey} for client {clientSlug}");

            return Result<ReleaseInstance>.Ok(release);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating release: {ex.Message}");
            return Result<ReleaseInstance>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Advance a release to the next stage
    /// Orchestrates: validation → Discord automation → state transition → audit log
    /// </summary>
    public async Task<Result<ReleaseInstance>> AdvanceReleaseStageAsync(
        Guid releaseId,
        string targetStage,
        string? notes = null)
    {
        try
        {
            var release = await _context.ReleaseInstances
                .Include(r => r.Client)
                    .ThenInclude(c => c!.DiscordConfiguration)
                        .ThenInclude(dc => dc!.Channels)
                .Include(r => r.ReleaseCycleTemplate)
                .FirstOrDefaultAsync(r => r.Id == releaseId);

            if (release == null)
                return Result<ReleaseInstance>.Error("Release not found");

            // Validate transition
            if (!ReleaseStateMachine.IsValidTransition(release.CurrentStage, targetStage))
                return Result<ReleaseInstance>.Error(
                    $"Cannot transition from '{release.CurrentStage}' to '{targetStage}'");

            var fromStage = release.CurrentStage;
            var discordConfig = release.Client?.DiscordConfiguration;

            // Get ritual mappings for automation
            var toRitualMapping = await _context.RitualMappings
                .FirstOrDefaultAsync(rm =>
                    rm.ClientId == release.ClientId &&
                    rm.ReleaseCycleTemplateId == release.ReleaseCycleTemplateId &&
                    rm.StageType == targetStage);

            var fromRitualMapping = string.IsNullOrEmpty(fromStage) ? null : await _context.RitualMappings
                .FirstOrDefaultAsync(rm =>
                    rm.ClientId == release.ClientId &&
                    rm.ReleaseCycleTemplateId == release.ReleaseCycleTemplateId &&
                    rm.StageType == fromStage);

            var stateTransition = new ReleaseStateTransition
            {
                Id = Guid.NewGuid(),
                ReleaseInstanceId = release.Id,
                FromStage = fromStage,
                ToStage = targetStage,
                TransitionReason = "Manual",
                OccurredAt = DateTime.UtcNow,
                Notes = notes
            };

            // Automate Discord if configured
            if (discordConfig?.IsActive == true)
            {
                await AutomateDiscordTransitionAsync(
                    stateTransition,
                    discordConfig,
                    fromRitualMapping,
                    toRitualMapping);
            }

            // Update release state
            release.CurrentStage = targetStage;
            if (release.Status == "Draft" && targetStage != "Draft")
                release.Status = "Scheduled";
            if (release.Status == "Scheduled" && targetStage == "Archive")
                release.Status = "Archived";

            // Update stage execution record
            var stageExecution = release.StageExecutions
                .FirstOrDefault(se => se.StageName == targetStage);

            if (stageExecution != null)
            {
                stageExecution.Status = "Active";
                stageExecution.StartedAt = DateTime.UtcNow;
            }

            _context.ReleaseInstances.Update(release);
            _context.ReleaseStateTransitions.Add(stateTransition);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"Released advanced from '{fromStage}' to '{targetStage}' for release {release.Key}");

            return Result<ReleaseInstance>.Ok(release);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error advancing release stage: {ex.Message}");
            return Result<ReleaseInstance>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Orchestrate Discord automation on state transitions
    /// Implements: unlock channels, send announcements, apply permissions, lock channels
    /// </summary>
    private async Task AutomateDiscordTransitionAsync(
        ReleaseStateTransition transition,
        DiscordConfiguration discordConfig,
        RitualMapping? fromRitual,
        RitualMapping? toRitual)
    {
        try
        {
            var openedChannels = new List<string>();
            var lockedChannels = new List<string>();

            // Close channel from previous stage
            if (fromRitual != null && !string.IsNullOrEmpty(fromRitual.TargetChannelId))
            {
                if (fromRitual.AutomaticallyLockChannel)
                {
                    await _discordBot.LockChannelAsync(
                        discordConfig.GuildId!,
                        fromRitual.TargetChannelId,
                        discordConfig.BotToken!);
                    lockedChannels.Add(fromRitual.TargetChannelId);
                }

                // Send closing message if configured
                if (!string.IsNullOrEmpty(fromRitual.ClosingMessage))
                {
                    await _discordBot.SendMessageAsync(
                        discordConfig.GuildId!,
                        fromRitual.TargetChannelId,
                        fromRitual.ClosingMessage,
                        discordConfig.BotToken!);
                }
            }

            // Open channel for new stage
            if (toRitual != null)
            {
                // Resolve channel ID from Discord configuration
                var targetChannel = discordConfig.Channels
                    .FirstOrDefault(ch => ch.Purpose == toRitual.TargetChannelPurpose);

                if (targetChannel != null)
                {
                    toRitual.TargetChannelId = targetChannel.ChannelId;

                    if (toRitual.AutomaticallyUnlockChannel)
                    {
                        await _discordBot.UnlockChannelAsync(
                            discordConfig.GuildId!,
                            targetChannel.ChannelId,
                            discordConfig.BotToken!);
                        openedChannels.Add(targetChannel.ChannelId);
                    }

                    // Apply slow mode if configured
                    if (!string.IsNullOrEmpty(toRitual.SlowModeSeconds))
                    {
                        var slowModeSeconds = int.Parse(toRitual.SlowModeSeconds);
                        await _discordBot.SetSlowModeAsync(
                            discordConfig.GuildId!,
                            targetChannel.ChannelId,
                            slowModeSeconds,
                            discordConfig.BotToken!);
                    }

                    // Send announcement if configured
                    if (!string.IsNullOrEmpty(toRitual.AnnouncementMessage))
                    {
                        await _discordBot.SendMessageAsync(
                            discordConfig.GuildId!,
                            targetChannel.ChannelId,
                            toRitual.AnnouncementMessage,
                            discordConfig.BotToken!);
                    }
                }
            }

            // Record what happened for audit
            if (openedChannels.Any())
                transition.DiscordChannelsOpened = string.Join(",", openedChannels);
            if (lockedChannels.Any())
                transition.DiscordChannelsLocked = string.Join(",", lockedChannels);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error automating Discord transition: {ex.Message}");
            // Don't throw - let release transition succeed even if Discord automation fails
        }
    }

    /// <summary>
    /// Get ritual mapping for a specific ritual stage
    /// </summary>
    public async Task<Result<RitualMapping>> GetRitualMappingAsync(Guid clientId, string ritualName)
    {
        try
        {
            var mapping = await _context.RitualMappings
                .FirstOrDefaultAsync(rm =>
                    rm.ClientId == clientId &&
                    rm.RitualName == ritualName &&
                    rm.IsActive);

            if (mapping == null)
                return Result<RitualMapping>.Error($"Ritual mapping '{ritualName}' not found for client");

            return Result<RitualMapping>.Ok(mapping);
        }
        catch (Exception ex)
        {
            return Result<RitualMapping>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Get complete state transition history for a release
    /// </summary>
    public async Task<Result<List<ReleaseStateTransition>>> GetReleaseHistoryAsync(Guid releaseId)
    {
        try
        {
            var history = await _context.ReleaseStateTransitions
                .Where(t => t.ReleaseInstanceId == releaseId)
                .OrderBy(t => t.OccurredAt)
                .ToListAsync();

            return Result<List<ReleaseStateTransition>>.Ok(history);
        }
        catch (Exception ex)
        {
            return Result<List<ReleaseStateTransition>>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Validate if a transition is allowed
    /// </summary>
    public async Task<bool> IsValidTransitionAsync(Guid releaseId, string targetStage)
    {
        var release = await _context.ReleaseInstances
            .FirstOrDefaultAsync(r => r.Id == releaseId);

        if (release == null)
            return false;

        return ReleaseStateMachine.IsValidTransition(release.CurrentStage, targetStage);
    }
}
