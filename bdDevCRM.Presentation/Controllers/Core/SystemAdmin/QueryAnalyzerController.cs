using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;

using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    IEnumerable<QueryAnalyzerDto> queryAnalyzers = await _serviceManager.QueryAnalyzer.CustomizedReportInfo(trackChanges: false);
    return Ok(queryAnalyzers);
  }



}