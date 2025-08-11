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
public class ToeflInformationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public ToeflInformationController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.ToeflInformationDDL)]
  public async Task<IActionResult> ToeflInformationDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.TOEFLInformations.GetToeflinformationsDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<TOEFLInformationDto>>("No TOEFL information found"));

    return Ok(ResponseHelper.Success(res, "TOEFL information retrieved successfully"));
  }

  [HttpGet(RouteConstants.ToeflInformationByApplicantId)]
  public async Task<IActionResult> ToeflInformationByApplicantId([FromRoute] int applicantId)
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

    var res = await _serviceManager.TOEFLInformations.GetToeflinformationByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "TOEFL information retrieved successfully"));
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.ToeflInformationSummary)]
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

    var summaryGrid = await _serviceManager.TOEFLInformations.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  // --------- 3. Create ----------------------------------------------
  [HttpPost(RouteConstants.CreateToeflInformation)]
  [RequestSizeLimit(1_000_000)]
  public async Task<IActionResult> CreateNewRecord([FromBody] TOEFLInformationDto modelDto)
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
        return BadRequest(ResponseHelper.BadRequest("TOEFL information data is required"));

      TOEFLInformationDto res = await _serviceManager.TOEFLInformations.CreateNewRecordAsync(modelDto, currentUser);

      if (res.TOEFLInformationId > 0)
        return Ok(ResponseHelper.Created(res, "TOEFL information created successfully"));
      else
        return StatusCode(500, ResponseHelper.InternalServerError("Failed to create TOEFL information"));
    }
    catch (System.Text.Json.JsonException)
    {
      return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in TOEFL information data"));
    }
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateToeflInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateToeflInformation([FromRoute] int key, [FromBody] TOEFLInformationDto modelDto)
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

      var res = await _serviceManager.TOEFLInformations.UpdateRecordAsync(key, modelDto, false);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "TOEFL information updated successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteToeflInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteToeflInformation([FromRoute] int key, [FromBody] TOEFLInformationDto modelDto)
  {
    try
    {
      int userId = HttpContext.GetUserId();
      var currentUser = HttpContext.GetCurrentUser();

      var res = await _serviceManager.TOEFLInformations.DeleteRecordAsync(key, modelDto);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "TOEFL information deleted successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 6. Get Single Record -----------------------------------
  [HttpGet(RouteConstants.UpdateToeflInformation)]
  public async Task<IActionResult> GetToeflInformation([FromRoute] int key)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      var res = await _serviceManager.TOEFLInformations.GetToeflinformationAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(res, "TOEFL information retrieved successfully"));
    }
    catch (Exception)
    {
      throw;
    }
  }
}