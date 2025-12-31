using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/clients/{clientSlug}/[controller]")]
public class SplashPageController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<SplashPageController> _logger;

    public SplashPageController(Route4DbContext context, ILogger<SplashPageController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetSplashPage(string clientSlug)
    {
        var client = await _context.Clients
            .Include(c => c.SplashPages)
                .ThenInclude(sp => sp.Benefits)
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
        {
            return NotFound(new { Message = "Client not found" });
        }

        var splashPage = client.SplashPages.FirstOrDefault(sp => sp.IsPublished);
        if (splashPage == null)
        {
            return NotFound(new { Message = "No published splash page found" });
        }

        var dto = new SplashPageDto
        {
            Id = splashPage.Id,
            Title = splashPage.Title,
            Subtitle = splashPage.Subtitle,
            Description = splashPage.Description,
            HeroImageUrl = splashPage.HeroImageUrl,
            LogoUrl = splashPage.LogoUrl,
            Benefits = splashPage.Benefits
                .OrderBy(b => b.DisplayOrder)
                .Select(b => new BenefitDto
                {
                    Icon = b.Icon,
                    Title = b.Title,
                    Description = b.Description
                })
                .ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSplashPage(string clientSlug, [FromBody] CreateSplashPageRequest request)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Slug == clientSlug);
        if (client == null)
        {
            return NotFound(new { Message = "Client not found" });
        }

        var splashPage = new SplashPage
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            Title = request.Title,
            Subtitle = request.Subtitle,
            Description = request.Description,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow,
            Benefits = request.Benefits.Select((b, index) => new Benefit
            {
                Id = Guid.NewGuid(),
                Icon = b.Icon,
                Title = b.Title,
                Description = b.Description,
                DisplayOrder = index + 1
            }).ToList()
        };

        _context.SplashPages.Add(splashPage);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created splash page {Id} for client {ClientSlug}", splashPage.Id, clientSlug);

        return CreatedAtAction(nameof(GetSplashPage), new { clientSlug }, new { Id = splashPage.Id });
    }

    [HttpPut("{id}/publish")]
    public async Task<IActionResult> PublishSplashPage(string clientSlug, Guid id)
    {
        var splashPage = await _context.SplashPages
            .Include(sp => sp.Client)
            .FirstOrDefaultAsync(sp => sp.Id == id && sp.Client!.Slug == clientSlug);

        if (splashPage == null)
        {
            return NotFound();
        }

        // Unpublish other splash pages for this client
        var otherPages = await _context.SplashPages
            .Where(sp => sp.ClientId == splashPage.ClientId && sp.Id != id)
            .ToListAsync();

        foreach (var page in otherPages)
        {
            page.IsPublished = false;
        }

        splashPage.IsPublished = true;
        splashPage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { Message = "Splash page published successfully" });
    }
}
