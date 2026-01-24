using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[AuthenticatedUser]
public class QueryAnalyzerController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  public QueryAnalyzerController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  //[HttpPost(RouteConstants.GroupSummary)]
  //public async Task<IActionResult> GroupSummary([FromBody] CRMGridOptions options)
  //{
  //  var groupSummary = await _serviceManager.Groups.GroupSummary(trackChanges: false, options);
  //  return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  //}

  [HttpGet(RouteConstants.GetCustomizedReportInfo)]
  public async Task<IActionResult> GetCustomizedReportInfo()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    IEnumerable<QueryAnalyzerDto> res = await _serviceManager.QueryAnalyzer.CustomizedReportByPermission(currentUser, trackChanges: false);
    //return Ok(queryAnalyzers);

    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<QueryAnalyzerDto>>("No reports found"));

    return Ok(ResponseHelper.Success(res, "reports retrieved successfully"));
  }



}