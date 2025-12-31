using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(Route4DbContext context, ILogger<ClientsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _context.Clients
            .Where(c => c.IsActive)
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.CreatedAt
            })
            .ToListAsync();

        return Ok(clients);
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
}

public record CreateClientRequest(string Name, string? Description);
