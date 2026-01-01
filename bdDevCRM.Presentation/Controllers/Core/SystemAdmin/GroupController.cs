using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Common;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing user groups and permissions
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] //Controller-level authentication
public class GroupController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public GroupController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves paginated summary grid of groups
    /// </summary>
    /// <param name="options">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of groups</returns>
    [HttpPost(RouteConstants.GroupSummary)]
    public async Task<IActionResult> GroupSummary([FromBody] CRMGridOptions options)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (options == null)
            throw new NullModelBadRequestException("Grid options cannot be null");

        // Execute business logic
        var groupSummary = await _serviceManager.Groups.GroupSummary(
            trackChanges: false, options);

        // Return standardized response
        if (groupSummary == null || !groupSummary.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<GroupSummaryDto>>(
                "No groups found"));

        return Ok(ResponseHelper.Success(groupSummary, 
            "Groups retrieved successfully"));
    }

    /// <summary>
    /// Retrieves all permissions for a specific group
    /// </summary>
    /// <param name="groupId">Group ID</param>
    /// <returns>List of group permissions</returns>
    [HttpGet(RouteConstants.GroupPermisionsbyGroupId)]
    public async Task<IActionResult> GroupPermisionsbyGroupId([FromRoute] int groupId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (groupId <= 0)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var res = await _serviceManager.Groups.GroupPermisionsbyGroupId(groupId);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GroupPermissionDto>>(
                "No permissions found for the specified group"));

        return Ok(ResponseHelper.Success(res, 
            "Group permissions retrieved successfully"));
    }

    /// <summary>
    /// Retrieves all available access controls
    /// </summary>
    /// <returns>List of access controls</returns>
    [HttpGet(RouteConstants.GetAccesses)]
    public async Task<IActionResult> GetAccesses()
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Execute business logic
        var accessControls = await _serviceManager.Groups.GetAccesses();

        // Return standardized response
        if (accessControls == null || !accessControls.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<AccessControlDto>>(
                "No access controls found"));

        return Ok(ResponseHelper.Success(accessControls, 
            "Access controls retrieved successfully"));
    }

    /// <summary>
    /// Creates a new group
    /// </summary>
    /// <param name="objGroup">Group data to create</param>
    /// <returns>Created group record</returns>
    [HttpPost(RouteConstants.CreateGroup)]
    public async Task<IActionResult> SaveGroup([FromBody] GroupDto objGroup)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (objGroup == null)
            throw new ArgumentNullException(nameof(objGroup));

        // Execute business logic
        var model = await _serviceManager.Groups.CreateAsync(objGroup);

        // Return standardized response
        if (model == null || model.GroupId <= 0)
            throw new InvalidCreateOperationException("Failed to create group");

        return Ok(ResponseHelper.Created(model, 
            "Group created successfully"));
    }

    /// <summary>
    /// Updates an existing group
    /// </summary>
    /// <param name="key">Group ID</param>
    /// <param name="modelDto">Updated group data</param>
    /// <returns>Updated group record</returns>
    [HttpPut(RouteConstants.UpdateGroup)]
    public async Task<IActionResult> UpdateGroup(
        [FromRoute] int key, 
        [FromBody] GroupDto modelDto)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new IdParametersBadRequestException();

        if (modelDto == null)
            throw new NullModelBadRequestException("Group data cannot be null");

        // Execute business logic
        var returnData = await _serviceManager.Groups.UpdateAsync(key, modelDto);

        // Return standardized response
        if (returnData == null)
            throw new InvalidUpdateOperationException("Failed to update group");

        return Ok(ResponseHelper.Updated(returnData, 
            "Group updated successfully"));
    }

    /// <summary>
    /// Retrieves all groups for user settings
    /// </summary>
    /// <returns>List of groups</returns>
    [HttpGet(RouteConstants.Groups)]
    public async Task<IActionResult> GetGroups()
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Execute business logic
        var groupForUserSettings = await _serviceManager.Groups.GetGroups(
            trackChanges: false);

        // Return standardized response
        if (groupForUserSettings == null || !groupForUserSettings.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GroupForUserSettings>>(
                "No groups found"));

        return Ok(ResponseHelper.Success(groupForUserSettings.ToList(), 
            "Groups retrieved successfully"));
    }

    /// <summary>
    /// Retrieves group memberships for a specific user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of group memberships</returns>
    [HttpGet(RouteConstants.GroupMemberByUserId)]
    public async Task<IActionResult> GroupMemberByUserId([FromQuery] int userId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var currentUserId = HttpContext.GetUserId();

        // Validate input parameters
        if (userId <= 0)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var groupForUserSettings = await _serviceManager.Groups
            .GroupMemberByUserId(userId, trackChanges: false);

        // Return standardized response
        if (groupForUserSettings == null || !groupForUserSettings.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GroupMemberDto>>(
                "No group memberships found for this user"));

        return Ok(ResponseHelper.Success(groupForUserSettings.ToList(), 
            "Group memberships retrieved successfully"));
    }

    /// <summary>
    /// Retrieves access permissions for the current user
    /// </summary>
    /// <param name="model">Common property model</param>
    /// <returns>List of permissions</returns>
    [HttpPost(RouteConstants.GetAccessPermisionForCurrentUser)]
    public async Task<IActionResult> GetAccessPermisionForCurrentUser([FromBody] CommonProperty model)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (model == null)
            throw new NullModelBadRequestException(nameof(CommonProperty));

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var groupPermissions = await _serviceManager.Groups
            .GetAccessPermisionForCurrentUser(31, userId);

        // Return standardized response
        if (groupPermissions == null || !groupPermissions.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<GroupPermissionDto>>(
                "No group permissions found"));

        return Ok(ResponseHelper.Success(groupPermissions.ToList(), 
            "Group permissions retrieved successfully"));
    }
}