//using bdDevCRM.Api.ContentFormatter;
//using bdDevCRM.LoggerSevice;
//using bdDevCRM.Presentation.ActionFIlters;
//using bdDevCRM.Repositories;
//using bdDevCRM.RepositoriesContracts;
//using bdDevCRM.Services;
//using bdDevCRM.ServicesContract;
//using bdDevCRM.Shared.ApiResponse;
////using bdDevCRM.Shared.Services.Common;
//using bdDevCRM.Sql.Context;
//using bdDevCRM.Shared.Exceptions;
//using bdDevCRM.Shared.Exceptions.BaseException;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.RateLimiting;
//using Microsoft.AspNetCore.ResponseCompression;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.IdentityModel.Tokens;
//using System.ComponentModel.DataAnnotations;
//using System.Net;
//using System.Text;
//using System.Text.Json;
//using System.Threading.RateLimiting;

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

///////////////////////////////////////////////////////////////////////////////
/////

////using bdDevCRM.Api.ApiResponseError;
////using bdDevCRM.Api.ContentFormatter;
////using bdDevCRM.LoggerSevice;
////using bdDevCRM.Repositories;
////using bdDevCRM.RepositoriesContracts;
////using bdDevCRM.Services;
////using bdDevCRM.ServicesContract;
////using bdDevCRM.Shared.ApiResponse;
////using bdDevCRM.Sql.Context;
////using bdDevCRM.Presentation.ActionFIlters;
////using bdDevCRM.Services.Common;
////using bdDevCRM.Services.Monitoring;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Http.Features;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.ResponseCompression;
////using Microsoft.AspNetCore.RateLimiting;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.IdentityModel.Tokens;
////using System.Text;
////using System.Threading.RateLimiting;
////using Microsoft.Extensions.Diagnostics.HealthChecks;
////// Remove deprecated: using Microsoft.AspNetCore.Mvc.Versioning;

////namespace bdDevCRM.Api.Extensions;

////public static class ServiceExtensions
////{
////  public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
////  {
////    options.AddPolicy("CorsPolicy", builder =>
////    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
////  });

////  public static void Configureiisintegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
////  {
////    options.AutomaticAuthentication = false;
////  });

////  public static void ConfigureLoggerService(this IServiceCollection services)
////  {
////    services.AddSingleton<ILoggerManager, LoggerManager>();
////  }

////  public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

////  public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

////  public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<CRMContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbLocation")));

////  public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

////  public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
////  {
////    services.AddAuthentication(options =>
////    {
////      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////    })
////    .AddJwtBearer(options =>
////    {
////      options.TokenValidationParameters = new TokenValidationParameters
////      {
////        ValidateIssuer = true,
////        ValidateAudience = true,
////        ValidateLifetime = true,
////        ValidateIssuerSigningKey = true,
////        ValidIssuer = configuration["Jwt:Issuer"],
////        ValidAudience = configuration["Jwt:Audience"],
////        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
////      };
////    });
////  }

////  public static void ConfigureApiBehaviorOptions(this IServiceCollection services, IConfiguration configuration)
////  {
////    services.Configure<ApiBehaviorOptions>(options =>
////    {
////      options.SuppressModelStateInvalidFilter = true;
////      options.InvalidModelStateResponseFactory = actionContext =>
////      {
////        var errors = actionContext.ModelState
////           .Where(e => e.Value.Errors.Count() > 0)
////           .SelectMany(x => x.Value.Errors)
////           .Select(y => y.ErrorMessage).ToArray();

////        var errorResponse = new ApiValidationErrorResponse
////        {
////          Errors = errors
////        };
////        return new BadRequestObjectResult(errorResponse);
////      };
////    });
////  }

////  public static void ConfigureAuthorization(this IServiceCollection services)
////  {
////    services.AddAuthorization();
////  }

////  public static void ConfigureResponseCompression(this IServiceCollection services)
////  {
////    services.AddResponseCompression(options =>
////    {
////      options.EnableForHttps = true;
////      options.Providers.Add<GzipCompressionProvider>();
////      options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
////          new[] { "application/json" }
////      );
////    }
////    );
////  }

