using bdDevCRM.Shared.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly IHostEnvironment _env;
    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }
    public void OnException(ExceptionContext context)
    {
        // This acts as a backup for exceptions not caught by middleware
        if (!context.ExceptionHandled)
        {
            _logger.LogError(context.Exception, "Unhandled exception in action filter");
            
            context.Result = new ObjectResult(new ApiException(500, "Internal server error"))
            {
                StatusCode = 500
            };
            
            context.ExceptionHandled = true;
        }
    }
}