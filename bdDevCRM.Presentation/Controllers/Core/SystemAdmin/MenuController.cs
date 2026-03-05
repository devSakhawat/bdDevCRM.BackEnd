
using bdDevCRM.Presentation.ActionFilters;
using bdDevCRM.Presentation.AuthorizeAttributes;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


/// <summary>
/// Menu management endpoints.
///
/// [AuthorizeUser] at class-level ensures:
///    - Every request validates user via attribute
///    - CurrentUser / CurrentUserId available from BaseApiController
///    - No auth checks needed in controller methods
///    - Exceptions handled by StandardExceptionMiddleware
/// </summary>
[AuthorizeUser]
public class MenuController : BaseApiController
{
	private readonly IMemoryCache _cache;

	public MenuController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
	{
		_cache = cache;
	}


	[HttpGet(RouteConstants.SelectMenuByUserPermission)]
	[ResponseCache(Duration = 300)] // Browser caching for 5 minutes
	public async Task<IActionResult> SelectMenuByUserPermission()
	{
		var userId = CurrentUserId;

		if (userId <= 0)
			throw new GenericUnauthorizedException("User authentication required. Please log in again.");

		string cacheKey = $"menu_permissions_{userId}";

		if (_cache.TryGetValue(cacheKey, out IEnumerable<MenuDto> cachedMenus))
			return Ok(ApiResponseHelper.Success(cachedMenus, "Menus retrieved from cache"));

		var menusDtoTask = _serviceManager.Menus.SelectMenuByUserPermission(userId, trackChanges: false);

		// Add timeout to prevent long-running queries
		var completedTask = await Task.WhenAny(menusDtoTask, Task.Delay(5000));

		if (completedTask != menusDtoTask)
			throw new RequestTimeoutException("Request timeout while retrieving menu data");

		var menusDto = await menusDtoTask;

		if (!menusDto.Any())
			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found for this user."));

		// Cache the result
		var cacheOptions = new MemoryCacheEntryOptions()
			.SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
			.SetPriority(CacheItemPriority.High);

		_cache.Set(cacheKey, menusDto, cacheOptions);

		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
	}

	[HttpGet(RouteConstants.GetParentMenuByMenu)]
	public async Task<IActionResult> GetParentMenuByMenu(int parentMenuId)
	{
		var menusDto = await _serviceManager.Menus.GetParentMenuByMenu(parentMenuId, false);
		if (!menusDto.Any())
			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No parent menus found."));

		return Ok(ApiResponseHelper.Success(menusDto, "Parent menus retrieved successfully"));
	}

	[HttpGet(RouteConstants.GetMenus)]
	[ResponseCache(Duration = 60)] // Browser caching for 5 minutes
	public async Task<IActionResult> GetMenus()
	{
		IEnumerable<MenuDto> menusDto = await _serviceManager.Menus.GetMenusAsync(trackChanges: false);
		if (!menusDto.Any())
			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found."));
		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
	}

	[HttpGet(RouteConstants.MenusByModuleId)]
	[ResponseCache(Duration = 60)]
	public async Task<IActionResult> MenusByModuleId([FromRoute] int moduleId)
	{
		// No need for manual auth checks - [AuthorizeUser] at class level handles it
		// CurrentUser and CurrentUserId are available from BaseApiController

		var res = await _serviceManager.Menus.MenusByModuleId(moduleId, trackChanges: false);

		if (res == null || !res.Any())
			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found for this module."));

		return Ok(ApiResponseHelper.Success(res, "Data retrieved successfully"));
	}


	/// <summary>
	/// Get menu summary with pagination
	/// </summary>
	[HttpPost(RouteConstants.MenuSummary)]
	public async Task<IActionResult> GetMenuSummary([FromBody] CRMGridOptions options)
	{
		var res = await _serviceManager.Menus.MenuSummary(trackChanges: false, options);

		if (res == null || !res.Items.Any())
			return Ok(ApiResponseHelper.Success(new GridEntity<MenuDto>(), "No menus found."));

		// PaginationMetadata with response
		int totalPages = (int)Math.Ceiling(res.TotalCount / (double)options.pageSize);
		var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
		var links = ApiResponseHelper.GeneratePaginationLinks(baseUrl, options.page, totalPages, options.pageSize);

		return Ok(ApiResponseHelper.SuccessWithPagination(
				data: res,
				currentPage: options.page,
				pageSize: options.pageSize,
				totalCount: res.TotalCount,
				message: "Menu summary retrieved successfully",
				links: links
		));
	}


