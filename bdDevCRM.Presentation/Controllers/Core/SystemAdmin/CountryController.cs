
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class CountryController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public CountryController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }


  [HttpGet(RouteConstants.CountryDDL)]
  public async Task<IActionResult> CountryDDL()
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

    //var res = await _serviceManager.Currencies.GetCurrenciesDDLAsync();
    var res = await _serviceManager.Countries.GetCountriesDDLAsync(trackChanges: false);
    return Ok(res);
  }


  [HttpPost(RouteConstants.CountrySummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
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
    if (options == null)
    {
      return BadRequest("CRMGridOptions cannot be null.");
    }
    var summaryGrid = await _serviceManager.Countries.SummaryGrid(options);
    return (summaryGrid != null) ? Ok(summaryGrid) : NoContent();
  }


  [HttpPost(RouteConstants.CreateCountry)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> CreateNewRcord([FromBody] CountryDto modelDto)
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

    var res = await _serviceManager.Countries.CreateNewRecordAsync(modelDto);

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }


  [HttpPut(RouteConstants.UpdateCountry)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateCountry([FromRoute] int key, [FromBody] CountryDto modelDto)
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
    var res = await _serviceManager.Countries.UpdateNewRecordAsync(key, modelDto, false);

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }


  [HttpDelete(RouteConstants.DeleteCountry)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteCountry([FromRoute] int key, [FromBody] CountryDto modelDto)
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
      return Unauthorized("UnAuthorized attempted");
    }
    var res = await _serviceManager.Countries.DeleteRecordAsync(key, modelDto);

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }


  [HttpPost(RouteConstants.CreateOrUpdateCountry)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CountryDto modelDto)
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
    var res = await _serviceManager.Countries.SaveOrUpdate(key, modelDto);

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