using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

[AuthorizeUser]
public class UsersController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public UsersController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpPost(RouteConstants.UserSummary)]
  public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options, [FromQuery] int companyId)
  {
		//Get authenticated user from HttpContext
		var currentUser = HttpContext.GetCurrentUser();
		var userId = HttpContext.GetUserId();

		// Validate user data
		if (currentUser == null)
            return Unauthorized("User not found in cache.");

        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var summaryGrid = await _serviceManager.Users.UsersSummary(companyId, trackChanges: false, options, currentUser);

    // Return standardized response
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<UsersDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.SaveUser)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveUser([FromBody] UsersDto usersDto)
  {
    //Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate user data
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    // Validate input parameters
    if (usersDto == null)
      throw new NullModelBadRequestException("User data cannot be null");

    // Execute business logic
    UsersDto res = await _serviceManager.Users.SaveUser(usersDto);

    ////Audittail
    //var shortDescription = userId == 0 ? "User Information try to Inserted" : "User Information try to Updated";
    //var actionType = userId == 0 ? "Insert" : "Update";
    //var audit = hendler.CreateAuditObject(users, shortDescription, actionType, res);
    //aService.SendAudit(audit);

    // Validate result
    if (res.UserId <= 0)
      throw new InvalidUpdateOperationException("Failed to save user record.");

    // Return success response
    return Ok(ResponseHelper.Success(res, "User saved successfully"));
  }
  

  [HttpPut(RouteConstants.UpdateUser)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateUser([FromRoute] int key, [FromBody] UsersDto usersDto)
  {
    //Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate user data
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    // Validate input parameters
    if (key <= 0)
      throw new GenericBadRequestException("Invalid user ID. ID must be greater than 0.");

    if (usersDto == null)
      throw new NullModelBadRequestException("User data cannot be null");

    // Execute business logic
    UsersDto res = await _serviceManager.Users.SaveUser(usersDto);

    ////Audittail
    //var shortDescription = userId == 0 ? "User Information try to Inserted" : "User Information try to Updated";
    //var actionType = userId == 0 ? "Insert" : "Update";
    //var audit = hendler.CreateAuditObject(users, shortDescription, actionType, res);
    //aService.SendAudit(audit);

    // Validate result
    if (res.UserId <= 0)
      throw new InvalidUpdateOperationException("Failed to update user record.");

    // Return success response
    return Ok(ResponseHelper.Success(res, "User updated successfully"));
  }


}