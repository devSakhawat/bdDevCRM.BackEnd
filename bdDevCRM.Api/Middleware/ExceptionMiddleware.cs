using bdDevCRM.Api.ApiResponseError;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.Entities.Exceptions.BaseException;
using bdDevCRM.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace bdDevCRM.Api.Middleware;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private IHostEnvironment _env;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly ILoggerManager _loggerManager;

  public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger, ILoggerManager loggerManager)
  {
    _next = next;
    _env = env;
    _logger = logger;
    _loggerManager = loggerManager;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      // Generate correlation ID
      string correlationId = Guid.NewGuid().ToString();
      _logger.LogError(ex, $"Request error: {correlationId} - {ex.Message}");
      _loggerManager.LogError($"Request error: {correlationId} - {ex.Message}");

      context.Response.ContentType = "application/json";

      // Set default response
      int statusCode = (int)HttpStatusCode.InternalServerError;
      string message = null;
      string details = null;

      // Handle specific exception types
      if (ex is System.UnauthorizedAccessException)
      {
        statusCode = (int)HttpStatusCode.Unauthorized;
        message = "You are not authorized to perform this action.";
      }
      else if (ex is KeyNotFoundException)
      {
        statusCode = (int)HttpStatusCode.NotFound;
        message = "The requested resource was not found.";
      }
      else if (ex is ValidationException)
      {
        statusCode = (int)HttpStatusCode.BadRequest;
        message = "One or more validation errors occurred.";
      }
      //  custom validation exception start
      // Update your condition blocks in the middleware with the following pattern:
      else if (ex is UsernamePasswordMismatchException userPassMismatch)
      {
        statusCode = userPassMismatch.StatusCode; // 400 // proper appropriate status code here for username/password mismatch
        message = ex.Message; // "The username or password is incorrect. Please try again."
      }
      else if (ex is BadRequestException badRequestEx)
      {
        statusCode = badRequestEx.StatusCode;
        message = ex.Message;
      }
      else if (ex is ConflictException conflictEx)
      {
        statusCode = conflictEx.StatusCode;
        message = ex.Message;
      }
      else if (ex is NotFoundException notFoundEx)
      {
        statusCode = notFoundEx.StatusCode;
        message = ex.Message;
      }
      else if (ex is ServiceUnavailableException serviceEx)
      {
        statusCode = serviceEx.StatusCode;
        message = ex.Message;
      }
      else if (ex is ForbiddenAccessException forbiddenEx)
      {
        statusCode = forbiddenEx.StatusCode;
        message = ex.Message;
      }
      else if (ex is UnauthorizedException unauthorized)
      {
        statusCode = unauthorized.StatusCode;
        message = ex.Message;
      }
      // JWT Token exception handling
      else if (ex is SecurityTokenException || ex is SecurityTokenValidationException || ex is SecurityTokenExpiredException)
      {
        statusCode = (int)HttpStatusCode.Unauthorized;

        if (ex is SecurityTokenExpiredException)
        {
          message = "Your authentication token has expired. Please log in again.";
        }
        else
        {
          message = "Invalid authentication token. Please log in again.";
        }
      }
      // Database exception handling
      else if (ex is DbUpdateException)
      {
        message = SanitizeDatabaseErrorMessage(ex);
        details = _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null;
      }
      // Generic exception handling
      else
      {
        if (_env.IsDevelopment())
        {
          message = GetMostRelevantMessage(ex);
          details = GetMostRelevantStackTrace(ex);
        }
        else
        {
          message = "An unexpected error occurred.";
        }
      }

      context.Response.StatusCode = statusCode;

      // Create enhanced ApiException with additional properties
      var response = new ApiException(statusCode, message, details)
      {
        CorrelationId = correlationId,
        ErrorType = ex.GetType().Name,
        Timestamp = DateTime.UtcNow
      };

      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
      var json = JsonSerializer.Serialize(response, options);
      await context.Response.WriteAsync(json);
    }
  }

  private string GetMostRelevantMessage(Exception exception)
  {
    if (exception.InnerException?.InnerException != null)
      return exception.InnerException.InnerException.Message;

    if (exception.InnerException != null)
      return exception.InnerException.Message;

    return exception.Message;
  }

  private string GetMostRelevantStackTrace(Exception exception)
  {
    if (exception.InnerException?.InnerException != null)
      return exception.InnerException.InnerException.StackTrace;

    if (exception.InnerException != null)
      return exception.InnerException.StackTrace;

    return exception.StackTrace;
  }

  private string SanitizeDatabaseErrorMessage(Exception exception)
  {
    string message = GetMostRelevantMessage(exception);

    // Remove potentially sensitive information from database error messages
    if (message.Contains("foreign key"))
      return "A database relation constraint has been violated. The related record cannot be deleted.";

    if (message.Contains("unique") || message.Contains("duplicate"))
      return "This data already exists. Please use a different value.";

    return "A database error occurred. Please verify your input.";
  }
}


//public class ExceptionMiddleware1
//{
//  private readonly RequestDelegate _next;
//  private readonly IHostEnvironment _env;
//  private readonly ILogger<ExceptionMiddleware1> _logger;

//  public ExceptionMiddleware1(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware1> logger)
//  {
//    _next = next;
//    _env = env;
//    _logger = logger;
//  }

