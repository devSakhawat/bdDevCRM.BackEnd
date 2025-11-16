
// CRM.Solution.Presentation/Controllers/BaseApiController.cs (or wherever your BaseApiController is located)

using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants; // Add this using directive if IServiceManager depends on IMemoryCache directly
using bdDevCRM.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


[Route(RouteConstants.BaseRoute)]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[EnableCors]
// [AuthorizeUser]
// [ServiceFilter(typeof(LogActionAttribute))]
// [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
public class BaseApiController : ControllerBase
{
    protected readonly IServiceManager _serviceManager;

    public BaseApiController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    // Centralized method to get authenticated user
    protected UsersDto GetAuthenticatedUser()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new GenericUnauthorizedException("User authentication required.");

        if (!int.TryParse(userIdClaim, out int userId))
            throw new GenericBadRequestException("Invalid user ID format.");

        UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

        if (currentUser == null)
            throw new GenericUnauthorizedException("User session expired.");

        return currentUser;
    }

    // OPTIONAL: Get User ID only (without full user object)
    protected int GetAuthenticatedUserId()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new GenericUnauthorizedException("User authentication required.");

        if (!int.TryParse(userIdClaim, out int userId))
            throw new GenericBadRequestException("Invalid user ID format.");

        return userId;
    }

    // OPTIONAL: Try-pattern for scenarios where user might not exist
    protected bool TryGetAuthenticatedUser(out UsersDto? currentUser)
    {
        currentUser = null;

        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return false;

        if (!int.TryParse(userIdClaim, out int userId))
            return false;

        currentUser = _serviceManager.GetCache<UsersDto>(userId);
        return currentUser != null;
    }
}