using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using System.Security.Claims;

namespace bdDevCRM.Api.Extensions;

public static class HttpContextExtensions
{
  public static int GetUserId(this HttpContext httpContext)
  {
    if (httpContext.User.Identity?.IsAuthenticated != true)
      throw new UnauthorizedAccessException("User not authenticated.");

    string? id = httpContext.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    if (int.TryParse(id, out int userId))
      return userId;

    throw new InvalidOperationException("UserId claim not found or invalid.");
  }

  public static string GetUserName(this HttpContext httpContext)
  {
    return httpContext.User.Identity?.Name ?? "Unknown";
  }

  public static int GetUserIdFromClaim(this HttpContext context)
  {
    var userIdClaim = context.User?.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new InvalidOperationException("UserId not found in token.");

    if (!int.TryParse(userIdClaim, out var userId))
      throw new InvalidOperationException("Invalid UserId format in token.");

    return userId;
  }

  public static UsersDto GetCurrentUser(this HttpContext httpContext)
  {
    return new UsersDto
    {
      UserId = httpContext.GetUserId(),
      UserName = httpContext.GetUserName()
    };
  }

  public static UsersDto GetCurrentUserFromCache(this HttpContext context)
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
