namespace Route4MoviePlug.Api.Models;

public class Client
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    // Theme customization
    public string? ThemePrimaryColor { get; set; }
    public string? ThemeAccentColor { get; set; }
    public string? ThemeFontFamily { get; set; }
    public string? ThemeFontSize { get; set; }

    public ICollection<SplashPage> SplashPages { get; set; } = new List<SplashPage>();
    public ICollection<CastingCall> CastingCalls { get; set; } = new List<CastingCall>();

    // Route4 Architecture Navigation
    public DiscordConfiguration? DiscordConfiguration { get; set; }
}

public class SplashPage
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
}

public class Benefit
{
    public Guid Id { get; set; }
    public Guid SplashPageId { get; set; }
    public SplashPage? SplashPage { get; set; }
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int DisplayOrder { get; set; }
}

// Signal I — Casting Call (L2 Visibility - Public)
public class CastingCall
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public required string Title { get; set; }
    public required string ProjectStatus { get; set; }
    public required string ToneAndIntent { get; set; }
    public required string RolesDescription { get; set; }
    public required string Constraints { get; set; }
    public required string HowToRespond { get; set; }
    public bool IsActive { get; set; }
    public string? BackgroundImageUrl { get; set; }
    public string? ShortUrl { get; set; } // Added for direct short URL storage
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<CastingCallResponse> Responses { get; set; } = new List<CastingCallResponse>();
}

// Casting Call Response — witness presence logging
public class CastingCallResponse
{
    public Guid Id { get; set; }
    public Guid CastingCallId { get; set; }
    public CastingCall? CastingCall { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string RoleInterest { get; set; }
    public string? Note { get; set; }
    public DateTime RespondedAt { get; set; }
}

public class SplashPageDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public string? LogoUrl { get; set; }
    public List<BenefitDto> Benefits { get; set; } = new();
}

public class BenefitDto
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}

public class CreateSplashPageRequest
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public List<CreateBenefitRequest> Benefits { get; set; } = new();
}

public class CreateBenefitRequest
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}

// Casting Call DTOs
public class CastingCallDto
{
    public Guid Id { get; set; }
    public string ClientSlug { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required string ProjectStatus { get; set; }
    public required string ToneAndIntent { get; set; }
    public required string RolesDescription { get; set; }
    public required string Constraints { get; set; }
    public required string HowToRespond { get; set; }
    public bool IsActive { get; set; }
    public string? BackgroundImageUrl { get; set; }
}

public class CreateCastingCallRequest
{
    public required string Title { get; set; }
    public required string ProjectStatus { get; set; }
    public required string ToneAndIntent { get; set; }
    public required string RolesDescription { get; set; }
    public required string Constraints { get; set; }
    public required string HowToRespond { get; set; }
    public string? BackgroundImageUrl { get; set; }
}

public class CastingCallResponseRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string RoleInterest { get; set; }
    public string? Note { get; set; }
}

// Membership Tiers
public class MembershipTier
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string PriceInterval { get; set; } // "month", "year", "one-time"
    public string? TagLine { get; set; }
    public bool IsFeatured { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public ICollection<TierFeature> Features { get; set; } = new List<TierFeature>();
}

public class TierFeature
{
    public Guid Id { get; set; }
    public Guid TierId { get; set; }
    public MembershipTier? Tier { get; set; }
    public required string Text { get; set; }
    public bool IsHighlighted { get; set; }
    public int DisplayOrder { get; set; }
}

public class MembershipTierDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string PriceInterval { get; set; }
    public string? TagLine { get; set; }
    public bool IsFeatured { get; set; }
    public List<TierFeatureDto> Features { get; set; } = new();
}

public class TierFeatureDto
{
    public required string Text { get; set; }
    public bool IsHighlighted { get; set; }
}

// ============== STRIPE PAYMENT MODELS ==============

public class StripeAccount
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    
    // Stripe Connect account ID (acct_xxx)
    public required string StripeAccountId { get; set; }
    
    // OAuth refresh token for Stripe Connect
    public string? RefreshToken { get; set; }
    
    // Application fee percentage (e.g., 5 for 5%)
    public decimal ApplicationFeePercent { get; set; } = 5m;
    
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    
    public DateTime ConnectedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class Payment
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    
    public Guid? MembershipTierId { get; set; }
    public MembershipTier? MembershipTier { get; set; }
    
    // Stripe payment intent ID
    public required string StripePaymentIntentId { get; set; }
    
    // Customer identifier (email or Stripe customer ID)
    public required string CustomerEmail { get; set; }
    public string? CustomerName { get; set; }
    
    public decimal Amount { get; set; }
    public required string Currency { get; set; } = "USD";
    
    // "one-time" or "subscription"
    public required string PaymentType { get; set; }
    
    // Stripe subscription ID (if subscription payment)
    public string? StripeSubscriptionId { get; set; }
    
    // "pending", "completed", "failed", "cancelled"
    public required string Status { get; set; } = "pending";
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class Invoice
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public Payment? Payment { get; set; }
    
    // Stripe invoice ID
    public required string StripeInvoiceId { get; set; }
    
    public decimal Amount { get; set; }
    
    // Route4's cut of the transaction
    public decimal PlatformFee { get; set; }
    
    // Client's cut
    public decimal ClientAmount { get; set; }
    
    public required string Status { get; set; } = "pending";
    
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}

// ============== PAYMENT DTOS ==============

public class CreatePaymentIntentRequest
{
    public Guid MembershipTierId { get; set; }
    public required string CustomerEmail { get; set; }
    public string? CustomerName { get; set; }
}

public class PaymentIntentResponse
{
    public required string ClientSecret { get; set; }
    public required string PaymentIntentId { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}

public class WebhookEventRequest
{
    public required string Id { get; set; }
    public required string Type { get; set; }
    public DateTime Created { get; set; }
    public required Dictionary<string, object> Data { get; set; }
}

// ============== DISCORD MODELS ==============

public class ChannelTemplate
{
    public required string Name { get; set; }
    public ChannelType Type { get; set; }
    public string? Topic { get; set; }
    public int Position { get; set; }
    public bool IsPrivate { get; set; }
    public List<string>? AllowedRoles { get; set; }
}

public class ChannelProvisioningResult
{
    public required string ChannelId { get; set; }
    public required string ChannelName { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public class RoleTemplate
{
    public required string Name { get; set; }
    public uint Color { get; set; }
    public bool Hoist { get; set; }
    public bool Mentionable { get; set; }
    public List<string>? Permissions { get; set; }
}

public class RoleProvisioningResult
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public enum ChannelType
{
    Text = 0,
    Voice = 2,
    Category = 4,
    Forum = 15
}
