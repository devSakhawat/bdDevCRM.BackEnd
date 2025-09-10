using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace bdDevCRM.Presentation.Extensions
{
  public static class HttpContextExtensions
  {
    public static int GetUserId(this HttpContext context)
    {
      var userIdClaim = context.User?.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        throw new UnauthorizedAccessException("UserId not found in token.");

      if (!int.TryParse(userIdClaim, out var userId))
        throw new UnauthorizedAccessException("Invalid UserId format in token.");

      return userId;
    }

    public static UsersDto GetCurrentUser(this HttpContext context)
    {
      var userId = context.GetUserId();
      var serviceProvider = context.RequestServices;
      var serviceManager = serviceProvider.GetService<IServiceManager>();

      if (serviceManager == null)
        throw new InvalidOperationException("ServiceManager not found in the service provider.");

      var currentUser = serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        throw new UnauthorizedAccessException("User not found in cache.");

      return currentUser;
    }
  }
}