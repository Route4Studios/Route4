using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Controllers;

/// <summary>
/// Outreach AI - Phase I: Directory & Contact Tracking
/// Manages film community directory, outreach campaigns, and contact logging
/// </summary>
[ApiController]
[Route("api/outreach")]
public class OutreachController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<OutreachController> _logger;

    public OutreachController(Route4DbContext context, ILogger<OutreachController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // ============================================================================
    // COMMUNITY DIRECTORY ENDPOINTS
    // ============================================================================

    /// <summary>
    /// Get all communities (filterable by type, channel, location)
    /// GET /api/outreach/communities?type=FilmCommission&channel=Script&location=Ohio
    /// </summary>
    [HttpGet("communities")]
    public async Task<ActionResult<IEnumerable<OutreachCommunity>>> GetCommunities(
        [FromQuery] string? type = null,
        [FromQuery] string? channel = null,
        [FromQuery] string? location = null)
    {
        var query = _context.OutreachCommunities.Where(c => c.IsActive);

        if (!string.IsNullOrEmpty(type))
            query = query.Where(c => c.Type == type);

        if (!string.IsNullOrEmpty(channel))
            query = query.Where(c => c.Channel == channel);

        if (!string.IsNullOrEmpty(location))
            query = query.Where(c => c.LocationsJson != null && c.LocationsJson.Contains(location));

        var communities = await query.OrderByDescending(c => c.ConversionRate).ToListAsync();
        return Ok(communities);
    }

    /// <summary>
    /// Get single community by ID
    /// GET /api/outreach/communities/{id}
    /// </summary>
    [HttpGet("communities/{id}")]
    public async Task<ActionResult<OutreachCommunity>> GetCommunity(Guid id)
    {
        var community = await _context.OutreachCommunities.FindAsync(id);
        if (community == null)
            return NotFound();

        return Ok(community);
    }

    /// <summary>
    /// Create new community in directory
    /// POST /api/outreach/communities
    /// </summary>
    [HttpPost("communities")]
    public async Task<ActionResult<OutreachCommunity>> CreateCommunity([FromBody] OutreachCommunity community)
    {
        community.Id = Guid.NewGuid();
        community.CreatedAt = DateTime.UtcNow;
        community.UpdatedAt = DateTime.UtcNow;

        _context.OutreachCommunities.Add(community);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created community {Name} ({Type})", community.Name, community.Type);
        return CreatedAtAction(nameof(GetCommunity), new { id = community.Id }, community);
    }

    /// <summary>
    /// Update community (partial update)
    /// PUT /api/outreach/communities/{id}
    /// </summary>
    [HttpPut("communities/{id}")]
    public async Task<IActionResult> UpdateCommunity(Guid id, [FromBody] OutreachCommunity updatedCommunity)
    {
        var community = await _context.OutreachCommunities.FindAsync(id);
        if (community == null)
            return NotFound();

        // Update fields
        community.Name = updatedCommunity.Name;
        community.Type = updatedCommunity.Type;
        community.Channel = updatedCommunity.Channel;
        community.Website = updatedCommunity.Website;
        community.SubmissionFormUrl = updatedCommunity.SubmissionFormUrl;
        community.ApiEndpoint = updatedCommunity.ApiEndpoint;
        community.ContactEmail = updatedCommunity.ContactEmail;
        community.SocialHandle = updatedCommunity.SocialHandle;
        community.GenresJson = updatedCommunity.GenresJson;
        community.LocationsJson = updatedCommunity.LocationsJson;
        community.TagsJson = updatedCommunity.TagsJson;
        community.EstimatedReach = updatedCommunity.EstimatedReach;
        community.PostingRules = updatedCommunity.PostingRules;
        community.RequiresApproval = updatedCommunity.RequiresApproval;
        community.ComplianceNotes = updatedCommunity.ComplianceNotes;
        community.HasCaptcha = updatedCommunity.HasCaptcha;
        community.FormFieldMapJson = updatedCommunity.FormFieldMapJson;
        community.IsActive = updatedCommunity.IsActive;
        community.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated community {Id} ({Name})", id, community.Name);

        return NoContent();
    }

    /// <summary>
    /// Get community performance metrics
    /// GET /api/outreach/communities/{id}/performance
    /// </summary>
    [HttpGet("communities/{id}/performance")]
    public async Task<ActionResult<object>> GetCommunityPerformance(Guid id)
    {
        var community = await _context.OutreachCommunities.FindAsync(id);
        if (community == null)
            return NotFound();

        var contacts = await _context.OutreachContacts
            .Where(c => c.CommunityId == id)
            .ToListAsync();

        var topCampaigns = await _context.OutreachContacts
            .Where(c => c.CommunityId == id && c.DidConvert)
            .GroupBy(c => c.CastingCallId)
            .Select(g => new { CastingCallId = g.Key, Conversions = g.Count() })
            .OrderByDescending(x => x.Conversions)
            .Take(5)
            .ToListAsync();

        return Ok(new
        {
            community.TotalOutreachAttempts,
            community.SuccessfulConversions,
            community.ConversionRate,
            community.LastContactedAt,
            TotalClicks = contacts.Sum(c => c.TotalClicks),
            TopPerformingCampaigns = topCampaigns
        });
    }

    // ============================================================================
    // CONTACT TRACKING ENDPOINTS
    // ============================================================================

    /// <summary>
    /// Create outreach contact (log planned or completed outreach)
    /// POST /api/outreach/contacts
    /// </summary>
    [HttpPost("contacts")]
    public async Task<ActionResult<OutreachContact>> CreateContact([FromBody] OutreachContact contact)
    {
        contact.Id = Guid.NewGuid();
        contact.CreatedAt = DateTime.UtcNow;

        _context.OutreachContacts.Add(contact);

        // Update community stats
        var community = await _context.OutreachCommunities.FindAsync(contact.CommunityId);
        if (community != null)
        {
            community.TotalOutreachAttempts++;
            community.LastContactedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Created outreach contact for community {CommunityId}, casting call {CastingCallId}", 
            contact.CommunityId, contact.CastingCallId);

        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    /// <summary>
    /// Get single contact by ID
    /// GET /api/outreach/contacts/{id}
    /// </summary>
    [HttpGet("contacts/{id}")]
    public async Task<ActionResult<OutreachContact>> GetContact(Guid id)
    {
        var contact = await _context.OutreachContacts
            .Include(c => c.Community)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contact == null)
            return NotFound();

        return Ok(contact);
    }

    /// <summary>
    /// Update contact status (e.g., mark as Sent, Clicked, Converted)
    /// PUT /api/outreach/contacts/{id}/status
    /// </summary>
    [HttpPut("contacts/{id}/status")]
    public async Task<IActionResult> UpdateContactStatus(Guid id, [FromBody] UpdateContactStatusRequest request)
    {
        var contact = await _context.OutreachContacts.FindAsync(id);
        if (contact == null)
            return NotFound();

        contact.Status = request.Status;
        if (request.SentAt.HasValue)
            contact.SentAt = request.SentAt.Value;
        if (!string.IsNullOrEmpty(request.MessageId))
            contact.MessageId = request.MessageId;
        if (!string.IsNullOrEmpty(request.PostUrl))
            contact.PostUrl = request.PostUrl;
        if (!string.IsNullOrEmpty(request.Notes))
            contact.Notes = request.Notes;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated contact {Id} status to {Status}", id, request.Status);
        return NoContent();
    }

    /// <summary>
    /// Get contacts (filterable by casting call, status, community)
    /// GET /api/outreach/contacts?castingCallId={id}&status=Clicked
    /// </summary>
    [HttpGet("contacts")]
    public async Task<ActionResult<IEnumerable<OutreachContact>>> GetContacts(
        [FromQuery] Guid? castingCallId = null,
        [FromQuery] string? status = null,
        [FromQuery] Guid? communityId = null)
    {
        var query = _context.OutreachContacts.Include(c => c.Community).AsQueryable();

        if (castingCallId.HasValue)
            query = query.Where(c => c.CastingCallId == castingCallId.Value);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(c => c.Status == status);

        if (communityId.HasValue)
            query = query.Where(c => c.CommunityId == communityId.Value);

        var contacts = await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Log click on short URL (called from ShortUrlController webhook)
    /// POST /api/outreach/contacts/{id}/click
    /// </summary>
    [HttpPost("contacts/{id}/click")]
    public async Task<IActionResult> LogClick(Guid id, [FromBody] LogClickRequest request)
    {
        var contact = await _context.OutreachContacts.FindAsync(id);
        if (contact == null)
            return NotFound();

        if (contact.ClickedAt == null)
        {
            contact.ClickedAt = request.ClickedAt;
            contact.Status = "Clicked";
        }

        contact.TotalClicks++;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Logged click for contact {Id}, total clicks: {TotalClicks}", id, contact.TotalClicks);
        return NoContent();
    }

    // ============================================================================
    // CAMPAIGN MANAGEMENT ENDPOINTS
    // ============================================================================

    /// <summary>
    /// Create outreach campaign
    /// POST /api/outreach/campaigns
    /// </summary>
    [HttpPost("campaigns")]
    public async Task<ActionResult<OutreachCampaign>> CreateCampaign([FromBody] OutreachCampaign campaign)
    {
        campaign.Id = Guid.NewGuid();
        campaign.CreatedAt = DateTime.UtcNow;
        campaign.UpdatedAt = DateTime.UtcNow;

        _context.OutreachCampaigns.Add(campaign);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created campaign {Name} for casting call {CastingCallId}", campaign.Name, campaign.CastingCallId);
        return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
    }

    /// <summary>
    /// Get campaign by ID (with performance metrics)
    /// GET /api/outreach/campaigns/{id}
    /// </summary>
    [HttpGet("campaigns/{id}")]
    public async Task<ActionResult<object>> GetCampaign(Guid id)
    {
        var campaign = await _context.OutreachCampaigns.FindAsync(id);
        if (campaign == null)
            return NotFound();

        var contacts = await _context.OutreachContacts
            .Include(c => c.Community)
            .Where(c => c.CastingCallId == campaign.CastingCallId)
            .ToListAsync();

        return Ok(new
        {
            Campaign = campaign,
            Contacts = contacts,
            Performance = new
            {
                campaign.TotalContacts,
                campaign.TotalClicks,
                campaign.TotalConversions,
                campaign.ConversionRate,
                campaign.CostPerWitness
            }
        });
    }

    /// <summary>
    /// Pause campaign
    /// PUT /api/outreach/campaigns/{id}/pause
    /// </summary>
    [HttpPut("campaigns/{id}/pause")]
    public async Task<IActionResult> PauseCampaign(Guid id)
    {
        var campaign = await _context.OutreachCampaigns.FindAsync(id);
        if (campaign == null)
            return NotFound();

        campaign.Status = "Paused";
        campaign.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Paused campaign {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Get all campaigns for a client
    /// GET /api/clients/{clientSlug}/campaigns
    /// </summary>
    [HttpGet("/api/clients/{clientSlug}/campaigns")]
    public async Task<ActionResult<IEnumerable<OutreachCampaign>>> GetClientCampaigns(string clientSlug)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Slug == clientSlug);
        if (client == null)
            return NotFound();

        var campaigns = await _context.OutreachCampaigns
            .Where(c => c.ClientId == client.Id)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(campaigns);
    }
}

// ============================================================================
// REQUEST/RESPONSE DTOs
// ============================================================================

public class UpdateContactStatusRequest
{
    public required string Status { get; set; }
    public DateTime? SentAt { get; set; }
    public string? MessageId { get; set; }
    public string? PostUrl { get; set; }
    public string? Notes { get; set; }
}

public class LogClickRequest
{
    public DateTime ClickedAt { get; set; }
}
