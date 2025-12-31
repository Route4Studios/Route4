using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Route4MoviePlug.Api.Middleware;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantResolutionMiddleware> _logger;

    public TenantResolutionMiddleware(RequestDelegate next, ILogger<TenantResolutionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Extract tenant from subdomain, header, or path
        var tenantId = ResolveTenantId(context);
        
        if (!string.IsNullOrEmpty(tenantId))
        {
            context.Items["TenantId"] = tenantId;
            _logger.LogInformation("Resolved tenant: {TenantId}", tenantId);
        }

        await _next(context);
    }

    private string? ResolveTenantId(HttpContext context)
    {
        // Try to get from header first (for API calls)
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var headerValue))
        {
            return headerValue.ToString();
        }

        // Try to get from subdomain
        var host = context.Request.Host.Host;
        if (host.Contains('.'))
        {
            var parts = host.Split('.');
            if (parts.Length > 2) // subdomain.route4studios.com
            {
                return parts[0];
            }
        }

        // Try to get from path (/clients/{slug}/...)
        var path = context.Request.Path.Value;
        if (path?.StartsWith("/clients/") == true)
        {
            var segments = path.Split('/');
            if (segments.Length > 2)
            {
                return segments[2];
            }
        }

        return null;
    }
}

public static class TenantResolutionMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantResolution(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantResolutionMiddleware>();
    }
}
