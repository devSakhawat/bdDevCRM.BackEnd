using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;

public class StatusController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public StatusController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }



  [HttpGet(RouteConstants.StatusByMenuId)]
  //[AllowAnonymous]
  public async Task<IActionResult> StatusByMenuId([FromQuery] int menuId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    if (menuId == 0 || menuId == null) throw new IdParametersBadRequestException();

    IEnumerable<WfstateDto> groupPermissions = await _serviceManager.WfState.StatusByMenuId(menuId, trackChanges: false);
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
    var groupSummary = await _serviceManager.WfState.WorkflowSummary(trackChanges: false, options);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }



  [HttpPost(RouteConstants.CreateWorkFlow)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveState([FromBody] WfstateDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("UserId not found in token.");
    }
    var userId = Convert.ToInt32(userIdClaim);
    // userId : which key is responsible to when cache was created.
    // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }
    var res = await _serviceManager.WfState.SaveWorkflow(modelDto);

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }
  #endregion WorkFlow end

}