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
public class ApplicantCourseController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public ApplicantCourseController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.ApplicantCourseDDL)]
  public async Task<IActionResult> ApplicantCourseDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.ApplicantCourses.GetApplicantCoursesDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<ApplicantCourseDto>>("No applicant courses found"));

    return Ok(ResponseHelper.Success(res, "Applicant courses retrieved successfully"));
  }

  [HttpGet(RouteConstants.ApplicantCoursesByApplicantId)]
  public async Task<IActionResult> ApplicantCoursesByApplicantId([FromRoute] int applicantId)
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

    var res = await _serviceManager.ApplicantCourses.GetApplicantCoursesByApplicantIdAsync(applicantId, trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<ApplicantCourseDto>>("No applicant courses found"));

    return Ok(ResponseHelper.Success(res, "Applicant courses retrieved successfully"));
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.ApplicantCourseSummary)]
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

    var summaryGrid = await _serviceManager.ApplicantCourses.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  // --------- 3. Create ----------------------------------------------
  [HttpPost(RouteConstants.CreateApplicantCourse)]
  [RequestSizeLimit(1_000_000)]
  public async Task<IActionResult> CreateNewRecord([FromBody] ApplicantCourseDto modelDto)
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
        return BadRequest(ResponseHelper.BadRequest("Applicant course data is required"));

      ApplicantCourseDto res = await _serviceManager.ApplicantCourses.CreateNewRecordAsync(modelDto, currentUser);

      if (res.ApplicantCourseId > 0)
        return Ok(ResponseHelper.Created(res, "Applicant course created successfully"));
      else
        return StatusCode(500, ResponseHelper.InternalServerError("Failed to create applicant course"));
    }
    catch (System.Text.Json.JsonException)
    {
      return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in applicant course data"));
    }
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateApplicantCourse)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateApplicantCourse([FromRoute] int key, [FromBody] ApplicantCourseDto modelDto)
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

      var res = await _serviceManager.ApplicantCourses.UpdateRecordAsync(key, modelDto, false);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Applicant course updated successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteApplicantCourse)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteApplicantCourse([FromRoute] int key, [FromBody] ApplicantCourseDto modelDto)
  {
    try
    {
      int userId = HttpContext.GetUserId();
      var currentUser = HttpContext.GetCurrentUser();

      var res = await _serviceManager.ApplicantCourses.DeleteRecordAsync(key, modelDto);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Applicant course deleted successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception)
    {
      throw;
    }
  }

  // --------- 6. Get Single Record -----------------------------------
  [HttpGet(RouteConstants.UpdateApplicantCourse)]
  public async Task<IActionResult> GetApplicantCourse([FromRoute] int key)
  {
    try
    {
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (string.IsNullOrEmpty(userIdClaim))
        return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

      var res = await _serviceManager.ApplicantCourses.GetApplicantCourseAsync(key, trackChanges: false);
      return Ok(ResponseHelper.Success(res, "Applicant course retrieved successfully"));
    }
    catch (Exception)
    {
      throw;
    }
  }
}