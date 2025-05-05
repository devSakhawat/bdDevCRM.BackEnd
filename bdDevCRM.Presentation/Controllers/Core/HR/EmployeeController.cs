using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.HR;

public class EmployeeController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public EmployeeController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  [HttpGet(RouteConstants.EmployeeTypes)]
  public async Task<IActionResult> EmployeeTypes()
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    IEnumerable<EmployeeTypeDto> employees = await _serviceManager.Employees.EmployeeTypes((int)user.AccessParentCompany);
    return Ok(employees);
  }

  [HttpGet(RouteConstants.EmployeesByCompanyIdAndBranchIdAndDepartmentId)]
  public async Task<IActionResult> GetEmployeeByCompanyIdAndBranchIdAndDepartmentId([FromBody] int companyid, int branchId, int departmentId)
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);

    IEnumerable<EmployeesByCompanyBranchDepartmentDto> result = await _serviceManager.Employees.GetEmployeeByCompanyIdAndBranchIdAndDepartmentId(companyid, branchId, departmentId);

    return Ok(result);
  }




}
