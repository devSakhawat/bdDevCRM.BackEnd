using System.Diagnostics;

namespace bdDevCRM.Api.Middleware;

public class PerformanceMonitoringMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
    private readonly IConfiguration _configuration;
    private readonly int _slowRequestThresholdMs;
    private readonly int _verySlowRequestThresholdMs;

    public PerformanceMonitoringMiddleware(
        RequestDelegate next,
        ILogger<PerformanceMonitoringMiddleware> logger,
        IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
        _slowRequestThresholdMs = configuration.GetValue<int>("PerformanceMonitoring:SlowRequestThresholdMs", 1000);
        _verySlowRequestThresholdMs = configuration.GetValue<int>("PerformanceMonitoring:VerySlowRequestThresholdMs", 5000);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestPath = context.Request.Path.Value;
        var requestMethod = context.Request.Method;

        try
        {
            context.Items["RequestStartTime"] = DateTime.UtcNow;

            await _next(context);

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            // Log based on duration
            if (elapsedMs >= _verySlowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "VERY SLOW REQUEST: {Method} {Path} took {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }
            else if (elapsedMs >= _slowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "SLOW REQUEST: {Method} {Path} took {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }
            else
            {
                _logger.LogInformation(
                    "Request: {Method} {Path} completed in {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }

            // Add performance header
            context.Response.Headers["X-Response-Time-Ms"] = elapsedMs.ToString();
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex,
                "Request FAILED: {Method} {Path} after {Duration}ms",
                requestMethod, requestPath, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
