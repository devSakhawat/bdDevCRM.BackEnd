using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

//public class CRMInstituteController : BaseApiController
public class CRMInstituteController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public CRMInstituteController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.InstituteDDL)]
  public async Task<IActionResult> InstituteDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    return Ok(res);
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.InstituteSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    if (options == null)
      return BadRequest("CRMGridOptions cannot be null.");

    var summaryGrid = await _serviceManager.CRMInstitutes.SummaryGrid(options);
    return (summaryGrid != null) ? Ok(summaryGrid) : NoContent();
  }

  // --------- 3. Create ----------------------------------------------
  [HttpPost(RouteConstants.CreateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> CreateNewRecord([FromBody] CrmInstituteDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.CreateNewRecordAsync(modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateInstitute([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.UpdateRecordAsync(key, modelDto, false);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteInstitute([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("UnAuthorized attempted");

    var res = await _serviceManager.CRMInstitutes.DeleteRecordAsync(key, modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 6. SaveOrUpdate ----------------------------------------
  [HttpPost(RouteConstants.CreateOrUpdateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("UserId not found in token.");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.SaveOrUpdateAsync(key, modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }
}