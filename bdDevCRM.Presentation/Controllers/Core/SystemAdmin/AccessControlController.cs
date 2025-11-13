
using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class AccessControlController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public AccessControlController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpPost(RouteConstants.CreateAccessControl)]
  public async Task<IActionResult> SaveAccessControl([FromBody] AccessControlDto modelDto)
  {
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var model = await _serviceManager.AccessControl.CreateAsync(modelDto);
    //return (model != null) ? Ok(model) : NoContent();

    if (model.AccessId <= 0)
      throw new InvalidCreateOperationException("Failed to create new record.");

    return Ok(ResponseHelper.Created(model, "new record created successfully."));
  }

  [HttpPut(RouteConstants.UpdateAccessControl)]
  public async Task<IActionResult> UpdateAccessControl([FromRoute] int key, [FromBody] AccessControlDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    AccessControlDto returnData = await _serviceManager.AccessControl.UpdateAsync(key, modelDto);
    //return (returnData != null) ? Ok(returnData) : NoContent();

    if (modelDto.AccessId <= 0)
      throw new InvalidCreateOperationException("Failed to create new record.");

    return Ok(ResponseHelper.Updated(modelDto, "Record updated successfully."));
  }


  [HttpPost(RouteConstants.AccessControlSummary)]
  public async Task<IActionResult> AccessControlSummary([FromBody] CRMGridOptions options)
  {
    //var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    //var accessSummary = await _serviceManager.AccessControl.AccessControlSummary(trackChanges: false, options);
    //return (accessSummary != null) ? Ok(accessSummary) : NoContent();


    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null) throw new IdParametersBadRequestException();
    var summaryGrid = await _serviceManager.AccessControl.AccessControlSummary(trackChanges: false, options);
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<AccessControlDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }



}