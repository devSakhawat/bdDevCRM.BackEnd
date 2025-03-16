using bdDevCRM.Entities.ExceptionEntities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace bdDevCRM.Api;

public class GlobalExceptionHandler : IExceptionHandler
{
  private readonly ILoggerManager _logger;
  public GlobalExceptionHandler(ILoggerManager logger)
  {
    _logger = logger;
  }
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    httpContext.Response.ContentType = "application/json";
    var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
    if (contextFeature != null)
    {
      httpContext.Response.StatusCode = contextFeature.Error switch
      {
        NotFoundException => StatusCodes.Status404NotFound,
        BadRequestException => StatusCodes.Status400BadRequest,
        SecurityTokenValidationException => StatusCodes.Status401Unauthorized, // JWT token validation error
        _ => StatusCodes.Status500InternalServerError
      };

      _logger.LogError($"Something went wrong: {exception.Message}");
      await httpContext.Response.WriteAsync(new ErrorDetails()
      {
        StatusCode = httpContext.Response.StatusCode,
        Message = contextFeature.Error.Message,
        ErrorType = contextFeature.Error.GetType().Name
      }.ToString());
    }
    return true;
  }
}