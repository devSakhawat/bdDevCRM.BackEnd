using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.KendoGrid;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> ModuleSummary([FromBody] GridOptions options)
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


}