
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class AccessControlController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public AccessControlController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpPost(RouteConstants.CreateAccessControl)]
  public async Task<IActionResult> SaveAccessControl([FromBody] AccessControlDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    var model = await _serviceManager.AccessControl.CreateAsync(modelDto);
    return (model != null) ? Ok(model) : NoContent();
  }

  [HttpPut(RouteConstants.UpdateAccessControl)]
  public async Task<IActionResult> UpdateAccessControl([FromRoute] int key, [FromBody] AccessControlDto modelDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    AccessControlDto returnData = await _serviceManager.AccessControl.UpdateAsync(key, modelDto);
    return (returnData != null) ? Ok(returnData) : NoContent();
  }


  [HttpPost(RouteConstants.AccessControlSummary)]
  public async Task<IActionResult> AccessControlSummary([FromBody] CRMGridOptions options)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    var accessSummary = await _serviceManager.AccessControl.AccessControlSummary(trackChanges: false, options);
    return (accessSummary != null) ? Ok(accessSummary) : NoContent();
  }



}