	[HttpPost(RouteConstants.CreateMenu)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> CreateMenu([FromBody] MenuDto modelDto)
	{
		var model = await _serviceManager.Menus.CreateAsync(modelDto);

		if (model.MenuId <= 0)
			throw new InvalidCreateOperationException("Failed to create menu record.");

		return Ok(ApiResponseHelper.Created(model, "Menu created successfully."));
	}


	[HttpPut(RouteConstants.UpdateMenu)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> UpdateMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
	{
		MenuDto returnData = await _serviceManager.Menus.UpdateAsync(key, modelDto);

		if (returnData.MenuId <= 0)
			throw new InvalidUpdateOperationException("Failed to update menu record.");

		return Ok(ApiResponseHelper.Updated(returnData, "Menu updated successfully."));
	}

	[HttpDelete(RouteConstants.DeleteMenu)]
	public async Task<IActionResult> DeleteMenu([FromRoute] int key)
	{
		await _serviceManager.Menus.DeleteAsync(key);
		return Ok(ApiResponseHelper.NoContent<object>("Menu deleted successfully"));
	}


	/// <summary>
	/// Get menus for dropdown list
	/// </summary>
	[HttpGet(RouteConstants.MenuForDDL)]
	public async Task<IActionResult> MenuForDDL()
	{
		var menusDto = await _serviceManager.Menus.MenuForDDL();

		if (!menusDto.Any())
			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuForDDLDto>(), "No menus found."));

		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
	}


}











//using bdDevCRM.Presentation.AuthorizeAttributes;
//using bdDevCRM.Presentation.Extensions;
//using bdDevCRM.ServicesContract;
//using bdDevCRM.Shared.ApiResponse;
//using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
//using bdDevCRM.Shared.Exceptions;
//using bdDevCRM.Utilities.Constants;
//using bdDevCRM.Utilities.CRMGrid.GRID;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.IdentityModel.Tokens;

//namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


///// <summary>
///// Menu management endpoints.
/////
///// [AuthorizeUser] at class-level ensures:
/////    - Every request validates user via attribute
/////    - CurrentUser / CurrentUserId available from BaseApiController
/////    - No auth checks needed in controller methods
/////    - Exceptions handled by StandardExceptionMiddleware
///// </summary>
//[AuthorizeUser]
//public class MenuController : BaseApiController
//{
//	private readonly IMemoryCache _cache;

//	public MenuController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
//	{
//		_cache = cache;
//	}

//	[Produces("application/json")]
//	[HttpGet(RouteConstants.SelectMenuByUserPermission)]
//	[ResponseCache(Duration = 300)] // Browser caching for 5 minutes
//	public async Task<IActionResult> SelectMenuByUserPermission()
//	{
//		var userId = CurrentUserId;
//		if (userId <= 0)
//			throw new GenericUnauthorizedException("User authentication required. Please log in again.");


//		string cacheKey = $"menu_permissions_{userId}";
//		if (_cache.TryGetValue(cacheKey, out IEnumerable<MenuDto> cachedMenus))
//			return Ok(ApiResponseHelper.Success(cachedMenus, "Menus retrieved from cache"));

//		var menusDtoTask = _serviceManager.Menus.SelectMenuByUserPermission(userId, trackChanges: false);
//		// Add timeout to prevent long-running queries
//		var completedTask = await Task.WhenAny(menusDtoTask, Task.Delay(5000));
//		if (completedTask != menusDtoTask)
//			throw new RequestTimeoutException("Request timeout while retrieving menu data");

//		var menusDto = await menusDtoTask;
//		if (!menusDto.Any())
//			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found for this user."));

//		// Cache the result
//		var cacheOptions = new MemoryCacheEntryOptions()
//			.SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
//			.SetPriority(CacheItemPriority.High);

//		_cache.Set(cacheKey, menusDto, cacheOptions);

//		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
//	}

//	[HttpGet(RouteConstants.GetParentMenuByMenu)]
//	public async Task<IActionResult> GetParentMenuByMenu(int parentMenuId)
//	{
//		var menusDto = await _serviceManager.Menus.GetParentMenuByMenu(parentMenuId, false);
//		if (!menusDto.Any())
//			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No parent menus found."));

//		return Ok(ApiResponseHelper.Success(menusDto, "Parent menus retrieved successfully"));
//	}

//	[HttpGet(RouteConstants.GetMenus)]
//	[ResponseCache(Duration = 60)] // Browser caching for 5 minutes
//	public async Task<IActionResult> GetMenus()
//	{
//		IEnumerable<MenuDto> menusDto = await _serviceManager.Menus.GetMenusAsync(trackChanges: false);
//		if (!menusDto.Any())
//			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found."));
//		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
//	}

//	[HttpGet(RouteConstants.MenusByModuleId)]
//	[ResponseCache(Duration = 60)]
//	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//	public async Task<IActionResult> MenusByModuleId([FromRoute] int moduleId)
//	{
//		var currentUser = HttpContext.GetCurrentUser();
//		var userId = HttpContext.GetUserId();

//		if (string.IsNullOrEmpty(userId.ToString()))
//			throw new GenericUnauthorizedException("User authentication required.");
//		if (currentUser == null)
//			throw new GenericUnauthorizedException("User session expired.");

