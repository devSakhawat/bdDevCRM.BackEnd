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
public class PteInformationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public PteInformationController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    _cache = cache;
    _env = env;
  }

  [HttpGet(RouteConstants.PteInformationDDL)]
  public async Task<IActionResult> PteInformationDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.PTEInformations.GetPteinformationsDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<PTEInformationDto>>("No PTE information found"));

    return Ok(ResponseHelper.Success(res, "PTE information retrieved successfully"));
  }

  [HttpGet(RouteConstants.PteInformationByApplicantId)]
  public async Task<IActionResult> PteInformationByApplicantId([FromRoute] int applicantId)
  {
    if (applicantId <= 0)
      throw new GenericBadRequestException("Invalid applicant ID.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.PTEInformations.GetPteinformationByApplicantIdAsync(applicantId, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "PTE information retrieved successfully"));
  }

  [HttpPost(RouteConstants.PteInformationSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

    var summaryGrid = await _serviceManager.PTEInformations.SummaryGrid(options);
    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  [HttpPost(RouteConstants.CreatePteInformation)]
  public async Task<IActionResult> CreateNewRecord([FromBody] PTEInformationDto modelDto)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(Convert.ToInt32(userIdClaim));
    if (currentUser == null)
      return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

    PTEInformationDto res = await _serviceManager.PTEInformations.CreateNewRecordAsync(modelDto, currentUser);
    return Ok(ResponseHelper.Created(res, "PTE information created successfully"));
  }

  [HttpPut(RouteConstants.UpdatePteInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdatePteInformation([FromRoute] int key, [FromBody] PTEInformationDto modelDto)
  {
    var res = await _serviceManager.PTEInformations.UpdateRecordAsync(key, modelDto, false);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "PTE information updated successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpDelete(RouteConstants.DeletePteInformation)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeletePteInformation([FromRoute] int key, [FromBody] PTEInformationDto modelDto)
  {
    var res = await _serviceManager.PTEInformations.DeleteRecordAsync(key, modelDto);
    if (res == OperationMessage.Success)
      return Ok(ResponseHelper.Success(res, "PTE information deleted successfully"));
    else
      return Conflict(ResponseHelper.Conflict(res));
  }

  [HttpGet(RouteConstants.UpdatePteInformation)]
  public async Task<IActionResult> GetPteInformation([FromRoute] int key)
  {
    var res = await _serviceManager.PTEInformations.GetPteinformationAsync(key, trackChanges: false);
    return Ok(ResponseHelper.Success(res, "PTE information retrieved successfully"));
  }
}