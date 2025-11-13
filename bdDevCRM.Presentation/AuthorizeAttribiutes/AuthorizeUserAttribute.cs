using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace bdDevCRM.Presentation.AuthorizeAttribiutes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var httpContext = context.HttpContext;
    var userIdClaim = httpContext.User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      context.Result = new UnauthorizedResult();
      return;
    }

    var serviceManager = context.HttpContext.RequestServices.GetService<bdDevCRM.ServicesContract.IServiceManager>();
    try
    {
      var user = serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    }
    catch
    {
      context.Result = new UnauthorizedObjectResult("User not found in cache or session expired. Please log in again.");
    }
  }
}

// [AllowAnonymous] use anywhere you want to allow anonymous access.
