using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/clients/{clientSlug}/[controller]")]
public class CastingCallController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<CastingCallController> _logger;

    public CastingCallController(Route4DbContext context, ILogger<CastingCallController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get active casting call for a client (Signal I — Public L2 Visibility)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCastingCall(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.CastingCalls)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
        {
            return NotFound(new { Message = "Client not found" });
        }

        var castingCall = client.CastingCalls
            .Where(cc => cc.IsActive)
            .OrderByDescending(cc => cc.CreatedAt)
            .FirstOrDefault();

        if (castingCall == null)
        {
            return NotFound(new { Message = "No active casting call found" });
        }

        var dto = new CastingCallDto
        {
            Id = castingCall.Id,
            ClientSlug = clientSlug,
            Title = castingCall.Title,
            ProjectStatus = castingCall.ProjectStatus,
            ToneAndIntent = castingCall.ToneAndIntent,
            RolesDescription = castingCall.RolesDescription,
            Constraints = castingCall.Constraints,
            HowToRespond = castingCall.HowToRespond,
            IsActive = castingCall.IsActive,
            BackgroundImageUrl = castingCall.BackgroundImageUrl
        };

        return Ok(dto);
    }

    /// <summary>
    /// Create a new casting call (Admin only - will add auth later)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCastingCall(string clientSlug, [FromBody] CreateCastingCallRequest request)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Slug == clientSlug);
        if (client == null)
        {
            return NotFound(new { Message = "Client not found" });
        }

        var castingCall = new CastingCall
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            Title = request.Title,
            ProjectStatus = request.ProjectStatus,
            ToneAndIntent = request.ToneAndIntent,
            RolesDescription = request.RolesDescription,
            Constraints = request.Constraints,
            HowToRespond = request.HowToRespond,
            BackgroundImageUrl = request.BackgroundImageUrl,
            IsActive = false, // Must be explicitly activated
            CreatedAt = DateTime.UtcNow
        };

        _context.CastingCalls.Add(castingCall);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created casting call {Id} for client {ClientSlug}", castingCall.Id, clientSlug);

        return CreatedAtAction(nameof(GetCastingCall), new { clientSlug }, new { Id = castingCall.Id });
    }

    /// <summary>
    /// Activate a casting call (Admin only - Signal I goes live)
    /// </summary>
    [HttpPut("{id}/activate")]
    public async Task<IActionResult> ActivateCastingCall(string clientSlug, Guid id)
    {
        var castingCall = await _context.CastingCalls
            .Include(cc => cc.Client)
            .FirstOrDefaultAsync(cc => cc.Id == id && cc.Client!.Slug == clientSlug);

        if (castingCall == null)
        {
            return NotFound();
        }

        // Deactivate other casting calls for this client (only one active at a time)
        var otherCalls = await _context.CastingCalls
            .Where(cc => cc.ClientId == castingCall.ClientId && cc.Id != id)
            .ToListAsync();

        foreach (var call in otherCalls)
        {
            call.IsActive = false;
        }

        castingCall.IsActive = true;
        castingCall.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Activated casting call {Id} for client {ClientSlug}", id, clientSlug);

        return Ok(new { Message = "Casting call activated successfully - Signal I is live" });
    }

    /// <summary>
    /// Deactivate a casting call (Signal I closes)
    /// </summary>
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivateCastingCall(string clientSlug, Guid id)
    {
        var castingCall = await _context.CastingCalls
            .Include(cc => cc.Client)
            .FirstOrDefaultAsync(cc => cc.Id == id && cc.Client!.Slug == clientSlug);

        if (castingCall == null)
        {
            return NotFound();
        }

        castingCall.IsActive = false;
        castingCall.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Deactivated casting call {Id} for client {ClientSlug}", id, clientSlug);

        return Ok(new { Message = "Casting call deactivated - Signal I closed" });
    }

    /// <summary>
    /// Submit a response to the casting call
    /// </summary>
    [HttpPost("response")]
    public async Task<IActionResult> SubmitCastingCallResponse(string clientSlug, [FromBody] CastingCallResponseRequest request)
    {
        // Validate the client exists
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);
        if (client == null)
        {
            return NotFound(new { Message = "Client not found" });
        }

        // Validate an active casting call exists
        var castingCall = await _context.CastingCalls
            .FirstOrDefaultAsync(cc => cc.ClientId == client.Id && cc.IsActive);

        if (castingCall == null)
        {
            return BadRequest(new { Message = "No active casting call for this client" });
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.RoleInterest))
        {
            return BadRequest(new { Message = "Name, email, and role interest are required" });
        }

        // Create and save the response
        var response = new CastingCallResponse
        {
            Id = Guid.NewGuid(),
            CastingCallId = castingCall.Id,
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            RoleInterest = request.RoleInterest.Trim(),
            Note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim(),
            RespondedAt = DateTime.UtcNow
        };

        _context.CastingCallResponses.Add(response);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Casting call response recorded: Id={ResponseId}, ClientSlug={ClientSlug}, Name={Name}, Email={Email}, Role={Role}",
            response.Id, clientSlug, response.Name, response.Email, response.RoleInterest);

        return Ok(new { 
            Message = "Your response has been received",
            ClientSlug = clientSlug,
            ResponseId = response.Id,
            Timestamp = DateTime.UtcNow
        });
    }
}
