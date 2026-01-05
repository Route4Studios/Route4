using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Route4MoviePlug.Api.Controllers;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Xunit;

namespace Route4MoviePlug.Tests.Controllers;

public class ShortUrlControllerUtmTrackingTests : IDisposable
{
    private readonly Route4DbContext _context;
    private readonly ShortUrlController _controller;
    private readonly Mock<ILogger<ShortUrlController>> _loggerMock;

    public ShortUrlControllerUtmTrackingTests()
    {
        var options = new DbContextOptionsBuilder<Route4DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Route4DbContext(options);
        _loggerMock = new Mock<ILogger<ShortUrlController>>();
        _controller = new ShortUrlController(_context, _loggerMock.Object);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var community = new OutreachCommunity
        {
            Id = Guid.NewGuid(),
            Name = "Cleveland Film Commission",
            Type = "FilmCommission",
            Channel = "Script",
            Website = "https://clevelandfilm.com",
            RequiresApproval = true,
            HasCaptcha = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            ShortCode = "abc123",
            TargetUrl = "https://route4.studio/makingofmary/vip/register",
            CreatedAt = DateTime.UtcNow
        };

        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Sent",
            ShortUrlVariant = "https://route4.studio/abc123?utm_source=cleveland_film&utm_campaign=s1e1_casting",
            ScheduledAt = DateTime.UtcNow.AddHours(-2),
            SentAt = DateTime.UtcNow.AddHours(-1),
            TotalClicks = 0,
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };

