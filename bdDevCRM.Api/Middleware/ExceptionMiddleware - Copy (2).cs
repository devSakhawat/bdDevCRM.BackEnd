//using bdDevCRM.RepositoriesContracts;
//using bdDevCRM.Shared.ApiResponse;
//using bdDevCRM.Shared.Exceptions;
//using bdDevCRM.Shared.Exceptions.BaseException;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.ComponentModel.DataAnnotations;
//using System.Net;
//using System.Text.Json;

//namespace bdDevCRM.Api.Middleware;

//public class ExceptionMiddleware
//{
//  private readonly RequestDelegate _next;
//  private IHostEnvironment _env;
//  private readonly ILogger<ExceptionMiddleware> _logger;
//  private readonly ILoggerManager _loggerManager;

//  public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger, ILoggerManager loggerManager)
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
//      await HandleExceptionAsync(context, ex);
//    }
//  }

//  private async Task HandleExceptionAsync(HttpContext context, Exception ex)
//  {
//    // Generate correlation ID
//    string correlationId = Guid.NewGuid().ToString();
//    _logger.LogError(ex, $"Request error: {correlationId} - {ex.Message}");
//    _loggerManager.LogError($"Request error: {correlationId} - {ex.Message}");

//    context.Response.ContentType = "application/json";

//    ApiException response = ex switch
//    {
//      // Custom validation exceptions
//      UsernamePasswordMismatchException userPassMismatch => new ApiException(
//          userPassMismatch.StatusCode,
//          ex.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(UsernamePasswordMismatchException),
//      },

//      BadRequestException badRequestEx => new ApiException(
//          badRequestEx.StatusCode,
//          ex.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(BadRequestException),
//      },

//      DuplicateRecordException dupEx => new ApiException(dupEx.StatusCode, ex.Message)
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(DuplicateRecordException),
//      },

//      ConflictException conflictEx => new ApiException(
//          conflictEx.StatusCode,
//          ex is DuplicateRecordException ? ex.Message : "Conflict occurred."
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = ex.GetType().Name,
//      },

//      NotFoundException notFoundEx => new ApiException(notFoundEx.StatusCode, ex.Message)
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(NotFoundException),
//      },

//      ServiceUnavailableException serviceEx => new ApiException(
//          serviceEx.StatusCode,
//          ex.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(ServiceUnavailableException),
//      },

//      ForbiddenAccessException forbiddenEx => new ApiException(
//          forbiddenEx.StatusCode,
//          ex.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(ForbiddenAccessException),
//      },

//      UnauthorizedException unauthorized => new ApiException(
//          unauthorized.StatusCode,
//          ex.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(UnauthorizedException),
//      },

//      // Remove DuplicateRecordException as it is already handled by ConflictException
//      InvalidCreateOperationException invalidCreateEx => new ApiException(
//          invalidCreateEx.StatusCode,
//          invalidCreateEx.Message
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = nameof(InvalidCreateOperationException),
//      },

//      // Built-in exceptions
//      System.UnauthorizedAccessException => new ApiException(
//          401,
//          "You are not authorized to perform this action."
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = "UnauthorizedAccess",
//      },

//      KeyNotFoundException => new ApiException(404, "The requested resource was not found.")
//      {
//        CorrelationId = correlationId,
//        ErrorType = "NotFound",
//      },

//      ValidationException => new ApiException(400, "One or more validation errors occurred.")
//      {
//        CorrelationId = correlationId,
//        ErrorType = "Validation",
//      },

//      // JWT Token exceptions
//      SecurityTokenExpiredException => new ApiException(
//          401,
//          "Your authentication token has expired. Please log in again."
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = "TokenExpired",
//      },

//      SecurityTokenException or SecurityTokenValidationException => new ApiException(
//          401,
//          "Invalid authentication token. Please log in again."
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = "InvalidToken",
//      },

//      // Database exceptions
//      DbUpdateException => new ApiException(500, SanitizeDatabaseErrorMessage(ex))
//      {
//        CorrelationId = correlationId,
//        ErrorType = "DatabaseError",
//        Details = _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null,
//      },

//      // Generic exception
//      _ => new ApiException(
//          500,
//          _env.IsDevelopment() ? GetMostRelevantMessage(ex) : "An unexpected error occurred."
//      )
//      {
//        CorrelationId = correlationId,
//        ErrorType = ex.GetType().Name,
//        Details = _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null,
//      },
//    };

//    context.Response.StatusCode = response.StatusCode;

//    var options = new JsonSerializerOptions
//    {
//      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//      WriteIndented = _env.IsDevelopment(),
//    };

//    var json = JsonSerializer.Serialize(response, options);
//    await context.Response.WriteAsync(json);
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

