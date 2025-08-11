using bdDevCRM.RepositoriesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


public class LogActionAttribute : IAsyncActionFilter
{
  private readonly ILoggerManager _logger;

  public LogActionAttribute(ILoggerManager logger)
  {
    _logger = logger;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var stopwatch = Stopwatch.StartNew();
    var request = context.HttpContext.Request;
    var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
    var method = request.Method;
    var path = request.Path;
    var queryString = request.QueryString.HasValue ? request.QueryString.Value : "N/A";

    // Sensitive header filtering
    var filteredHeaders = request.Headers
        .Select(h => $"{h.Key}: {(IsSensitiveKey(h.Key) ? "[REDACTED]" : h.Value.ToString())}");
    var headers = string.Join(", ", filteredHeaders);

    // Read request body safely
    string requestBody = "N/A";
    if (request.ContentLength > 0 && request.Body.CanRead)
    {
      request.EnableBuffering();
      using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);
      var rawBody = await reader.ReadToEndAsync();
      request.Body.Position = 0;

      // Truncate large bodies
      if (rawBody.Length > 1000)
        rawBody = rawBody.Substring(0, 1000) + "... [TRUNCATED]";

      // Mask sensitive fields
      requestBody = MaskSensitiveData(rawBody);
    }

    _logger.LogInfo($"[REQUEST] User: {user}, Method: {method}, Path: {path}, Query: {queryString}, Headers: {headers}, Body: {requestBody}");

    // Execute action
    ActionExecutedContext executedContext = null;
    try
    {
      executedContext = await next();
    }
    finally
    {
      stopwatch.Stop();
      var statusCode = context.HttpContext.Response.StatusCode;

      // Exception details
      string exceptionDetails = "None";
      if (executedContext?.Exception != null)
      {
        exceptionDetails = $"{executedContext.Exception.Message} | StackTrace: {executedContext.Exception.StackTrace}";
      }

      _logger.LogInfo($"[RESPONSE] Status Code: {statusCode}, Execution Time: {stopwatch.ElapsedMilliseconds} ms, Exception: {exceptionDetails}");
    }
  }

  private bool IsSensitiveKey(string key)
  {
    var sensitiveKeys = new[] { "password", "token", "authorization", "apikey" };
    return sensitiveKeys.Any(s => key.Contains(s, StringComparison.OrdinalIgnoreCase));
  }

  private string MaskSensitiveData(string body)
  {
    if (string.IsNullOrEmpty(body)) return body;

    // Mask common sensitive fields in JSON
    var sensitiveFields = new[] { "password", "token", "authorization", "apikey" };
    foreach (var field in sensitiveFields)
    {
      var regex = new Regex($"(\"{field}\"\\s*:\\s*\").*?(\")", RegexOptions.IgnoreCase);
      body = regex.Replace(body, $"$1[REDACTED]$2");
    }
    return body;
  }
}



//public class LogActionAttribute : IActionFilter
//{
//  private readonly ILoggerManager _logger;
//  private Stopwatch _stopwatch;

//  public LogActionAttribute(ILoggerManager logger)
//  {
//    _logger = logger;
//  }

//  public void OnActionExecuting(ActionExecutingContext context)
//  {
//    _stopwatch = Stopwatch.StartNew();

//    var request = context.HttpContext.Request;
//    var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
//    var method = request.Method;
//    var path = request.Path;
//    var queryString = request.QueryString.HasValue ? request.QueryString.Value : "N/A";
//    var headers = string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"));

//    // Request Body Read
//    string requestBody = "N/A";
//    if (request.ContentLength > 0 && request.Body.CanRead)
//    {
//      request.EnableBuffering();
//      using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
//      {
//        requestBody = reader.ReadToEndAsync().Result;
//      }
//      request.Body.Position = 0;
//    }

//    // Final Log Message
//    var requestLog = $"[REQUEST] User: {user}, Method: {method}, Path: {path}, Query: {queryString}, Headers: {headers}, Body: {requestBody}";
//    _logger.LogInfo(requestLog);
//  }

//  public void OnActionExecuted(ActionExecutedContext context)
//  {
//    _stopwatch.Stop();
//    var elapsedTime = _stopwatch.ElapsedMilliseconds;

//    var response = context.HttpContext.Response;
//    var statusCode = response.StatusCode;

//    // Exception Handling
//    string exceptionMessage = context.Exception != null ? context.Exception.Message : "None";

//    // Final Log Message
//    var responseLog = $"[RESPONSE] Status Code: {statusCode}, Execution Time: {elapsedTime} ms, Exception: {exceptionMessage}";
//    _logger.LogInfo(responseLog);
//  }
//}
