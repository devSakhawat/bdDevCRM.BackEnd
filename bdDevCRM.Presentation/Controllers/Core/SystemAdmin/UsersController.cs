using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using System.Threading.Tasks;

public class UsersController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public UsersController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpPost(RouteConstants.UserSummary)]
  public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options, int companyId)
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);
    // get hr record id from user.
    if (user.HrRecordId == 0 || user.HrRecordId == null) throw new IdParametersBadRequestException();
    var groupSummary = await _serviceManager.Users.UsersSummary(companyId, trackChanges: false, options, user);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpPost(RouteConstants.SaveUser)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveUser([FromBody] UsersDto usersDto)
  {
    // from claim.
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("UserId not found in token.");
    }
    var userId = Convert.ToInt32(userIdClaim);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    string res = string.Empty;
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

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }

}