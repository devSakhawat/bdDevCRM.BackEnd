using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

    IEnumerable<WfstateDto> groupPermissions = await _serviceManager.WfState.StatusByMenuId(menuId, trackChanges:false);
    return Ok(groupPermissions);
  }

  [HttpGet(RouteConstants.ActionsByStatusIdForGroup)]
  //[AllowAnonymous]
  public async Task<IActionResult> ActionsByStatusIdForGroup([FromQuery] int statusId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    if (statusId == 0 || statusId == null) throw new IdParametersBadRequestException();

    IEnumerable<WfActionDto> groupPermissions = await _serviceManager.WfState.ActionsByStatusIdForGroup(statusId, trackChanges:false);
    return Ok(groupPermissions);
  }


}