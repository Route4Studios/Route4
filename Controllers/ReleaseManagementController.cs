using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Route4MoviePlug.Api.Services;

namespace Route4MoviePlug.Api.Controllers;

/// <summary>
/// Release Management Controller - Phase 3 &amp; 4 Implementation.
/// Manages release instances, state transitions, and ritual mapping.
/// </summary>
[ApiController]
[Route("api/admin/clients/{clientSlug}/releases")]
public class ReleaseManagementController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly IReleaseManagementService _releaseService;
    private readonly ILogger<ReleaseManagementController> _logger;

    public ReleaseManagementController(
        Route4DbContext context,
        IReleaseManagementService releaseService,
        ILogger<ReleaseManagementController> logger)
    {
        _context = context;
        _releaseService = releaseService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new release instance
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ReleaseStateDto>> CreateRelease(
        string clientSlug,
        [FromBody] CreateReleaseRequest request)
    {
        var result = await _releaseService.CreateReleaseAsync(clientSlug, request);

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage });

        var release = result.Data;
        var dto = MapToReleaseStateDto(release);

        return CreatedAtAction(nameof(GetRelease), new { releaseId = release.Id }, dto);
    }

    /// <summary>
    /// Get a specific release
    /// </summary>
    [HttpGet("{releaseId:guid}")]
    public async Task<ActionResult<ReleaseStateDto>> GetRelease(Guid releaseId)
    {
        var release = await _context.ReleaseInstances
            .Include(r => r.ReleaseCycleTemplate)
            .FirstOrDefaultAsync(r => r.Id == releaseId);

        if (release == null)
            return NotFound(new { error = "Release not found" });

        var dto = MapToReleaseStateDto(release);
        return Ok(dto);
    }

    /// <summary>
    /// List all releases for a client
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ReleaseStateDto>>> ListClientReleases(string clientSlug)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var releases = await _context.ReleaseInstances
            .Where(r => r.ClientId == client.Id)
            .Include(r => r.ReleaseCycleTemplate)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        var dtos = releases.Select(MapToReleaseStateDto).ToList();
        return Ok(dtos);
    }

    /// <summary>
    /// Advance a release to the next stage
    /// This is the core ritual automation trigger
    /// </summary>
    [HttpPost("{releaseId:guid}/advance")]
    public async Task<ActionResult<ReleaseStateDto>> AdvanceReleaseStage(
        Guid releaseId,
        [FromBody] AdvanceReleaseStageRequest request)
    {
        var result = await _releaseService.AdvanceReleaseStageAsync(releaseId, request.TargetStage, request.Notes);

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage });

        var dto = MapToReleaseStateDto(result.Data);
        return Ok(dto);
    }

    /// <summary>
    /// Get available next stages for a release
    /// Used by UI to show valid transitions
    /// </summary>
    [HttpGet("{releaseId:guid}/next-stages")]
    public async Task<ActionResult<List<string>>> GetNextStages(Guid releaseId)
    {
        var release = await _context.ReleaseInstances
            .FirstOrDefaultAsync(r => r.Id == releaseId);

        if (release == null)
            return NotFound(new { error = "Release not found" });

        var allowedNextStages = ReleaseStateMachine.ValidTransitions
            .TryGetValue(release.CurrentStage, out var nextStages)
            ? nextStages.ToList()
            : new List<string>();

        return Ok(allowedNextStages);
    }

    /// <summary>
    /// Get release state transition history (audit trail)
    /// </summary>
    [HttpGet("{releaseId:guid}/history")]
    public async Task<ActionResult<List<ReleaseHistoryDto>>> GetReleaseHistory(Guid releaseId)
    {
        var result = await _releaseService.GetReleaseHistoryAsync(releaseId);

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage });

        var dtos = result.Data.Select(t => new ReleaseHistoryDto
        {
            TransitionId = t.Id,
            FromStage = t.FromStage,
            ToStage = t.ToStage,
            Reason = t.TransitionReason,
            OccurredAt = t.OccurredAt,
            Notes = t.Notes,
            DiscordChannelsAffected = string.IsNullOrEmpty(t.DiscordChannelsOpened)
                ? null
                : (t.DiscordChannelsOpened?.Split(',').Length ?? 0) + 
                  (t.DiscordChannelsLocked?.Split(',').Length ?? 0)
        }).ToList();

        return Ok(dtos);
    }

    // ============ Ritual Mapping Endpoints (Phase 4) ============

    /// <summary>
    /// Configure ritual mappings for a release cycle template
    /// This is Phase 4: mapping rituals → channels → automation
    /// </summary>
    [HttpPost("ritual-mappings")]
    public async Task<ActionResult<RitualMappingDto>> CreateRitualMapping(
        string clientSlug,
        [FromBody] CreateRitualMappingRequest request)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var existingMapping = await _context.RitualMappings
            .FirstOrDefaultAsync(rm =>
                rm.ClientId == client.Id &&
                rm.ReleaseCycleTemplateId == request.ReleaseCycleTemplateId &&
                rm.RitualName == request.RitualName);

        if (existingMapping != null)
            return BadRequest(new { error = "Ritual mapping already exists" });

        var mapping = new RitualMapping
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            ReleaseCycleTemplateId = request.ReleaseCycleTemplateId,
            RitualName = request.RitualName,
            StageType = request.StageType,
            Description = request.Description,
            ExecutionOrder = request.ExecutionOrder,
            TargetChannelPurpose = request.TargetChannelPurpose,
            VisibilityLevel = request.VisibilityLevel,
            RequiredRoles = request.RequiredRoles,
            IsReadOnly = request.IsReadOnly,
            DefaultDurationHours = request.DefaultDurationHours,
            AutomaticallyUnlockChannel = request.AutomaticallyUnlockChannel,
            AutomaticallyLockChannel = request.AutomaticallyLockChannel,
            SlowModeSeconds = request.SlowModeSeconds,
            DisableFileUploads = request.DisableFileUploads,
            DisableExternalEmojis = request.DisableExternalEmojis,
            AnnouncementMessage = request.AnnouncementMessage,
            ClosingMessage = request.ClosingMessage,
            IsAnonymous = request.IsAnonymous,
            CanBeSkipped = request.CanBeSkipped,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.RitualMappings.Add(mapping);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created ritual mapping '{request.RitualName}' for client {clientSlug}");

        return CreatedAtAction(nameof(GetRitualMapping), new { id = mapping.Id }, MapToRitualMappingDto(mapping));
    }

    /// <summary>
    /// Get a specific ritual mapping
    /// </summary>
    [HttpGet("ritual-mappings/{id:guid}")]
    public async Task<ActionResult<RitualMappingDto>> GetRitualMapping(Guid id)
    {
        var mapping = await _context.RitualMappings.FirstOrDefaultAsync(rm => rm.Id == id);

        if (mapping == null)
            return NotFound(new { error = "Ritual mapping not found" });

        return Ok(MapToRitualMappingDto(mapping));
    }

    /// <summary>
    /// List all ritual mappings for a release cycle template
    /// </summary>
    [HttpGet("ritual-mappings")]
    public async Task<ActionResult<List<RitualMappingDto>>> ListRitualMappings(
        string clientSlug,
        [FromQuery] Guid? releaseCycleTemplateId = null)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var query = _context.RitualMappings
            .Where(rm => rm.ClientId == client.Id);

        if (releaseCycleTemplateId.HasValue)
            query = query.Where(rm => rm.ReleaseCycleTemplateId == releaseCycleTemplateId.Value);

        var mappings = await query
            .OrderBy(rm => rm.ExecutionOrder)
            .ToListAsync();

        var dtos = mappings.Select(MapToRitualMappingDto).ToList();
        return Ok(dtos);
    }

    /// <summary>
    /// Update a ritual mapping
    /// </summary>
    [HttpPut("ritual-mappings/{id:guid}")]
    public async Task<ActionResult<RitualMappingDto>> UpdateRitualMapping(
        Guid id,
        [FromBody] UpdateRitualMappingRequest request)
    {
        var mapping = await _context.RitualMappings.FirstOrDefaultAsync(rm => rm.Id == id);

        if (mapping == null)
            return NotFound(new { error = "Ritual mapping not found" });

        // Update fields
        mapping.TargetChannelPurpose = request.TargetChannelPurpose ?? mapping.TargetChannelPurpose;
        mapping.VisibilityLevel = request.VisibilityLevel ?? mapping.VisibilityLevel;
        mapping.IsReadOnly = request.IsReadOnly ?? mapping.IsReadOnly;
        mapping.DefaultDurationHours = request.DefaultDurationHours ?? mapping.DefaultDurationHours;
        mapping.AutomaticallyUnlockChannel = request.AutomaticallyUnlockChannel ?? mapping.AutomaticallyUnlockChannel;
        mapping.AutomaticallyLockChannel = request.AutomaticallyLockChannel ?? mapping.AutomaticallyLockChannel;
        mapping.SlowModeSeconds = request.SlowModeSeconds ?? mapping.SlowModeSeconds;
        mapping.AnnouncementMessage = request.AnnouncementMessage ?? mapping.AnnouncementMessage;
        mapping.ClosingMessage = request.ClosingMessage ?? mapping.ClosingMessage;
        mapping.RequiredRoles = request.RequiredRoles ?? mapping.RequiredRoles;
        mapping.UpdatedAt = DateTime.UtcNow;

        _context.RitualMappings.Update(mapping);
        await _context.SaveChangesAsync();

        return Ok(MapToRitualMappingDto(mapping));
    }

    /// <summary>
    /// Get the Mary-specific ritual sequence with all mappings
    /// </summary>
    [HttpGet("mary-ritual-sequence")]
    public async Task<ActionResult<MaryRitualSequenceDto>> GetMaryRitualSequence(string clientSlug)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
            return NotFound(new { error = $"Client '{clientSlug}' not found" });

        var template = await _context.ReleaseCycleTemplates
            .Include(t => t.Stages)
            .FirstOrDefaultAsync(t => t.ClientId == client.Id && t.IsActive);

        if (template == null)
            return NotFound(new { error = "Release cycle template not found" });

        var mappings = await _context.RitualMappings
            .Where(rm => rm.ClientId == client.Id && rm.ReleaseCycleTemplateId == template.Id && rm.IsActive)
            .OrderBy(rm => rm.ExecutionOrder)
            .ToListAsync();

        var sequence = new MaryRitualSequenceDto
        {
            ClientSlug = clientSlug,
            ReleaseCycleTemplateName = template.Name,
            Rituals = mappings.Select(m => new MaryRitualDto
            {
                RitualName = m.RitualName,
                StageType = m.StageType,
                TargetChannelPurpose = m.TargetChannelPurpose,
                VisibilityLevel = m.VisibilityLevel,
                ExecutionOrder = m.ExecutionOrder,
                Description = m.Description,
                AutomatesChannelUnlock = m.AutomaticallyUnlockChannel,
                AutomatesChannelLock = m.AutomaticallyLockChannel,
                AnnouncementMessage = m.AnnouncementMessage
            }).ToList()
        };

        return Ok(sequence);
    }

    // ============ Helpers ============

    private ReleaseStateDto MapToReleaseStateDto(ReleaseInstance release)
    {
        var allowedNextStages = ReleaseStateMachine.ValidTransitions
            .TryGetValue(release.CurrentStage, out var nextStages)
            ? nextStages.ToList()
            : new List<string>();

        // Map stage names to RitualMappingDto objects
        var ritualMappings = _context.RitualMappings
            .Where(rm => rm.ClientId == release.ClientId && allowedNextStages.Contains(rm.StageType))
            .Select(rm => MapToRitualMappingDto(rm))
            .ToList();

        return new ReleaseStateDto
        {
            Id = release.Id,
            Key = release.Key,
            Title = release.Title,
            CurrentStage = release.CurrentStage,
            Status = release.Status,
            CreatedAt = release.CreatedAt,
            ScheduledStartAt = release.ScheduledStartAt,
            StartedAt = release.StartedAt,
            AvailableNextStages = ritualMappings
        };
    }

    private RitualMappingDto MapToRitualMappingDto(RitualMapping mapping)
    {
        return new RitualMappingDto
        {
            Id = mapping.Id,
            RitualName = mapping.RitualName,
            StageType = mapping.StageType,
            TargetChannelPurpose = mapping.TargetChannelPurpose,
            VisibilityLevel = mapping.VisibilityLevel,
            DefaultDurationHours = mapping.DefaultDurationHours,
            AnnouncementMessage = mapping.AnnouncementMessage,
            IsReadOnly = mapping.IsReadOnly
        };
    }
}

