using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Route4MoviePlug.Api.Services;

public interface IStripePaymentService
{
    Task<PaymentIntentResponse> CreatePaymentIntentAsync(Guid clientId, Guid tierId, string customerEmail, string? customerName = null);
    Task<bool> ConfirmPaymentAsync(string paymentIntentId);
    Task<Payment?> GetPaymentAsync(string paymentIntentId);
    Task<StripeAccount?> GetClientStripeAccountAsync(Guid clientId);
}

public class StripePaymentService : IStripePaymentService
{
    private readonly Route4DbContext _context;
    private readonly StripeSettings _stripeSettings;
    private readonly ILogger<StripePaymentService> _logger;

    public StripePaymentService(Route4DbContext context, StripeSettings stripeSettings, ILogger<StripePaymentService> logger)
    {
        _context = context;
        _stripeSettings = stripeSettings;
        _logger = logger;
        StripeConfiguration.ApiKey = stripeSettings.SecretKey;
    }

    public async Task<PaymentIntentResponse> CreatePaymentIntentAsync(Guid clientId, Guid tierId, string customerEmail, string? customerName = null)
    {
        try
        {
            // Get tier details
            var tier = await _context.MembershipTiers
                .FirstOrDefaultAsync(t => t.Id == tierId && t.ClientId == clientId);
            
            if (tier == null)
                throw new InvalidOperationException("Membership tier not found");

            // Get client's Stripe account
            var stripeAccount = await _context.StripeAccounts
                .FirstOrDefaultAsync(s => s.ClientId == clientId && s.IsActive);
            
            if (stripeAccount == null)
                throw new InvalidOperationException("Client's Stripe account not configured");

            // Calculate amounts
            long amountInCents = (long)(tier.Price * 100);
            long applicationFeeInCents = (long)(amountInCents * stripeAccount.ApplicationFeePercent / 100);

            // Create payment intent with application fee (Route4 takes a cut)
            var intentOptions = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Description = $"{tier.Name} - {tier.Description.Substring(0, Math.Min(100, tier.Description.Length))}",
                ReceiptEmail = customerEmail,
                Metadata = new Dictionary<string, string>
                {
                    { "tier_id", tierId.ToString() },
                    { "client_id", clientId.ToString() },
                    { "customer_email", customerEmail }
                },
                ApplicationFeeAmount = applicationFeeInCents,
                OnBehalfOf = stripeAccount.StripeAccountId, // Send to client's Stripe account
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(intentOptions);

            // Save payment record
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                MembershipTierId = tierId,
                StripePaymentIntentId = paymentIntent.Id,
                CustomerEmail = customerEmail,
                CustomerName = customerName,
                Amount = tier.Price,
                Currency = "USD",
                PaymentType = tier.PriceInterval == "one-time" ? "one-time" : "subscription",
                Status = "pending",
                Description = tier.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Payment intent created: {paymentIntent.Id} for tier {tierId}");

            return new PaymentIntentResponse
            {
                ClientSecret = paymentIntent.ClientSecret ?? throw new InvalidOperationException("No client secret returned"),
                PaymentIntentId = paymentIntent.Id,
                Amount = tier.Price,
                Currency = "USD"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating payment intent: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
    {
        try
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                // Update payment record
                var payment = await _context.Payments
                    .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId);

                if (payment != null)
                {
                    payment.Status = "completed";
                    payment.CompletedAt = DateTime.UtcNow;
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Payment confirmed: {paymentIntentId}");
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error confirming payment: {ex.Message}");
            throw;
        }
    }

    public async Task<Payment?> GetPaymentAsync(string paymentIntentId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId);
    }

    public async Task<StripeAccount?> GetClientStripeAccountAsync(Guid clientId)
    {
        return await _context.StripeAccounts
            .FirstOrDefaultAsync(s => s.ClientId == clientId && s.IsActive);
    }
}
