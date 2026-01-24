using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
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

		var serviceManager = httpContext.RequestServices.GetService<IServiceManager>();
		var memoryCache = httpContext.RequestServices.GetService<IMemoryCache>();

		if (serviceManager == null || memoryCache == null)
		{
			context.Result = new StatusCodeResult(500); // Internal server error
			return;
		}

		try
		{
			int userId = Convert.ToInt32(userIdClaim);
			UsersDto? user = null;

			var cacheKey = $"User_{userId}";
			if (memoryCache.TryGetValue(cacheKey, out UsersDto? cachedUser))
			{
				user = cachedUser;
			}

			if (user == null)
			{
				user = serviceManager.Users.GetUser(userId, false);

				if (user != null)
				{

					var cacheOptions = new MemoryCacheEntryOptions()
						.SetSlidingExpiration(TimeSpan.FromHours(5))
						.SetAbsoluteExpiration(TimeSpan.FromHours(5));

					memoryCache.Set(cacheKey, user, cacheOptions);
				}
			}


			if (user == null)
			{
				context.Result = new UnauthorizedObjectResult(
					"User not found. Please log in again."
				);
				return;
			}


			httpContext.SetCurrentUser(user);
			httpContext.SetUserId(userId);
		}
		catch (Exception ex)
		{
			context.Result = new UnauthorizedObjectResult($"Authentication failed: {ex.Message}");
		}
	}
}


//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
//{
//  public void OnAuthorization(AuthorizationFilterContext context)
//  {
//    var httpContext = context.HttpContext;
//    var userIdClaim = httpContext.User.FindFirst("UserId")?.Value;
//    if (string.IsNullOrEmpty(userIdClaim))
//    {
//      context.Result = new UnauthorizedResult();
//      return;
//    }

//    var serviceManager = context.HttpContext.RequestServices.GetService<bdDevCRM.ServicesContract.IServiceManager>();
//    try
//    {
//      var user = serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
//    }
//    catch
//    {
//      context.Result = new UnauthorizedObjectResult("User not found in cache or session expired. Please log in again.");
//    }
//  }
//}

// [AllowAnonymous] use anywhere you want to allow anonymous access.
