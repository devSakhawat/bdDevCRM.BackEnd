using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


//public class EmployeeController : BaseApiController
[AuthorizeUser]
public class MenuController : BaseApiController
{
    //private readonly IServiceManager _serviceManager;
    private readonly IMemoryCache _cache;

    public MenuController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
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
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId) || userId <= 0)
        {
            return Unauthorized(ResponseHelper.Unauthorized("User authentication required. Please log in again."));
        }

        // Try to get from cache first
        string cacheKey = $"menu_permissions_{userId}";
        if (_cache.TryGetValue(cacheKey, out IEnumerable<MenuDto> cachedMenus))
        {
            return Ok(ResponseHelper.Success(cachedMenus, "Menus retrieved from cache"));
        }

        var menusDtoTask = _serviceManager.Menus.SelectMenuByUserPermission(userId, trackChanges: false);

        // Add timeout to prevent long-running queries
        var completedTask = await Task.WhenAny(menusDtoTask, Task.Delay(5000));

        if (completedTask != menusDtoTask)
        {
            //_logger.LogWarning("Menu query timeout for user {UserId}", userId);
            throw new RequestTimeoutException("Request timeout while retrieving menu data");
        }

        var menusDto = await menusDtoTask;

        if (!menusDto.Any())
            return Ok(ResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found for this user."));

        // Cache the result
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
            .SetPriority(CacheItemPriority.High);

        _cache.Set(cacheKey, menusDto, cacheOptions);

        return Ok(ResponseHelper.Success(menusDto, "Menus retrieved successfully"));
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
    [ResponseCache(Duration = 60)]
    //[IgnoreMediaTypeValidation]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> MenusByModuleId([FromRoute] int moduleId)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new GenericUnauthorizedException("User authentication required.");

        if (!int.TryParse(userIdClaim, out int userId))
            throw new GenericBadRequestException("Invalid user ID format.");

        UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
        if (currentUser == null)
            throw new GenericUnauthorizedException("User session expired.");

        var res = await _serviceManager.Menus.MenusByModuleId(moduleId, trackChanges: false);
        if (res == null)
            return Ok(ResponseHelper.NoContent<IEnumerable<ModuleDto>>("No data found!"));

        return Ok(ResponseHelper.Success(res, "Data retrieved successfully"));
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

        if (menuSummary == null)
            return Ok(ResponseHelper.NoContent<GridEntity<MenuDto>>("No data found!"));

        return Ok(ResponseHelper.Success(menuSummary, "Menu summary retrieved successfully"));
    }


    [HttpPost(RouteConstants.CreateMenu)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateMenu([FromBody] MenuDto modelDto)
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
        //return (model != null) ? Ok(model) : NoContent();
        if (model.MenuId <= 0)
            throw new InvalidCreateOperationException("Failed to create record.");

        return Ok(ResponseHelper.Created(model, "Menu created successfully."));
    }


    [HttpPut(RouteConstants.UpdateMenu)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("UserId not found in token.");
        }

        var userId = Convert.ToInt32(userIdClaim);

        UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
        if (currentUser == null)
        {
            return Unauthorized("User not found in cache.");
        }

        MenuDto returnData = await _serviceManager.Menus.UpdateAsync(key, modelDto);
        if (returnData.MenuId <= 0)
            throw new InvalidCreateOperationException("Failed to create record.");

        return Ok(ResponseHelper.Updated(returnData, "Menu created successfully."));
    }

    //[HttpDelete(RouteConstants.DeleteMenu)]
    ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<IActionResult> DeleteMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
    //{
    //    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    //    await _serviceManager.Menus.DeleteAsync(key, modelDto);
    //    return Ok("Success");
    //}

    [HttpDelete(RouteConstants.DeleteMenu)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteMenu([FromRoute] int key)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new GenericUnauthorizedException("User authentication required.");

        if (!int.TryParse(userIdClaim, out int userId))
            throw new GenericBadRequestException("Invalid user ID format.");

        UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
        if (currentUser == null)
            throw new GenericUnauthorizedException("User session expired.");

        await _serviceManager.Menus.DeleteAsync(key);
        return Ok(ResponseHelper.Success("Menu deleted successfully."));
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
