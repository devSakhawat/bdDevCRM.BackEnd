using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Api.Middleware;

public class EnhancedAuditMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EnhancedAuditMiddleware> _logger;
    private readonly IConfiguration _configuration;

    public EnhancedAuditMiddleware(RequestDelegate next, ILogger<EnhancedAuditMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, CRMContext dbContext)
    {
        // Check if audit is enabled
        if (!_configuration.GetValue<bool>("AuditLogging:EnableAuditMiddleware", true))
        {
            await _next(context);
            return;
        }

        // Skip audit for certain paths
        if (ShouldSkipAudit(context.Request.Path))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var correlationId = context.TraceIdentifier;
        var originalBodyStream = context.Response.Body;

        try
        {
            // Capture request body for POST/PUT/PATCH
            string? requestBody = null;
            if (context.Request.Method != "GET" && context.Request.ContentType?.Contains("application/json") == true)
            {
                context.Request.EnableBuffering();
                requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Execute request
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();

            // Create audit log
            var auditLog = CreateAuditLog(context, requestBody, stopwatch.ElapsedMilliseconds, correlationId);

            // Save audit log asynchronously
            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = context.RequestServices.CreateScope();
                    var scopedContext = scope.ServiceProvider.GetRequiredService<CRMContext>();
                    scopedContext.AuditLogs.Add(auditLog);
                    await scopedContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save audit log for {Path}", context.Request.Path);
                }
            });

            // Copy response back
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Error in audit middleware");

            // Log the exception
            var errorAuditLog = CreateErrorAuditLog(context, ex, stopwatch.ElapsedMilliseconds, correlationId);
            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = context.RequestServices.CreateScope();
                    var scopedContext = scope.ServiceProvider.GetRequiredService<CRMContext>();
                    scopedContext.AuditLogs.Add(errorAuditLog);
                    await scopedContext.SaveChangesAsync();
                }
                catch { }
            });

            throw;
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private bool ShouldSkipAudit(PathString path)
    {
        var pathValue = path.Value?.ToLower() ?? string.Empty;
        var skipPaths = _configuration.GetSection("AuditLogging:SkipPaths").Get<string[]>() ?? Array.Empty<string>();

        foreach (var skipPath in skipPaths)
        {
            if (pathValue.Contains(skipPath.ToLower()))
                return true;
        }

        return false;
    }

    private AuditLog CreateAuditLog(HttpContext context, string? requestBody, long durationMs, string correlationId)
    {
        var user = context.User;
        var request = context.Request;
        var response = context.Response;

        return new AuditLog
        {
            // Who
            UserId = GetUserId(user),
            Username = user?.Identity?.Name ?? "Anonymous",
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = request.Headers["User-Agent"].FirstOrDefault(),

            // What
            Action = GetActionFromMethod(request.Method),
            EntityType = GetEntityTypeFromPath(request.Path),
            EntityId = GetEntityIdFromPath(request.Path),
            Endpoint = $"{request.Method} {request.Path}",
            Module = GetModuleFromPath(request.Path),

            // Details
            NewValue = requestBody,

            // When
            Timestamp = DateTime.UtcNow,

            // Context
            CorrelationId = correlationId,
            SessionId = context.Session?.Id,
            RequestId = context.TraceIdentifier,

            // Result
            Success = response.StatusCode >= 200 && response.StatusCode < 300,
            StatusCode = response.StatusCode,
            DurationMs = (int)durationMs
        };
    }

    private AuditLog CreateErrorAuditLog(HttpContext context, Exception ex, long durationMs, string correlationId)
    {
        var auditLog = CreateAuditLog(context, null, durationMs, correlationId);
        auditLog.Success = false;
        auditLog.StatusCode = 500;
        auditLog.ErrorMessage = $"{ex.GetType().Name}: {ex.Message}";
        return auditLog;
    }

    private int? GetUserId(ClaimsPrincipal? user)
    {
        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    private string GetActionFromMethod(string method)
    {
        return method.ToUpper() switch
        {
            "POST" => "CREATE",
            "PUT" => "UPDATE",
            "PATCH" => "UPDATE",
            "DELETE" => "DELETE",
            "GET" => "VIEW",
            _ => method.ToUpper()
        };
    }

    private string GetEntityTypeFromPath(PathString path)
    {
        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments != null && segments.Length >= 2)
        {
            return segments[1];
        }
        return "Unknown";
    }

    private string? GetEntityIdFromPath(PathString path)
    {
        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments != null && segments.Length >= 3 && int.TryParse(segments[2], out _))
        {
            return segments[2];
        }
        return null;
    }

    private string GetModuleFromPath(PathString path)
    {
        var pathValue = path.Value?.ToLower() ?? string.Empty;

        if (pathValue.Contains("/systemadmin/")) return "SystemAdmin";
        if (pathValue.Contains("/hr/")) return "HR";
        if (pathValue.Contains("/crm/")) return "CRM";
        if (pathValue.Contains("/dms/")) return "DMS";
        if (pathValue.Contains("/authentication")) return "Authentication";

        return "General";
    }
}