//  private string SanitizeDatabaseErrorMessage(Exception exception)
//  {
//    string message = GetMostRelevantMessage(exception);

//    if (message.Contains("foreign key"))
//      return "A database relation constraint has been violated. The related record cannot be deleted.";
//    if (message.Contains("unique") || message.Contains("duplicate"))
//      return "This data already exists. Please use a different value.";
//    return "A database error occurred. Please verify your input.";
//  }
//}

//public static class ExceptionToApiResponseMapper
//{
//  public static ApiException Map(Exception ex, IHostEnvironment env, out int statusCode)
//  {
//    string message = ex.Message;
//    string errorType = ex.GetType().Name;
//    string details = env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null;

//    (statusCode, message, errorType) = ex switch
//    {
//      // Custom exception types
//      BadRequestException => (
//          (int)HttpStatusCode.BadRequest,
//          message,
//          nameof(BadRequestException)
//      ),
//      UnauthorizedException => (
//          (int)HttpStatusCode.Unauthorized,
//          message,
//          nameof(UnauthorizedException)
//      ),
//      ForbiddenAccessException => (
//          (int)HttpStatusCode.Forbidden,
//          message,
//          nameof(ForbiddenAccessException)
//      ),
//      NotFoundException => ((int)HttpStatusCode.NotFound, message, nameof(NotFoundException)),
//      ConflictException => ((int)HttpStatusCode.Conflict, message, nameof(ConflictException)),
//      ServiceUnavailableException => (
//          (int)HttpStatusCode.ServiceUnavailable,
//          message,
//          nameof(ServiceUnavailableException)
//      ),

//      // JWT and token issues
//      SecurityTokenExpiredException => (
//          401,
//          "Your authentication token has expired. Please log in again.",
//          "TokenExpired"
//      ),
//      SecurityTokenValidationException => (
//          401,
//          "Invalid authentication token. Please log in again.",
//          "InvalidToken"
//      ),

//      // Framework
//      KeyNotFoundException => (404, "The requested resource was not found.", "KeyNotFound"),
//      UnauthorizedAccessException => (
//          401,
//          "You are not authorized to perform this action.",
//          "UnauthorizedAccess"
//      ),
//      ValidationException => (400, "One or more validation errors occurred.", "Validation"),

//      // EF Core
//      DbUpdateException => (500, SanitizeDatabaseErrorMessage(ex), "DatabaseError"),

//      // Default fallback
//      _ => (
//          500,
//          env.IsDevelopment() ? GetMostRelevantMessage(ex) : "An unexpected error occurred.",
//          errorType
//      ),
//    };

//    return new ApiException(statusCode, message) { ErrorType = errorType, Details = details };
//  }

//  private static string GetMostRelevantMessage(Exception exception)
//  {
//    if (exception.InnerException?.InnerException != null)
//      return exception.InnerException.InnerException.Message;
//    if (exception.InnerException != null)
//      return exception.InnerException.Message;
//    return exception.Message;
//  }

//  private static string GetMostRelevantStackTrace(Exception exception)
//  {
//    if (exception.InnerException?.InnerException != null)
//      return exception.InnerException.InnerException.StackTrace;
//    if (exception.InnerException != null)
//      return exception.InnerException.StackTrace;
//    return exception.StackTrace;
//  }

//  private static string SanitizeDatabaseErrorMessage(Exception exception)
//  {
//    string message = GetMostRelevantMessage(exception);

//    if (message.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
//      return "A database relation constraint has been violated. The related record cannot be deleted.";
//    if (
//        message.Contains("unique", StringComparison.OrdinalIgnoreCase)
//        || message.Contains("duplicate", StringComparison.OrdinalIgnoreCase)
//    )
//      return "This data already exists. Please use a different value.";
//    return "A database error occurred. Please verify your input.";
//  }
//}


//#region Old Code
///////////////// Old
////public class ExceptionMiddleware
////{
////  private readonly RequestDelegate _next;
////  private IHostEnvironment _env;
////  private readonly ILogger<ExceptionMiddleware> _logger;
////  private readonly ILoggerManager _loggerManager;

////  public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger, ILoggerManager loggerManager)
////  {
////    _next = next;
////    _env = env;
////    _logger = logger;
////    _loggerManager = loggerManager;
////  }

////  public async Task InvokeAsync(HttpContext context)
////  {
////    try
////    {
////      await _next(context);
////    }
////    catch (Exception ex)
////    {
////      // Generate correlation ID
////      string correlationId = Guid.NewGuid().ToString();
////      _logger.LogError(ex, $"Request error: {correlationId} - {ex.Message}");
////      _loggerManager.LogError($"Request error: {correlationId} - {ex.Message}");

