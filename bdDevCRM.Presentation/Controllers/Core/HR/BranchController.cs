using bdDevCRM.Entities.Exceptions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.HR;

public class BranchController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public BranchController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.BranchByCompanyIdForCombo)]
  public async Task<IActionResult> BranchByCompanyIdForCombo([FromQuery] int companyId)
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    IEnumerable<BranchDto> branchList = await _serviceManager.Branches.BranchesByCompanyIdForCombo(companyId, user);
    return Ok(branchList);
  }


  //[HttpGet(RouteConstants.StatusByMenuId)]
  ////[AllowAnonymous]
  //public async Task<IActionResult> StatusByMenuId([FromQuery] int menuId)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  if (menuId == 0 || menuId == null) throw new IdParametersBadRequestException();

  //  IEnumerable<WfstateDto> groupPermissions = await _serviceManager.WfState.StatusByMenuId(menuId, trackChanges: false);
  //  return Ok(groupPermissions);
  //}




}