// ============ DTOs for Ritual Mapping ============

public class CreateRitualMappingRequest
{
    public required Guid ReleaseCycleTemplateId { get; set; }
    public required string RitualName { get; set; }
    public required string StageType { get; set; }
    public string? Description { get; set; }
    public int ExecutionOrder { get; set; }
    public required string TargetChannelPurpose { get; set; }
    public required string VisibilityLevel { get; set; }
    public string[]? RequiredRoles { get; set; }
    public bool IsReadOnly { get; set; }
    public int? DefaultDurationHours { get; set; }
    public bool AutomaticallyUnlockChannel { get; set; }
    public bool AutomaticallyLockChannel { get; set; }
    public string? SlowModeSeconds { get; set; }
    public bool DisableFileUploads { get; set; }
    public bool DisableExternalEmojis { get; set; }
    public string? AnnouncementMessage { get; set; }
    public string? ClosingMessage { get; set; }
    public bool IsAnonymous { get; set; }
    public bool CanBeSkipped { get; set; }
}

public class UpdateRitualMappingRequest
{
    public string? TargetChannelPurpose { get; set; }
    public string? VisibilityLevel { get; set; }
    public bool? IsReadOnly { get; set; }
    public int? DefaultDurationHours { get; set; }
    public bool? AutomaticallyUnlockChannel { get; set; }
    public bool? AutomaticallyLockChannel { get; set; }
    public string? SlowModeSeconds { get; set; }
    public string? AnnouncementMessage { get; set; }
    public string? ClosingMessage { get; set; }
    public string[]? RequiredRoles { get; set; }
}

public class MaryRitualSequenceDto
{
    public required string ClientSlug { get; set; }
    public required string ReleaseCycleTemplateName { get; set; }
    public required List<MaryRitualDto> Rituals { get; set; }
}

public class MaryRitualDto
{
    public required string RitualName { get; set; }
    public required string StageType { get; set; }
    public required string TargetChannelPurpose { get; set; }
    public required string VisibilityLevel { get; set; }
    public int ExecutionOrder { get; set; }
    public string? Description { get; set; }
    public bool AutomatesChannelUnlock { get; set; }
    public bool AutomatesChannelLock { get; set; }
    public string? AnnouncementMessage { get; set; }
}
