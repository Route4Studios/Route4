namespace Route4MoviePlug.Api.Services;

public class StripeSettings
{
    public required string SecretKey { get; set; }
    public required string PublishableKey { get; set; }
    public required string WebhookSecret { get; set; }
    public required string ClientId { get; set; }
}
