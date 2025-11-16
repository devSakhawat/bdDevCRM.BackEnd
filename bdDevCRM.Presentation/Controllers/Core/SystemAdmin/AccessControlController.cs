using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing access controls
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class AccessControlController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public AccessControlController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  /// <summary>
  /// Creates a new access control record
  /// </summary>
  /// <param name="modelDto">Access control data to create</param>
  /// <returns>Created access control record</returns>
  [HttpPost(RouteConstants.CreateAccessControl)]
  public async Task<IActionResult> SaveAccessControl([FromBody] AccessControlDto modelDto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (modelDto == null)
      throw new NullModelBadRequestException("Access control data cannot be null");

    // Execute business logic
    var model = await _serviceManager.AccessControl.CreateAsync(modelDto);

    // Validate result
    if (model.AccessId <= 0)
      throw new InvalidCreateOperationException("Failed to create new record.");

    // Return standardized response
    return Ok(ResponseHelper.Created(model, "New record created successfully."));
  }

  /// <summary>
  /// Updates an existing access control record
  /// </summary>
  /// <param name="key">Access control ID</param>
  /// <param name="modelDto">Updated access control data</param>
  /// <returns>Updated access control record</returns>
  [HttpPut(RouteConstants.UpdateAccessControl)]
  public async Task<IActionResult> UpdateAccessControl([FromRoute] int key, [FromBody] AccessControlDto modelDto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (key <= 0)
      throw new IdParametersBadRequestException();

    if (modelDto == null)
      throw new NullModelBadRequestException("Access control data cannot be null");

    // Execute business logic
    AccessControlDto returnData = await _serviceManager.AccessControl.UpdateAsync(key, modelDto);

    // Validate result
    if (returnData.AccessId <= 0)
      throw new InvalidUpdateOperationException("Failed to update record.");

    // Return standardized response
    return Ok(ResponseHelper.Updated(returnData, "Record updated successfully."));
  }

  /// <summary>
  /// Retrieves paginated summary grid of access controls
  /// </summary>
  /// <param name="options">Grid options for pagination, sorting, and filtering</param>
  /// <returns>Paginated grid of access controls</returns>
  [HttpPost(RouteConstants.AccessControlSummary)]
  public async Task<IActionResult> AccessControlSummary([FromBody] CRMGridOptions options)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate user data
    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    // Validate input parameters
    if (options == null)
      throw new NullModelBadRequestException("Grid options cannot be null");

    // Execute business logic
    var summaryGrid = await _serviceManager.AccessControl.AccessControlSummary(trackChanges: false, options);

    // Return standardized response
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<AccessControlDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }
}