using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Common;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

public class GroupController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public GroupController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpPost(RouteConstants.GroupSummary)]
  public async Task<IActionResult> GroupSummary([FromBody] CRMGridOptions options)
  {
    var groupSummary = await _serviceManager.Groups.GroupSummary(trackChanges: false, options);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpGet(RouteConstants.GroupPermisionsbyGroupId)]
  public async Task<IActionResult> GroupPermisionsbyGroupId([FromRoute] int groupId)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");
    if (groupId == 0 || groupId == null) throw new IdParametersBadRequestException();

    var res = await _serviceManager.Groups.GroupPermisionsbyGroupId(groupId);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GroupPermissionDto>>("No data found for the specified group"));

    return Ok(ResponseHelper.Success(res, "Data retrieved successfully"));
  }

  [HttpGet(RouteConstants.GetAccesses)]
  public async Task<IActionResult> GetAccesses()
  {
    //var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    //IEnumerable<AccessControlDto> accessControlls = await _serviceManager.Groups.GetAccesses();
    //return Ok(accessControlls);

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.Groups.GetAccesses();
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<AccessControlDto>>("No data found!"));

    return Ok(ResponseHelper.Success(res, "Data retrieved successfully"));
  }


  [HttpPost(RouteConstants.CreateGroup)]
  public async Task<IActionResult> SaveGroup([FromBody] GroupDto objGroup)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    //var strGroup = strGroupInfo.Replace("^", "&");
    //var objGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<GroupDto>(strGroup);
    if (objGroup == null) throw new ArgumentNullException(nameof(objGroup));

    var model = await _serviceManager.Groups.CreateAsync(objGroup);
    return (model != null) ? Ok(model) : NoContent();
  }


  [HttpPut(RouteConstants.UpdateGroup)]
  public async Task<IActionResult> UpdateGroup([FromRoute] int groupId, [FromBody] GroupDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");
    if (groupId == 0 || groupId == null) throw new IdParametersBadRequestException();

    var res = await _serviceManager.Groups.UpdateAsync(groupId, modelDto);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GroupPermissionDto>>("No data found for the specified group"));

    return Ok(ResponseHelper.Updated(res, "Data Updated successfully"));
  }


  [HttpGet(RouteConstants.Groups)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute
  public async Task<IActionResult> GetGroups()
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    IEnumerable<GroupForUserSettings> groupForUserSettings = await _serviceManager.Groups.GetGroups(trackChanges: false);
    return Ok(groupForUserSettings.ToList());
  }


  [HttpGet(RouteConstants.GetGroupsByUserId)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute
  public async Task<IActionResult> GetGroupsByUserId(int usersUserId)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    //IEnumerable<GroupForUserSettings> groupForUserSettings = await _serviceManager.Groups.GetGroups(trackChanges: false);
    //return Ok(groupForUserSettings.ToList());

    var res = await _serviceManager.Groups.GetGroupsByUserId(usersUserId, trackChanges: false);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GroupForUserSettings>>("No data found!"));

    return Ok(ResponseHelper.Success(res, "Data retrieved successfully"));
  }


  //[HttpGet(RouteConstants.GetGroupMemberByUserId)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute

  [HttpGet(RouteConstants.GroupMemberByUserId)]
  public async Task<IActionResult> GroupMemberByUserId([FromQuery] int userId)
  {
    // from claim.
    var userIdFromSession = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userIdFromSession);

    IEnumerable<GroupMemberDto> groupForUserSettings = await _serviceManager.Groups.GroupMemberByUserId( userId ,trackChanges: false);
    return Ok(groupForUserSettings.ToList());
  }


  //[HttpGet(RouteConstants.GetGroupMemberByUserId)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute

  [HttpPost(RouteConstants.GetAccessPermisionForCurrentUser)]
  public async Task<IActionResult> GetAccessPermisionForCurrentUser([FromBody]CommonProperty model)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    if (model == null)
      throw new NullModelBadRequestException(nameof(CommonProperty));
    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null) throw new IdParametersBadRequestException();

    IEnumerable<GroupPermissionDto> groupPermissions = await _serviceManager.Groups.GetAccessPermisionForCurrentUser(31, userId);
    if (groupPermissions == null || !groupPermissions.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<GroupPermissionDto>>("No Group peremission info found"));

    return Ok(ResponseHelper.Success(groupPermissions.ToList(), "Group peremission info retrieved successfully"));
  }

 


}