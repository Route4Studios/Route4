using Microsoft.AspNetCore.Mvc;
using Route4MoviePlug.Api.Services;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("api/payments")]
public class GlobalPaymentsController : ControllerBase
{
    private readonly IStripePaymentService _stripeService;
    private readonly ILogger<GlobalPaymentsController> _logger;

    public GlobalPaymentsController(IStripePaymentService stripeService, ILogger<GlobalPaymentsController> logger)
    {
        _stripeService = stripeService;
        _logger = logger;
    }

    /// <summary>
    /// Confirm payment after Stripe processing - Global endpoint
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

            _logger.LogInformation($"Attempting to confirm payment: {paymentIntentId}");

            var success = await _stripeService.ConfirmPaymentAsync(paymentIntentId);

            if (success)
            {
                var payment = await _stripeService.GetPaymentAsync(paymentIntentId);
                _logger.LogInformation($"Payment confirmed successfully: {paymentIntentId}");
                
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

            _logger.LogWarning($"Payment confirmation failed: {paymentIntentId}");
            return BadRequest(new { error = "Failed to confirm payment" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error confirming payment: {ex.Message}");
            return StatusCode(500, new { error = "Failed to confirm payment", details = ex.Message });
        }
    }
}