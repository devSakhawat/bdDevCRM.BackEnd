//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Hosting;
//using bdDevCRM.Services.Monitoring;
//using bdDevCRM.RepositoriesContracts;
//using bdDevCRM.Shared.Exceptions.BaseException;
//using Microsoft.EntityFrameworkCore;
//using System.Text.Json;

//namespace bdDevCRM.Shared.Services.Common;

///// <summary>
///// Interface for exception handling service
///// </summary>
//public interface IExceptionHandlingService
//{
//  Task HandleExceptionAsync(Exception exception, HttpContext context);
//  Task<bool> IsCriticalExceptionAsync(Exception exception);
//  Task LogExceptionAsync(Exception exception, string correlationId, HttpContext context);
//}

///// <summary>
///// Exception handling service for centralized exception management
///// </summary>
//public class ExceptionHandlingService : IExceptionHandlingService
//{
//  private readonly ILogger<ExceptionHandlingService> _logger;
//  private readonly ILoggerManager _loggerManager;
//  private readonly IHostEnvironment _environment;
//  private readonly IErrorMonitoringService _errorMonitoringService;

//  public ExceptionHandlingService(
//      ILogger<ExceptionHandlingService> logger,
//      ILoggerManager loggerManager,
//      IHostEnvironment environment,
//      IErrorMonitoringService errorMonitoringService)
//  {
//    _logger = logger;
//    _loggerManager = loggerManager;
//    _environment = environment;
//    _errorMonitoringService = errorMonitoringService;
//  }

//  public async Task HandleExceptionAsync(Exception exception, HttpContext context)
//  {
//    var correlationId = context.TraceIdentifier ?? Guid.NewGuid().ToString();

//    try
//    {
//      // Log the exception
//      await LogExceptionAsync(exception, correlationId, context);

//      // Track exception in monitoring service
//      await _errorMonitoringService.TrackExceptionAsync(exception, correlationId, context);

//      // Handle critical exceptions
//      if (await IsCriticalExceptionAsync(exception))
//      {
//        await HandleCriticalExceptionAsync(exception, correlationId, context);
//      }

//      // Handle business exceptions differently
//      if (exception is BaseCustomException businessException)
//      {
//        await HandleBusinessExceptionAsync(businessException, correlationId, context);
//      }
//    }
//    catch (Exception handlingException)
//    {
//      // Don't let exception handling itself fail the application
//      _logger.LogError(handlingException,
//          "Failed to handle exception with correlation ID: {CorrelationId}. Original exception: {OriginalException}",
//          correlationId, exception.Message);
//    }
//  }

//  public async Task<bool> IsCriticalExceptionAsync(Exception exception)
//  {
//    return await Task.FromResult(exception switch
//    {
//      OutOfMemoryException => true,
//      StackOverflowException => true,
//      AccessViolationException => true,
//      DbUpdateException => true,
//      InvalidOperationException when exception.Message.Contains("database") => true,
//      TimeoutException => true,
//      _ => false
//    });
//  }

//  public async Task LogExceptionAsync(Exception exception, string correlationId, HttpContext context)
//  {
//    try
//    {
//      var logLevel = await IsCriticalExceptionAsync(exception) ? LogLevel.Critical : LogLevel.Error;

//      var logData = new
//      {
//        CorrelationId = correlationId,
//        ExceptionType = exception.GetType().Name,
//        Message = exception.Message,
//        StackTrace = _environment.IsDevelopment() ? exception.StackTrace : "Hidden in production",
//        RequestPath = context.Request.Path.Value,
//        RequestMethod = context.Request.Method,
//        UserId = context.User?.FindFirst("UserId")?.Value,
//        UserAgent = context.Request.Headers.UserAgent.ToString(),
//        RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString()
//      };

//      _logger.Log(logLevel, exception,
//          "Exception occurred with correlation ID: {CorrelationId}. Details: {@LogData}",
//          correlationId, logData);

//      _loggerManager.LogError($"Exception: {correlationId} - {exception.Message}");
//    }
//    catch (Exception loggingException)
//    {
//      // Fallback logging
//      _logger.LogError(loggingException, "Failed to log exception with correlation ID: {CorrelationId}", correlationId);
//    }
//  }

//  private async Task HandleCriticalExceptionAsync(Exception exception, string correlationId, HttpContext context)
//  {
//    try
//    {
//      // Send immediate alert for critical exceptions
//      await _errorMonitoringService.SendAlertAsync(exception, correlationId);

//      // Log as critical
//      _logger.LogCritical(exception,
//          "Critical exception occurred with correlation ID: {CorrelationId}", correlationId);

//      // Additional critical exception handling
//      // - Notify operations team
//      // - Create incident tickets
//      // - Trigger auto-scaling or failover if needed
//    }
//    catch (Exception ex)
//    {
//      _logger.LogError(ex, "Failed to handle critical exception with correlation ID: {CorrelationId}", correlationId);
//    }
//  }

//  private async Task HandleBusinessExceptionAsync(BaseCustomException businessException, string correlationId, HttpContext context)
//  {
//    try
//    {
//      // Business exceptions are usually expected, so log as warning instead of error
//      _logger.LogWarning(businessException,
//          "Business exception occurred with correlation ID: {CorrelationId}. Error Code: {ErrorCode}",
//          correlationId, businessException.ErrorCode);

//      // Track business exception metrics
//      // This helps identify common business rule violations
//      await _errorMonitoringService.TrackExceptionAsync(businessException, correlationId, context);
//    }
//    catch (Exception ex)
//    {
//      _logger.LogError(ex, "Failed to handle business exception with correlation ID: {CorrelationId}", correlationId);
//    }
//  }
//}