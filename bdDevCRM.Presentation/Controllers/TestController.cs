using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Region;
using Newtonsoft.Json;

namespace bdDevCRM.Presentation.Controllers;


//public class TestController : BaseApiController
//{
//  private readonly IMemoryCache _cache;
//  private readonly IWebHostEnvironment _env;

//  public TestController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
//  {
//    //_serviceManager = serviceManager;
//    _cache = cache;
//    _env = env;
//  }

//  // --------- 1. DDL --------------------------------------------------

//  // GitHub Copilot: generate the code by using ResponseHelper for this method InstituteDDL.
//  [HttpGet(RouteConstants.TestDDL)]
// [Produces("text/csv")] // CSV format
//  public async Task<IActionResult> TestDDL()
//  {
//    int userId = HttpContext.GetUserId();
//    var currentUser = HttpContext.GetCurrentUser();

//    var res = await _serviceManager.CrmInstitutes.GetInstitutesDDLAsync(trackChanges: false);
//    if (res == null || !res.Any())
//      return Ok(ResponseHelper.NoContent<IEnumerable<CrmInstituteDto>>("No institutes found"));

//    return Ok(ResponseHelper.Success(res, "Institutes retrieved successfully"));
//  }

//  // --------- 2. Summary Grid ----------------------------------------
//  [HttpPost(RouteConstants.InstituteSummary)]
//  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
//  {
//    var userIdClaim = User.FindFirst("UserId")?.Value;
//    if (string.IsNullOrEmpty(userIdClaim))
//      return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token"));

//    int userId = Convert.ToInt32(userIdClaim);
//    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
//    if (currentUser == null)
//      return Unauthorized(ResponseHelper.Unauthorized("User not found in cache"));

//    if (options == null)
//      return BadRequest(ResponseHelper.BadRequest("CRMGridOptions cannot be null"));


//    var summaryGrid = await _serviceManager.CrmInstitutes.SummaryGrid(options);
//    //return (summaryGrid != null) ? Ok(summaryGrid) : NoContent();
//    if (summaryGrid == null || !summaryGrid.Items.Any())
//      return Ok(ResponseHelper.NoContent<GridEntity<CrmInstituteDto>>("No data found"));

//    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
//  }


//  [HttpPost(RouteConstants.TestCreate)]
//  [RequestSizeLimit(100_000_000)]
//  [AllowAnonymous]
//  public async Task<IActionResult> CreateNewRecord(IFormCollection form)
//  {
//    var userIdClaim = User.FindFirst("UserId")?.Value;
//    if (string.IsNullOrEmpty(userIdClaim))
//      return Unauthorized("UserId not found in token.");

//    int userId = Convert.ToInt32(userIdClaim);
//    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
//    if (currentUser == null)
//      return Unauthorized("User not found in cache.");

//    var modelDto = form["modelDto"]; // JSON String as a normal field
//    if (string.IsNullOrEmpty(modelDto))
//      return BadRequest("Model DTO cannot be null or empty");
//    var logoFile = form.Files["InstitutionLogoFile"];
//    var prospectusFile = form.Files["InstitutionProspectusFile"];

//    var instituteModel = JsonConvert.DeserializeObject<CrmInstituteDto>(modelDto);
//    instituteModel.InstitutionLogoFile = logoFile;
//    instituteModel.InstitutionProspectusFile = prospectusFile;

//    CrmInstituteDto res = await _serviceManager.CrmInstitutes.CreateNewRecordAsync(instituteModel, currentUser);
//    // DMS call

//    if (res.InstituteId > 0)
//      return Ok(ResponseHelper.Created(res, "Institute created successfully"));
//    else
//      return StatusCode(500 ,ResponseHelper.InternalServerError("Operation failed!"));
//  }

//  // --------- 4. Update ----------------------------------------------
//  [HttpPut(RouteConstants.TestUpdate)]
//  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
//  public async Task<IActionResult> TestUpdate([FromRoute] int key, [FromForm] CrmInstituteDto modelDto)
//  {
//    try
//    {
//      var userIdClaim = User.FindFirst("UserId")?.Value;
//      if (string.IsNullOrEmpty(userIdClaim))
//        return Unauthorized(ResponseHelper.Unauthorized("UserId not found in token."));

