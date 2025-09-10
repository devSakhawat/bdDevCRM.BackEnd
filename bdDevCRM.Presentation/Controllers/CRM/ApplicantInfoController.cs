using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.CRM;

[ApiController]
public class ApplicantInfoController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public ApplicantInfoController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.ApplicantInfoDDL)]
  public async Task<IActionResult> ApplicantInfoDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.ApplicantInfos.GetApplicantInfosDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<ApplicantInfoDto>>("No applicant info found"));

    return Ok(ResponseHelper.Success(res, "Applicant info retrieved successfully"));
  }

  [HttpGet(RouteConstants.ApplicantInfoByApplicationId)]
  public async Task<IActionResult> ApplicantInfoByApplicationId([FromRoute] int applicationId)
  {
    if (applicationId <= 0)
      throw new GenericBadRequestException("Invalid application ID. Application ID must be greater than 0.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.ApplicantInfos.GetApplicantInfoByApplicationIdAsync(applicationId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Applicant info retrieved successfully"));
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.ApplicantInfoSummary)]
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

    var summaryGrid = await _serviceManager.ApplicantInfos.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  // --------- 3. Create ----------------------------------------------
  [HttpPost(RouteConstants.CreateApplicantInfo)]
  [RequestSizeLimit(1_000_000)]
  public async Task<IActionResult> CreateNewRecord([FromBody] ApplicantInfoDto modelDto)
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
        return BadRequest(ResponseHelper.BadRequest("Applicant info data is required"));

      ApplicantInfoDto res = await _serviceManager.ApplicantInfos.CreateNewRecordAsync(modelDto, currentUser);

      if (res.ApplicantId > 0)
        return Ok(ResponseHelper.Created(res, "Applicant info created successfully"));
      else
        return StatusCode(500, ResponseHelper.InternalServerError("Failed to create applicant info"));
    }
    catch (System.Text.Json.JsonException)
    {
      return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in applicant info data"));
    }
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateApplicantInfo)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateApplicantInfo([FromRoute] int key, [FromBody] ApplicantInfoDto modelDto)
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

      var res = await _serviceManager.ApplicantInfos.UpdateRecordAsync(key, modelDto, false);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Applicant info updated successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteApplicantInfo)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteApplicantInfo([FromRoute] int key, [FromBody] ApplicantInfoDto modelDto)
  {
    try
    {
      int userId = HttpContext.GetUserId();
      var currentUser = HttpContext.GetCurrentUser();

      var res = await _serviceManager.ApplicantInfos.DeleteRecordAsync(key, modelDto);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Applicant info deleted successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 6. Get Single Record -----------------------------------
  [HttpGet(RouteConstants.UpdateApplicantInfo)]
  public async Task<IActionResult> GetApplicantInfo([FromRoute] int key)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      var res = await _serviceManager.ApplicantInfos.GetApplicantInfoAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(res, "Applicant info retrieved successfully"));
    }
    catch (Exception)
    {
      throw;
    }
  }
}