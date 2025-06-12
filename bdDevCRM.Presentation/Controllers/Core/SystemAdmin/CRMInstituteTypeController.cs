using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


public class CRMInstituteTypeController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public CRMInstituteTypeController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = svc;
    _cache = cache;
  }

  // 1️⃣ DDL ------------------------------------------------------------
  [HttpGet(RouteConstants.InstituteTypeDDL)]
  public async Task<IActionResult> InstituteTypeDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstituteTypes.GetInstituteTypesDDLAsync(trackChanges: false);
    return Ok(res);
  }

  // 2️⃣ Summary Grid --------------------------------------------------
  [HttpPost(RouteConstants.InstituteTypeSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    if (options == null) return BadRequest("CRMGridOptions cannot be null.");

    var summary = await _serviceManager.CRMInstituteTypes.SummaryGrid(options);
    return (summary != null) ? Ok(summary) : NoContent();
  }

  // 3️⃣ Create --------------------------------------------------------
  [HttpPost(RouteConstants.CreateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Create([FromBody] CRMInstituteTypeDto dto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstituteTypes.CreateNewRecordAsync(dto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // 4️⃣ Update --------------------------------------------------------
  [HttpPut(RouteConstants.UpdateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Update([FromRoute] int key, [FromBody] CRMInstituteTypeDto dto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstituteTypes.UpdateRecordAsync(key, dto, false);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // 5️⃣ Delete --------------------------------------------------------
  [HttpDelete(RouteConstants.DeleteInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Delete([FromRoute] int key, [FromBody] CRMInstituteTypeDto dto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("UnAuthorized attempted");

    var res = await _serviceManager.CRMInstituteTypes.DeleteRecordAsync(key, dto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // 6️⃣ SaveOrUpdate --------------------------------------------------
  [HttpPost(RouteConstants.CreateOrUpdateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key,
                                                [FromBody] CRMInstituteTypeDto dto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstituteTypes.SaveOrUpdateAsync(key, dto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }
}
