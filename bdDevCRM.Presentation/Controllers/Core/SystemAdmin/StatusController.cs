using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;

public class StatusController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public StatusController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.StatusByMenuId)]
  //[AllowAnonymous]
  public async Task<IActionResult> StatusByMenuId([FromQuery] int menuId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    if (menuId == 0 || menuId == null) throw new IdParametersBadRequestException();

    IEnumerable<WfStateDto> groupPermissions = await _serviceManager.WfState.StatusByMenuId(menuId, trackChanges: false);
    return Ok(groupPermissions);
  }

  [HttpGet(RouteConstants.ActionsByStatusIdForGroup)]
  //[AllowAnonymous]
  public async Task<IActionResult> ActionsByStatusIdForGroup([FromQuery] int statusId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    if (statusId == 0 || statusId == null) throw new IdParametersBadRequestException();

    IEnumerable<WfActionDto> groupPermissions = await _serviceManager.WfState.ActionsByStatusIdForGroup(statusId, trackChanges: false);
    return Ok(groupPermissions);
  }

  #region WorkFlow start
  [HttpPost(RouteConstants.WorkFlowSummary)]
  public async Task<IActionResult> GetWorkFlowSummary([FromBody] CRMGridOptions options)
  {
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null) throw new IdParametersBadRequestException();
    var summaryGrid = await _serviceManager.WfState.WorkflowSummary(trackChanges: false, options);
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<WfStateDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  #region Old_Code
  //[HttpPost(RouteConstants.CreateWorkFlow)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> SaveState([FromBody] WfStateDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }
  //  var res = await _serviceManager.WfState.SaveWorkflow(modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}


  //[HttpPost(RouteConstants.UpdateWorkFlow)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> UpdateWorkFlow([FromRoute] int key, [FromBody] WfStateDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }
  //  var res = await _serviceManager.WfState.SaveWorkflow(modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}

  #endregion Old_Code

  // --------- 3. Create ----------------------------------------------

  [HttpPost(RouteConstants.CreateWorkFlow)]
  [RequestSizeLimit(1_000_000)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> CreateWorkFlow([FromBody] WfStateDto modelDto)
  {
    try
    {
      if (modelDto == null)
        return BadRequest(ResponseHelper.BadRequest("Status data is required"));

      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

      if (modelDto == null)
        return BadRequest(ResponseHelper.BadRequest("Status data is required"));

      WfStateDto res = await _serviceManager.WfState.CreateNewRecordAsync(modelDto, currentUser);

      if (res.WfStateId > 0)
        return Ok(ResponseHelper.Created(res, "Status created successfully"));
      else
        return StatusCode(500, ResponseHelper.InternalServerError("Failed to create data."));
    }
    catch (System.Text.Json.JsonException)
    {
      return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in workflow data."));
    }
  }


  // --------- Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateWorkFlow)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateWorkFlow([FromRoute] int key, [FromBody] WfStateDto modelDto)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token."));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User not found in cache."));

      var res = await _serviceManager.WfState.UpdateRecordAsync(key, modelDto, false, currentUser);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Record updated successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  [HttpPost(RouteConstants.CreateAction)]
  [RequestSizeLimit(1_000_000)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> CreateAction([FromBody] WfActionDto modelDto)
  {
    // Authentication check
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new UnauthorizedException("User authentication required");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new UnauthorizedException("User session expired");

    modelDto.WfActionId = 0;

    WfActionDto result = await _serviceManager.WfState.CreateWfActionNewRecordAsync(modelDto, currentUser, false);

    return Ok(ResponseHelper.Created(result, "Action created successfully"));
  }

  [HttpPut(RouteConstants.UpdateAction)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateAction([FromRoute] int key, [FromBody] WfActionDto modelDto)
  {
    // Authentication check
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new UnauthorizedException("User authentication required");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new UnauthorizedException("User session expired");

    // Key validation
    if (key <= 0)
      throw new BadRequestException("Valid action ID is required");

    modelDto.WfActionId = key;

    string result = await _serviceManager.WfState.UpdateWfActionRecordAsync(key, modelDto, currentUser, false);

    if (result == OperationMessage.Success)
      return Ok(ResponseHelper.Updated(result, "Action updated successfully"));
    else
      throw new GenericConflictException(result);
  }


  [HttpDelete(RouteConstants.DeleteAction)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteAction([FromRoute] int key, [FromBody] WfActionDto modelDto)
  {
    // Authentication check
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new UnauthorizedException("User authentication required");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new UnauthorizedException("User session expired");

    // Key validation
    if (key <= 0)
      throw new BadRequestException("Valid action ID is required");

    // Ensure ID consistency
    modelDto.WfActionId = key;

    string result = await _serviceManager.WfState.DeleteAction(key, modelDto);

    return Ok(ResponseHelper.Success(result, "Action deleted successfully"));
  }

  [HttpGet(RouteConstants.GetNextStatesByMenu)]
  public async Task<IActionResult> GetNextStatesByMenu([FromQuery] int menuId)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

      if (menuId <= 0)
        return BadRequest(ResponseHelper.BadRequest("Valid menu ID is required"));

      var res = await _serviceManager.WfState.GetNextStatesByMenu(menuId);

      if (res != null && res.Any())
        return Ok(ResponseHelper.Success(res, "Next states retrieved successfully"));
      else
        return Ok(ResponseHelper.NoContent<IEnumerable<WfStateDto>>("No next states found for this menu"));
    }
    catch (Exception)
    {
      return StatusCode(500, ResponseHelper.InternalServerError("An error occurred while retrieving next states"));
    }
  }


  [HttpPost(RouteConstants.GetActionSummaryByStatusId)]
  public async Task<IActionResult> GetActionByStatusId([FromBody] CRMGridOptions options, [FromQuery] int stateId)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

      if (stateId <= 0)
        return BadRequest(ResponseHelper.BadRequest("Valid state ID is required"));

      if (options == null)
        return BadRequest(ResponseHelper.BadRequest("Grid options are required"));

      var res = await _serviceManager.WfState.GetActionByStatusId(stateId, options);

      if (res != null && res.Items.Any())
        return Ok(ResponseHelper.Success(res, "Actions retrieved successfully"));
      else
        return Ok(ResponseHelper.NoContent<GridEntity<WfActionDto>>("No actions found for this status"));
    }
    catch (Exception)
    {
      return StatusCode(500, ResponseHelper.InternalServerError("An error occurred while retrieving actions"));
    }
  }
  #endregion WorkFlow end


  [HttpGet(RouteConstants.StatusByMenuNUserId)]
  public async Task<IActionResult> StatusByMenuNUserId()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    //var menu = ManageMenu.GetAsync(this, _serviceManager).GetAwaiter().GetResult();
    // Prefer await over GetAwaiter().GetResult()
    var menu = await ManageMenu.GetAsync(this, _serviceManager);

    if (!menu.MenuId.HasValue)
      throw new GenericBadRequestException("Valid MenuId is required.");

    if (!currentUser.UserId.HasValue)
      throw new GenericBadRequestException("Valid UserId is required.");
    // menu.MenuId and currentUser.UserId are nullable so we use .Value after checking HasValue
    var res = await _serviceManager.WfState.GetWFStateByUserPermission(menu.MenuId.Value, currentUser.UserId.Value);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

    return Ok(ResponseHelper.Success(res, "Application retrieved successfully"));
  }


  [HttpGet(RouteConstants.StatusByMenuName)]
  public async Task<IActionResult> StatusByMenuName([FromBody] string menuName)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    //var menu = ManageMenu.GetAsync(this, _serviceManager).GetAwaiter().GetResult();
    // Prefer await over GetAwaiter().GetResult()
    var menu = await ManageMenu.CheckByMenuName(this, menuName ,_serviceManager);

    if (!menu.MenuId.HasValue)
      throw new GenericBadRequestException("Valid MenuId is required.");

    if (!currentUser.UserId.HasValue)
      throw new GenericBadRequestException("Valid UserId is required.");
    // menu.MenuId and currentUser.UserId are nullable so we use .Value after checking HasValue


    var res = await _serviceManager.WfState.GetWFStateByUserPermission(menu.MenuId.Value, currentUser.UserId.Value);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

    return Ok(ResponseHelper.Success(res, "Application retrieved successfully"));
  }

  [HttpDelete(RouteConstants.DeleteWorkFlow)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteWorkFlow([FromRoute] int key, [FromBody] WfStateDto modelDto)
  {
    // Authentication check
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new UnauthorizedException("User authentication required");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new UnauthorizedException("User session expired");

    // Key validation
    if (key <= 0 && key != modelDto.WfStateId)
      throw new BadRequestException("Valid workflow status ID is required");

    string result = await _serviceManager.WfState.DeleteWorkflow(key);

    // If we reach here, deletion was successful
    return Ok(ResponseHelper.Success(result, "Workflow status deleted successfully"));
  }
}
