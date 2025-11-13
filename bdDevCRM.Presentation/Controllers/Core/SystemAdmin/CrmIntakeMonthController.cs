using bdDevCRM.Entities.CRMGrid.GRID;
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

public class CrmIntakeMonthController : BaseApiController
{
  private readonly IMemoryCache _cache;

  public CrmIntakeMonthController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }


  [HttpGet(RouteConstants.IntakeMonthByKey)]
  public async Task<IActionResult> GetIntakeMonth(int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake month ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var intakeMonth = await _serviceManager.CrmIntakeMonths.GetIntakeMonthAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(intakeMonth, "Intake month retrieved successfully"));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("IntakeMonth", "ID", key.ToString());
    }
  }

  [HttpGet(RouteConstants.IntakeMonthDDL)]
  public async Task<IActionResult> GetIntakeMonthsDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    var intakeMonths = await _serviceManager.CrmIntakeMonths.GetIntakeMonthsDDLAsync(trackChanges: false);

    if (intakeMonths == null || !intakeMonths.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmIntakeMonthDDL>>("No intake months found"));

    return Ok(ResponseHelper.Success(intakeMonths, "Intake months retrieved successfully"));
  }

  //[HttpGet(RouteConstants.IntakeMonthDDL)]
  //public async Task<IActionResult> GetIntakeMonths()
  //{
  //  if (!TryGetLoggedInUser(out UsersDto currentUser))
  //    return Unauthorized("Unauthorized attempt to get data!");

  //  if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
  //    throw new IdParametersBadRequestException();

  //  var intakeMonths = await _serviceManager.CrmIntakeMonths.GetIntakeMonthsAsync(trackChanges: false);

  //  if (intakeMonths == null || !intakeMonths.Any())
  //    return Ok(ResponseHelper.NoContent<IEnumerable<CrmIntakeMonthDto>>("No intake months found"));

  //  return Ok(ResponseHelper.Success(intakeMonths, "Intake months retrieved successfully"));
  //}

  [HttpPost(RouteConstants.IntakeMonthSummary)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> GetSummaryGrid([FromBody] CRMGridOptions options)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (options == null)
      throw new NullModelBadRequestException("CRMGridOptions cannot be null");

    var grid = await _serviceManager.CrmIntakeMonths.SummaryGrid(options);

    if (grid == null || !grid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CrmIntakeMonthDto>>("No data found"));

    return Ok(ResponseHelper.Success(grid, "Intake months grid retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreateIntakeMonth)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> CreateIntakeMonth([FromBody] CrmIntakeMonthDto intakeMonthDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeMonthDto == null)
      throw new NullModelBadRequestException("Intake month data cannot be null");

    var createdIntakeMonth = await _serviceManager.CrmIntakeMonths.CreateIntakeMonthAsync(intakeMonthDto);

    if (createdIntakeMonth.IntakeMonthId <= 0)
      throw new InvalidCreateOperationException("Failed to create intake month record.");

    return Ok(ResponseHelper.Success(createdIntakeMonth, "Intake month created successfully"));
  }

  [HttpPost(RouteConstants.CreateOrUpdateIntakeMonth)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> SaveOrUpdate(int key, [FromBody] CrmIntakeMonthDto intakeMonthDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeMonthDto == null)
      throw new NullModelBadRequestException("Intake month data cannot be null");

    var result = await _serviceManager.CrmIntakeMonths.SaveOrUpdate(key, intakeMonthDto);
    return Ok(ResponseHelper.Success(intakeMonthDto, result));
  }

  [HttpPut(RouteConstants.UpdateIntakeMonth)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> UpdateIntakeMonth(int key, [FromBody] CrmIntakeMonthDto intakeMonthDto)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake month ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeMonthDto == null)
      throw new NullModelBadRequestException("Intake month data cannot be null");

    var result = await _serviceManager.CrmIntakeMonths.UpdateNewRecordAsync(key, intakeMonthDto, trackChanges: true);
    return Ok(ResponseHelper.Success(intakeMonthDto, result));
  }

  [HttpDelete(RouteConstants.DeleteIntakeMonth)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> DeleteIntakeMonth(int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake month ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var intakeMonth = await _serviceManager.CrmIntakeMonths.GetIntakeMonthAsync(key, trackChanges: false);
      var result = await _serviceManager.CrmIntakeMonths.DeleteRecordAsync(key, intakeMonth);
      return Ok(ResponseHelper.Success<string?>(null, result));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("IntakeMonth", "ID", key.ToString());
    }
  }

}