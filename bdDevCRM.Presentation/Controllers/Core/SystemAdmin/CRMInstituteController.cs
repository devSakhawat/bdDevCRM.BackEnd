using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;


public class CRMInstituteController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly IWebHostEnvironment _env;

  public CRMInstituteController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
    _env = env;
  }

  // --------- 1. DDL --------------------------------------------------
  [HttpGet(RouteConstants.InstituteDDL)]
  public async Task<IActionResult> InstituteDDL()
  {
    //UsersDto loggedInUser;
    //var userIdClaim = User.FindFirst("UserId")?.Value;
    //if (string.IsNullOrEmpty(userIdClaim))
    //  return Unauthorized("Unauthorized attempt to get data!");

    //int userId = Convert.ToInt32(userIdClaim);
    //var currentUser = TryGetLoggedInUser(out loggedInUser);
    //if (currentUser == null)
    //  return Unauthorized("User not found in cache.");
    int userId = HttpContext.GetUserId();
    var currentUser = HttpContext.GetCurrentUser();


    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    return Ok(res);
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.InstituteSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    //var userIdClaim = User.FindFirst("UserId")?.Value;
    //if (string.IsNullOrEmpty(userIdClaim))
    //  return Unauthorized("Unauthorized attempt to get data!");

    //int userId = Convert.ToInt32(userIdClaim);
    //UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    //if (currentUser == null)
    //  return Unauthorized("User not found in cache.");

    //if (options == null)
    //  return BadRequest("CRMGridOptions cannot be null.");

    int userId = HttpContext.GetUserId();
    var currentUser = HttpContext.GetCurrentUser();


    var summaryGrid = await _serviceManager.CRMInstitutes.SummaryGrid(options);
    return (summaryGrid != null) ? Ok(summaryGrid) : NoContent();
  }

  /* --------------------------------------------- */
  /*  POST: /crm-institute  (Create)               */
  /* --------------------------------------------- */
  [HttpPost(RouteConstants.CreateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> CreateNewRecord([FromForm] CrmInstituteDto modelDto)
  {
    int userId = HttpContext.GetUserId();
    var currentUser = HttpContext.GetCurrentUser();

    var res = await _serviceManager.CRMInstitutes.CreateNewRecordAsync(modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateInstitute([FromRoute] int key, [FromForm] CrmInstituteDto modelDto)
  {
    //var userIdClaim = User.FindFirst("UserId")?.Value;
    //if (string.IsNullOrEmpty(userIdClaim))
    //  return Unauthorized("UserId not found in token.");

    //int userId = Convert.ToInt32(userIdClaim);
    //UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    //if (currentUser == null)
    //  return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.UpdateRecordAsync(key, modelDto, false);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 5. Delete ----------------------------------------------
  [HttpDelete(RouteConstants.DeleteInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteInstitute([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    //var userIdClaim = User.FindFirst("UserId")?.Value;
    //if (string.IsNullOrEmpty(userIdClaim))
    //  return Unauthorized("UserId not found in token.");

    //int userId = Convert.ToInt32(userIdClaim);
    //UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    //if (currentUser == null)
    //  return Unauthorized("UnAuthorized attempted");
    int userId = HttpContext.GetUserId();
    var currentUser = HttpContext.GetCurrentUser();


    var res = await _serviceManager.CRMInstitutes.DeleteRecordAsync(key, modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }

  // --------- 6. SaveOrUpdate ----------------------------------------
  [HttpPost(RouteConstants.CreateOrUpdateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    //var userIdClaim = User.FindFirst("UserId")?.Value;
    //if (string.IsNullOrEmpty(userIdClaim))
    //  return Unauthorized("UserId not found in token.");

    //int userId = Convert.ToInt32(userIdClaim);
    //UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    //if (currentUser == null)
    //  return Unauthorized("User not found in cache.");

    int userId = HttpContext.GetUserId();
    var currentUser = HttpContext.GetCurrentUser();

    var res = await _serviceManager.CRMInstitutes.SaveOrUpdateAsync(key, modelDto);
    return (res == OperationMessage.Success) ? Ok(res) : Conflict(res);
  }


  /* =================================================================
      PRIVATE HELPERS
   ==================================================================*/


  // 'File' Suffix is mendatory to use this function for every file or image fields.
  private async Task SaveInstituteFilesAsync(CrmInstituteDto dto, int? idOverride = null)
  {
    // 🔔 ইনস্টিটিউট আইডি—নতুন হলে GuidHash (temp) ব্যবহার
    int id = idOverride ?? dto.InstituteId;
    if (id == 0) id = Guid.NewGuid().GetHashCode();

    // 📂 রুট ফোল্ডার: wwwroot/uploads/institutes/{id}
    string root = Path.Combine(_env.WebRootPath, "uploads", "institutes", id.ToString());
    if (!Directory.Exists(root))
      Directory.CreateDirectory(root);

    /* ---------- Logo ---------- */
    if (dto.InstitutionLogoFile != null)
    {
      string ext = Path.GetExtension(dto.InstitutionLogoFile.FileName);
      string path = Path.Combine(root, "logo" + ext);

      await using var fs = new FileStream(path, FileMode.Create);
      await dto.InstitutionLogoFile.CopyToAsync(fs);

      dto.InstitutionLogo = $"/uploads/institutes/{id}/logo{ext}";
    }

    /* ---------- Prospectus ---------- */
    if (dto.InstitutionProspectusFile != null)
    {
      string ext = Path.GetExtension(dto.InstitutionProspectusFile.FileName);
      string path = Path.Combine(root, "prospectus" + ext);

      await using var fs = new FileStream(path, FileMode.Create);
      await dto.InstitutionProspectusFile.CopyToAsync(fs);

      dto.InstitutionProspectus = $"/uploads/institutes/{id}/prospectus{ext}";
    }
  }

}