////  public static void ConfigureGzipCompression(this IServiceCollection services)
////  {
////    services.Configure<GzipCompressionProviderOptions>(options =>
////    {
////      options.Level = System.IO.Compression.CompressionLevel.Optimal;
////    });
////  }

////  public static void ConfigureFileLimit(this IServiceCollection services)
////  {
////    services.Configure<FormOptions>(options =>
////    {
////      options.MultipartBodyLengthLimit = 10_000_000; // 10MB
////      options.ValueLengthLimit = int.MaxValue;
////      options.ValueCountLimit = int.MaxValue;
////      options.KeyLengthLimit = int.MaxValue;
////    });
////  }

////  // ================ EXCEPTION HANDLING CONFIGURATION ================

////  /// <summary>
////  /// Configure exception handling services for enterprise-level error management
////  /// </summary>
////  public static void ConfigureExceptionHandling(this IServiceCollection services)
////  {
////    // Register global exception filter and your existing services
////    services.AddScoped<GlobalExceptionFilter>();

////    // Register your implemented services from IDESTATE
////    services.AddScoped<IExceptionHandlingService, ExceptionHandlingService>();
////    services.AddScoped<IErrorMonitoringService, ErrorMonitoringService>();

////    // Add when you implement notification service:
////    // services.AddScoped<INotificationService, NotificationService>();
////  }

////  /// <summary>
////  /// Configure global filters including exception handling
////  /// </summary>
////  public static void ConfigureGlobalFilters(this IServiceCollection services)
////  {
////    services.Configure<MvcOptions>(options =>
////    {
////      // Add global exception filter as backup
////      options.Filters.Add<GlobalExceptionFilter>();

////      // Add other global filters
////      options.Filters.Add<EmptyObjectFilterAttribute>();
////    });
////  }

////  /// <summary>
////  /// Configure health checks for monitoring - FIXED FOR .NET 8
////  /// </summary>
////  public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
////  {
////    var healthChecksBuilder = services.AddHealthChecks();

////    // Basic self check
////    healthChecksBuilder.AddCheck("self", () => HealthCheckResult.Healthy("Application is running"));

////    // Custom database health check
////    healthChecksBuilder.AddCheck<DatabaseHealthCheck>("database");

////    // Register the custom health check
////    services.AddScoped<DatabaseHealthCheck>();
////  }

////  /// <summary>
////  /// Configure monitoring - Only basic without external packages
////  /// </summary>
////  public static void ConfigureMonitoring(this IServiceCollection services)
////  {
////    // Add custom metrics collection service when you implement it:
////    // services.AddSingleton<IMetricsCollectionService, MetricsCollectionService>();
////  }

////  /// <summary>
////  /// Configure caching strategy - Memory cache only for now
////  /// </summary>
////  public static void ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
////  {
////    // Memory cache only (no external packages required)
////    services.AddMemoryCache(options =>
////    {
////      options.SizeLimit = configuration.GetValue<long?>("Cache:SizeLimit") ?? 1000;
////      options.CompactionPercentage = configuration.GetValue<double?>("Cache:CompactionPercentage") ?? 0.25;
////    });
////  }

////  /// <summary>
////  /// Configure background services
////  /// </summary>
////  public static void ConfigureBackgroundServices(this IServiceCollection services)
////  {
////    // Add when you implement these background services:
////    // services.AddHostedService<CleanupBackgroundService>();
////    // services.AddHostedService<MonitoringBackgroundService>();
////  }

////  /// <summary>
////  /// Configure security headers
////  /// </summary>
////  public static void ConfigureSecurityHeaders(this IServiceCollection services)
////  {
////    services.AddHsts(options =>
////    {
////      options.Preload = true;
////      options.IncludeSubDomains = true;
////      options.MaxAge = TimeSpan.FromDays(365);
////    });
////  }

////  /// <summary>
////  /// Configure rate limiting
////  /// </summary>
////  public static void ConfigureRateLimiting(this IServiceCollection services, IConfiguration configuration)
////  {
////    services.AddRateLimiter(options =>
////    {
////      options.RejectionStatusCode = 429;

