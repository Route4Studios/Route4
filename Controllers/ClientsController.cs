using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Route4MoviePlug.Api.Services.Discord;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientsController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<ClientsController> _logger;
    private readonly IDiscordBotService _discordBot;
    private readonly IConfiguration _configuration;

    public ClientsController(
        Route4DbContext context, 
        ILogger<ClientsController> logger,
        IDiscordBotService discordBot,
        IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _discordBot = discordBot;
        _configuration = configuration;
    }

    // --- Casting Call Endpoints ---

    /// <summary>
    /// Create a new casting call for a client by slug
    /// </summary>
    [HttpPost("{clientSlug}/castingcall")]
    public async Task<IActionResult> CreateCastingCall(string clientSlug, [FromBody] CreateCastingCallRequest request)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);
        if (client == null)
            return NotFound(new { Message = "Client not found" });

        // Deactivate any existing active casting calls for this client
        var activeCalls = await _context.CastingCalls.Where(cc => cc.ClientId == client.Id && cc.IsActive).ToListAsync();
        foreach (var cc in activeCalls)
        {
            cc.IsActive = false;
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
            IsActive = true,
            BackgroundImageUrl = request.BackgroundImageUrl,
            CreatedAt = DateTime.UtcNow
        };
        _context.CastingCalls.Add(castingCall);
        await _context.SaveChangesAsync();

        return Ok(new { Id = castingCall.Id });
    }

    /// <summary>
    /// Get all clients from Discord - Discord is the source of truth
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            // Get bot token from configuration
            var botToken = _configuration["Discord:BotToken"];
            if (string.IsNullOrEmpty(botToken))
            {
                return BadRequest(new { error = "Discord bot token not configured" });
            }

            // Get all Discord guilds the bot has access to (source of truth)
            var guilds = await _discordBot.GetAllGuildsAsync(botToken);
            
            // Map to client-like structure
            // In the future, you might match guild names to client slugs from DB
            var clients = guilds.Select(guild => new
            {
                Id = guild.GuildId,
                Name = guild.Name,
                Slug = System.Text.RegularExpressions.Regex.Replace(
                    guild.Name.ToLower()
                        .Replace("route4", "")
                        .Replace(" - ", "-")
                        .Replace(" ", "-")
                        .Replace("(mock)", "")
                        .Replace("--", "-")
                        .Trim('-'),
                    @"-+", "-"),
                Description = $"Discord server with {guild.MemberCount} members",
                CreatedAt = guild.CreatedAt,
                IsActive = true,
                DiscordConfiguration = new
                {
                    GuildId = guild.GuildId,
                    IsActive = true,
                    ChannelCount = guild.ChannelCount,
                    RoleCount = guild.RoleCount,
                    UpdatedAt = (DateTime?)null,
                    IconUrl = guild.IconUrl,
                    MemberCount = guild.MemberCount
                }
            }).ToList();

            _logger.LogInformation("Retrieved {Count} Discord guilds as clients", clients.Count);
            return Ok(clients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch clients from Discord");
            return StatusCode(500, new { error = "Failed to fetch clients from Discord" });
        }
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetClient(string slug)
    {
        var client = await _context.Clients
            .Include(c => c.SplashPages)
            .FirstOrDefaultAsync(c => c.Slug == slug && c.IsActive);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            client.Id,
            client.Name,
            client.Slug,
            client.Description,
            client.CreatedAt,
            SplashPagesCount = client.SplashPages.Count
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest request)
    {
        var slug = GenerateSlug(request.Name);

        if (await _context.Clients.AnyAsync(c => c.Slug == slug))
        {
            return BadRequest(new { Message = "A client with this name already exists" });
        }

        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Slug = slug,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created client {Id}: {Name}", client.Id, client.Name);

        return CreatedAtAction(nameof(GetClient), new { slug = client.Slug }, new
        {
            client.Id,
            client.Name,
            client.Slug,
            client.Description
        });
    }

    private string GenerateSlug(string name)
    {
        return name.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace("\"", "");
    }

    /// <summary>
    /// Client Intake - Register new client project with minimum information
    /// Creates initial entry in database with pending approval flag
    /// Notifies admins via email with link to admin dashboard
    /// </summary>
    [HttpPost("intake")]
    public async Task<ActionResult<ClientIntakeResponse>> SubmitClientIntake([FromBody] ClientIntakeRequest request)
    {
        // Validate request
        if (string.IsNullOrWhiteSpace(request.ProjectName) || 
            string.IsNullOrWhiteSpace(request.ContactEmail) ||
            string.IsNullOrWhiteSpace(request.ContactName))
        {
            return BadRequest(new { message = "Project name, contact name, and email are required" });
        }

        // Validate email format
        try
        {
            var addr = new System.Net.Mail.MailAddress(request.ContactEmail);
            if (addr.Address != request.ContactEmail)
                return BadRequest(new { message = "Invalid email format" });
        }
        catch
        {
            return BadRequest(new { message = "Invalid email format" });
        }

        // Create new client
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = request.ProjectName,
            Slug = GenerateSlug(request.ProjectName),
            Description = request.ProjectDescription,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"New client intake submitted: {client.Name}");

            // TODO: Send admin notification email with link to /admin/clients
            // This would call a notification service to email admins

            return Ok(new ClientIntakeResponse(
                client.Id,
                client.Name,
                "Thank you! Your registration has been received. Our team will review it shortly.",
                "http://localhost:4200/admin/clients"
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing client intake: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred processing your request. Please try again." });
        }
    }
}

public record CreateClientRequest(string Name, string? Description);

public record ClientIntakeRequest(
    string ProjectName,
    string ContactEmail,
    string ContactName,
    string? OrganizationName = null,
    string? ProjectDescription = null,
    string? NeedsStudioSpace = null,
    bool? NeedsCameras = null,
    bool? NeedsAudio = null,
    bool? NeedsEditing = null,
    bool? NeedsStreaming = null,
    bool? NeedsRecording = null,
    string? StudioTimeline = null
);

public record ClientIntakeResponse(
    Guid ClientId,
    string ProjectName,
    string Message,
    string AdminDashboardUrl
);
