using Microsoft.AspNetCore.Mvc;

namespace Route4MoviePlug.Api.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Health()
    {
        return Ok(new
        {
            Service = "Route4-MoviePlug API",
            Status = "Running",
            Version = "1.0.0",
            Endpoints = new
            {
                Clients = "/api/clients",
                SplashPage = "/api/clients/{slug}/splashpage"
            }
        });
    }

    [HttpGet("api/health")]
    public IActionResult ApiHealth()
    {
        return Ok(new { status = "ok", service = "Route4-MoviePlug API" });
    }
}
