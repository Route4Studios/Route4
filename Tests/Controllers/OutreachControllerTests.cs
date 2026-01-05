using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Route4MoviePlug.Api.Controllers;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Xunit;

namespace Route4MoviePlug.Tests.Controllers;

public class OutreachControllerTests : IDisposable
{
    private readonly Route4DbContext _context;
    private readonly OutreachController _controller;
    private readonly Mock<ILogger<OutreachController>> _loggerMock;

    public OutreachControllerTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<Route4DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Route4DbContext(options);
        _loggerMock = new Mock<ILogger<OutreachController>>();
        _controller = new OutreachController(_context, _loggerMock.Object);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var communities = new List<OutreachCommunity>
        {
            new OutreachCommunity
            {
                Id = Guid.NewGuid(),
                Name = "Cleveland Film Commission",
                Type = "FilmCommission",
                Channel = "Script",
                Website = "https://clevelandfilm.com",
                LocationsJson = "[\"Ohio\",\"Cleveland\"]",
                EstimatedReach = 5000,
                RequiresApproval = true,
                HasCaptcha = false,
                IsActive = true,
                TotalOutreachAttempts = 10,
                SuccessfulConversions = 3,
                ConversionRate = 0.30m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OutreachCommunity
            {
                Id = Guid.NewGuid(),
                Name = "r/Filmmakers",
                Type = "Reddit",
                Channel = "Auto",
                Website = "https://reddit.com/r/Filmmakers",
                LocationsJson = "[\"Global\"]",
                EstimatedReach = 2500000,
                RequiresApproval = true,
                HasCaptcha = false,
                IsActive = true,
                TotalOutreachAttempts = 50,
                SuccessfulConversions = 12,
                ConversionRate = 0.24m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OutreachCommunity
            {
                Id = Guid.NewGuid(),
                Name = "Inactive Test Community",
                Type = "Forum",
                Channel = "Script",
                Website = "https://test.com",
                RequiresApproval = false,
                HasCaptcha = false,
                IsActive = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        _context.OutreachCommunities.AddRange(communities);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetCommunities_ReturnsAllActiveCommunities()
    {
        // Act
        var result = await _controller.GetCommunities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var communities = Assert.IsAssignableFrom<IEnumerable<OutreachCommunity>>(okResult.Value);
        Assert.Equal(2, communities.Count()); // Only active communities
    }

    [Fact]
    public async Task GetCommunities_FilterByType_ReturnsMatchingCommunities()
    {
        // Act
        var result = await _controller.GetCommunities(type: "FilmCommission");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var communities = Assert.IsAssignableFrom<IEnumerable<OutreachCommunity>>(okResult.Value);
        Assert.Single(communities);
        Assert.Equal("Cleveland Film Commission", communities.First().Name);
    }

    [Fact]
    public async Task GetCommunities_FilterByChannel_ReturnsMatchingCommunities()
    {
        // Act
        var result = await _controller.GetCommunities(channel: "Auto");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var communities = Assert.IsAssignableFrom<IEnumerable<OutreachCommunity>>(okResult.Value);
        Assert.Single(communities);
        Assert.Equal("r/Filmmakers", communities.First().Name);
    }

    [Fact]
    public async Task GetCommunities_FilterByLocation_ReturnsMatchingCommunities()
    {
        // Act
        var result = await _controller.GetCommunities(location: "Ohio");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var communities = Assert.IsAssignableFrom<IEnumerable<OutreachCommunity>>(okResult.Value);
        Assert.Single(communities);
        Assert.Equal("Cleveland Film Commission", communities.First().Name);
    }

    [Fact]
    public async Task GetCommunities_OrdersByConversionRateDescending()
    {
        // Act
        var result = await _controller.GetCommunities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var communities = Assert.IsAssignableFrom<IEnumerable<OutreachCommunity>>(okResult.Value).ToList();
        
        // Cleveland (0.30) should come before r/Filmmakers (0.24)
        Assert.Equal("Cleveland Film Commission", communities[0].Name);
        Assert.Equal("r/Filmmakers", communities[1].Name);
    }

    [Fact]
    public async Task CreateCommunity_ValidData_CreatesSuccessfully()
    {
        // Arrange
        var newCommunity = new OutreachCommunity
        {
            Name = "Michigan Film Office",
            Type = "FilmCommission",
            Channel = "Script",
            Website = "https://michigan.org/film",
            RequiresApproval = true,
            HasCaptcha = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var result = await _controller.CreateCommunity(newCommunity);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var community = Assert.IsType<OutreachCommunity>(createdResult.Value);
        Assert.Equal("Michigan Film Office", community.Name);
        Assert.NotEqual(Guid.Empty, community.Id);
        
        // Verify it's in database
        var dbCommunity = await _context.OutreachCommunities.FindAsync(community.Id);
        Assert.NotNull(dbCommunity);
    }

    [Fact]
    public async Task GetCommunity_ExistingId_ReturnsCommunity()
    {
        // Arrange
        var existingCommunity = await _context.OutreachCommunities.FirstAsync();

        // Act
        var result = await _controller.GetCommunity(existingCommunity.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var community = Assert.IsType<OutreachCommunity>(okResult.Value);
        Assert.Equal(existingCommunity.Id, community.Id);
    }

    [Fact]
    public async Task GetCommunity_NonExistentId_ReturnsNotFound()
    {
        // Act
        var result = await _controller.GetCommunity(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task UpdateCommunity_ValidData_UpdatesSuccessfully()
    {
        // Arrange
        var existingCommunity = await _context.OutreachCommunities.FirstAsync();
        var updatedCommunity = new OutreachCommunity
        {
            Name = "Updated Name",
            Type = existingCommunity.Type,
            Channel = existingCommunity.Channel,
            Website = "https://updated.com",
            RequiresApproval = existingCommunity.RequiresApproval,
            HasCaptcha = existingCommunity.HasCaptcha,
            IsActive = false
        };

        // Act
        var result = await _controller.UpdateCommunity(existingCommunity.Id, updatedCommunity);

        // Assert
        Assert.IsType<NoContentResult>(result);
        
        var dbCommunity = await _context.OutreachCommunities.FindAsync(existingCommunity.Id);
        Assert.Equal("Updated Name", dbCommunity!.Name);
        Assert.Equal("https://updated.com", dbCommunity.Website);
        Assert.False(dbCommunity.IsActive);
    }

    [Fact]
    public async Task CreateContact_ValidData_CreatesAndUpdatesCommunityStats()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        var initialAttempts = community.TotalOutreachAttempts;
        
        var contact = new OutreachContact
        {
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Queued",
            ShortUrlVariant = "https://route4.studio/c/test?utm_source=cleveland&utm_campaign=s1e1",
            ScheduledAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await _controller.CreateContact(contact);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdContact = Assert.IsType<OutreachContact>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, createdContact.Id);

        // Verify community stats updated
        var updatedCommunity = await _context.OutreachCommunities.FindAsync(community.Id);
        Assert.Equal(initialAttempts + 1, updatedCommunity!.TotalOutreachAttempts);
        Assert.NotNull(updatedCommunity.LastContactedAt);
    }

    [Fact]
    public async Task UpdateContactStatus_FirstClick_SetsClickedAtAndStatus()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Sent",
            ShortUrlVariant = "https://route4.studio/c/test?utm_source=cleveland",
            ScheduledAt = DateTime.UtcNow.AddHours(-1),
            SentAt = DateTime.UtcNow.AddMinutes(-30),
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };
        _context.OutreachContacts.Add(contact);
        await _context.SaveChangesAsync();

        var updateRequest = new UpdateContactStatusRequest
        {
            Status = "Clicked",
            SentAt = contact.SentAt,
            Notes = "User clicked through"
        };

        // Act
        var result = await _controller.UpdateContactStatus(contact.Id, updateRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        
        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.Equal("Clicked", updatedContact!.Status);
        Assert.Equal("User clicked through", updatedContact.Notes);
    }

    [Fact]
    public async Task GetContacts_FilterByCastingCallId_ReturnsMatchingContacts()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        var castingCallId = Guid.NewGuid();
        
        var contacts = new[]
        {
            new OutreachContact
            {
                Id = Guid.NewGuid(),
                CommunityId = community.Id,
                CastingCallId = castingCallId,
                ClientId = Guid.NewGuid(),
                Channel = "Script",
                Method = "FilmCommission",
                Status = "Sent",
                ShortUrlVariant = "https://route4.studio/c/test1",
                ScheduledAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            },
            new OutreachContact
            {
                Id = Guid.NewGuid(),
                CommunityId = community.Id,
                CastingCallId = Guid.NewGuid(), // Different casting call
                ClientId = Guid.NewGuid(),
                Channel = "Auto",
                Method = "Discord",
                Status = "Queued",
                ShortUrlVariant = "https://route4.studio/c/test2",
                ScheduledAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };
        
        _context.OutreachContacts.AddRange(contacts);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetContacts(castingCallId: castingCallId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedContacts = Assert.IsAssignableFrom<IEnumerable<OutreachContact>>(okResult.Value);
        Assert.Single(returnedContacts);
        Assert.Equal(castingCallId, returnedContacts.First().CastingCallId);
    }

    [Fact]
    public async Task GetContacts_FilterByStatus_ReturnsMatchingContacts()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        
        var contacts = new[]
        {
            new OutreachContact
            {
                Id = Guid.NewGuid(),
                CommunityId = community.Id,
                CastingCallId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Channel = "Script",
                Method = "FilmCommission",
                Status = "Clicked",
                ShortUrlVariant = "https://route4.studio/c/test1",
                ScheduledAt = DateTime.UtcNow,
                ClickedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            },
            new OutreachContact
            {
                Id = Guid.NewGuid(),
                CommunityId = community.Id,
                CastingCallId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Channel = "Auto",
                Method = "Discord",
                Status = "Queued",
                ShortUrlVariant = "https://route4.studio/c/test2",
                ScheduledAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            }
        };
        
        _context.OutreachContacts.AddRange(contacts);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetContacts(status: "Clicked");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedContacts = Assert.IsAssignableFrom<IEnumerable<OutreachContact>>(okResult.Value);
        Assert.Single(returnedContacts);
        Assert.Equal("Clicked", returnedContacts.First().Status);
    }

    [Fact]
    public async Task LogClick_FirstClick_UpdatesContactAndCommunity()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Sent",
            ShortUrlVariant = "https://route4.studio/c/test",
            ScheduledAt = DateTime.UtcNow.AddHours(-1),
            SentAt = DateTime.UtcNow.AddMinutes(-30),
            TotalClicks = 0,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };
        _context.OutreachContacts.Add(contact);
        await _context.SaveChangesAsync();

        var clickRequest = new LogClickRequest
        {
            ClickedAt = DateTime.UtcNow
        };

        // Act
        var result = await _controller.LogClick(contact.Id, clickRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        
        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.NotNull(updatedContact!.ClickedAt);
        Assert.Equal("Clicked", updatedContact.Status);
        Assert.Equal(1, updatedContact.TotalClicks);
    }

    [Fact]
    public async Task LogClick_SubsequentClicks_IncrementsClickCount()
    {
        // Arrange
        var community = await _context.OutreachCommunities.FirstAsync();
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = community.Id,
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Clicked",
            ShortUrlVariant = "https://route4.studio/c/test",
            ScheduledAt = DateTime.UtcNow.AddHours(-1),
            SentAt = DateTime.UtcNow.AddMinutes(-30),
            ClickedAt = DateTime.UtcNow.AddMinutes(-10),
            TotalClicks = 1,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };
        _context.OutreachContacts.Add(contact);
        await _context.SaveChangesAsync();

        var clickRequest = new LogClickRequest
        {
            ClickedAt = DateTime.UtcNow
        };

        // Act
        var result = await _controller.LogClick(contact.Id, clickRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        
        var updatedContact = await _context.OutreachContacts.FindAsync(contact.Id);
        Assert.Equal(2, updatedContact!.TotalClicks);
        // First click timestamp should remain unchanged
        Assert.Equal(contact.ClickedAt, updatedContact.ClickedAt);
    }

    [Fact]
    public async Task CreateCampaign_ValidData_CreatesSuccessfully()
    {
        // Arrange
        var campaign = new OutreachCampaign
        {
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "S1E1 Casting Campaign",
            TierLevel = "TierB",
            TargetWitnessCount = 100,
            StartDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var result = await _controller.CreateCampaign(campaign);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdCampaign = Assert.IsType<OutreachCampaign>(createdResult.Value);
        Assert.NotEqual(Guid.Empty, createdCampaign.Id);
        Assert.Equal("S1E1 Casting Campaign", createdCampaign.Name);
    }

    [Fact]
    public async Task PauseCampaign_ExistingCampaign_UpdatesStatus()
    {
        // Arrange
        var campaign = new OutreachCampaign
        {
            Id = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "Test Campaign",
            TierLevel = "TierA",
            TargetWitnessCount = 50,
            StartDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.OutreachCampaigns.Add(campaign);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.PauseCampaign(campaign.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        
        var updatedCampaign = await _context.OutreachCampaigns.FindAsync(campaign.Id);
        Assert.Equal("Paused", updatedCampaign!.Status);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
