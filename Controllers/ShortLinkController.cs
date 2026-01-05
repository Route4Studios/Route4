using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;

namespace Route4MoviePlug.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ShortLinkController : ControllerBase
{
    private readonly Route4DbContext _context;
    public ShortLinkController(Route4DbContext context)
    {
        _context = context;
    }

    // Matches /{clientSlug}/{shortCode}
    [HttpGet("/{clientSlug}/{shortCode}")]
    public async Task<IActionResult> RedirectShortLink(string clientSlug, string shortCode)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        if (shortUrl == null || (shortUrl.ExpiresAt.HasValue && shortUrl.ExpiresAt < DateTime.UtcNow))
        {
            return NotFound();
        }
        // Enforce clientSlug matches the client for this short code
        var castingCall = await _context.CastingCalls.FirstOrDefaultAsync(cc => cc.ShortUrl == shortUrl.TargetUrl);
        if (castingCall == null)
        {
            return NotFound();
        }
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == castingCall.ClientId);
        if (client == null || !string.Equals(client.Slug, clientSlug, StringComparison.OrdinalIgnoreCase))
        {
            return NotFound();
        }
        return Redirect(shortUrl.TargetUrl);
    }
}
