using bdDevCRM.Utilities.CRMGrid.GRID;
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
public class WorkExperienceController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public WorkExperienceController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  [HttpGet(RouteConstants.WorkExperienceDDL)]
  public async Task<IActionResult> WorkExperienceDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.WorkExperiences.GetWorkExperiencesDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<WorkExperienceHistoryDto>>("No work experience found"));

    return Ok(ResponseHelper.Success(res, "Work experience retrieved successfully"));
  }

  [HttpGet(RouteConstants.WorkExperienceByApplicantId)]
  public async Task<IActionResult> WorkExperienceByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.WorkExperiences.GetWorkExperiencesByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Work experience retrieved successfully"));
  }

  [HttpPost(RouteConstants.WorkExperienceSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

    var summaryGrid = await _serviceManager.WorkExperiences.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreateWorkExperience)]
  public async Task<IActionResult> CreateNewRecord([FromBody] WorkExperienceHistoryDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

    WorkExperienceHistoryDto res = await _serviceManager.WorkExperiences.CreateNewRecordAsync(modelDto, currentUser);
    return Ok(ResponseHelper.Created(res, "Work experience created successfully"));
  }

  [HttpPut(RouteConstants.UpdateWorkExperience)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateWorkExperience([FromRoute] int key, [FromBody] WorkExperienceHistoryDto modelDto)
  {
    var res = await _serviceManager.WorkExperiences.UpdateRecordAsync(key, modelDto, false);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Work experience updated successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpDelete(RouteConstants.DeleteWorkExperience)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteWorkExperience([FromRoute] int key, [FromBody] WorkExperienceHistoryDto modelDto)
  {
    var res = await _serviceManager.WorkExperiences.DeleteRecordAsync(key, modelDto);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Work experience deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpGet(RouteConstants.UpdateWorkExperience)]
  public async Task<IActionResult> GetWorkExperience([FromRoute] int key)
  {
    var res = await _serviceManager.WorkExperiences.GetWorkExperienceAsync(key, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Work experience retrieved successfully"));
  }
}