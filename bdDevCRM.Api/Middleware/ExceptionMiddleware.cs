using bdDevCRM.RepositoriesContracts;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions.BaseException;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace bdDevCRM.Api.Middleware;

/// <summary>
/// Global exception handling middleware for the application.
/// Captures all unhandled exceptions and returns structured API responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly ILoggerManager _loggerManager;

    public ExceptionMiddleware(
          RequestDelegate next,
          IHostEnvironment env,
          ILogger<ExceptionMiddleware> logger,
          ILoggerManager loggerManager)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions and returns appropriate API response
    /// Priority: Custom Message → System Message → Generic Message
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Generate correlation ID for tracking
        string correlationId = Guid.NewGuid().ToString();

        // Log the error with full details
        _logger.LogError(ex, $"[{correlationId}] {ex.GetType().Name}: {ex.Message}");
        _loggerManager.LogError($"[{correlationId}] {ex.GetType().Name}: {ex.Message}");

        context.Response.ContentType = "application/json";

        // Map exception to API response
        ApiException response = MapExceptionToResponse(ex, correlationId);

        context.Response.StatusCode = response.StatusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _env.IsDevelopment(),
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Maps exceptions to structured API responses with priority-based messaging
    /// </summary>
    private ApiException MapExceptionToResponse(Exception ex, string correlationId) => ex switch
    {
        // ==========================================
        // 🔴 CUSTOM BUSINESS LOGIC EXCEPTIONS (Highest Priority)
        // ==========================================

        // Conflict Exceptions (409) - Order matters: Most specific first
        GenericConflictException genericConflict => CreateResponse(
            genericConflict.StatusCode,
            ex.Message, // ✅ Custom message gets priority
            nameof(GenericConflictException),
            correlationId
        ),

        DuplicateRecordException duplicate => CreateResponse(
            duplicate.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(DuplicateRecordException),
            correlationId
        ),

        ConflictException conflict => CreateResponse(
            conflict.StatusCode,
            ex.Message, // ✅ Custom message from derived types
            nameof(ConflictException),
            correlationId
        ),

        // BadRequest Exceptions (400)
        InvalidCreateOperationException invalidCreate => CreateResponse(
            invalidCreate.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(InvalidCreateOperationException),
            correlationId
        ),

        InvalidUpdateOperationException invalidUpdate => CreateResponse(
            invalidUpdate.StatusCode,  // ✅ Will work after BadRequestException fix
            ex.Message,
            nameof(InvalidUpdateOperationException),
            correlationId
        ),

        IdMismatchBadRequestException idMismatch => CreateResponse(
            idMismatch.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(IdMismatchBadRequestException),
            correlationId
        ),

        NullModelBadRequestException nullModel => CreateResponse(
            nullModel.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(NullModelBadRequestException),
            correlationId
        ),

        GenericBadRequestException genericBadRequest => CreateResponse(
            genericBadRequest.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(GenericBadRequestException),
            correlationId
        ),

        UsernamePasswordMismatchException authMismatch => CreateResponse(
            authMismatch.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(UsernamePasswordMismatchException),
            correlationId
        ),

        BadRequestException badRequest => CreateResponse(
            badRequest.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(BadRequestException),
            correlationId
        ),

        // NotFound Exceptions (404)
        GenericNotFoundException genericNotFound => CreateResponse(
            genericNotFound.StatusCode,
            ex.Message, // ✅ Custom message with details
            nameof(GenericNotFoundException),
            correlationId
        ),

        NotFoundException notFound => CreateResponse(
            notFound.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(NotFoundException),
            correlationId
        ),

        // Unauthorized Exceptions (401)
        GenericUnauthorizedException genericUnauthorized => CreateResponse(
            genericUnauthorized.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(GenericUnauthorizedException),
            correlationId
        ),

      //  UsernamePasswordMismatchException authMismatch => CreateResponse(
      //    authMismatch.StatusCode,
      //    ex.Message, // ✅ Custom message
      //    nameof(UsernamePasswordMismatchException),
      //    correlationId
      //),

      UnauthorizedException unauthorized => CreateResponse(
            unauthorized.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(UnauthorizedException),
            correlationId
        ),

        // Forbidden Exceptions (403)
        ForbiddenAccessException forbidden => CreateResponse(
            forbidden.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(ForbiddenAccessException),
            correlationId
        ),

        // ServiceUnavailable Exceptions (503)
        ServiceUnavailableException serviceUnavailable => CreateResponse(
            serviceUnavailable.StatusCode,
            ex.Message, // ✅ Custom message
            nameof(ServiceUnavailableException),
            correlationId
        ),

        // ==========================================
        // 🟡 AUTHENTICATION & AUTHORIZATION EXCEPTIONS
        // ==========================================

        // JWT Token Exceptions (401)
        SecurityTokenExpiredException tokenExpired => CreateResponse(
            401,
            "Your authentication token has expired. Please log in again.",
            "TokenExpired",
            correlationId
        ),

        SecurityTokenException or SecurityTokenValidationException => CreateResponse(
            401,
            "Invalid authentication token. Please log in again.",
            "InvalidToken",
            correlationId
        ),

        System.UnauthorizedAccessException => CreateResponse(
            401,
            "You are not authorized to perform this action.",
            "UnauthorizedAccess",
            correlationId
        ),

        // ==========================================
        // 🟢 FRAMEWORK & SYSTEM EXCEPTIONS
        // ==========================================

        // Validation Exceptions (400)
        ValidationException validation => CreateResponse(
            400,
            "One or more validation errors occurred.",
            "Validation",
            correlationId
        ),

        ArgumentNullException argNull => CreateResponse(
            400,
            $"Required parameter '{argNull.ParamName}' is missing.",
            "ArgumentNull",
            correlationId
        ),

        ArgumentException argument => CreateResponse(
            400,
            ex.Message, // ✅ Framework message
            "ArgumentError",
            correlationId
        ),

        KeyNotFoundException => CreateResponse(
            404,
            "The requested resource was not found.",
            "KeyNotFound",
            correlationId
        ),

        // ==========================================
        // 🔵 DATABASE EXCEPTIONS
        // ==========================================

        DbUpdateException dbUpdate => CreateResponse(
            500,
            SanitizeDatabaseErrorMessage(ex), // ✅ Safe database message
            "DatabaseError",
            correlationId,
            includeStackTrace: true
        ),

        // ==========================================
        // ⚫ GENERIC FALLBACK
        // ==========================================

        _ => CreateResponse(
            500,
            _env.IsDevelopment()
                ? GetMostRelevantMessage(ex) // Development: Show actual error
                : "An unexpected error occurred. Please try again later.", // Production: Generic message
            ex.GetType().Name,
            correlationId,
            includeStackTrace: _env.IsDevelopment()
        ),
    };

    /// <summary>
    /// Creates structured API exception response
    /// </summary>
    private ApiException CreateResponse(
        int statusCode,
        string message,
        string errorType,
        string correlationId,
        bool includeStackTrace = false)
    {
        var response = new ApiException(statusCode, message)
        {
            ErrorType = errorType,
            CorrelationId = correlationId,
        };


        if (includeStackTrace && _env.IsDevelopment())
        {
            // Option 1: Use Environment.StackTrace (Simple)
            response.Details = Environment.StackTrace;

            // Option 2: Use new StackTrace() instance (Better control)
            // var stackTrace = new System.Diagnostics.StackTrace(true);
            // response.Details = stackTrace.ToString();
        }

        return response;
    }

    /// <summary>
    /// Extracts the most relevant error message from exception chain
    /// </summary>
    private string GetMostRelevantMessage(Exception exception)
    {
        // Check innermost exception first
        if (exception.InnerException?.InnerException != null)
            return exception.InnerException.InnerException.Message;

        if (exception.InnerException != null)
            return exception.InnerException.Message;

        return exception.Message;
    }

    /// <summary>
    /// Extracts the most relevant stack trace from exception chain
    /// </summary>
    private string GetMostRelevantStackTrace(Exception exception)
    {
        if (exception.InnerException?.InnerException != null)
            return exception.InnerException.InnerException.StackTrace;

        if (exception.InnerException != null)
            return exception.InnerException.StackTrace;

        return exception.StackTrace;
    }

    /// <summary>
    /// Sanitizes database error messages to prevent sensitive data exposure
    /// </summary>
    private string SanitizeDatabaseErrorMessage(Exception exception)
    {
        string message = GetMostRelevantMessage(exception);

        // Foreign key constraint violation
        if (message.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
            return "Cannot delete this record because it is referenced by other data. Please remove related records first.";

        // Unique constraint violation
        if (message.Contains("unique", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
            return "This data already exists. Please use a different value.";

        // Null constraint violation
        if (message.Contains("null", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("required", StringComparison.OrdinalIgnoreCase))
            return "Required field is missing. Please provide all necessary information.";

        // Connection or timeout issues
        if (message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("connection", StringComparison.OrdinalIgnoreCase))
            return "Database connection issue. Please try again later.";

        // Generic database error (don't expose internal details)
        return "A database error occurred. Please verify your input and try again.";
    }
}

/// <summary>
/// Static mapper for additional exception handling scenarios
/// </summary>
public static class ExceptionToApiResponseMapper
{
    public static ApiException Map(Exception ex, IHostEnvironment env, out int statusCode)
    {
        string message = ex.Message;
        string errorType = ex.GetType().Name;
        string details = env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null;

        (statusCode, message, errorType) = ex switch
        {
            // Custom exceptions - Always use custom message
            BadRequestException badReq => (badReq.StatusCode, ex.Message, nameof(BadRequestException)),
            UnauthorizedException unauth => (unauth.StatusCode, ex.Message, nameof(UnauthorizedException)),
            ForbiddenAccessException forbidden => (forbidden.StatusCode, ex.Message, nameof(ForbiddenAccessException)),
            NotFoundException notFound => (notFound.StatusCode, ex.Message, nameof(NotFoundException)),
            ConflictException conflict => (conflict.StatusCode, ex.Message, nameof(ConflictException)),
            ServiceUnavailableException service => (service.StatusCode, ex.Message, nameof(ServiceUnavailableException)),

            // JWT Token issues
            SecurityTokenExpiredException => (401, "Your authentication token has expired. Please log in again.", "TokenExpired"),
            SecurityTokenValidationException => (401, "Invalid authentication token. Please log in again.", "InvalidToken"),

            // Framework exceptions
            KeyNotFoundException => (404, "The requested resource was not found.", "KeyNotFound"),
            UnauthorizedAccessException => (401, "You are not authorized to perform this action.", "UnauthorizedAccess"),
            ValidationException => (400, "One or more validation errors occurred.", "Validation"),

            // Database exceptions
            DbUpdateException => (500, SanitizeDatabaseErrorMessage(ex), "DatabaseError"),

            // Generic fallback
            _ => (500, env.IsDevelopment() ? GetMostRelevantMessage(ex) : "An unexpected error occurred.", errorType),
        };

        return new ApiException(statusCode, message) { ErrorType = errorType, Details = details };
    }

    private static string GetMostRelevantMessage(Exception exception)
    {
        if (exception.InnerException?.InnerException != null)
            return exception.InnerException.InnerException.Message;
        if (exception.InnerException != null)
            return exception.InnerException.Message;
        return exception.Message;
    }

    private static string GetMostRelevantStackTrace(Exception exception)
    {
        if (exception.InnerException?.InnerException != null)
            return exception.InnerException.InnerException.StackTrace;
        if (exception.InnerException != null)
            return exception.InnerException.StackTrace;
        return exception.StackTrace;
    }

    private static string SanitizeDatabaseErrorMessage(Exception exception)
    {
        string message = GetMostRelevantMessage(exception);

        if (message.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
            return "A database relation constraint has been violated. The related record cannot be deleted.";
        if (message.Contains("unique", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
            return "This data already exists. Please use a different value.";
        return "A database error occurred. Please verify your input.";
    }
}