
// CRM.Solution.Presentation/Controllers/BaseApiController.cs (or wherever your BaseApiController is located)

using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants; // Add this using directive if IServiceManager depends on IMemoryCache directly
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;















#region Old_Code


[Route(RouteConstants.BaseRoute)] // Assuming RouteConstants.BaseRoute is defined elsewhere, e.g., "api/[controller]"
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[AuthorizeUser]
// [ServiceFilter(typeof(LogActionAttribute))]
// [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
[EnableCors]
public class BaseApiController : ControllerBase
{
  // Inject IServiceManager here
  protected readonly IServiceManager _serviceManager; // Use 'protected' to make it accessible in derived classes

  // The constructor for BaseApiController now needs IServiceManager
  public BaseApiController(IServiceManager serviceManager)
  {
    _serviceManager = serviceManager;
  }

  // ---------- Helper Method for Logged-in User ------------------------------------------------
  protected bool TryGetLoggedInUser(out UsersDto currentUser)
  {
    currentUser = null!; // Initialize currentUser to null

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      // Log for debugging if necessary: _serviceManager.Logger.LogWarning("UserId claim not found in token.");
      return false;
    }

    if (!int.TryParse(userIdClaim, out int userId))
    {
      // Log for debugging if necessary: _serviceManager.Logger.LogError($"Invalid UserId claim format: {userIdClaim}");
      return false;
    }

    try
    {
      currentUser = _serviceManager.GetCache<UsersDto>(userId);
      return currentUser != null;
    }
    catch (UnauthorizedAccessCRMException ex) // Catch your specific unauthorized exception if GetCache throws it
    {
      // Log this exception: _serviceManager.Logger.LogError($"User not found in cache or session expired: {ex.Message}");
      return false;
    }
    catch (Exception ex)
    {
      // Log any other unexpected errors during cache retrieval
      // _serviceManager.Logger.LogError($"An unexpected error occurred while retrieving user from cache: {ex.Message}");
      return false;
    }

  }
}



















//using Microsoft.AspNetCore.Mvc;

//namespace bdDevCRM.Presentation.Controllers;

//using bdDevCRM.Presentation.ActionFIlters;
//using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
//using bdDevCRM.Utilities.Constants;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;

//[Route(RouteConstants.BaseRoute)]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
////[ServiceFilter(typeof(LogActionAttribute))]
////[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
//[EnableCors]
//public class BaseApiController : ControllerBase
//{

//}


#endregion Old_Code