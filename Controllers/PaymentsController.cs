using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;
using Route4MoviePlug.Api.Services;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/clients/{clientSlug}/payments")]
public class PaymentsController : ControllerBase
{
    private readonly Route4DbContext _context;
    private readonly IStripePaymentService _stripeService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(Route4DbContext context, IStripePaymentService stripeService, ILogger<PaymentsController> logger)
    {
        _context = context;
        _stripeService = stripeService;
        _logger = logger;
    }

    /// <summary>
    /// Create a payment intent for a membership tier
    /// </summary>
    [HttpPost("create-intent")]
    public async Task<ActionResult<PaymentIntentResponse>> CreatePaymentIntent(
        string clientSlug,
        [FromBody] CreatePaymentIntentRequest request)
    {
        try
        {
            // Verify client exists
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

            if (client == null)
            {
                return NotFound(new { error = $"Client '{clientSlug}' not found" });
            }

            // Verify tier exists and belongs to client
            var tier = await _context.MembershipTiers
                .FirstOrDefaultAsync(t => t.Id == request.MembershipTierId && 
                                         t.ClientId == client.Id && 
                                         t.IsActive);

            if (tier == null)
            {
                return NotFound(new { error = "Membership tier not found" });
            }

            // Create payment intent
            var response = await _stripeService.CreatePaymentIntentAsync(
                client.Id, 
                request.MembershipTierId, 
                request.CustomerEmail,
                request.CustomerName);

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"Invalid operation: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating payment intent: {ex.Message}");
            return StatusCode(500, new { error = "Failed to create payment intent" });
        }
    }

    /// <summary>
    /// Confirm payment after Stripe processing
    /// </summary>
    [HttpPost("confirm")]
    public async Task<ActionResult<object>> ConfirmPayment([FromBody] Dictionary<string, string> request)
    {
        try
        {
            if (!request.TryGetValue("paymentIntentId", out var paymentIntentId))
            {
                return BadRequest(new { error = "paymentIntentId is required" });
            }

            var success = await _stripeService.ConfirmPaymentAsync(paymentIntentId);

            if (success)
            {
                var payment = await _stripeService.GetPaymentAsync(paymentIntentId);
                return Ok(new { 
                    success = true, 
                    message = "Payment confirmed",
                    payment = new {
                        id = payment?.Id,
                        amount = payment?.Amount,
                        status = payment?.Status,
                        completedAt = payment?.CompletedAt
                    }
                });
            }

            return BadRequest(new { error = "Failed to confirm payment" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error confirming payment: {ex.Message}");
            return StatusCode(500, new { error = "Failed to confirm payment" });
        }
    }

    /// <summary>
    /// Get payment details
    /// </summary>
    [HttpGet("status/{paymentIntentId}")]
    public async Task<ActionResult<object>> GetPaymentStatus(string paymentIntentId)
    {
        try
        {
            var payment = await _stripeService.GetPaymentAsync(paymentIntentId);

            if (payment == null)
            {
                return NotFound(new { error = "Payment not found" });
            }

            return Ok(new {
                id = payment.Id,
                stripePaymentIntentId = payment.StripePaymentIntentId,
                amount = payment.Amount,
                status = payment.Status,
                customerEmail = payment.CustomerEmail,
                createdAt = payment.CreatedAt,
                completedAt = payment.CompletedAt
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting payment status: {ex.Message}");
            return StatusCode(500, new { error = "Failed to get payment status" });
        }
    }

    /// <summary>
    /// Get Stripe account configuration status for client
    /// </summary>
    [HttpGet("stripe-account")]
    public async Task<ActionResult<object>> GetStripeAccount(string clientSlug)
    {
        try
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Slug == clientSlug && c.IsActive);

            if (client == null)
            {
                return NotFound(new { error = $"Client '{clientSlug}' not found" });
            }

            var stripeAccount = await _context.StripeAccounts
                .FirstOrDefaultAsync(s => s.ClientId == client.Id);

            if (stripeAccount == null)
            {
                return Ok(new { 
                    configured = false,
                    message = "Stripe account not configured for this client"
                });
            }

            return Ok(new {
                configured = stripeAccount.IsActive,
                verified = stripeAccount.IsVerified,
                applicationFeePercent = stripeAccount.ApplicationFeePercent,
                connectedAt = stripeAccount.ConnectedAt
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting Stripe account: {ex.Message}");
            return StatusCode(500, new { error = "Failed to get Stripe account" });
        }
    }
}