        _context.OutreachCommunities.Add(community);
        _context.ShortUrls.Add(shortUrl);
        _context.OutreachContacts.Add(contact);
        _context.SaveChanges();
    }

    [Fact]
    public async Task RedirectShortUrl_WithUtmParams_TracksClickOnMatchingContact()
    {
        // Arrange
        var shortCode = "abc123";
        var utmSource = "cleveland_film";
        var utmCampaign = "s1e1_casting";

        var contact = await _context.OutreachContacts.FirstAsync();
        Assert.Null(contact.ClickedAt);
        Assert.Equal(0, contact.TotalClicks);
        Assert.Equal("Sent", contact.Status);

        // Act
        var result = await _controller.RedirectShortUrl(shortCode, utmSource, null, utmCampaign);

        // Assert
        Assert.IsType<RedirectResult>(result);

        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.NotNull(updatedContact!.ClickedAt);
        Assert.Equal("Clicked", updatedContact.Status);
        Assert.Equal(1, updatedContact.TotalClicks);
    }

    [Fact]
    public async Task RedirectShortUrl_MultipleClicks_IncrementsClickCount()
    {
        // Arrange
        var shortCode = "abc123";
        var utmSource = "cleveland_film";

        // First click
        await _controller.RedirectShortUrl(shortCode, utmSource, null, null);
        
        var contact = await _context.OutreachContacts.FirstAsync();
        var firstClickTime = contact.ClickedAt;
        Assert.Equal(1, contact.TotalClicks);

        // Wait a moment to ensure time difference
        await Task.Delay(100);

        // Act - Second click
        var result = await _controller.RedirectShortUrl(shortCode, utmSource, null, null);

        // Assert
        Assert.IsType<RedirectResult>(result);

        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.Equal(2, updatedContact!.TotalClicks);
        Assert.Equal(firstClickTime, updatedContact.ClickedAt); // First click time preserved
    }

    [Fact]
    public async Task RedirectShortUrl_WithUtmSource_UpdatesCommunityLastContacted()
    {
        // Arrange
        var shortCode = "abc123";
        var utmSource = "cleveland_film";
        
        var community = await _context.OutreachCommunities.FirstAsync();
        var initialLastContacted = community.LastContactedAt;

        // Act
        await _controller.RedirectShortUrl(shortCode, utmSource, null, null);

        // Assert
        var updatedCommunity = await _context.OutreachCommunities.FindAsync(community.Id);
        Assert.NotEqual(initialLastContacted, updatedCommunity!.LastContactedAt);
    }

    [Fact]
    public async Task RedirectShortUrl_NoUtmParams_DoesNotTrackClick()
    {
        // Arrange
        var shortCode = "abc123";
        var contact = await _context.OutreachContacts.FirstAsync();
        var initialClicks = contact.TotalClicks;

        // Act
        var result = await _controller.RedirectShortUrl(shortCode);

        // Assert
        Assert.IsType<RedirectResult>(result);

        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.Equal(initialClicks, updatedContact!.TotalClicks); // No change
        Assert.Null(updatedContact.ClickedAt);
    }

    [Fact]
    public async Task RedirectShortUrl_NonMatchingUtmSource_DoesNotTrackClick()
    {
        // Arrange
        var shortCode = "abc123";
        var utmSource = "nonexistent_source";
        
        var contact = await _context.OutreachContacts.FirstAsync();
        var initialClicks = contact.TotalClicks;

        // Act
        var result = await _controller.RedirectShortUrl(shortCode, utmSource, null, null);

        // Assert
        Assert.IsType<RedirectResult>(result);

        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.Equal(initialClicks, updatedContact!.TotalClicks); // No change
        Assert.Null(updatedContact.ClickedAt);
    }

    [Fact]
    public async Task RedirectShortUrl_PartialUtmMatch_FindsContact()
    {
        // Arrange - Contact has both utm_source and utm_campaign in ShortUrlVariant
        var shortCode = "abc123";
        var utmSource = "cleveland_film";
        // Only provide utm_source, not utm_campaign

        // Act
        var result = await _controller.RedirectShortUrl(shortCode, utmSource, null, null);

        // Assert
        Assert.IsType<RedirectResult>(result);

        var contact = await _context.OutreachContacts.FirstAsync();
        Assert.NotNull(contact.ClickedAt);
        Assert.Equal("Clicked", contact.Status);
    }

    [Fact]
    public async Task RedirectShortUrl_InvalidShortCode_ReturnsNotFound()
    {
        // Act
        var result = await _controller.RedirectShortUrl("nonexistent", "test_source", null, null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task RedirectShortUrl_ExpiredShortUrl_ReturnsNotFound()
    {
        // Arrange
        var expiredUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            ShortCode = "expired123",
            TargetUrl = "https://example.com",
            ExpiresAt = DateTime.UtcNow.AddDays(-1), // Expired yesterday
            CreatedAt = DateTime.UtcNow.AddDays(-10)
        };
        _context.ShortUrls.Add(expiredUrl);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.RedirectShortUrl("expired123");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task RedirectShortUrl_MultipleContacts_MatchesMostRecent()
    {
        // Arrange - Create two contacts with same UTM params, different scheduled times
        var community = await _context.OutreachCommunities.FirstAsync();
        
        var olderContact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "Forum",
            Status = "Sent",
            ShortUrlVariant = "https://route4.studio/abc123?utm_source=test_community&utm_campaign=campaign1",
            ScheduledAt = DateTime.UtcNow.AddHours(-10),
            SentAt = DateTime.UtcNow.AddHours(-9),
            CreatedAt = DateTime.UtcNow.AddHours(-10)
        };

        var newerContact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "Forum",
            Status = "Sent",
            ShortUrlVariant = "https://route4.studio/abc123?utm_source=test_community&utm_campaign=campaign1",
            ScheduledAt = DateTime.UtcNow.AddHours(-2),
            SentAt = DateTime.UtcNow.AddHours(-1),
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };

        _context.OutreachContacts.AddRange(olderContact, newerContact);
        await _context.SaveChangesAsync();

        // Act
        await _controller.RedirectShortUrl("abc123", "test_community", null, "campaign1");

        // Assert - Newer contact should be updated
        var updatedNewer = await _context.OutreachContacts.FindAsync(newerContact.Id);
        var updatedOlder = await _context.OutreachContacts.FindAsync(olderContact.Id);

        Assert.NotNull(updatedNewer!.ClickedAt);
        Assert.Equal("Clicked", updatedNewer.Status);
        
        Assert.Null(updatedOlder!.ClickedAt); // Older not updated
        Assert.Equal("Sent", updatedOlder.Status);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
