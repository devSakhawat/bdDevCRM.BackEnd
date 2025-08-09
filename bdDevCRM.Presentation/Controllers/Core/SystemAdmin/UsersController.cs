using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;

using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using System.Threading.Tasks;

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
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null) throw new IdParametersBadRequestException();
    var summaryGrid = await _serviceManager.Users.UsersSummary(companyId, trackChanges: false, options, currentUser);
    //return (groupSummary != null) ? Ok(groupSummary) : NoContent();
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<UsersDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.SaveUser)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveUser([FromBody] UsersDto usersDto)
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

    UsersDto res = new UsersDto();
    if (currentUser != null)
    {
      res = await _serviceManager.Users.SaveUser(usersDto);

      ////Audittail
      //var shortDescription = userId == 0 ? "User Information try to Inserted" : "User Information try to Updated";
      //var actionType = userId == 0 ? "Insert" : "Update";
      //var audit = hendler.CreateAuditObject(users, shortDescription, actionType, res);
      //aService.SendAudit(audit);

    }

    //
    if (res.UserId <= 0)
      throw new InvalidUpdateOperationException("Failed to update institute record.");

    // Return success response
    return Ok(ResponseHelper.Success(usersDto, "Institute updated successfully"));

    //if (res == OperationMessage.Success)
    //{
    //  return Ok(res);
    //}
    //else
    //{
    //  return Conflict(res);
    //}
  }
  

  [HttpPut(RouteConstants.UpdateUser)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateUser([FromRoute] int key, [FromBody] UsersDto usersDto)
  {
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");
    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    UsersDto res = new UsersDto();
    if (currentUser != null)
    {

      //usersDto.UserName = usersDto.UserName.Replace("'", "''");
      //usersDto.UserId = userId;

      res = await _serviceManager.Users.SaveUser(usersDto);

      ////Audittail
      //var shortDescription = userId == 0 ? "User Information try to Inserted" : "User Information try to Updated";
      //var actionType = userId == 0 ? "Insert" : "Update";
      //var audit = hendler.CreateAuditObject(users, shortDescription, actionType, res);
      //aService.SendAudit(audit);

    }

    //
    if (res.UserId <= 0)
      throw new InvalidUpdateOperationException("Failed to update institute record.");

    // Return success response
    return Ok(ResponseHelper.Success(res, "Institute updated successfully"));
  }


}