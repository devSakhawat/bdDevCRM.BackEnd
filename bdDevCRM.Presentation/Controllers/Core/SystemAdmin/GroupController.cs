using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.ServicesContract;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class GroupController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public GroupController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  //[HttpPost(RouteConstants.GroupSummary)]
  ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> GroupSummary([FromBody] CRMGridOptions options)
  //{
  //  var groupSummary = await _serviceManager.Groups.GroupSummary(trackChanges: false, options);
  //  return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  //}


  //[HttpGet(RouteConstants.Groups)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> GetGroups()
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  IEnumerable<GroupDto> groupsDto = await _serviceManager.Groups.GetGroupsAsync(trackChanges: false);
  //  return Ok(groupsDto.ToList());
  //}

  //[HttpGet(RouteConstants.GroupsByModuleId)]
  //[ResponseCache(Duration = 60)] // Browser caching for 1 minute
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> GroupsByModuleId(int moduleId)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  IEnumerable<GroupDto> groupsDto = await _serviceManager.Groups.GroupsByModuleId(moduleId, trackChanges: false);
  //  return Ok(groupsDto.ToList());
  //}



  //[HttpPost(RouteConstants.CreateGroup)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //public async Task<IActionResult> SaveGroup([FromBody] GroupDto modelDto)
  //{
  //  var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
  //  var model = await _serviceManager.Groups.CreateAsync(modelDto);
  //  return (model != null) ? Ok(model) : NoContent();
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