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
public class OthersInformationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public OthersInformationController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  [HttpGet(RouteConstants.OthersInformationDDL)]
  public async Task<IActionResult> OthersInformationDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.OthersInformations.GetOthersinformationsDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<OTHERSInformationDto>>("No others information found"));

    return Ok(ResponseHelper.Success(res, "Others information retrieved successfully"));
  }

  [HttpGet(RouteConstants.OthersInformationByApplicantId)]
  public async Task<IActionResult> OthersInformationByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.OthersInformations.GetOthersinformationByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Others information retrieved successfully"));
  }

  [HttpPost(RouteConstants.OthersInformationSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

    var summaryGrid = await _serviceManager.OthersInformations.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreateOthersInformation)]
  public async Task<IActionResult> CreateNewRecord([FromBody] OTHERSInformationDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

    OTHERSInformationDto res = await _serviceManager.OthersInformations.CreateNewRecordAsync(modelDto, currentUser);
    return Ok(ResponseHelper.Created(res, "Others information created successfully"));
  }

  [HttpPut(RouteConstants.UpdateOthersInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateOthersInformation([FromRoute] int key, [FromBody] OTHERSInformationDto modelDto)
  {
    var res = await _serviceManager.OthersInformations.UpdateRecordAsync(key, modelDto, false);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Others information updated successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpDelete(RouteConstants.DeleteOthersInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteOthersInformation([FromRoute] int key, [FromBody] OTHERSInformationDto modelDto)
  {
    var res = await _serviceManager.OthersInformations.DeleteRecordAsync(key, modelDto);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "Others information deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpGet(RouteConstants.UpdateOthersInformation)]
  public async Task<IActionResult> GetOthersInformation([FromRoute] int key)
  {
    var res = await _serviceManager.OthersInformations.GetOthersinformationAsync(key, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "Others information retrieved successfully"));
  }
}