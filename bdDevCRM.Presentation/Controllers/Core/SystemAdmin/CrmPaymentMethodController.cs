using bdDevCRM.Entities.CRMGrid.GRID;
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
/// Controller for managing CRM payment methods
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class CrmPaymentMethodController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public CrmPaymentMethodController(IServiceManager serviceManager, IMemoryCache cache) 
        : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves a specific payment method by ID
    /// </summary>
    /// <param name="key">Payment method ID</param>
    /// <returns>Payment method details</returns>
    [HttpGet(RouteConstants.PaymentMethodByKey)]
    public async Task<IActionResult> GetPaymentMethod(int key)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid payment method ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        try
        {
            // Execute business logic
            var paymentMethod = await _serviceManager.CrmPaymentMethods
                .GetPaymentMethodAsync(key, trackChanges: false);

            // Return standardized response
            return Ok(ResponseHelper.Success(paymentMethod, 
                "Payment method retrieved successfully"));
        }
        catch (GenericNotFoundException)
        {
            throw new GenericNotFoundException("PaymentMethod", "ID", key.ToString());
        }
    }

    /// <summary>
    /// Retrieves all payment methods for dropdown list
    /// </summary>
    /// <returns>List of payment methods for dropdown</returns>
    [HttpGet(RouteConstants.PaymentMethodDDL)]
    public async Task<IActionResult> GetPaymentMethodsDDL()
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var paymentMethods = await _serviceManager.CrmPaymentMethods
            .GetPaymentMethodsDDLAsync(trackChanges: false);

        // Return standardized response
        if (paymentMethods == null || !paymentMethods.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<CrmPaymentMethodDDL>>(
                "No payment methods found"));

        return Ok(ResponseHelper.Success(paymentMethods, 
            "Payment methods retrieved successfully"));
    }

    /// <summary>
    /// Retrieves all online payment methods for dropdown list
    /// </summary>
    /// <returns>List of online payment methods for dropdown</returns>
    [HttpGet(RouteConstants.OnlinePaymentMethodDDL)]
    public async Task<IActionResult> GetOnlinePaymentMethodsDDL()
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Execute business logic
        var paymentMethods = await _serviceManager.CrmPaymentMethods
            .GetOnlinePaymentMethodsDDLAsync(trackChanges: false);

        // Return standardized response
        if (paymentMethods == null || !paymentMethods.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<CrmPaymentMethodDDL>>(
                "No online payment methods found"));

        return Ok(ResponseHelper.Success(paymentMethods, 
            "Online payment methods retrieved successfully"));
    }

    /// <summary>
    /// Retrieves paginated summary grid of payment methods
    /// </summary>
    /// <param name="options">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of payment methods</returns>
    [HttpPost(RouteConstants.PaymentMethodSummary)]
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
        var grid = await _serviceManager.CrmPaymentMethods.SummaryGrid(options);

        // Return standardized response
        if (grid == null || !grid.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<CrmPaymentMethodDto>>(
                "No payment methods found"));

        return Ok(ResponseHelper.Success(grid, 
            "Payment methods grid retrieved successfully"));
    }

    /// <summary>
    /// Creates a new payment method record
    /// </summary>
    /// <param name="paymentMethodDto">Payment method data to create</param>
    /// <returns>Created payment method record</returns>
    [HttpPost(RouteConstants.CreatePaymentMethod)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> CreatePaymentMethod(
        [FromBody] CrmPaymentMethodDto paymentMethodDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (paymentMethodDto == null)
            throw new NullModelBadRequestException("Payment method data cannot be null");

        // Execute business logic
        var createdPaymentMethod = await _serviceManager.CrmPaymentMethods
            .CreatePaymentMethodAsync(paymentMethodDto);

        // Validate result
        if (createdPaymentMethod.PaymentMethodId <= 0)
            throw new InvalidCreateOperationException(
                "Failed to create payment method record.");

        // Return standardized response
        return Ok(ResponseHelper.Created(createdPaymentMethod, 
            "Payment method created successfully"));
    }

    /// <summary>
    /// Creates or updates a payment method record
    /// </summary>
    /// <param name="key">Payment method ID (0 for create, >0 for update)</param>
    /// <param name="paymentMethodDto">Payment method data</param>
    /// <returns>Operation result message</returns>
    [HttpPost(RouteConstants.CreateOrUpdatePaymentMethod)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> SaveOrUpdate(
        int key, 
        [FromBody] CrmPaymentMethodDto paymentMethodDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (paymentMethodDto == null)
            throw new NullModelBadRequestException("Payment method data cannot be null");

        // Execute business logic
        var result = await _serviceManager.CrmPaymentMethods
            .SaveOrUpdate(key, paymentMethodDto);

        // Return standardized response
        return Ok(ResponseHelper.Success(paymentMethodDto, result));
    }

    /// <summary>
    /// Updates an existing payment method record
    /// </summary>
    /// <param name="key">Payment method ID</param>
    /// <param name="paymentMethodDto">Updated payment method data</param>
    /// <returns>Operation result message</returns>
    [HttpPut(RouteConstants.UpdatePaymentMethod)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> UpdatePaymentMethod(
        int key, 
        [FromBody] CrmPaymentMethodDto paymentMethodDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid payment method ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        // Validate input parameters
        if (paymentMethodDto == null)
            throw new NullModelBadRequestException("Payment method data cannot be null");

        // Execute business logic
        var result = await _serviceManager.CrmPaymentMethods
            .UpdateNewRecordAsync(key, paymentMethodDto, trackChanges: true);

        // Return standardized response
        return Ok(ResponseHelper.Updated(paymentMethodDto, result));
    }

    /// <summary>
    /// Deletes a payment method record
    /// </summary>
    /// <param name="key">Payment method ID to delete</param>
    /// <returns>Operation result message</returns>
    [HttpDelete(RouteConstants.DeletePaymentMethod)]
    [ServiceFilter(typeof(LogActionAttribute))]
    public async Task<IActionResult> DeletePaymentMethod(int key)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (key <= 0)
            throw new GenericBadRequestException(
                "Invalid payment method ID. ID must be greater than 0.");

        // Validate user data
        if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
            throw new IdParametersBadRequestException();

        try
        {
            // Load record before deletion
            var paymentMethod = await _serviceManager.CrmPaymentMethods
                .GetPaymentMethodAsync(key, trackChanges: false);

            // Execute deletion
            var result = await _serviceManager.CrmPaymentMethods
                .DeleteRecordAsync(key, paymentMethod);

            // Return standardized response
            return Ok(ResponseHelper.Success<string?>(null, result));
        }
        catch (GenericNotFoundException)
        {
            throw new GenericNotFoundException("PaymentMethod", "ID", key.ToString());
        }
    }
}