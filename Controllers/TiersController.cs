using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/clients/{clientSlug}/tiers")]
public class TiersController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<TiersController> _logger;

    public TiersController(Route4DbContext context, ILogger<TiersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipTierDto>>> GetTiers(string clientSlug)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
        {
            return NotFound(new { error = $"Client '{clientSlug}' not found" });
        }

        var tiers = await _context.MembershipTiers
            .Include(t => t.Features)
            .Where(t => t.ClientId == client.Id && t.IsActive)
            .OrderBy(t => t.DisplayOrder)
            .Select(t => new MembershipTierDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                PriceInterval = t.PriceInterval,
                TagLine = t.TagLine,
                IsFeatured = t.IsFeatured,
                Features = t.Features
                    .OrderBy(f => f.DisplayOrder)
                    .Select(f => new TierFeatureDto
                    {
                        Text = f.Text,
                        IsHighlighted = f.IsHighlighted
                    })
                    .ToList()
            })
            .ToListAsync();

        return Ok(tiers);
    }

    [HttpGet("{tierId}")]
    public async Task<ActionResult<MembershipTierDto>> GetTier(string clientSlug, Guid tierId)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

        if (client == null)
        {
            return NotFound(new { error = $"Client '{clientSlug}' not found" });
        }

        var tier = await _context.MembershipTiers
            .Include(t => t.Features)
            .FirstOrDefaultAsync(t => t.Id == tierId && t.ClientId == client.Id);

        if (tier == null)
        {
            return NotFound(new { error = "Tier not found" });
        }

        var tierDto = new MembershipTierDto
        {
            Id = tier.Id,
            Name = tier.Name,
            Description = tier.Description,
            Price = tier.Price,
            PriceInterval = tier.PriceInterval,
            TagLine = tier.TagLine,
            IsFeatured = tier.IsFeatured,
            Features = tier.Features
                .OrderBy(f => f.DisplayOrder)
                .Select(f => new TierFeatureDto
                {
                    Text = f.Text,
                    IsHighlighted = f.IsHighlighted
                })
                .ToList()
        };

        return Ok(tierDto);
    }
}
