using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing CRM intake years
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class CrmIntakeYearController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public CrmIntakeYearController(IServiceManager serviceManager, IMemoryCache cache) 
        : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves all intake years for dropdown list
    /// </summary>
    /// <returns>List of intake years for dropdown</returns>
    [HttpGet(RouteConstants.IntakeYearDDL)]
    public async Task<IActionResult> GetIntakeYearsDDL()
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var intakeYears = await _serviceManager.CrmIntakeYears
            .GetIntakeYearsDDLAsync(trackChanges: false);

        // Return standardized response
        if (intakeYears == null || !intakeYears.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<CrmIntakeYearDDL>>(
                "No intake years found"));

        return Ok(ResponseHelper.Success(intakeYears, 
            "Intake years retrieved successfully"));
    }

    /// <summary>
    /// Retrieves paginated summary grid of intake years
    /// </summary>
    /// <param name="options">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of intake years</returns>
    [HttpPost(RouteConstants.IntakeYearSummary)]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> GetSummaryGrid([FromBody] CRMGridOptions options)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (options == null)
            throw new NullModelBadRequestException("CRMGridOptions cannot be null");

        // Execute business logic
        var grid = await _serviceManager.CrmIntakeYears.SummaryGrid(
            trackChanges: false, options, currentUser);

        // Return standardized response
        if (grid == null || !grid.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<CrmIntakeYearDto>>(
                "No intake years found"));

        return Ok(ResponseHelper.Success(grid, 
            "Intake years grid retrieved successfully"));
    }

    /// <summary>
    /// Retrieves a specific intake year by ID
    /// </summary>
    /// <param name="key">Intake year ID</param>
    /// <returns>Intake year details</returns>
    [HttpGet(RouteConstants.IntakeYearByKey)]
    public async Task<IActionResult> GetIntakeYear([FromQuery] int key)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid intake year ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        try
        {
            // Execute business logic
            var intakeYear = await _serviceManager.CrmIntakeYears
                .GetIntakeYearAsync(key, trackChanges: false);

            // Return standardized response
            return Ok(ResponseHelper.Success(intakeYear, 
                "Intake year retrieved successfully"));
        }
        catch (GenericNotFoundException)
        {
            throw new GenericNotFoundException("IntakeYear", "ID", key.ToString());
        }
    }

    /// <summary>
    /// Creates a new intake year record
    /// </summary>
    /// <param name="intakeYearDto">Intake year data to create</param>
    /// <returns>Created intake year record</returns>
    [HttpPost(RouteConstants.CreateIntakeYear)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> CreateIntakeYear(
        [FromBody] CrmIntakeYearDto intakeYearDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (intakeYearDto == null)
            throw new NullModelBadRequestException("Intake year data cannot be null");

        // Execute business logic
        var createdIntakeYear = await _serviceManager.CrmIntakeYears
            .CreateIntakeYearAsync(intakeYearDto);

        // Validate result
        if (createdIntakeYear.IntakeYearId <= 0)
            throw new InvalidCreateOperationException(
                "Failed to create intake year record.");

        // Return standardized response
        return Ok(ResponseHelper.Created(createdIntakeYear, 
            "Intake year created successfully"));
    }

    /// <summary>
    /// Creates or updates an intake year record
    /// </summary>
    /// <param name="key">Intake year ID (0 for create, >0 for update)</param>
    /// <param name="intakeYearDto">Intake year data</param>
    /// <returns>Operation result message</returns>
    [HttpPost(RouteConstants.CreateOrUpdateIntakeYear)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> SaveOrUpdate(
        int key, 
        [FromBody] CrmIntakeYearDto intakeYearDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (intakeYearDto == null)
            throw new NullModelBadRequestException("Intake year data cannot be null");

        // Execute business logic
        var result = await _serviceManager.CrmIntakeYears
            .SaveOrUpdate(key, intakeYearDto);

        // Return standardized response
        return Ok(ResponseHelper.Success(intakeYearDto, result));
    }

    /// <summary>
    /// Updates an existing intake year record
    /// </summary>
    /// <param name="key">Intake year ID</param>
    /// <param name="intakeYearDto">Updated intake year data</param>
    /// <returns>Operation result message</returns>
    [HttpPut(RouteConstants.UpdateIntakeYear)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> UpdateIntakeYear(
        int key, 
        [FromBody] CrmIntakeYearDto intakeYearDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid intake year ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (intakeYearDto == null)
            throw new NullModelBadRequestException("Intake year data cannot be null");

        // Execute business logic
        var result = await _serviceManager.CrmIntakeYears
            .UpdateNewRecordAsync(key, intakeYearDto, trackChanges: true);

        // Return standardized response
        return Ok(ResponseHelper.Updated(intakeYearDto, result));
    }

    /// <summary>
    /// Deletes an intake year record
    /// </summary>
    /// <param name="key">Intake year ID to delete</param>
    /// <returns>Operation result message</returns>
    [HttpDelete(RouteConstants.DeleteIntakeYear)]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> DeleteIntakeYear(int key)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid intake year ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        try
        {
            // Load record before deletion
            var intakeYear = await _serviceManager.CrmIntakeYears
                .GetIntakeYearAsync(key, trackChanges: false);

            // Execute deletion
            var result = await _serviceManager.CrmIntakeYears
                .DeleteRecordAsync(key, intakeYear);

            // Return standardized response
            return Ok(ResponseHelper.Success<string?>(null, result));
        }
        catch (GenericNotFoundException)
        {
            throw new GenericNotFoundException("IntakeYear", "ID", key.ToString());
        }
    }
}