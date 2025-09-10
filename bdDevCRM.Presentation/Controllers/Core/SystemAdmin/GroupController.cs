using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;

using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
  public async Task<IActionResult> GroupPermisionsbyGroupId([FromQuery] int groupId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    if (groupId == 0 || groupId == null) throw new IdParametersBadRequestException();

    IEnumerable<GroupPermissionDto> groupPermissions = await _serviceManager.Groups.GroupPermisionsbyGroupId(groupId);
    return Ok(groupPermissions);
  }

  [HttpGet(RouteConstants.GetAccesses)]
  public async Task<IActionResult> GetAccesses()
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    IEnumerable<AccessControlDto> accessControlls = await _serviceManager.Groups.GetAccesses();
    return Ok(accessControlls);
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
  public async Task<IActionResult> UpdateGroup([FromRoute] int key, [FromBody] GroupDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    GroupDto returnData = await _serviceManager.Groups.UpdateAsync(key, modelDto);
    return (returnData != null) ? Ok(returnData) : NoContent();
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

  //[HttpGet(RouteConstants.GroupsByModuleId)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute
  //public async Task<IActionResult> GroupsByModuleId(int moduleId)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  IEnumerable<GroupDto> groupsDto = await _serviceManager.Groups.GroupsByModuleId(moduleId, trackChanges: false);
  //  return Ok(groupsDto.ToList());
  //}

  //[HttpPut(RouteConstants.UpdateGroup)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> UpdateGroup([FromRoute] int key, [FromBody] GroupDto modelDto)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

  //  GroupDto returnData = await _serviceManager.Groups.UpdateAsync(key, modelDto);
  //  return (returnData != null) ? Ok(returnData) : NoContent();
  //}

  //[HttpDelete(RouteConstants.DeleteGroup)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> DeleteGroup([FromRoute] int key, [FromBody] GroupDto modelDto)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

  //  await _serviceManager.Groups.DeleteAsync(key, modelDto);
  //  return Ok("Success");
  //}

}