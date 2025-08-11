using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
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
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    IEnumerable<BranchDto> res = await _serviceManager.Branches.BranchesByCompanyIdForCombo(companyId, currentUser);
    //return Ok(branchList);

    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<BranchDto>>("No Branch found"));

    return Ok(ResponseHelper.Success(res, "Branches retrieved successfully"));
  }


  //[HttpGet(RouteConstants.StatusByMenuId)]
  ////[AllowAnonymous]
  //public async Task<IActionResult> StatusByMenuId([FromQuery] int menuId)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  if (menuId == 0 || menuId == null) throw new IdParametersBadRequestException();

  //  IEnumerable<WfStateDto> groupPermissions = await _serviceManager.WfState.StatusByMenuId(menuId, trackChanges: false);
  //  return Ok(groupPermissions);
  //}




}
