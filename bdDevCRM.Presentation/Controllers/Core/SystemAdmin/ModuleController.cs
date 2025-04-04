//using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
//using bdDevCRM.Utilities.KendoGrid;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class ModuleController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;
  public ModuleController(IServiceManager serviceManager, IMemoryCache cache)
  {
    _serviceManager = serviceManager;
    _cache = cache;
  }

  //[HttpGet("SelectMenuByUserPermission")]
  [HttpPost(RouteConstants.ModuleSummary)]
  //[Produces("application/json")]
  //[ResponseCache(Duration = 300)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> ModuleSummary([FromBody] CRMGridOptions options)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    var moduleSummary = await _serviceManager.Modules.ModuleSummary(false, options);
    return (moduleSummary != null) ? Ok(moduleSummary) : NoContent();
  }


  //[HttpGet("SelectMenuByUserPermission")]
  [HttpGet(RouteConstants.Modules)]
  //[Produces("application/json")]
  [ResponseCache(Duration = 60)] // Browser caching for 5 minutes
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetModulesAsync()
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    var modulesDto = await _serviceManager.Modules.GetModulesAsync(false);
    return Ok(modulesDto.ToList());
  }

  [HttpPost(RouteConstants.CreateModule)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> SaveModule([FromBody] ModuleDto moduleDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    var module = await _serviceManager.Modules.CreateModuleAsync(moduleDto);
    return (module != null) ? Ok(module) : NoContent();
  }

  [HttpPut(RouteConstants.UpdateModule)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateModule([FromRoute] int key, [FromBody] ModuleDto moduleDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    ModuleDto returnData = await _serviceManager.Modules.UpdateModuleAsync(key, moduleDto);
    return (returnData != null) ? Ok(returnData) : NoContent();
  }

  [HttpDelete(RouteConstants.DeleteModule)]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteModule([FromRoute] int key, [FromBody] ModuleDto moduleDto)
  {
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

    await _serviceManager.Modules.DeleteModuleAsync(key, moduleDto);
    return Ok("Success");
  }



}