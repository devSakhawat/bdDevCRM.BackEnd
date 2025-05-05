using bdDevCRM.Entities.Exceptions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.HR;

public class DepartmentController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public DepartmentController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.DepartmentByCompanyIdForCombo)]
  [AllowAnonymous]
  public async Task<IActionResult> DepartmentByCompanyIdForCombo([FromQuery] int companyId)
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    IEnumerable<DepartmentDto> result = await _serviceManager.departments.DepartmentesByCompanyIdForCombo(companyId, user);
    return Ok(result);
  }




}
