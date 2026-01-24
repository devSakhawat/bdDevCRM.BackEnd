
//using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.Presentation.Extensions;
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

/// <summary>
/// Controller for managing application modules
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] //Controller-level authentication
public class ModuleController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public ModuleController(IServiceManager serviceManager, IMemoryCache cache)
        : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves paginated summary grid of modules
    /// </summary>
    /// <param name="options">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of modules</returns>
    [HttpPost(RouteConstants.ModuleSummary)]
    public async Task<IActionResult> ModuleSummary([FromBody] CRMGridOptions options)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (options == null)
            throw new NullModelBadRequestException("Grid options cannot be null");

        // Execute business logic
        var moduleSummary = await _serviceManager.Modules.ModuleSummary(
            false, options);

        // Return standardized response
        if (moduleSummary == null || !moduleSummary.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<ModuleDto>>(
                "No modules found"));

        return Ok(ResponseHelper.Success(moduleSummary,
            "Modules retrieved successfully"));
    }

    /// <summary>
    /// Retrieves all modules accessible by the current user
    /// </summary>
    /// <returns>List of modules</returns>
    [HttpGet(RouteConstants.Modules)]
    [ResponseCache(Duration = 60)] // Browser caching for 1 minute
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetModulesAsync()
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate menu constant
        if (!MenuConstant.TryGetPath("CRMApplication", out var menuPath))
            throw new GenericBadRequestException("Invalid menu name.");

        // Execute business logic
        var res = await _serviceManager.Modules.GetModulesAsync(currentUser, false);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<ModuleDto>>("No modules found"));

        return Ok(ResponseHelper.Success(res, "Modules retrieved successfully"));
    }

    /// <summary>
    /// Creates a new module
    /// </summary>
    /// <param name="moduleDto">Module data to create</param>
    /// <returns>Created module record</returns>
    [HttpPost(RouteConstants.CreateModule)]
    public async Task<IActionResult> SaveModule([FromBody] ModuleDto moduleDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (moduleDto == null)
            throw new NullModelBadRequestException("Module data cannot be null");

        // Execute business logic
        var module = await _serviceManager.Modules.CreateModuleAsync(moduleDto);

        // Return standardized response
        if (module == null || module.ModuleId <= 0)
            throw new InvalidCreateOperationException("Failed to create module");

        return Ok(ResponseHelper.Created(module,
            "Module created successfully"));
    }

    /// <summary>
    /// Updates an existing module
    /// </summary>
    /// <param name="key">Module ID</param>
    /// <param name="moduleDto">Updated module data</param>
    /// <returns>Updated module record</returns>
    [HttpPut(RouteConstants.UpdateModule)]
    public async Task<IActionResult> UpdateModule(
        [FromRoute] int key,
        [FromBody] ModuleDto moduleDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new IdParametersBadRequestException();

        if (moduleDto == null)
            throw new NullModelBadRequestException("Module data cannot be null");

        // Execute business logic
        var returnData = await _serviceManager.Modules.UpdateModuleAsync(
            key, moduleDto);

        // Return standardized response
        if (returnData == null)
            throw new InvalidUpdateOperationException("Failed to update module");

        return Ok(ResponseHelper.Updated(returnData,
            "Module updated successfully"));
    }

    /// <summary>
    /// Deletes a module
    /// </summary>
    /// <param name="key">Module ID to delete</param>
    /// <param name="moduleDto">Module data for validation</param>
    /// <returns>Operation result message</returns>
    [HttpDelete(RouteConstants.DeleteModule)]
    public async Task<IActionResult> DeleteModule([FromRoute] int key)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new IdParametersBadRequestException();

        // Execute business logic
        await _serviceManager.Modules.DeleteModuleAsync(key);

        // Return standardized response
        return Ok(ResponseHelper.Success<string?>(null,
            "Module deleted successfully"));
    }

}