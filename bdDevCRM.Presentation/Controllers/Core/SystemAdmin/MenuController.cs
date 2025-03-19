using bdDevCRM.Entities.Entities;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.KendoGrid;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MenuController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public MenuController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  //[HttpGet("SelectMenuByUserPermission")]
  [HttpGet(RouteConstants.SelectMenuByUserPermission)]
  //[Produces("application/json")]
  [ResponseCache(Duration = 300)] // Browser caching for 5 minutes
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> SelectMenuByUserPermission()
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    // Try to get from cache first
    string cacheKey = $"menu_permissions_{userId}";
    if (_cache.TryGetValue(cacheKey, out IEnumerable<MenuDto> cachedMenus))
    {
      //_logger.LogInformation("Menu returned from cache for user {UserId}", userId);
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
    var cacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
        .SetPriority(CacheItemPriority.High);

    _cache.Set(cacheKey, menusDto, cacheOptions);

    return Ok(menusDto);
  }

  [HttpGet(RouteConstants.GetParentMenuByMenu)]
  public async Task<IActionResult> GetParentMenuByMenu(int parentMenuId)
  {
    var menusDto = await _serviceManager.Menus.GetParentMenuByMenu(parentMenuId, false);
    return menusDto.Any() ? Ok(menusDto) : NoContent();
  }



  // After login to system
  [HttpPost(RouteConstants.CreateMenu)]
  public IActionResult SaveMenu([FromBody] MenuDto model)
  {
    try
    {
      var res = _serviceManager.Menus.CreateMenu(model);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
    }
  }


  [HttpPost(RouteConstants.MenuSummary)]
  //[IgnoreMediaTypeValidation]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetMenuSummary([FromBody] GridOptions options)
  {
    var menuSummary = await _serviceManager.Menus.MenuSummary(trackChanges: false , options);
    return (menuSummary != null ) ? Ok(menuSummary) : NoContent();
  }


  [HttpGet(RouteConstants.GetMenus)]
  [ResponseCache(Duration = 60)] // Browser caching for 5 minutes
  //[IgnoreMediaTypeValidation]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    IEnumerable<MenuDto> menusDto = await _serviceManager.Menus.MenusByModuleId(moduleId,trackChanges: false);
    //return (menuSummary != null ) ? Ok(menuSummary) : NoContent();
    return Ok(menusDto.ToList());
  }


}
