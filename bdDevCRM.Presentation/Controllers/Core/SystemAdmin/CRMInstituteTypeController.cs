using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing CRM institute types
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class CRMInstituteTypeController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public CRMInstituteTypeController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = svc;
    _cache = cache;
  }

  // 1️⃣ DDL ------------------------------------------------------------
  /// <summary>
  /// Retrieves all institute types for dropdown list
  /// </summary>
  /// <returns>List of institute types for dropdown</returns>
  [HttpGet(RouteConstants.InstituteTypeDDL)]
  public async Task<IActionResult> InstituteTypeDDL()
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Execute business logic
    var res = await _serviceManager.CrmInstituteTypes.GetInstituteTypesDDLAsync(trackChanges: false);

    // Return standardized response
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CRMInstituteTypeDto>>("No institute type found"));

    return Ok(ResponseHelper.Success(res, "Institute type retrieved successfully"));
  }

  // 2️⃣ Summary Grid --------------------------------------------------
  /// <summary>
  /// Retrieves paginated summary grid of institute types
  /// </summary>
  /// <param name="options">Grid options for pagination, sorting, and filtering</param>
  /// <returns>Paginated grid of institute types</returns>
  [HttpPost(RouteConstants.InstituteTypeSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (options == null)
      throw new NullModelBadRequestException("CRMGridOptions cannot be null");

    // Execute business logic
    var summary = await _serviceManager.CrmInstituteTypes.SummaryGrid(options);

    // Return standardized response
    if (summary == null || !summary.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CRMInstituteTypeDto>>("No data found"));

    return Ok(ResponseHelper.Success(summary, "Data retrieved successfully"));
  }

  // 3️⃣ Create --------------------------------------------------------
  /// <summary>
  /// Creates a new institute type record
  /// </summary>
  /// <param name="dto">Institute type data to create</param>
  /// <returns>Operation result message</returns>
  [HttpPost(RouteConstants.CreateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Create([FromBody] CRMInstituteTypeDto dto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (dto == null)
      throw new NullModelBadRequestException("Institute type data cannot be null");

    // Execute business logic
    var res = await _serviceManager.CrmInstituteTypes.CreateNewRecordAsync(dto);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Created(res, "Institute type created successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  // 4️⃣ Update --------------------------------------------------------
  /// <summary>
  /// Updates an existing institute type record
  /// </summary>
  /// <param name="key">Institute type ID</param>
  /// <param name="dto">Updated institute type data</param>
  /// <returns>Operation result message</returns>
  [HttpPut(RouteConstants.UpdateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Update([FromRoute] int key, [FromBody] CRMInstituteTypeDto dto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (key <= 0)
      throw new IdParametersBadRequestException();

    if (dto == null)
      throw new NullModelBadRequestException("Institute type data cannot be null");

    // Execute business logic
    var res = await _serviceManager.CrmInstituteTypes.UpdateRecordAsync(key, dto, false);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Updated(res, "Institute type updated successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  // 5️⃣ Delete --------------------------------------------------------
  /// <summary>
  /// Deletes an institute type record
  /// </summary>
  /// <param name="key">Institute type ID to delete</param>
  /// <param name="dto">Institute type data for validation</param>
  /// <returns>Operation result message</returns>
  [HttpDelete(RouteConstants.DeleteInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Delete([FromRoute] int key, [FromBody] CRMInstituteTypeDto dto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (key <= 0)
      throw new IdParametersBadRequestException();

    if (dto == null)
      throw new NullModelBadRequestException("Institute type data cannot be null");

    // Execute business logic
    var res = await _serviceManager.CrmInstituteTypes.DeleteRecordAsync(key, dto);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Institute type deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  // 6️⃣ SaveOrUpdate --------------------------------------------------
  /// <summary>
  /// Creates or updates an institute type record
  /// </summary>
  /// <param name="key">Institute type ID (0 for create, >0 for update)</param>
  /// <param name="dto">Institute type data</param>
  /// <returns>Operation result message</returns>
  [HttpPost(RouteConstants.CreateOrUpdateInstituteType)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key,
                                                [FromBody] CRMInstituteTypeDto dto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (dto == null)
      throw new NullModelBadRequestException("Institute type data cannot be null");

    // Execute business logic
    var res = await _serviceManager.CrmInstituteTypes.SaveOrUpdateAsync(key, dto);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Institute type saved successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }
}
