using System.Diagnostics;
using System.IO;
using System.Text;
using bdDevCRM.RepositoriesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

public class LogActionAttribute : IActionFilter
{
  private readonly ILoggerManager _logger;
  private Stopwatch _stopwatch;

  public LogActionAttribute(ILoggerManager logger)
  {
    _logger = logger;
  }

  public void OnActionExecuting(ActionExecutingContext context)
  {
    _stopwatch = Stopwatch.StartNew();

    var request = context.HttpContext.Request;
    var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
    var method = request.Method;
    var path = request.Path;
    var queryString = request.QueryString.HasValue ? request.QueryString.Value : "N/A";
    var headers = string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"));

    // Request Body Read
    string requestBody = "N/A";
    if (request.ContentLength > 0 && request.Body.CanRead)
    {
      request.EnableBuffering();
      using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
      {
        requestBody = reader.ReadToEndAsync().Result;
      }
      request.Body.Position = 0;
    }

    // Final Log Message
    var requestLog = $"[REQUEST] User: {user}, Method: {method}, Path: {path}, Query: {queryString}, Headers: {headers}, Body: {requestBody}";
    _logger.LogInfo(requestLog);
  }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    _stopwatch.Stop();
    var elapsedTime = _stopwatch.ElapsedMilliseconds;

    var response = context.HttpContext.Response;
    var statusCode = response.StatusCode;

    // Exception Handling
    string exceptionMessage = context.Exception != null ? context.Exception.Message : "None";

    // Final Log Message
    var responseLog = $"[RESPONSE] Status Code: {statusCode}, Execution Time: {elapsedTime} ms, Exception: {exceptionMessage}";
    _logger.LogInfo(responseLog);
  }
}