////      context.Response.ContentType = "application/json";

////      // Set default response
////      int statusCode = (int)HttpStatusCode.InternalServerError;
////      string message = null;
////      string details = null;

////      // Handle specific exception types
////      if (ex is System.UnauthorizedAccessException)
////      {
////        statusCode = (int)HttpStatusCode.Unauthorized;
////        message = "You are not authorized to perform this action.";
////      }
////      else if (ex is KeyNotFoundException)
////      {
////        statusCode = (int)HttpStatusCode.NotFound;
////        message = "The requested resource was not found.";
////      }
////      else if (ex is ValidationException)
////      {
////        statusCode = (int)HttpStatusCode.BadRequest;
////        message = "One or more validation errors occurred.";
////      }
////      //  custom validation exception start
////      // Update your condition blocks in the middleware with the following pattern:
////      else if (ex is UsernamePasswordMismatchException userPassMismatch)
////      {
////        statusCode = userPassMismatch.StatusCode; // 400 // proper appropriate status code here for username/password mismatch
////        message = ex.Message; // "The username or password is incorrect. Please try again."
////      }
////      else if (ex is BadRequestException badRequestEx)
////      {
////        statusCode = badRequestEx.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is ConflictException conflictEx)
////      {
////        statusCode = conflictEx.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is NotFoundException notFoundEx)
////      {
////        statusCode = notFoundEx.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is ServiceUnavailableException serviceEx)
////      {
////        statusCode = serviceEx.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is ForbiddenAccessException forbiddenEx)
////      {
////        statusCode = forbiddenEx.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is UnauthorizedException unauthorized)
////      {
////        statusCode = unauthorized.StatusCode;
////        message = ex.Message;
////      }
////      else if (ex is DuplicateRecordException dupEx)
////      {
////        statusCode = dupEx.StatusCode;
////        message = dupEx.Message;
////      }
////      else if (ex is InvalidCreateOperationException invalidCreateEx)
////      {
////        statusCode = invalidCreateEx.StatusCode;
////        message = invalidCreateEx.Message;
////      }
////      // JWT Token exception handling
////      else if (ex is SecurityTokenException || ex is SecurityTokenValidationException || ex is SecurityTokenExpiredException)
////      {
////        statusCode = (int)HttpStatusCode.Unauthorized;

////        if (ex is SecurityTokenExpiredException)
////        {
////          message = "Your authentication token has expired. Please log in again.";
////        }
////        else
////        {
////          message = "Invalid authentication token. Please log in again.";
////        }
////      }
////      // Database exception handling
////      else if (ex is DbUpdateException)
////      {
////        message = SanitizeDatabaseErrorMessage(ex);
////        details = _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null;
////      }
////      // Generic exception handling
////      else
////      {
////        if (_env.IsDevelopment())
////        {
////          message = GetMostRelevantMessage(ex);
////          details = GetMostRelevantStackTrace(ex);
////        }
////        else
////        {
////          message = "An unexpected error occurred.";
////        }
////      }

////      context.Response.StatusCode = statusCode;
////      // Create enhanced ApiException with additional properties
////      var response = new ApiException(statusCode, message, details)
////      {
////        CorrelationId = correlationId,
////        ErrorType = ex.GetType().Name,
////        Timestamp = DateTime.UtcNow
////      };

////      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
////      var json = JsonSerializer.Serialize(response, options);
////      await context.Response.WriteAsync(json);
////    }
////  }

////  private string GetMostRelevantMessage(Exception exception)
////  {
////    if (exception.InnerException?.InnerException != null)
////      return exception.InnerException.InnerException.Message;

////    if (exception.InnerException != null)
////      return exception.InnerException.Message;

////    return exception.Message;
////  }

////  private string GetMostRelevantStackTrace(Exception exception)
////  {
////    if (exception.InnerException?.InnerException != null)
////      return exception.InnerException.InnerException.StackTrace;

////    if (exception.InnerException != null)
////      return exception.InnerException.StackTrace;

////    return exception.StackTrace;
////  }

////  private string SanitizeDatabaseErrorMessage(Exception exception)
////  {
////    string message = GetMostRelevantMessage(exception);

////    // Remove potentially sensitive information from database error messages
////    if (message.Contains("foreign key"))
////      return "A database relation constraint has been violated. The related record cannot be deleted.";

////    if (message.Contains("unique") || message.Contains("duplicate"))
////      return "This data already exists. Please use a different value.";

////    return "A database error occurred. Please verify your input.";
////  }
////}


////public class ExceptionMiddleware1
////{
////  private readonly RequestDelegate _next;
////  private readonly IHostEnvironment _env;
////  private readonly ILogger<ExceptionMiddleware1> _logger;

