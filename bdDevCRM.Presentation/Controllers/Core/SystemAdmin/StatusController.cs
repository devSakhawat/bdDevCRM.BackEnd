using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing workflow statuses and actions
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] //Controller-level authentication
public class StatusController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public StatusController(IServiceManager serviceManager, IMemoryCache cache) 
        : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves workflow statuses by menu ID
    /// </summary>
    /// <param name="menuId">Menu ID</param>
    /// <returns>List of workflow statuses</returns>
    [HttpGet(RouteConstants.StatusByMenuId)]
    public async Task<IActionResult> StatusByMenuId([FromQuery] int menuId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (menuId <= 0)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var groupPermissions = await _serviceManager.WfState.StatusByMenuId(
            menuId, trackChanges: false);

        // Return standardized response
        if (groupPermissions == null || !groupPermissions.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<WfStateDto>>(
                "No statuses found for this menu"));

        return Ok(ResponseHelper.Success(groupPermissions, 
            "Statuses retrieved successfully"));
    }

    /// <summary>
    /// Retrieves workflow actions by status ID for group
    /// </summary>
    /// <param name="statusId">Status ID</param>
    /// <returns>List of workflow actions</returns>
    [HttpGet(RouteConstants.ActionsByStatusIdForGroup)]
    public async Task<IActionResult> ActionsByStatusIdForGroup([FromQuery] int statusId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (statusId <= 0)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var groupPermissions = await _serviceManager.WfState
            .ActionsByStatusIdForGroup(statusId, trackChanges: false);

        // Return standardized response
        if (groupPermissions == null || !groupPermissions.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<WfActionDto>>(
                "No actions found for this status"));

        return Ok(ResponseHelper.Success(groupPermissions, 
            "Actions retrieved successfully"));
    }

    #region WorkFlow Management

    /// <summary>
    /// Retrieves paginated workflow summary grid
    /// </summary>
    /// <param name="options">Grid options</param>
    /// <returns>Paginated workflow grid</returns>
    [HttpPost(RouteConstants.WorkFlowSummary)]
    public async Task<IActionResult> GetWorkFlowSummary([FromBody] CRMGridOptions options)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (options == null)
            throw new NullModelBadRequestException("Grid options cannot be null");

        // Execute business logic
        var summaryGrid = await _serviceManager.WfState.WorkflowSummary(
            trackChanges: false, options);

        // Return standardized response
        if (summaryGrid == null || !summaryGrid.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<WfStateDto>>(
                "No workflow data found"));

        return Ok(ResponseHelper.Success(summaryGrid, 
            "Workflow data retrieved successfully"));
    }

    /// <summary>
    /// Creates a new workflow status
    /// </summary>
    /// <param name="modelDto">Workflow status data</param>
    /// <returns>Created workflow status</returns>
    [HttpPost(RouteConstants.CreateWorkFlow)]
    [RequestSizeLimit(1_000_000)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> CreateWorkFlow([FromBody] WfStateDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (modelDto == null)
            return BadRequest(ResponseHelper.BadRequest("Status data is required"));

        try
        {
            // Execute business logic
            WfStateDto res = await _serviceManager.WfState
                .CreateNewRecordAsync(modelDto, currentUser);

            // Return standardized response
            if (res.WfStateId > 0)
                return Ok(ResponseHelper.Created(res, 
                    "Workflow status created successfully"));
            else
                return StatusCode(500, ResponseHelper.InternalServerError(
                    "Failed to create workflow status"));
        }
        catch (System.Text.Json.JsonException)
        {
            return BadRequest(ResponseHelper.BadRequest(
                "Invalid JSON format in workflow data"));
        }
    }

    /// <summary>
    /// Updates an existing workflow status
    /// </summary>
    /// <param name="key">Workflow status ID</param>
    /// <param name="modelDto">Updated workflow status data</param>
    /// <returns>Operation result</returns>
    [HttpPut(RouteConstants.UpdateWorkFlow)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> UpdateWorkFlow(
        [FromRoute] int key, 
        [FromBody] WfStateDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new IdParametersBadRequestException();

        if (modelDto == null)
            throw new NullModelBadRequestException("Workflow status data cannot be null");

        // Execute business logic
        var res = await _serviceManager.WfState.UpdateRecordAsync(
            key, modelDto, false, currentUser);

        // Return standardized response
        if (res == OperationMessage.Success)
            return Ok(ResponseHelper.Success(res, 
                "Workflow status updated successfully"));
        else
            return Conflict(ResponseHelper.Conflict(res));
    }

    /// <summary>
    /// Deletes a workflow status
    /// </summary>
    /// <param name="key">Workflow status ID</param>
    /// <param name="modelDto">Workflow status data</param>
    /// <returns>Operation result</returns>
    [HttpDelete(RouteConstants.DeleteWorkFlow)]
    public async Task<IActionResult> DeleteWorkFlow(
        [FromRoute] int key, 
        [FromBody] WfStateDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0 || key != modelDto.WfStateId)
            throw new BadRequestException("Valid workflow status ID is required");

        // Execute business logic
        string result = await _serviceManager.WfState.DeleteWorkflow(key);

        // Return standardized response
        return Ok(ResponseHelper.Success(result, 
            "Workflow status deleted successfully"));
    }

    #endregion WorkFlow Management

    #region Workflow Actions

    /// <summary>
    /// Creates a new workflow action
    /// </summary>
    /// <param name="modelDto">Workflow action data</param>
    /// <returns>Created workflow action</returns>
    [HttpPost(RouteConstants.CreateAction)]
    [RequestSizeLimit(1_000_000)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> CreateAction([FromBody] WfActionDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (modelDto == null)
            throw new NullModelBadRequestException("Action data cannot be null");

        modelDto.WfActionId = 0;

        // Execute business logic
        WfActionDto result = await _serviceManager.WfState
            .CreateWfActionNewRecordAsync(modelDto, currentUser, false);

        // Return standardized response
        return Ok(ResponseHelper.Created(result, 
            "Workflow action created successfully"));
    }

    /// <summary>
    /// Updates an existing workflow action
    /// </summary>
    /// <param name="key">Workflow action ID</param>
    /// <param name="modelDto">Updated workflow action data</param>
    /// <returns>Operation result</returns>
    [HttpPut(RouteConstants.UpdateAction)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> UpdateAction(
        [FromRoute] int key, 
        [FromBody] WfActionDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new BadRequestException("Valid action ID is required");

        if (modelDto == null)
            throw new NullModelBadRequestException("Action data cannot be null");

        modelDto.WfActionId = key;

        // Execute business logic
        string result = await _serviceManager.WfState
            .UpdateWfActionRecordAsync(key, modelDto, currentUser, false);

        // Return standardized response
        if (result == OperationMessage.Success)
            return Ok(ResponseHelper.Updated(result, 
                "Workflow action updated successfully"));
        else
            throw new GenericConflictException(result);
    }

    /// <summary>
    /// Deletes a workflow action
    /// </summary>
    /// <param name="key">Workflow action ID</param>
    /// <param name="modelDto">Workflow action data</param>
    /// <returns>Operation result</returns>
    [HttpDelete(RouteConstants.DeleteAction)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> DeleteAction(
        [FromRoute] int key, 
        [FromBody] WfActionDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new BadRequestException("Valid action ID is required");

        if (modelDto == null)
            throw new NullModelBadRequestException("Action data cannot be null");

        modelDto.WfActionId = key;

        // Execute business logic
        string result = await _serviceManager.WfState.DeleteAction(key, modelDto);

        // Return standardized response
        return Ok(ResponseHelper.Success(result, 
            "Workflow action deleted successfully"));
    }

    /// <summary>
    /// Retrieves paginated action summary by status ID
    /// </summary>
    /// <param name="options">Grid options</param>
    /// <param name="stateId">State ID</param>
    /// <returns>Paginated action grid</returns>
    [HttpPost(RouteConstants.GetActionSummaryByStatusId)]
    public async Task<IActionResult> GetActionByStatusId(
        [FromBody] CRMGridOptions options, 
        [FromQuery] int stateId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (stateId <= 0)
            return BadRequest(ResponseHelper.BadRequest("Valid state ID is required"));

        if (options == null)
            return BadRequest(ResponseHelper.BadRequest("Grid options are required"));

        // Execute business logic
        var res = await _serviceManager.WfState.GetActionByStatusId(stateId, options);

        // Return standardized response
        if (res != null && res.Items.Any())
            return Ok(ResponseHelper.Success(res, 
                "Workflow actions retrieved successfully"));
        else
            return Ok(ResponseHelper.NoContent<GridEntity<WfActionDto>>(
                "No actions found for this status"));
    }

    #endregion Workflow Actions

    #region Next States

    /// <summary>
    /// Retrieves next states by menu ID
    /// </summary>
    /// <param name="menuId">Menu ID</param>
    /// <returns>List of next states</returns>
    [HttpGet(RouteConstants.GetNextStatesByMenu)]
    public async Task<IActionResult> GetNextStatesByMenu([FromQuery] int menuId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (menuId <= 0)
            return BadRequest(ResponseHelper.BadRequest("Valid menu ID is required"));

        // Execute business logic
        var res = await _serviceManager.WfState.GetNextStatesByMenu(menuId);

        // Return standardized response
        if (res != null && res.Any())
            return Ok(ResponseHelper.Success(res, 
                "Next states retrieved successfully"));
        else
            return Ok(ResponseHelper.NoContent<IEnumerable<WfStateDto>>(
                "No next states found for this menu"));
    }

    #endregion Next States

    #region Status By Menu and User

    /// <summary>
    /// Retrieves workflow statuses by menu and current user
    /// </summary>
    /// <returns>List of workflow statuses</returns>
    [HttpGet(RouteConstants.StatusByMenuNUserId)]
    public async Task<IActionResult> StatusByMenuNUserId()
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (!currentUser.UserId.HasValue)
            throw new GenericBadRequestException("Valid UserId is required.");

        // Get menu information
        var menu = await ManageMenu.GetAsync(this, _serviceManager);

        // Validate menu data
        if (!menu.MenuId.HasValue)
            throw new GenericBadRequestException("Valid MenuId is required.");

        // Execute business logic
        var res = await _serviceManager.WfState.GetWFStateByUserPermission(
            menu.MenuId.Value, currentUser.UserId.Value);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>(
                "No workflow statuses found for this menu and user"));

        return Ok(ResponseHelper.Success(res, 
            "Workflow statuses retrieved successfully"));
    }

    /// <summary>
    /// Retrieves workflow statuses by menu name
    /// </summary>
    /// <param name="menuName">Menu name</param>
    /// <returns>List of workflow statuses</returns>
    [HttpGet(RouteConstants.StatusByMenuName)]
    public async Task<IActionResult> StatusByMenuName([FromBody] string menuName)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (string.IsNullOrWhiteSpace(menuName))
            throw new GenericBadRequestException("Menu name is required.");

        // Validate user data
        if (!currentUser.UserId.HasValue)
            throw new GenericBadRequestException("Valid UserId is required.");

        // Get menu information
        var menu = await ManageMenu.CheckByMenuName(this, menuName, _serviceManager);

        // Validate menu data
        if (!menu.MenuId.HasValue)
            throw new GenericBadRequestException("Valid MenuId is required.");

        // Execute business logic
        var res = await _serviceManager.WfState.GetWFStateByUserPermission(
            menu.MenuId.Value, currentUser.UserId.Value);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>(
                "No workflow statuses found for this menu"));

        return Ok(ResponseHelper.Success(res, 
            "Workflow statuses retrieved successfully"));
    }

    #endregion Status By Menu and User
}