//		//UsersDto currentUser2 = _serviceManager.GetCache<UsersDto>(userId);
//		//if (currentUser2 == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		var res = await _serviceManager.Menus.MenusByModuleId(moduleId, trackChanges: false);

//		if (res == null || !res.Any())
//			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuDto>(), "No menus found for this module."));

//		return Ok(ApiResponseHelper.Success(res, "Data retrieved successfully"));
//	}


//	/// <summary>
//	/// After login to system
//	/// </summary>
//	/// <param name="options"></param>
//	/// <returns></returns>
//	[HttpPost(RouteConstants.MenuSummary)]
//	public async Task<IActionResult> GetMenuSummary([FromBody] CRMGridOptions options)
//	{
//		//var currentUser = HttpContext.GetCurrentUser();
//		//var userId = HttpContext.GetUserId();

//		//if (string.IsNullOrEmpty(userId.ToString()))
//		//	throw new GenericUnauthorizedException("User authentication required.");
//		//if (currentUser == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		var res = await _serviceManager.Menus.MenuSummary(trackChanges: false, options);

//		if (res == null || !res.Items.Any())
//			return Ok(ApiResponseHelper.Success(new GridEntity<MenuDto>(), "No menus found."));

//		// PaginationMetadata with response
//		int totalPages = (int)Math.Ceiling(res.TotalCount / (double)options.pageSize);
//		var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
//		var links = ApiResponseHelper.GeneratePaginationLinks(
//				baseUrl, options.page, totalPages, options.pageSize);

//		return Ok(ApiResponseHelper.SuccessWithPagination(
//				data: res,
//				currentPage: options.page,
//				pageSize: options.pageSize,
//				totalCount: res.TotalCount,
//				message: "Menu summary retrieved successfully",
//				links: links
//		));
//	}


//	[HttpPost(RouteConstants.CreateMenu)]
//	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//	public async Task<IActionResult> CreateMenu([FromBody] MenuDto modelDto)
//	{
//		//var currentUser = HttpContext.GetCurrentUser();
//		//var userId = HttpContext.GetUserId();

//		//if (string.IsNullOrEmpty(userId.ToString()))
//		//	throw new GenericUnauthorizedException("User authentication required.");
//		//if (currentUser == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		var model = await _serviceManager.Menus.CreateAsync(modelDto);
//		if (model.MenuId <= 0)
//			throw new InvalidCreateOperationException("Failed to create record.");

//		return Ok(ApiResponseHelper.Created(model, "Menu created successfully."));
//	}


//	[HttpPut(RouteConstants.UpdateMenu)]
//	public async Task<IActionResult> UpdateMenu([FromRoute] int key, [FromBody] MenuDto modelDto)
//	{
//		//var currentUser = HttpContext.GetCurrentUser();
//		//var userId = HttpContext.GetUserId();

//		//if (string.IsNullOrEmpty(userId.ToString()))
//		//	throw new GenericUnauthorizedException("User authentication required.");
//		//if (currentUser == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		MenuDto returnData = await _serviceManager.Menus.UpdateAsync(key, modelDto);

//		if (returnData.MenuId <= 0)
//			throw new InvalidCreateOperationException("Failed to update record.");

//		return Ok(ApiResponseHelper.Updated(returnData, "Menu updated successfully."));
//	}

//	[HttpDelete(RouteConstants.DeleteMenu)]
//	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//	public async Task<IActionResult> DeleteMenu([FromRoute] int key)
//	{
//		//var currentUser = HttpContext.GetCurrentUser();
//		//var userId = HttpContext.GetUserId();

//		//if (string.IsNullOrEmpty(userId.ToString()))
//		//	throw new GenericUnauthorizedException("User authentication required.");
//		//if (currentUser == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		await _serviceManager.Menus.DeleteAsync(key);
//		return Ok(ApiResponseHelper.Success("Menu deleted successfully.", null));
//	}


//	/// <summary>
//	/// menus for Dropdown list
//	/// </summary>
//	[HttpGet(RouteConstants.MenuForDDL)]
//	public async Task<IActionResult> MenuForDDL()
//	{
//		//var currentUser = HttpContext.GetCurrentUser();
//		//var userId = HttpContext.GetUserId();

//		//if (string.IsNullOrEmpty(userId.ToString()))
//		//	throw new GenericUnauthorizedException("User authentication required.");
//		//if (currentUser == null)
//		//	throw new GenericUnauthorizedException("User session expired.");

//		var menusDto = await _serviceManager.Menus.MenuForDDL();
//		if (!menusDto.Any())
//			return Ok(ApiResponseHelper.Success(Enumerable.Empty<MenuForDDLDto>(), "No menus found."));

//		return Ok(ApiResponseHelper.Success(menusDto, "Menus retrieved successfully"));
//	}


//}