////      options.AddFixedWindowLimiter("GlobalLimiter", limiterOptions =>
////      {
////        limiterOptions.PermitLimit = configuration.GetValue<int>("RateLimit:PermitLimit", 100);
////        limiterOptions.Window = TimeSpan.FromMinutes(configuration.GetValue<int>("RateLimit:WindowMinutes", 1));
////        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
////        limiterOptions.QueueLimit = configuration.GetValue<int>("RateLimit:QueueLimit", 10);
////      });

////      options.AddPolicy("AuthenticatedUserPolicy", context =>
////      {
////        var userId = context.User?.FindFirst("UserId")?.Value;
////        return RateLimitPartition.GetFixedWindowLimiter(
////          partitionKey: userId ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
////          factory: _ => new FixedWindowRateLimiterOptions
////          {
////            PermitLimit = configuration.GetValue<int>("RateLimit:AuthenticatedUserLimit", 200),
////            Window = TimeSpan.FromMinutes(1)
////          });
////      });
////    });
////  }

////  /// <summary>
////  /// Configure essential enterprise services (working without external packages)
////  /// </summary>
////  public static void ConfigureEnterpriseServices(this IServiceCollection services, IConfiguration configuration)
////  {
////    services.ConfigureExceptionHandling();
////    services.ConfigureGlobalFilters();
////    services.ConfigureHealthChecks(configuration);
////    services.ConfigureMonitoring();
////    services.ConfigureCaching(configuration);
////    services.ConfigureSecurityHeaders();
////    services.ConfigureRateLimiting(configuration);
////    services.ConfigureBackgroundServices();
////  }
////}

////// ================ CUSTOM HEALTH CHECK IMPLEMENTATION ================

/////// <summary>
/////// Custom database health check implementation without external packages
/////// </summary>
////public class DatabaseHealthCheck : IHealthCheck
////{
////  private readonly CRMContext _context;

////  public DatabaseHealthCheck(CRMContext context)
////  {
////    _context = context;
////  }

////  public async Task<HealthCheckResult> CheckHealthAsync(
////    HealthCheckContext context,
////    CancellationToken cancellationToken = default)
////  {
////    try
////    {
////      // Try to connect to database
////      await _context.Database.CanConnectAsync(cancellationToken);
////      return HealthCheckResult.Healthy("Database connection is healthy");
////    }
////    catch (Exception ex)
////    {
////      return HealthCheckResult.Unhealthy("Database connection failed", ex);
////    }
////  }
////}

////// ================ INTERFACES (ALREADY IMPLEMENTED IN YOUR PROJECT) ================

/////// <summary>
/////// Interface for exception handling service - IMPLEMENTED IN YOUR PROJECT
/////// </summary>
////public interface IExceptionHandlingService
////{
////  Task HandleExceptionAsync(Exception exception, HttpContext context);
////  Task<bool> IsCriticalExceptionAsync(Exception exception);
////  Task LogExceptionAsync(Exception exception, string correlationId, HttpContext context);
////}

/////// <summary>
/////// Interface for error monitoring service - IMPLEMENTED IN YOUR PROJECT
/////// </summary>
////public interface IErrorMonitoringService
////{
////  Task TrackExceptionAsync(Exception exception, string correlationId, HttpContext context);
////  Task SendAlertAsync(Exception exception, string correlationId);
////  Task<Dictionary<string, object>> CollectContextDataAsync(HttpContext context);
////}

/////// <summary>
/////// Interface for notification service - TO BE IMPLEMENTED
/////// </summary>
////public interface INotificationService
////{
////  Task SendCriticalErrorNotificationAsync(Exception exception, HttpContext context);
////  Task SendErrorSummaryAsync(DateTime from, DateTime to);
////}

/////// <summary>
/////// Interface for metrics collection service - TO BE IMPLEMENTED
/////// </summary>
////public interface IMetricsCollectionService
////{
////  void IncrementExceptionCounter(string exceptionType);
////  void RecordResponseTime(string endpoint, double milliseconds);
////  void RecordApiCall(string endpoint, int statusCode);
////}



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