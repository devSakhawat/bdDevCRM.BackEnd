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
/// Enhanced global exception handling middleware using standardized response format
/// Captures all unhandled exceptions and returns consistent API responses
/// </summary>
public class StandardExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<StandardExceptionMiddleware> _logger;
    private readonly ILoggerManager _loggerManager;
    private const string ApiVersion = "1.0";

    public StandardExceptionMiddleware( RequestDelegate next, IHostEnvironment env, ILogger<StandardExceptionMiddleware> logger, ILoggerManager loggerManager)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Generate correlation ID for tracking
        string correlationId = Guid.NewGuid().ToString();

        // Log the error with full details
        _logger.LogError(ex, $"[{correlationId}] {ex.GetType().Name}: {ex.Message}");
        _loggerManager.LogError($"[{correlationId}] {ex.GetType().Name}: {ex.Message}");

        context.Response.ContentType = "application/json";

        // Map exception to standardized API response
        var response = MapExceptionToStandardResponse(ex, correlationId);

        context.Response.StatusCode = response.StatusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _env.IsDevelopment(),
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }

    private StandardApiResponse<object> MapExceptionToStandardResponse(Exception ex, string correlationId)
    {
        // Handle BaseCustomException first (highest priority)
        if (ex is BaseCustomException customEx)
        {
            return CreateStandardResponse(
                customEx.StatusCode,
                customEx.UserFriendlyMessage ?? customEx.Message,
                customEx.ErrorCode,
                ex.GetType().Name,
                GetMostRelevantMessage(ex),
                customEx.AdditionalData,
                correlationId,
                _env.IsDevelopment() ? customEx.StackTrace : null
            );
        }

        // Pattern matching for specific exceptions
        return ex switch
        {
            // Conflict Exceptions (409)
            GenericConflictException or DuplicateRecordException or ConflictException =>
                CreateStandardResponse(409, ex.Message, "CONFLICT", ex.GetType().Name, null, null, correlationId),

            // BadRequest Exceptions (400)
            InvalidCreateOperationException or InvalidUpdateOperationException or
            IdMismatchBadRequestException or NullModelBadRequestException or
            GenericBadRequestException or UsernamePasswordMismatchException or BadRequestException =>
                CreateStandardResponse(400, ex.Message, "BAD_REQUEST", ex.GetType().Name, null, null, correlationId),

            // NotFound Exceptions (404)
            GenericNotFoundException or NotFoundException =>
                CreateStandardResponse(404, ex.Message, "NOT_FOUND", ex.GetType().Name, null, null, correlationId),

            // Unauthorized Exceptions (401)
            GenericUnauthorizedException or UnauthorizedException =>
                CreateStandardResponse(401, ex.Message, "UNAUTHORIZED", ex.GetType().Name, null, null, correlationId),

            // Forbidden Exceptions (403)
            ForbiddenAccessException =>
                CreateStandardResponse(403, ex.Message, "FORBIDDEN", ex.GetType().Name, null, null, correlationId),

            // ServiceUnavailable Exceptions (503)
            ServiceUnavailableException =>
                CreateStandardResponse(503, ex.Message, "SERVICE_UNAVAILABLE", ex.GetType().Name, null, null, correlationId),

            // JWT Token Exceptions
            SecurityTokenExpiredException =>
                CreateStandardResponse(401, "Your authentication token has expired. Please log in again.",
                    "TOKEN_EXPIRED", "SecurityTokenExpired", null, null, correlationId),

            SecurityTokenException or SecurityTokenValidationException =>
                CreateStandardResponse(401, "Invalid authentication token. Please log in again.",
                    "INVALID_TOKEN", "SecurityTokenInvalid", null, null, correlationId),

            System.UnauthorizedAccessException =>
                CreateStandardResponse(401, "You are not authorized to perform this action.",
                    "UNAUTHORIZED_ACCESS", "UnauthorizedAccess", null, null, correlationId),

            // Validation Exceptions
            ValidationException =>
                CreateStandardResponse(400, "One or more validation errors occurred.",
                    "VALIDATION_ERROR", "Validation", null, null, correlationId),

            ArgumentNullException argNull =>
                CreateStandardResponse(400, $"Required parameter '{argNull.ParamName}' is missing.",
                    "ARGUMENT_NULL", "ArgumentNull", null, null, correlationId),

            ArgumentException =>
                CreateStandardResponse(400, ex.Message, "ARGUMENT_ERROR", "ArgumentError", null, null, correlationId),

            KeyNotFoundException =>
                CreateStandardResponse(404, "The requested resource was not found.",
                    "KEY_NOT_FOUND", "KeyNotFound", null, null, correlationId),

            // Database Exceptions
            DbUpdateException =>
                CreateStandardResponse(500, SanitizeDatabaseErrorMessage(ex),
                    "DATABASE_ERROR", "DatabaseError", null, null, correlationId,
                    _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null),

            // Generic Fallback
            _ => CreateStandardResponse(
                500,
                _env.IsDevelopment() ? GetMostRelevantMessage(ex) : "An unexpected error occurred. Please try again later.",
                "INTERNAL_ERROR",
                ex.GetType().Name,
                _env.IsDevelopment() ? GetMostRelevantMessage(ex) : null,
                null,
                correlationId,
                _env.IsDevelopment() ? GetMostRelevantStackTrace(ex) : null
            )
        };
    }

    private StandardApiResponse<object> CreateStandardResponse(
        int statusCode,
        string message,
        string errorCode,
        string errorType,
        string details,
        Dictionary<string, object> additionalData,
        string correlationId,
        string stackTrace = null)
    {
        return new StandardApiResponse<object>
        {
            StatusCode = statusCode,
            Success = false,
            Message = message,
            Version = ApiVersion,
            CorrelationId = correlationId,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = errorType,
                Details = details,
                StackTrace = stackTrace,
                AdditionalData = additionalData
            }
        };
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

        if (message.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
            return "Cannot delete this record because it is referenced by other data. Please remove related records first.";

        if (message.Contains("unique", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
            return "This data already exists. Please use a different value.";

        if (message.Contains("null", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("required", StringComparison.OrdinalIgnoreCase))
            return "Required field is missing. Please provide all necessary information.";

        if (message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("connection", StringComparison.OrdinalIgnoreCase))
            return "Database connection issue. Please try again later.";

        return "A database error occurred. Please verify your input and try again.";
    }
}
