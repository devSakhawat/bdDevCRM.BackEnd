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
public class ApplicantReferenceController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public ApplicantReferenceController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  [HttpGet(RouteConstants.ApplicantReferenceDDL)]
  public async Task<IActionResult> ApplicantReferenceDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.ApplicantReferences.GetApplicantReferencesDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<ReferenceDto>>("No applicant references found"));

    return Ok(ResponseHelper.Success(res, "Applicant references retrieved successfully"));
  }

  [HttpGet(RouteConstants.ApplicantReferenceByApplicantId)]
  public async Task<IActionResult> ApplicantReferenceByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.ApplicantReferences.GetApplicantReferencesByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Applicant references retrieved successfully"));
  }

  [HttpPost(RouteConstants.ApplicantReferenceSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

    var summaryGrid = await _serviceManager.ApplicantReferences.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreateApplicantReference)]
  public async Task<IActionResult> CreateNewRecord([FromBody] ReferenceDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

    ReferenceDto res = await _serviceManager.ApplicantReferences.CreateNewRecordAsync(modelDto, currentUser);
    return Ok(ResponseHelper.Created(res, "Applicant reference created successfully"));
  }

  [HttpPut(RouteConstants.UpdateApplicantReference)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateApplicantReference([FromRoute] int key, [FromBody] ReferenceDto modelDto)
  {
    var res = await _serviceManager.ApplicantReferences.UpdateRecordAsync(key, modelDto, false);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Applicant reference updated successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpDelete(RouteConstants.DeleteApplicantReference)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteApplicantReference([FromRoute] int key, [FromBody] ReferenceDto modelDto)
  {
    var res = await _serviceManager.ApplicantReferences.DeleteRecordAsync(key, modelDto);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Applicant reference deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpGet(RouteConstants.UpdateApplicantReference)]
  public async Task<IActionResult> GetApplicantReference([FromRoute] int key)
  {
    var res = await _serviceManager.ApplicantReferences.GetApplicantReferenceAsync(key, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Applicant reference retrieved successfully"));
  }
}