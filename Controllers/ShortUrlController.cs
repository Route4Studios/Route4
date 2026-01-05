using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/shorturl")]
public class ShortUrlController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly ILogger<ShortUrlController> _logger;

    public ShortUrlController(Route4DbContext context, ILogger<ShortUrlController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortUrl([FromBody] CreateShortUrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.TargetUrl) || request.CastingCallId == Guid.Empty)
            return BadRequest(new { Message = "TargetUrl and CastingCallId are required" });

        // Generate a unique short code
        string shortCode = GenerateShortCode();
        while (await _context.ShortUrls.AnyAsync(s => s.ShortCode == shortCode))
        {
            shortCode = GenerateShortCode();
        }

        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            ShortCode = shortCode,
            TargetUrl = request.TargetUrl,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = request.ExpiresAt
        };
        _context.ShortUrls.Add(shortUrl);

        // Save short URL to CastingCall
        var castingCall = await _context.CastingCalls.FindAsync(request.CastingCallId);
        if (castingCall == null)
            return NotFound(new { Message = "CastingCall not found" });
        var baseUrl = request.BaseUrl?.TrimEnd('/') ?? "https://localhost";
        var fullShortUrl = $"{baseUrl}/{shortCode}";
        castingCall.ShortUrl = fullShortUrl;
        await _context.SaveChangesAsync();

        return Ok(new { shortUrl = fullShortUrl, shortCode });
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectShortUrl(string shortCode)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        if (shortUrl == null || (shortUrl.ExpiresAt.HasValue && shortUrl.ExpiresAt < DateTime.UtcNow))
        {
            return NotFound();
        }
        return Redirect(shortUrl.TargetUrl);
    }

    private static string GenerateShortCode(int length = 7)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var data = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);
        var sb = new StringBuilder(length);
        foreach (var b in data)
        {
            sb.Append(chars[b % chars.Length]);
        }
        return sb.ToString();
    }
}

public class CreateShortUrlRequest
{
    public string TargetUrl { get; set; } = null!;
    public DateTime? ExpiresAt { get; set; }
    public string? BaseUrl { get; set; } // Optional, for local/dev
    public Guid CastingCallId { get; set; }
}