//  public async Task InvokeAsync(HttpContext context)
//  {
//    try
//    {
//      await _next(context);
//    }
//    catch (Exception ex)
//    {
//      string correlationId = Guid.NewGuid().ToString();
//      _logger.LogError(ex, $"Request error: {correlationId} - {ex.Message}");

//      await HandleExceptionAsync(context, ex, correlationId);
//    }
//  }

//  private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
//  {
//    context.Response.ContentType = "application/json";

//    var response = CreateErrorResponse(exception, correlationId);
//    context.Response.StatusCode = response.StatusCode;

//    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//    var json = JsonSerializer.Serialize(response, options);

//    await context.Response.WriteAsync(json);
//  }

//  private ApiErrorResponse CreateErrorResponse(Exception exception, string correlationId)
//  {
//    // Get the most relevant exception message
//    string message = GetMostRelevantMessage(exception);
//    string stackTrace = _env.IsDevelopment() ? GetMostRelevantStackTrace(exception) : null;

//    // Default status code
//    int statusCode = (int)HttpStatusCode.InternalServerError;
//    string errorType = exception.GetType().Name;

//    // Handle specific exception types
//    if (exception is DbUpdateException)
//    {
//      // Database specific errors
//      message = SanitizeDatabaseErrorMessage(message);
//    }
//    else if (exception is UnauthorizedAccessException)
//    {
//      statusCode = (int)HttpStatusCode.Unauthorized;
//    }
//    else if (exception is KeyNotFoundException)
//    {
//      statusCode = (int)HttpStatusCode.NotFound;
//    }
//    else if (exception is ValidationException)
//    {
//      statusCode = (int)HttpStatusCode.BadRequest;
//    }
//    else if (exception is ArgumentException)
//    {
//      statusCode = (int)HttpStatusCode.BadRequest;
//    }

//    return new ApiErrorResponse
//    {
//      StatusCode = statusCode,
//      Message = message,
//      ErrorType = errorType,
//      CorrelationId = correlationId,
//      StackTrace = stackTrace,
//      Timestamp = DateTime.UtcNow
//    };
//  }

//  private string GetMostRelevantMessage(Exception exception)
//  {
//    if (exception.InnerException?.InnerException != null)
//      return exception.InnerException.InnerException.Message;

//    if (exception.InnerException != null)
//      return exception.InnerException.Message;

//    return exception.Message;
//  }

//  private string GetMostRelevantStackTrace(Exception exception)
//  {
//    if (exception.InnerException?.InnerException != null)
//      return exception.InnerException.InnerException.StackTrace;

//    if (exception.InnerException != null)
//      return exception.InnerException.StackTrace;

//    return exception.StackTrace;
//  }

//  private string SanitizeDatabaseErrorMessage(string message)
//  {
//    // Remove potentially sensitive information from database error messages
//    // This is just a simple example - you might need more sophisticated logic
//    if (message.Contains("foreign key"))
//      return "একটি ডাটাবেস রিলেশন সীমাবদ্ধতা লঙ্ঘন করা হয়েছে। সংশ্লিষ্ট রেকর্ড মুছে ফেলা যাবে না।";

//    if (message.Contains("unique") || message.Contains("duplicate"))
//      return "এই তথ্য ইতিমধ্যে বিদ্যমান। অনুগ্রহ করে একটি ভিন্ন মান ব্যবহার করুন।";

//    return "ডাটাবেসে একটি ত্রুটি ঘটেছে। অনুগ্রহ করে আপনার ইনপুট যাচাই করুন।";
//  }
//}

//public class ApiErrorResponse2
//{
//  public int StatusCode { get; set; }
//  public string Message { get; set; }
//  public string ErrorType { get; set; }
//  public string CorrelationId { get; set; }
//  public string StackTrace { get; set; }
//  public DateTime Timestamp { get; set; }
//}



//// old code 

//public class ExceptionMiddleware2
//{
//  private readonly RequestDelegate _next;
//  private IHostEnvironment _env;
//  private readonly ILogger<ExceptionMiddleware2> _logger;
//  private ILoggerManager _loggerManager;

//  public ExceptionMiddleware2(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware2> logger, ILoggerManager loggerManager)
//  {
//    _next = next;
//    _env = env;
//    _logger = logger;
//    _loggerManager = loggerManager;
//  }

//  public async Task InvokeAsync(HttpContext context)
//  {
//    try
//    {
//      await _next(context);
//    }
//    catch (Exception ex)
//    {
//      _logger.LogError(ex, ex.Message);
//      _loggerManager.LogError(ex.Message);
//      context.Response.ContentType = "application/json";
//      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

//      var response = new ApiException((int)HttpStatusCode.InternalServerError);

//      if (ex is DbUpdateException)
//      {
//        response = ex.InnerException?.InnerException != null
//           ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace)
//           : (ex.InnerException != null
//              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace)
//              : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
//        );
//      }
//      else if (_env.IsDevelopment())
//      {
//        response = ex.InnerException?.InnerException != null
//           ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace)
//           : (ex.InnerException != null
//              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace)
//              : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
//        );
//      }
//      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//      var json = JsonSerializer.Serialize(response, options);
//      await context.Response.WriteAsync(json);
//    }
//  }
//}
