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


/// <summary>
/// Users management endpoints.
///
/// [AuthorizeUser] at class-level ensures:
///    - Every request validates user via attribute
///    - CurrentUser / CurrentUserId available from BaseApiController
///    - No auth checks needed in controller methods
///    - Exceptions handled by StandardExceptionMiddleware
/// </summary>
[AuthorizeUser]
public class UsersController : BaseApiController
{
	private readonly IMemoryCache _cache;

	public UsersController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
	{
		_cache = cache;
	}

	[HttpPost(RouteConstants.UserSummary)]
	public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options, [FromQuery] int companyId)
	{
		// CurrentUser is guaranteed to be available by [AuthorizeUser]
		var currentUser = CurrentUser;

		if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
			throw new IdParametersBadRequestException();

		var summaryGrid = await _serviceManager.Users.UsersSummary(companyId, trackChanges: false, options, currentUser);

		if (summaryGrid == null || !summaryGrid.Items.Any())
			return Ok(ApiResponseHelper.NoContent<GridEntity<UsersDto>>("No data found"));

		return Ok(ApiResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
	}

	[HttpPost(RouteConstants.SaveUser)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> SaveUser([FromBody] UsersDto usersDto)
	{
		// CurrentUser is guaranteed to be available by [AuthorizeUser]
		var currentUser = CurrentUser;

		if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
			throw new IdParametersBadRequestException();

		// EmptyObjectFilterAttribute already validates null, but service will do business validation
		UsersDto res = await _serviceManager.Users.SaveUser(usersDto);

		if (res.UserId <= 0)
			throw new InvalidCreateOperationException("Failed to save user record.");

		return Ok(ApiResponseHelper.Created(res, "User saved successfully"));
	}


	[HttpPut(RouteConstants.UpdateUser)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> UpdateUser([FromRoute] int key, [FromBody] UsersDto usersDto)
	{
		// CurrentUser is guaranteed to be available by [AuthorizeUser]
		var currentUser = CurrentUser;

		if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
			throw new IdParametersBadRequestException();

		if (key <= 0)
			throw new GenericBadRequestException("Invalid user ID. ID must be greater than 0.");

		// EmptyObjectFilterAttribute already validates null
		UsersDto res = await _serviceManager.Users.SaveUser(usersDto);

		if (res.UserId <= 0)
			throw new InvalidUpdateOperationException("Failed to update user record.");

		return Ok(ApiResponseHelper.Updated(res, "User updated successfully"));
	}
}