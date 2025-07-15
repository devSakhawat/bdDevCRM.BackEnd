using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.CRM;

[ApiController]
public class IeltsInformationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public IeltsInformationController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.IeltsInformationDDL)]
  public async Task<IActionResult> IeltsInformationDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.IeltsInformations.GetIeltsinformationsDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<IELTSInformationDto>>("No IELTS information found"));

    return Ok(ResponseHelper.Success(res, "IELTS information retrieved successfully"));
  }

  [HttpGet(RouteConstants.IeltsInformationByApplicantId)]
  public async Task<IActionResult> IeltsInformationByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID. Applicant ID must be greater than 0.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.IeltsInformations.GetIeltsinformationByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "IELTS information retrieved successfully"));
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.IeltsInformationSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

    if (options == null)
      return BadRequest(ResponseHelper.BadRequest("CRMGridOptions cannot be null"));

    var summaryGrid = await _serviceManager.IeltsInformations.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  // --------- 3. Create ----------------------------------------------
  [HttpPost(RouteConstants.CreateIeltsInformation)]
  [RequestSizeLimit(1_000_000)]
  public async Task<IActionResult> CreateNewRecord([FromBody] IELTSInformationDto modelDto)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

      if (modelDto == null)
        return BadRequest(ResponseHelper.BadRequest("IELTS information data is required"));

      IELTSInformationDto res = await _serviceManager.IeltsInformations.CreateNewRecordAsync(modelDto, currentUser);

      if (res.IELTSInformationId > 0)
        return Ok(ResponseHelper.Created(res, "IELTS information created successfully"));
      else
        return StatusCode(500, ResponseHelper.InternalServerError("Failed to create IELTS information"));
    }
    catch (System.Text.Json.JsonException)
    {
      return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in IELTS information data"));
    }
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateIeltsInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateIeltsInformation([FromRoute] int key, [FromBody] IELTSInformationDto modelDto)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token."));

      int userId = Convert.ToInt32(userIdClaim);
      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        return Unauthorized(ResponseHelper.Unauthorized("User not found in cache."));

      var res = await _serviceManager.IeltsInformations.UpdateRecordAsync(key, modelDto, false);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "IELTS information updated successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteIeltsInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteIeltsInformation([FromRoute] int key, [FromBody] IELTSInformationDto modelDto)
  {
    try
    {
      int userId = HttpContext.GetUserId();
      var currentUser = HttpContext.GetCurrentUser();

      var res = await _serviceManager.IeltsInformations.DeleteRecordAsync(key, modelDto);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "IELTS information deleted successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 6. Get Single Record -----------------------------------
  [HttpGet(RouteConstants.UpdateIeltsInformation)]
  public async Task<IActionResult> GetIeltsInformation([FromRoute] int key)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      var res = await _serviceManager.IeltsInformations.GetIeltsinformationAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(res, "IELTS information retrieved successfully"));
    }
    catch (Exception)
    {
      throw;
    }
  }
}