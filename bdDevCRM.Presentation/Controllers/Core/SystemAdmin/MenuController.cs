using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


public class EmployeeController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public EmployeeController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }


  [HttpGet(RouteConstants.SelectMenuByUserPermission)]
  //[Produces("application/json")]
  [ResponseCache(Duration = 300)] // Browser caching for 5 minutes
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> SelectMenuByUserPermission()
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    // Try to get from cache first
    string cacheKey = $"menu_permissions_{userId}";
    if (_cache.TryGetValue(cacheKey, out IEnumerable<MenuDto> cachedMenus))
    {
      return Ok(cachedMenus);
    }
    var menusDtoTask = _serviceManager.Menus.SelectMenuByUserPermission(userId, trackChanges: false);

    // Add timeout to prevent long-running queries
    var completedTask = await Task.WhenAny(menusDtoTask, Task.Delay(5000));

    if (completedTask != menusDtoTask)
    {
      //_logger.LogWarning("Menu query timeout for user {UserId}", userId);
      return StatusCode(408, "Request timeout while retrieving menu data");
    }

    var menusDto = await menusDtoTask;

    if (!menusDto.Any())
      return NoContent();

    // Cache the result
    var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)).SetPriority(CacheItemPriority.High);

    _cache.Set(cacheKey, menusDto, cacheOptions);

    return Ok(menusDto);
  }

  [HttpGet(RouteConstants.GetParentMenuByMenu)]
  public async Task<IActionResult> GetParentMenuByMenu(int parentMenuId)
  {
    var menusDto = await _serviceManager.Menus.GetParentMenuByMenu(parentMenuId, false);
    return menusDto.Any() ? Ok(menusDto) : NoContent();
  }

  [HttpGet(RouteConstants.GetMenus)]
  [ResponseCache(Duration = 60)] // Browser caching for 5 minutes
  //[IgnoreMediaTypeValidation]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetMenus()
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    IEnumerable<MenuDto> menusDto = await _serviceManager.Menus.GetMenusAsync(trackChanges: false);
    //return (menuSummary != null ) ? Ok(menuSummary) : NoContent();
    return Ok(menusDto.ToList());
  }

  [HttpGet(RouteConstants.MenusByModuleId)]
  [ResponseCache(Duration = 60)] // Browser caching for 5 minutes
  //[IgnoreMediaTypeValidation]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MenusByModuleId(int moduleId)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    IEnumerable<MenuDto> menusDto = await _serviceManager.Menus.MenusByModuleId(moduleId, trackChanges: false);
    //return (menuSummary != null ) ? Ok(menuSummary) : NoContent();
    return Ok(menusDto.ToList());
  }


  /// <summary>
  /// After login to system
  /// </summary>
  /// <param name="options"></param>
  /// <returns></returns>
  [HttpPost(RouteConstants.MenuSummary)]
  //[IgnoreMediaTypeValidation]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetMenuSummary([FromBody] CRMGridOptions options)
  {
    var menuSummary = await _serviceManager.Menus.MenuSummary(trackChanges: false, options);
    return (menuSummary != null) ? Ok(menuSummary) : NoContent();
  }


  [HttpPost(RouteConstants.CreateMenu)]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> SaveMenu([FromBody] MenuDto modelDto)
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

    var model = await _serviceManager.Menus.CreateAsync(modelDto);
    return (model != null) ? Ok(model) : NoContent();
  }


  [HttpPut(RouteConstants.UpdateMenu)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    MenuDto returnData = await _serviceManager.Menus.UpdateAsync(key, modelDto);
    return (returnData != null) ? Ok(OperationMessage.Success) : NoContent();
  }

  [HttpDelete(RouteConstants.DeleteMenu)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    await _serviceManager.Menus.DeleteAsync(key, modelDto);
    return Ok("Success");
  }





  [HttpGet(RouteConstants.MenuForDDL)]
  //[ResponseCache(Duration = 60)] // Browser caching for 5 minutes
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MenuForDDL()
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

    IEnumerable<MenuForDDLDto> menusDto = await _serviceManager.Menus.MenuForDDL();
    return Ok(menusDto.ToList());
  }





}
