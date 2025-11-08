//using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
//using bdDevCRM.Utilities.KendoGrid;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class ModuleController : BaseApiController
{
    //private readonly IServiceManager _serviceManager;
    private readonly IMemoryCache _cache;
    public ModuleController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
    {
        //_serviceManager = serviceManager;
        _cache = cache;
    }


    [HttpPost(RouteConstants.ModuleSummary)]
    public async Task<IActionResult> ModuleSummary([FromBody] CRMGridOptions options)
    {
        var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
        var moduleSummary = await _serviceManager.Modules.ModuleSummary(false, options);
        return (moduleSummary != null) ? Ok(moduleSummary) : NoContent();
    }


  //[HttpGet("SelectMenuByUserPermission")]
  [HttpGet(RouteConstants.Modules)]
  [ResponseCache(Duration = 300)] // Browser caching for 5 minutes
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetModulesAsync()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    if (!MenuConstant.TryGetPath("CRMApplication", out var menuPath))
      throw new GenericBadRequestException("Invalid menu name.");
    var rawUrl = $"..{menuPath}";

    var res = await _serviceManager.Modules.GetModulesAsync(false);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<ModuleDto>>("No data found!"));

    return Ok(ResponseHelper.Success(res, "Data retrieved successfully"));
  }

    [HttpPost(RouteConstants.CreateModule)]
    public async Task<IActionResult> SaveModule([FromBody] ModuleDto moduleDto)
    {
        var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

        var module = await _serviceManager.Modules.CreateModuleAsync(moduleDto);
        return (module != null) ? Ok(module) : NoContent();
    }

    [HttpPut(RouteConstants.UpdateModule)]
    public async Task<IActionResult> UpdateModule([FromRoute] int key, [FromBody] ModuleDto moduleDto)
    {
        var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

        ModuleDto returnData = await _serviceManager.Modules.UpdateModuleAsync(key, moduleDto);
        return (returnData != null) ? Ok(returnData) : NoContent();
    }

    [HttpDelete(RouteConstants.DeleteModule)]
    public async Task<IActionResult> DeleteModule([FromRoute] int key, [FromBody] ModuleDto moduleDto)
    {
        var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

        await _serviceManager.Modules.DeleteModuleAsync(key, moduleDto);
        return Ok("Success");
    }



}