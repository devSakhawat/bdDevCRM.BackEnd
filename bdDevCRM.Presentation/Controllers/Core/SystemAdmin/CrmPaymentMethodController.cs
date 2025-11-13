using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


public class CrmPaymentMethodController : BaseApiController
{
  private readonly IMemoryCache _cache;

  public CrmPaymentMethodController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.PaymentMethodByKey)]
  public async Task<IActionResult> GetPaymentMethod(int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid payment method ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var paymentMethod = await _serviceManager.CrmPaymentMethods.GetPaymentMethodAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(paymentMethod, "Payment method retrieved successfully"));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("PaymentMethod", "ID", key.ToString());
    }
  }


  //[HttpGet(RouteConstants.PaymentMethodByKey)]
  //public async Task<IActionResult> GetPaymentMethods()
  //{
  //  if (!TryGetLoggedInUser(out UsersDto currentUser))
  //    return Unauthorized("Unauthorized attempt to get data!");

  //  if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
  //    throw new IdParametersBadRequestException();

  //  var paymentMethods = await _serviceManager.CrmPaymentMethods.GetPaymentMethodsAsync(trackChanges: false);

  //  if (paymentMethods == null || !paymentMethods.Any())
  //    return Ok(ResponseHelper.NoContent<IEnumerable<CrmPaymentMethodDto>>("No payment methods found"));

  //  return Ok(ResponseHelper.Success(paymentMethods, "Payment methods retrieved successfully"));
  //}


  [HttpGet(RouteConstants.PaymentMethodDDL)]
  public async Task<IActionResult> GetPaymentMethodsDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    var paymentMethods = await _serviceManager.CrmPaymentMethods.GetPaymentMethodsDDLAsync(trackChanges: false);

    if (paymentMethods == null || !paymentMethods.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmPaymentMethodDDL>>("No payment methods found"));

    return Ok(ResponseHelper.Success(paymentMethods, "Payment methods retrieved successfully"));
  }

  [HttpGet(RouteConstants.OnlinePaymentMethodDDL)]
  public async Task<IActionResult> GetOnlinePaymentMethodsDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    var paymentMethods = await _serviceManager.CrmPaymentMethods.GetOnlinePaymentMethodsDDLAsync(trackChanges: false);

    if (paymentMethods == null || !paymentMethods.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmPaymentMethodDDL>>("No online payment methods found"));

    return Ok(ResponseHelper.Success(paymentMethods, "Online payment methods retrieved successfully"));
  }

  [HttpPost(RouteConstants.PaymentMethodSummary)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> GetSummaryGrid([FromBody] CRMGridOptions options)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (options == null)
      throw new NullModelBadRequestException("CRMGridOptions cannot be null");

    var grid = await _serviceManager.CrmPaymentMethods.SummaryGrid(options);

    if (grid == null || !grid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CrmPaymentMethodDto>>("No data found"));

    return Ok(ResponseHelper.Success(grid, "Payment methods grid retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreatePaymentMethod)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> CreatePaymentMethod([FromBody] CrmPaymentMethodDto paymentMethodDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (paymentMethodDto == null)
      throw new NullModelBadRequestException("Payment method data cannot be null");

    var createdPaymentMethod = await _serviceManager.CrmPaymentMethods.CreatePaymentMethodAsync(paymentMethodDto);

    if (createdPaymentMethod.PaymentMethodId <= 0)
      throw new InvalidCreateOperationException("Failed to create payment method record.");

    return Ok(ResponseHelper.Success(createdPaymentMethod, "Payment method created successfully"));
  }

  [HttpPost(RouteConstants.CreateOrUpdatePaymentMethod)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> SaveOrUpdate(int key, [FromBody] CrmPaymentMethodDto paymentMethodDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (paymentMethodDto == null)
      throw new NullModelBadRequestException("Payment method data cannot be null");

    var result = await _serviceManager.CrmPaymentMethods.SaveOrUpdate(key, paymentMethodDto);
    return Ok(ResponseHelper.Success(paymentMethodDto, result));
  }

  [HttpPut(RouteConstants.UpdatePaymentMethod)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> UpdatePaymentMethod(int key, [FromBody] CrmPaymentMethodDto paymentMethodDto)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid payment method ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (paymentMethodDto == null)
      throw new NullModelBadRequestException("Payment method data cannot be null");

    var result = await _serviceManager.CrmPaymentMethods.UpdateNewRecordAsync(key, paymentMethodDto, trackChanges: true);
    return Ok(ResponseHelper.Success(paymentMethodDto, result));
  }

  [HttpDelete(RouteConstants.DeletePaymentMethod)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> DeletePaymentMethod(int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid payment method ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var paymentMethod = await _serviceManager.CrmPaymentMethods.GetPaymentMethodAsync(key, trackChanges: false);
      var result = await _serviceManager.CrmPaymentMethods.DeleteRecordAsync(key, paymentMethod);
      return Ok(ResponseHelper.Success<string?>(null, result));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("PaymentMethod", "ID", key.ToString());
    }
  }



}