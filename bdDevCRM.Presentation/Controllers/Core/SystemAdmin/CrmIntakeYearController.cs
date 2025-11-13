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


public class CrmIntakeYearController : BaseApiController
{
  private readonly IMemoryCache _cache;

  public CrmIntakeYearController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }



  [HttpGet(RouteConstants.IntakeYearDDL)]
  public async Task<IActionResult> GetIntakeYearsDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    var intakeYears = await _serviceManager.CrmIntakeYears.GetIntakeYearsDDLAsync(trackChanges: false);

    if (intakeYears == null || !intakeYears.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmIntakeYearDDL>>("No intake years found"));

    return Ok(ResponseHelper.Success(intakeYears, "Intake years retrieved successfully"));
  }

  //[HttpGet(RouteConstants.IntakeYearDDL)]
  //public async Task<IActionResult> GetIntakeYears()
  //{
  //  if (!TryGetLoggedInUser(out UsersDto currentUser))
  //    return Unauthorized("Unauthorized attempt to get data!");

  //  if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
  //    throw new IdParametersBadRequestException();

  //  var intakeYears = await _serviceManager.CrmIntakeYears.GetIntakeYearsAsync(trackChanges: false);

  //  if (intakeYears == null || !intakeYears.Any())
  //    return Ok(ResponseHelper.NoContent<IEnumerable<CrmIntakeYearDto>>("No intake years found"));

  //  return Ok(ResponseHelper.Success(intakeYears, "Intake years retrieved successfully"));
  //}

  [HttpPost(RouteConstants.IntakeYearSummary)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> GetSummaryGrid([FromBody] CRMGridOptions options)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (options == null)
      throw new NullModelBadRequestException("CRMGridOptions cannot be null");

    var grid = await _serviceManager.CrmIntakeYears.SummaryGrid(trackChanges: false, options, currentUser);

    if (grid == null || !grid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CrmIntakeYearDto>>("No data found"));

    return Ok(ResponseHelper.Success(grid, "Intake years grid retrieved successfully"));
  }

  [HttpGet(RouteConstants.IntakeYearByKey)]
  public async Task<IActionResult> GetIntakeYear([FromQuery]int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake year ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var intakeYear = await _serviceManager.CrmIntakeYears.GetIntakeYearAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(intakeYear, "Intake year retrieved successfully"));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("IntakeYear", "ID", key.ToString());
    }
  }

  [HttpPost(RouteConstants.CreateIntakeYear)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> CreateIntakeYear([FromBody] CrmIntakeYearDto intakeYearDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeYearDto == null)
      throw new NullModelBadRequestException("Intake year data cannot be null");

    var createdIntakeYear = await _serviceManager.CrmIntakeYears.CreateIntakeYearAsync(intakeYearDto);

    if (createdIntakeYear.IntakeYearId <= 0)
      throw new InvalidCreateOperationException("Failed to create intake year record.");

    return Ok(ResponseHelper.Success(createdIntakeYear, "Intake year created successfully"));
  }

  [HttpPost(RouteConstants.CreateOrUpdateIntakeYear)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> SaveOrUpdate(int key, [FromBody] CrmIntakeYearDto intakeYearDto)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeYearDto == null)
      throw new NullModelBadRequestException("Intake year data cannot be null");

    var result = await _serviceManager.CrmIntakeYears.SaveOrUpdate(key, intakeYearDto);
    return Ok(ResponseHelper.Success(intakeYearDto, result));
  }

  [HttpPut(RouteConstants.UpdateIntakeYear)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> UpdateIntakeYear(int key, [FromBody] CrmIntakeYearDto intakeYearDto)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake year ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    if (intakeYearDto == null)
      throw new NullModelBadRequestException("Intake year data cannot be null");

    var result = await _serviceManager.CrmIntakeYears.UpdateNewRecordAsync(key, intakeYearDto, trackChanges: true);
    return Ok(ResponseHelper.Success(intakeYearDto, result));
  }

  [HttpDelete(RouteConstants.DeleteIntakeYear)]
  [ServiceFilter(typeof(LogActionAttribute))]
  public async Task<IActionResult> DeleteIntakeYear(int key)
  {
    if (key <= 0)
      throw new GenericBadRequestException("Invalid intake year ID. ID must be greater than 0.");

    if (!TryGetLoggedInUser(out UsersDto currentUser))
      return Unauthorized("Unauthorized attempt to get data!");

    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    try
    {
      var intakeYear = await _serviceManager.CrmIntakeYears.GetIntakeYearAsync(key, trackChanges: false);
      var result = await _serviceManager.CrmIntakeYears.DeleteRecordAsync(key, intakeYear);
      return Ok(ResponseHelper.Success<string?>(null, result));
    }
    catch (GenericNotFoundException)
    {
      throw new GenericNotFoundException("IntakeYear", "ID", key.ToString());
    }
  }



}