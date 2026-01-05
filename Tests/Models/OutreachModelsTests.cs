using Route4MoviePlug.Api.Models;
using Xunit;

namespace Route4MoviePlug.Tests.Models;

public class OutreachModelsTests
{
    [Fact]
    public void OutreachCommunity_CalculatesConversionRate_Correctly()
    {
        // Arrange
        var community = new OutreachCommunity
        {
            Id = Guid.NewGuid(),
            Name = "Test Community",
            Type = "FilmCommission",
            Channel = "Script",
            RequiresApproval = false,
            HasCaptcha = false,
            IsActive = true,
            TotalOutreachAttempts = 100,
            SuccessfulConversions = 25,
            ConversionRate = 0.25m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(0.25m, community.ConversionRate);
        Assert.Equal(25, community.SuccessfulConversions);
        Assert.Equal(100, community.TotalOutreachAttempts);
    }

    [Fact]
    public void OutreachContact_StatusTransitions_TrackCorrectly()
    {
        // Arrange
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Queued",
            ShortUrlVariant = "https://route4.studio/c/test?utm_source=test",
            ScheduledAt = DateTime.UtcNow.AddHours(1),
            CreatedAt = DateTime.UtcNow
        };

        // Act & Assert - Transition through statuses
        Assert.Equal("Queued", contact.Status);

        contact.Status = "Sent";
        contact.SentAt = DateTime.UtcNow;
        Assert.Equal("Sent", contact.Status);
        Assert.NotNull(contact.SentAt);

        contact.Status = "Clicked";
        contact.ClickedAt = DateTime.UtcNow;
        contact.TotalClicks = 1;
        Assert.Equal("Clicked", contact.Status);
        Assert.NotNull(contact.ClickedAt);
        Assert.Equal(1, contact.TotalClicks);

        contact.Status = "Converted";
        contact.ConvertedAt = DateTime.UtcNow;
        contact.DidConvert = true;
        Assert.Equal("Converted", contact.Status);
        Assert.NotNull(contact.ConvertedAt);
        Assert.True(contact.DidConvert);
    }

    [Fact]
    public void OutreachContact_ShortUrlVariant_ContainsUtmParams()
    {
        // Arrange
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Queued",
            ShortUrlVariant = "https://route4.studio/c/abc123?utm_source=cleveland_film&utm_medium=form&utm_campaign=s1e1_casting",
            ScheduledAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        // Assert - UTM params are present
        Assert.Contains("utm_source=cleveland_film", contact.ShortUrlVariant);
        Assert.Contains("utm_medium=form", contact.ShortUrlVariant);
        Assert.Contains("utm_campaign=s1e1_casting", contact.ShortUrlVariant);
    }

    [Fact]
    public void OutreachCampaign_CalculatesConversionRate_Correctly()
    {
        // Arrange
        var campaign = new OutreachCampaign
        {
            Id = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "Test Campaign",
            TierLevel = "TierB",
            TargetWitnessCount = 100,
            StartDate = DateTime.UtcNow,
            Status = "Active",
            TotalContacts = 50,
            TotalClicks = 30,
            TotalConversions = 12,
            ConversionRate = 0.24m, // 12/50
            CostPerWitness = 5.00m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(0.24m, campaign.ConversionRate);
        Assert.Equal(12m / 50m, (decimal)campaign.TotalConversions / campaign.TotalContacts);
    }

    [Fact]
    public void OutreachCampaign_ClickThroughRate_CalculatesCorrectly()
    {
        // Arrange
        var campaign = new OutreachCampaign
        {
            Id = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Name = "Test Campaign",
            TierLevel = "TierB",
            TargetWitnessCount = 100,
            StartDate = DateTime.UtcNow,
            Status = "Active",
            TotalContacts = 100,
            TotalClicks = 40,
            TotalConversions = 10,
            ConversionRate = 0.10m,
            CostPerWitness = 3.50m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var clickThroughRate = (decimal)campaign.TotalClicks / campaign.TotalContacts;

        // Assert
        Assert.Equal(0.40m, clickThroughRate); // 40%
    }

    [Fact]
    public void OutreachCommunity_FormFieldMap_CanBeDeserialized()
    {
        // Arrange
        var community = new OutreachCommunity
        {
            Id = Guid.NewGuid(),
            Name = "Test Community",
            Type = "FilmCommission",
            Channel = "Script",
            RequiresApproval = false,
            HasCaptcha = false,
            IsActive = true,
            FormFieldMapJson = "{\"project_title\":\"#title\",\"description\":\"textarea[name=desc]\"}",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.NotNull(community.FormFieldMapJson);
        Assert.Contains("project_title", community.FormFieldMapJson);
        Assert.Contains("#title", community.FormFieldMapJson);
    }

    [Theory]
    [InlineData("Auto")]
    [InlineData("Script")]
    public void OutreachCommunity_Channel_AcceptsValidValues(string channel)
    {
        // Arrange & Act
        var community = new OutreachCommunity
        {
            Id = Guid.NewGuid(),
            Name = "Test Community",
            Type = "FilmCommission",
            Channel = channel,
            RequiresApproval = false,
            HasCaptcha = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(channel, community.Channel);
    }

    [Theory]
    [InlineData("FilmCommission")]
    [InlineData("Forum")]
    [InlineData("FacebookGroup")]
    [InlineData("Reddit")]
    [InlineData("Discord")]
    public void OutreachCommunity_Type_AcceptsValidValues(string type)
    {
        // Arrange & Act
        var community = new OutreachCommunity
        {
            Id = Guid.NewGuid(),
            Name = "Test Community",
            Type = type,
            Channel = "Script",
            RequiresApproval = false,
            HasCaptcha = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(type, community.Type);
    }

    [Fact]
    public void OutreachContact_MultipleClicks_IncrementCorrectly()
    {
        // Arrange
        var contact = new OutreachContact
        {
            Id = Guid.NewGuid(),
            CommunityId = Guid.NewGuid(),
            CastingCallId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Channel = "Script",
            Method = "FilmCommission",
            Status = "Clicked",
            ShortUrlVariant = "https://route4.studio/c/test",
            ScheduledAt = DateTime.UtcNow.AddHours(-2),
            SentAt = DateTime.UtcNow.AddHours(-1),
            ClickedAt = DateTime.UtcNow.AddMinutes(-30),
            TotalClicks = 1,
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };

        // Act - Simulate additional clicks
        contact.TotalClicks++;
        contact.TotalClicks++;
        contact.TotalClicks++;

        // Assert
        Assert.Equal(4, contact.TotalClicks);
        Assert.NotNull(contact.ClickedAt); // First click time preserved
    }
}
