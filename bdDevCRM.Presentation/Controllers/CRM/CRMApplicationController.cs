using bdDevCRM.Entities.CRMGrid.GRID;

using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;

public class CRMApplicationController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public CRMApplicationController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }


  #region Course Details start
  [HttpGet(RouteConstants.CRMCountryDLL)]
  public async Task<IActionResult> CRMCountryDLL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("UserId not found in token.");
    }
    var userId = Convert.ToInt32(userIdClaim);
    // userId : which key is responsible to when cache was created.
    // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var groupSummary = await _serviceManager.Countries.GetCountriesDDLAsync(trackChanges: false);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpGet(RouteConstants.CRMInstituteDLLByCountry)]
  public async Task<IActionResult> CRMInstituteDLLByCountry([FromQuery] int countryId)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("Unauthorized attempt to get data!");
    }
    var userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync( trackChanges: false);
    //var res = await _serviceManager.CRMInstitutes.CRMInstituteDLLByCountry(countryId, trackChanges: false);

    return Ok(res);
  }

  //[HttpGet(RouteConstants.CRMCourseDLLByInstitute)]
  //public async Task<IActionResult> CRMCourseDLLByInstitute([FromQuery] int countryId)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("Unauthorized attempt to get data!");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }

  //  var res = await _serviceManager.CRMCourses.CRMCourseDLLByInstitute(countryId, trackChanges: false);

  //  return Ok(res);
  //}
  

  [HttpGet(RouteConstants.CRMInstituteTypeDDL)]
  public async Task<IActionResult> CRMInstituteTypeDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("Unauthorized attempt to get data!");
    }
    var userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstituteTypes.GetInstituteTypesDDLAsync();

    return Ok(res);
  }
 


  //[HttpPost(RouteConstants.CreateWorkFlow)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> SaveState([FromBody] WfstateDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }
  //  var res = await _serviceManager.WfState.SaveWorkflow(modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}



  //[HttpPost(RouteConstants.CreateAction)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> CreateAction([FromBody]  WfActionDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }

  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }
  //  var res = await _serviceManager.WfState.CreateActionAsync(modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}



  //[HttpPut(RouteConstants.UpdateAction)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> UpdateAction([FromRoute] int key, [FromBody] WfActionDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }

  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }
  //  var res = await _serviceManager.WfState.CreateActionAsync(modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}


  //[HttpDelete(RouteConstants.DeleteAction)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> DeleteAction([FromRoute] int key, [FromBody]  WfActionDto modelDto)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("UserId not found in token.");
  //  }

  //  var userId = Convert.ToInt32(userIdClaim);
  //  // userId : which key is responsible to when cache was created.
  //  // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //  if (currentUser == null)
  //  {
  //    return Unauthorized("UnAuthorized attempted");
  //  }
  //  var res = await _serviceManager.WfState.DeleteAction(key,modelDto);

  //  if (res == OperationMessage.Success)
  //  {
  //    return Ok(res);
  //  }
  //  else
  //  {
  //    return Conflict(res);
  //  }
  //}




  //[HttpGet(RouteConstants.GetNextStatesByMenu)]
  //public async Task<IActionResult> GetNextStatesByMenu([FromQuery] int menuId)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("Unauthorized attempt to get data!");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }

  //  var res = await _serviceManager.WfState.GetNextStatesByMenu(menuId);

  //  return Ok(res);
  //}


  //[HttpPost(RouteConstants.GetActionSummaryByStatusId)]
  //public async Task<IActionResult> GetActionByStatusId([FromBody] CRMGridOptions options, [FromQuery]int stateId)
  //{
  //  var userIdClaim = User.FindFirst("UserId")?.Value;
  //  if (string.IsNullOrEmpty(userIdClaim))
  //  {
  //    return Unauthorized("Unauthorized attempt to get data!");
  //  }
  //  var userId = Convert.ToInt32(userIdClaim);
  //  UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

  //  if (currentUser == null)
  //  {
  //    return Unauthorized("User not found in cache.");
  //  }

  //  var res = await _serviceManager.WfState.GetActionByStatusId(stateId, options);

  //  return Ok(res);
  //}
  #endregion  Course Details end

}