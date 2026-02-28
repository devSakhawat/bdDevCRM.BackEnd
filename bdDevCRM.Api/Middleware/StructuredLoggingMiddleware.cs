using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace bdDevCRM.Api.Middleware;

/// <summary>
/// Enhanced middleware for structured request/response logging
/// Captures detailed information for debugging and monitoring
/// </summary>
public class StructuredLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<StructuredLoggingMiddleware> _logger;
    private readonly IConfiguration _configuration;
    private readonly bool _isEnabled;
    private readonly bool _logRequestBody;
    private readonly bool _logResponseBody;
    private readonly int _maxBodySize;

    public StructuredLoggingMiddleware(
        RequestDelegate next,
        ILogger<StructuredLoggingMiddleware> logger,
        IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;

        _isEnabled = _configuration.GetValue("Logging:StructuredLogging:Enabled", true);
        _logRequestBody = _configuration.GetValue("Logging:StructuredLogging:LogRequestBody", true);
        _logResponseBody = _configuration.GetValue("Logging:StructuredLogging:LogResponseBody", false);
        _maxBodySize = _configuration.GetValue("Logging:StructuredLogging:MaxBodySize", 4096);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_isEnabled)
        {
            await _next(context);
            return;
        }

        var correlationId = Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;

        var stopwatch = Stopwatch.StartNew();
        var requestLog = await CaptureRequest(context);

        // Replace response stream to capture response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var responseLog = await CaptureResponse(context, responseBody);

            // Log structured data
            LogRequestResponse(requestLog, responseLog, stopwatch.ElapsedMilliseconds, correlationId);

            // Copy response to original stream
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task<RequestLog> CaptureRequest(HttpContext context)
    {
        var request = context.Request;

        var requestLog = new RequestLog
        {
            Method = request.Method,
            Path = request.Path,
            QueryString = request.QueryString.ToString(),
            Scheme = request.Scheme,
            Host = request.Host.ToString(),
            ContentType = request.ContentType,
            ContentLength = request.ContentLength,
            Headers = CaptureHeaders(request.Headers),
            UserAgent = request.Headers["User-Agent"].ToString(),
            RemoteIp = context.Connection.RemoteIpAddress?.ToString(),
            User = context.User?.Identity?.Name
        };

        // Capture request body if enabled
        if (_logRequestBody && request.ContentLength.HasValue && request.ContentLength > 0)
        {
            request.EnableBuffering();
            requestLog.Body = await ReadBodyAsync(request.Body);
            request.Body.Seek(0, SeekOrigin.Begin);
        }

        return requestLog;
    }

    private async Task<ResponseLog> CaptureResponse(HttpContext context, MemoryStream responseBody)
    {
        var response = context.Response;

        var responseLog = new ResponseLog
        {
            StatusCode = response.StatusCode,
            ContentType = response.ContentType,
            ContentLength = responseBody.Length,
            Headers = CaptureHeaders(response.Headers)
        };

        // Capture response body if enabled
        if (_logResponseBody && responseBody.Length > 0)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            responseLog.Body = await ReadBodyAsync(responseBody);
        }

        return responseLog;
    }

    private Dictionary<string, string> CaptureHeaders(IHeaderDictionary headers)
    {
        var sensitiveHeaders = new[] { "authorization", "cookie", "x-api-key", "x-auth-token" };
        var capturedHeaders = new Dictionary<string, string>();

        foreach (var header in headers)
        {
            var key = header.Key.ToLower();
            if (sensitiveHeaders.Contains(key))
            {
                capturedHeaders[header.Key] = "[REDACTED]";
            }
            else
            {
                capturedHeaders[header.Key] = header.Value.ToString();
            }
        }

        return capturedHeaders;
    }

    private async Task<string> ReadBodyAsync(Stream body)
    {
        try
        {
            using var reader = new StreamReader(
                body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 4096,
                leaveOpen: true);

            var bodyContent = await reader.ReadToEndAsync();

            // Truncate if too large
            if (bodyContent.Length > _maxBodySize)
            {
                bodyContent = bodyContent.Substring(0, _maxBodySize) + "... [TRUNCATED]";
            }

            // Mask sensitive fields
            bodyContent = MaskSensitiveData(bodyContent);

            return bodyContent;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to read body stream");
            return "[ERROR READING BODY]";
        }
    }

    private string MaskSensitiveData(string content)
    {
        var sensitiveFields = new[] { "password", "token", "apikey", "secret", "authorization" };

        foreach (var field in sensitiveFields)
        {
            // Simple pattern matching for JSON fields
            var pattern = $"\"{field}\"\\s*:\\s*\"([^\"]+)\"";
            content = System.Text.RegularExpressions.Regex.Replace(
                content,
                pattern,
                $"\"{field}\": \"[REDACTED]\"",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        return content;
    }

    private void LogRequestResponse(RequestLog request, ResponseLog response, long durationMs, string correlationId)
    {
        var logData = new
        {
            CorrelationId = correlationId,
            Request = request,
            Response = response,
            DurationMs = durationMs,
            Timestamp = DateTime.UtcNow
        };

        var logLevel = response.StatusCode >= 500 ? LogLevel.Error :
                       response.StatusCode >= 400 ? LogLevel.Warning :
                       durationMs > 5000 ? LogLevel.Warning :
                       LogLevel.Information;

        _logger.Log(logLevel,
            "HTTP {Method} {Path} responded {StatusCode} in {Duration}ms | CorrelationId: {CorrelationId}",
            request.Method, request.Path, response.StatusCode, durationMs, correlationId);

        // Log full details at debug level
        _logger.LogDebug("Request/Response Details: {LogData}",
            JsonSerializer.Serialize(logData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }));
    }

    private class RequestLog
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string UserAgent { get; set; }
        public string RemoteIp { get; set; }
        public string User { get; set; }
        public string Body { get; set; }
    }

    private class ResponseLog
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }
}
