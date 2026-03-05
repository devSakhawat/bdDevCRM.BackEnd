using bdDevCRM.Presentation.ActionFilters;
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
using bdDevCRM.Utilities.CRMGrid.GRID;

namespace bdDevCRM.Presentation.Controllers.CRM;

[ApiController]
public class PTEInformationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public PTEInformationController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  [HttpGet(RouteConstants.PTEInformationDDL)]
  public async Task<IActionResult> PTEInformationDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.PTEInformations.GetPTEInformationsDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ApiResponseHelper.NoContent<IEnumerable<PTEInformationDto>>("No PTE information found"));

    return Ok(ApiResponseHelper.Success(res, "PTE information retrieved successfully"));
  }

  [HttpGet(RouteConstants.PTEInformationByApplicantId)]
  public async Task<IActionResult> PTEInformationByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.PTEInformations.GetPTEInformationByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ApiResponseHelper.Success(res, "PTE information retrieved successfully"));
  }

  [HttpPost(RouteConstants.PTEInformationSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ApiResponseHelper.Unauthorized<PTEInformationDto>("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ApiResponseHelper.Unauthorized<PTEInformationDto>("User not found in cache"));

    var summaryGrid = await _serviceManager.PTEInformations.SummaryGrid(options);
    return Ok(ApiResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreatePTEInformation)]
  public async Task<IActionResult> CreateNewRecord([FromBody] PTEInformationDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ApiResponseHelper.Unauthorized<PTEInformationDto>("User authentication required"));

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      return Unauthorized(ApiResponseHelper.Unauthorized<PTEInformationDto>("User session expired")); 
    PTEInformationDto res = await _serviceManager.PTEInformations.CreateNewRecordAsync(modelDto, currentUser);
    return Ok(ApiResponseHelper.Created(res, "PTE information created successfully"));
  }

  [HttpPut(RouteConstants.UpdatePTEInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdatePTEInformation([FromRoute] int key, [FromBody] PTEInformationDto modelDto)
  {
    var res = await _serviceManager.PTEInformations.UpdateRecordAsync(key, modelDto, false);
    if (res == OperationMessage.Success)
      return Ok(ApiResponseHelper.Success(res, "PTE information updated successfully"));
    else
      return Conflict(ApiResponseHelper.Conflict<PTEInformationDto>(res));
  }

  [HttpDelete(RouteConstants.DeletePTEInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeletePTEInformation([FromRoute] int key, [FromBody] PTEInformationDto modelDto)
  {
    var res = await _serviceManager.PTEInformations.DeleteRecordAsync(key, modelDto);
    if (res == OperationMessage.Success)
      return Ok(ApiResponseHelper.Success(res, "PTE information deleted successfully"));
    else
      return Conflict(ApiResponseHelper.Conflict<PTEInformationDto>(res));
  }

  [HttpGet(RouteConstants.UpdatePTEInformation)]
  public async Task<IActionResult> GetPTEInformation([FromRoute] int key)
  {
    var res = await _serviceManager.PTEInformations.GetPTEInformationAsync(key, trackChanges: false);
    return Ok(ApiResponseHelper.Success(res, "PTE information retrieved successfully"));
  }
}