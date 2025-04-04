using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
  public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options)
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    var cacheKey = $"User_{userId}";

    UsersDto user = _serviceManager.GetCache<UsersDto>(cacheKey);
    var hrRecordId = user.HrRecordId ?? 0;
    if (hrRecordId == 0 || hrRecordId == null) throw new IdParametersBadRequestException();
    var groupSummary = await _serviceManager.Users.UsersSummary(trackChanges: false, options, hrRecordId);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }



}