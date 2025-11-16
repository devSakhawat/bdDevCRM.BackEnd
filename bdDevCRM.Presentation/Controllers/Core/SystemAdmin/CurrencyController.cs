using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
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
/// Controller for managing currencies
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class CurrencyController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public CurrencyController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  /// <summary>
  /// Retrieves all currencies for dropdown list
  /// </summary>
  /// <returns>List of currencies for dropdown</returns>
  [HttpGet(RouteConstants.CurrencyDDL)]
  public async Task<IActionResult> CurrencyDDL()
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Execute business logic
    var res = await _serviceManager.Currencies.GetCurrenciesDDLAsync();

    // Return standardized response
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CurrencyDDL>>("No currency found"));

    return Ok(ResponseHelper.Success(res, "Currency retrieved successfully"));
  }

  /// <summary>
  /// Retrieves paginated summary grid of currencies
  /// </summary>
  /// <param name="options">Grid options for pagination, sorting, and filtering</param>
  /// <returns>Paginated grid of currencies</returns>
  [HttpPost(RouteConstants.CurrencySummary)]
  public async Task<IActionResult> CurrencySummary([FromBody] CRMGridOptions options)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (options == null)
      throw new NullModelBadRequestException("Grid options cannot be null");

    // Execute business logic
    var currencySummary = await _serviceManager.Currencies.CurrecySummary(options);

    // Return standardized response
    if (currencySummary == null || !currencySummary.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CurrencyDto>>("No data found"));

    return Ok(ResponseHelper.Success(currencySummary, "Data retrieved successfully"));
  }

  /// <summary>
  /// Creates or updates a currency record
  /// </summary>
  /// <param name="key">Currency ID (0 for create, >0 for update)</param>
  /// <param name="modelDto">Currency data</param>
  /// <returns>Operation result message</returns>
  [HttpPost(RouteConstants.CreateOrUpdateCurrency)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CurrencyDto modelDto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (modelDto == null)
      throw new NullModelBadRequestException("Currency data cannot be null");

    // Execute business logic
    var res = await _serviceManager.Currencies.SaveOrUpdate(key, modelDto);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Currency saved successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  /// <summary>
  /// Deletes a currency record
  /// </summary>
  /// <param name="key">Currency ID to delete</param>
  /// <param name="modelDto">Currency data for validation</param>
  /// <returns>Operation result message</returns>
  [HttpDelete(RouteConstants.DeleteCurrency)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteCurrency([FromRoute] int key, [FromBody] CurrencyDto modelDto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (key <= 0)
      throw new IdParametersBadRequestException();

    if (modelDto == null)
      throw new NullModelBadRequestException("Currency data cannot be null");

    // Execute business logic
    var res = await _serviceManager.Currencies.DeleteCurrency(key, modelDto);

    // Return standardized response
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Currency deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }
}