////  public ExceptionMiddleware1(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware1> logger)
////  {
////    _next = next;
////    _env = env;
////    _logger = logger;
////  }

////  public async Task InvokeAsync(HttpContext context)
////  {
////    try
////    {
////      await _next(context);
////    }
////    catch (Exception ex)
////    {
////      string correlationId = Guid.NewGuid().ToString();
////      _logger.LogError(ex, $"Request error: {correlationId} - {ex.Message}");

////      await HandleExceptionAsync(context, ex, correlationId);
////    }
////  }

////  private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
////  {
////    context.Response.ContentType = "application/json";

////    var response = CreateErrorResponse(exception, correlationId);
////    context.Response.StatusCode = response.StatusCode;

////    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
////    var json = JsonSerializer.Serialize(response, options);

////    await context.Response.WriteAsync(json);
////  }

////  private ApiErrorResponse CreateErrorResponse(Exception exception, string correlationId)
////  {
////    // Get the most relevant exception message
////    string message = GetMostRelevantMessage(exception);
////    string stackTrace = _env.IsDevelopment() ? GetMostRelevantStackTrace(exception) : null;

////    // Default status code
////    int statusCode = (int)HttpStatusCode.InternalServerError;
////    string errorType = exception.GetType().Name;

////    // Handle specific exception types
////    if (exception is DbUpdateException)
////    {
////      // Database specific errors
////      message = SanitizeDatabaseErrorMessage(message);
////    }
////    else if (exception is UnauthorizedAccessException)
////    {
////      statusCode = (int)HttpStatusCode.Unauthorized;
////    }
////    else if (exception is KeyNotFoundException)
////    {
////      statusCode = (int)HttpStatusCode.NotFound;
////    }
////    else if (exception is ValidationException)
////    {
////      statusCode = (int)HttpStatusCode.BadRequest;
////    }
////    else if (exception is ArgumentException)
////    {
////      statusCode = (int)HttpStatusCode.BadRequest;
////    }

////    return new ApiErrorResponse
////    {
////      StatusCode = statusCode,
////      Message = message,
////      ErrorType = errorType,
////      CorrelationId = correlationId,
////      StackTrace = stackTrace,
////      Timestamp = DateTime.UtcNow
////    };
////  }

////  private string GetMostRelevantMessage(Exception exception)
////  {
////    if (exception.InnerException?.InnerException != null)
////      return exception.InnerException.InnerException.Message;

////    if (exception.InnerException != null)
////      return exception.InnerException.Message;

////    return exception.Message;
////  }

////  private string GetMostRelevantStackTrace(Exception exception)
////  {
////    if (exception.InnerException?.InnerException != null)
////      return exception.InnerException.InnerException.StackTrace;

////    if (exception.InnerException != null)
////      return exception.InnerException.StackTrace;

////    return exception.StackTrace;
////  }



////public class ApiErrorResponse2
////{
////  public int StatusCode { get; set; }
////  public string Message { get; set; }
////  public string ErrorType { get; set; }
////  public string CorrelationId { get; set; }
////  public string StackTrace { get; set; }
////  public DateTime Timestamp { get; set; }
////}



////// old code 

////public class ExceptionMiddleware2
////{
////  private readonly RequestDelegate _next;
////  private IHostEnvironment _env;
////  private readonly ILogger<ExceptionMiddleware2> _logger;
////  private ILoggerManager _loggerManager;

////  public ExceptionMiddleware2(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware2> logger, ILoggerManager loggerManager)
////  {
////    _next = next;
////    _env = env;
////    _logger = logger;
////    _loggerManager = loggerManager;
////  }

////  public async Task InvokeAsync(HttpContext context)
////  {
////    try
////    {
////      await _next(context);
////    }
////    catch (Exception ex)
////    {
////      _logger.LogError(ex, ex.Message);
////      _loggerManager.LogError(ex.Message);
////      context.Response.ContentType = "application/json";
////      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

////      var response = new ApiException((int)HttpStatusCode.InternalServerError);

////      if (ex is DbUpdateException)
////      {
////        response = ex.InnerException?.InnerException != null
////           ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace)
////           : (ex.InnerException != null
////              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace)
////              : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
////        );
////      }
////      else if (_env.IsDevelopment())
////      {
////        response = ex.InnerException?.InnerException != null
////           ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace)
////           : (ex.InnerException != null
////              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace)
////              : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
////        );
////      }
////      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
////      var json = JsonSerializer.Serialize(response, options);
////      await context.Response.WriteAsync(json);
////    }
////  }
////}

//#endregion Old Code