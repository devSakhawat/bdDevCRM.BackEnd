namespace bdDevCRM.Api.Middleware;

/// <summary>
/// Middleware for adding rate limiting information headers to responses
/// Provides X-RateLimit-* headers for client-side throttling
/// </summary>
public class RateLimitHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitHeaderMiddleware> _logger;
    private readonly IConfiguration _configuration;

    // Default rate limits (can be overridden via configuration)
    private readonly int _defaultLimit;
    private readonly int _defaultWindowSeconds;

    public RateLimitHeaderMiddleware(
        RequestDelegate next,
        ILogger<RateLimitHeaderMiddleware> logger,
        IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;

        // Load rate limit configuration
        _defaultLimit = _configuration.GetValue("RateLimit:DefaultLimit", 1000);
        _defaultWindowSeconds = _configuration.GetValue("RateLimit:WindowSeconds", 3600);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get rate limit configuration for the current endpoint
        var (limit, remaining, resetTime) = GetRateLimitInfo(context);

        // Add rate limit headers
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-RateLimit-Limit"] = limit.ToString();
            context.Response.Headers["X-RateLimit-Remaining"] = remaining.ToString();
            context.Response.Headers["X-RateLimit-Reset"] = resetTime.ToString();

            // Additional headers for better client experience
            context.Response.Headers["X-RateLimit-Window"] = _defaultWindowSeconds.ToString();
            context.Response.Headers["X-RateLimit-Policy"] = GetRateLimitPolicy(context);

            return Task.CompletedTask;
        });

        await _next(context);
    }

    private (int limit, int remaining, long resetTime) GetRateLimitInfo(HttpContext context)
    {
        // In a real implementation, this would check actual rate limit counters
        // from Redis/memory cache based on user identity or IP address

        var path = context.Request.Path.Value?.ToLower() ?? "";
        var user = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

        // Determine limit based on endpoint type
        int limit = GetLimitForEndpoint(path);

        // For demo purposes, returning mock data
        // In production, you would check actual usage from cache/database
        int remaining = limit - GetCurrentUsage(user, path);

        // Calculate reset time (next hour)
        var now = DateTimeOffset.UtcNow;
        var resetTime = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, 0, 0, TimeSpan.Zero)
            .AddHours(1)
            .ToUnixTimeSeconds();

        return (limit, Math.Max(0, remaining), resetTime);
    }

    private int GetLimitForEndpoint(string path)
    {
        // Higher limits for read operations, lower for writes
        if (path.Contains("/api/authentication") || path.Contains("/api/auth"))
            return 50; // Strict limit for auth endpoints

        if (path.Contains("/api/upload") || path.Contains("/api/import"))
            return 100; // Limit for resource-intensive operations

        // Default limit
        return _defaultLimit;
    }

    private int GetCurrentUsage(string identifier, string path)
    {
        // In production, this would query actual usage from cache
        // For now, return a mock value
        return new Random().Next(0, 100);
    }

    private string GetRateLimitPolicy(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower() ?? "";

        if (path.Contains("/api/authentication") || path.Contains("/api/auth"))
            return "strict";

        if (path.Contains("/api/upload") || path.Contains("/api/import"))
            return "resource-intensive";

        return "standard";
    }
}