//      int userId = Convert.ToInt32(userIdClaim);
//      UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
//      if (currentUser == null)
//        return Unauthorized(ResponseHelper.Unauthorized("User not found in cache."));

//      var res = await _serviceManager.CrmInstitutes.UpdateRecordAsync(key, modelDto, false);

//      return Ok(ResponseHelper.Success(res, "Institute updated successfully"));
//    }
//    catch (Exception ex)
//    {
//      // Global Exception Middleware will handle it
//      throw;
//    }
//  }


//  [HttpDelete(RouteConstants.TestDelete)]
//  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
//  public async Task<IActionResult> DeleteInstitute([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
//  {
//    try
//    {
//      int userId = HttpContext.GetUserId();
//      var currentUser = HttpContext.GetCurrentUser();

//      var res = await _serviceManager.CrmInstitutes.DeleteRecordAsync(key, modelDto);
//      return Ok(ResponseHelper.Success("Institute deleted successfully"));
//    }
//    catch (Exception ex)
//    {
//      // Global Exception Middleware will handle it
//      throw;
//    }
//  }

//  [HttpGet(RouteConstants.TestSearch)]
//  public async Task<IActionResult> SearchInstitutes(string query)
//  {
//    var results = await _serviceManager.CrmInstitutes.GetInstitutesDDLAsync();
//    if (!results.Any())
//      return Ok(ResponseHelper.NoContent<List<CrmInstituteDto>>("No institutes found"));

//    return Ok(ResponseHelper.Success(results));
//  }

//  //[HttpDelete("{id}")]
//  //public async Task<IActionResult> DeleteInstitute(int id)
//  //{
//  //  var currentUser = GetCurrentUser();
//  //  var institute = await _service.GetByIdAsync(id);

//  //  if (institute.CreatedBy != currentUser.Id && !currentUser.IsAdmin)
//  //    return Forbid(ResponseHelper.Forbidden("You don't have permission to delete this institute"));

//  //  // Process...
//  //}

//  //[HttpGet("{id}")]
//  //public async Task<IActionResult> GetInstitute(int id)
//  //{
//  //  var institute = await _service.GetByIdAsync(id);
//  //  if (institute == null)
//  //    return NotFound(ResponseHelper.NotFound("Institute not found"));

//  //  return Ok(ResponseHelper.Success(institute));
//  //}

//  //[HttpPost]
//  //public async Task<IActionResult> CreateInstitute(CreateInstituteDto dto)
//  //{
//  //  if (!ModelState.IsValid)
//  //  {
//  //    var errors = ModelState
//  //        .Where(x => x.Value.Errors.Count > 0)
//  //        .ToDictionary(
//  //            kvp => kvp.Key,
//  //            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
//  //        );

//  //    return UnprocessableEntity(ResponseHelper.ValidationError("Validation failed", errors));
//  //  }

//  //  // Process...
//  //}

//  //[HttpPost]
//  //public async Task<IActionResult> CreateInstitute(CreateInstituteDto dto)
//  //{
//  //  if (!ModelState.IsValid)
//  //  {
//  //    var errors = ModelState
//  //        .Where(x => x.Value.Errors.Count > 0)
//  //        .ToDictionary(
//  //            kvp => kvp.Key,
//  //            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
//  //        );

//  //    return UnprocessableEntity(ResponseHelper.ValidationError("Validation failed", errors));
//  //  }

//  //  // Process...
//  //}

//  //[HttpPost]
//  //public async Task<IActionResult> CreateInstitute(CreateInstituteDto dto)
//  //{
//  //  try
//  //  {
//  //    var result = await _service.CreateAsync(dto);
//  //    return Created($"/api/institutes/{result.Id}",
//  //                   ResponseHelper.Created(result, "Institute created successfully"));
//  //  }
//  //  catch (Exception ex)
//  //  {
//  //    _logger.LogError(ex, "Error creating institute");
//  //    return StatusCode(500, ResponseHelper.InternalServerError("An error occurred while creating the institute"));
//  //  }
//  //}

//}