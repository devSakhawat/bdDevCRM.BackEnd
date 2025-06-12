
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class CurrencyController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public CurrencyController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.CurrencyDDL)]
  public async Task<IActionResult> CurrencyDDL()
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

    var res = await _serviceManager.Currencies.GetCurrenciesDDLAsync();
    return Ok(res);
  }

  
  [HttpPost(RouteConstants.CurrencySummary)]
  public async Task<IActionResult> CurrencySummary([FromBody] CRMGridOptions options)
  {
    var currencySummary = await _serviceManager.Currencies.CurrecySummary(options);
    return (currencySummary != null) ? Ok(currencySummary) : NoContent();
  }


  [HttpPost(RouteConstants.CreateOrUpdateCurrency)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CurrencyDto modelDto)
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
    var res = await _serviceManager.Currencies.SaveOrUpdate(key,modelDto);

    if (res == OperationMessage.Success)
    {
      return Ok(res);
    }
    else
    {
      return Conflict(res);
    }
  }

  //[HttpPut(RouteConstants.UpdateCurrency)]
  //[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  //public async Task<IActionResult> UpdateCurrency([FromRoute] int key, [FromBody] WfActionDto modelDto)
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


  [HttpDelete(RouteConstants.DeleteCurrency)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteCurrency([FromRoute] int key, [FromBody] CurrencyDto modelDto)
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
    var res = await _serviceManager.Currencies.DeleteCurrency(key, modelDto);

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