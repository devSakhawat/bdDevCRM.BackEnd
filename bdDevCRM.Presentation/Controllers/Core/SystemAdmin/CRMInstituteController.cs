using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

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

  // GitHub Copilot: generate the code by using ResponseHelper for this method InstituteDDL.
  [HttpGet(RouteConstants.InstituteDDL)]
  public async Task<IActionResult> InstituteDDL()
  {
    //int userId = HttpContext.GetUserId();
    //var currentUser = HttpContext.GetCurrentUser();

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return Unauthorized("Unauthorized attempt to get data!");

    int userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null) return Unauthorized("User not found in cache.");

    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmInstituteDto>>("No institutes found"));

    return Ok(ResponseHelper.Success(res, "Institutes retrieved successfully"));
  }

  [HttpGet(RouteConstants.InstituteDDLByCountryId)]
  public async Task<IActionResult> InstituteDDLByCountryId(int countryId)
  {
    // Parameter validation
    if (countryId <= 0)
      throw new GenericBadRequestException("Invalid country ID. Country ID must be greater than 0.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.CRMInstitutes.GetInstitutesByCountryIdDDLAsync(countryId, trackChanges: false);
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CrmInstituteDto>>("No institutes found for the specified country"));

    return Ok(ResponseHelper.Success(res, "Institutes retrieved successfully"));
  }

  // --------- 2. Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.InstituteSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    if (options == null)
      throw new NullModelBadRequestException(nameof(CRMGridOptions));

    var summaryGrid = await _serviceManager.CRMInstitutes.SummaryGrid(options);
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CrmInstituteDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }

  /* --------------------------------------------- */
  /*  POST: /crm-institute  (Create)               */
  /* --------------------------------------------- */
  [HttpPost(RouteConstants.CreateInstitute)]
  [RequestSizeLimit(5_000_000)]
  public async Task<IActionResult> CreateNewRecord([FromForm] CrmInstituteDto form)
  {
    if (form == null)
      throw new NullModelBadRequestException(nameof(CrmInstituteDto));

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    if (!Request.HasFormContentType)
      throw new GenericBadRequestException("Invalid content type. Expected multipart/form-data.");

    // Save institute record
    CrmInstituteDto savedDto = await _serviceManager.CRMInstitutes.CreateNewRecordAsync(form, currentUser);

    // Save attached files (Logo, Prospectus)
    await SaveInstituteFilesAsync(savedDto, currentUser);

    if (savedDto.InstituteId <= 0)
      throw new InvalidCreateOperationException("Failed to create institute record.");

    return Ok(ResponseHelper.Created(savedDto, "Institute created successfully."));
  }

  //public async Task<IActionResult> CreateNewRecord(IFormCollection form)
  //{
  //  try
  //  {
  //    var userIdClaim = User.FindFirst("UserId")?.Value;
  //    if (string.IsNullOrEmpty(userIdClaim))
  //      return Unauthorized(ResponseHelper.Unauthorized("User authentication required"));

  //    int userId = Convert.ToInt32(userIdClaim);
  //    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
  //    if (currentUser == null)
  //      return Unauthorized(ResponseHelper.Unauthorized("User session expired"));

  //    var modelDto = form["modelDto"];
  //    if (string.IsNullOrEmpty(modelDto))
  //      return BadRequest(ResponseHelper.BadRequest("Institute data is required"));

  //    var logoFile = form.Files["InstitutionLogoFile"];
  //    var prospectusFile = form.Files["InstitutionProspectusFile"];

  //    var instituteModel = JsonConvert.DeserializeObject<CrmInstituteDto>(modelDto);
  //    instituteModel.InstitutionLogoFile = logoFile;
  //    instituteModel.InstitutionProspectusFile = prospectusFile;

  //    CrmInstituteDto res = await _serviceManager.CRMInstitutes.CreateNewRecordAsync(instituteModel, currentUser);
  //    await SaveInstituteFilesAsync(res, currentUser);

  //    if (res.InstituteId > 0)
  //      return Ok(ResponseHelper.Created(res, "Institute created successfully"));
  //    else
  //      return StatusCode(500, ResponseHelper.InternalServerError("Failed to create institute"));
  //  }
  //  catch (JsonException)
  //  {
  //    return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in institute data"));
  //  }
  //}


  // --------- 4. Update ----------------------------------------------
  [HttpPut(RouteConstants.UpdateInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> UpdateInstitute([FromRoute] int key, [FromForm] CrmInstituteDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(CrmInstituteDto));

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    if (!Request.HasFormContentType)
      throw new GenericBadRequestException("Invalid content type. Expected multipart/form-data.");

    // Get existing institute record
    CrmInstituteDto updatedDto = await _serviceManager.CRMInstitutes.UpdateRecordAsync(key, modelDto, false);


    // Save attached files (Logo, Prospectus)
    await SaveInstituteFilesAsync(modelDto, currentUser);

    //
    if (updatedDto.InstituteId <= 0)
      throw new InvalidUpdateOperationException("Failed to update institute record.");

    // Return success response
    return Ok(ResponseHelper.Success(updatedDto, "Institute updated successfully"));
  }


  [HttpDelete(RouteConstants.DeleteInstitute)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> DeleteInstitute([FromRoute] int key, [FromBody] CrmInstituteDto modelDto)
  {
    try
    {
      int userId = HttpContext.GetUserId();
      var currentUser = HttpContext.GetCurrentUser();

      var res = await _serviceManager.CRMInstitutes.DeleteRecordAsync(key, modelDto);

      if (res == OperationMessage.Success)
        return Ok(ResponseHelper.Success(res, "Institute deleted successfully"));
      else
        return Conflict(ResponseHelper.Conflict(res));
    }
    catch (Exception ex)
    {
      // Global Exception Middleware will handle it
      throw;
    }
  }


  /* =================================================================
      PRIVATE HELPERS
   ==================================================================*/

  // 'File' Suffix is mendatory to use this function for every file or image fields.
  private async Task SaveInstituteFilesAsync(CrmInstituteDto dto, UsersDto currentUser)
  {
    // Get institute ID - use override or dto's InstituteId
    int id = dto.InstituteId;
    if (id == 0) id = Guid.NewGuid().GetHashCode();

    /* ---------- Save Institution Logo File via DMS ---------- */
    if (dto.InstitutionLogoFile != null)
    {
      // Create DMSDto object for Logo file
      var logoDMSDto = new DMSDto
      {
        // DocumentType properties
        DocumentTypeName = "Institution_Logo",
        DocumentType = "Logo",
        IsMandatory = true,
        AcceptedExtensions = ".png",
        MaxFileSizeMb = 1,

        // Document properties
        Title = $"Logo_{dto.InstituteName}_{DateTime.Now:yyyyMMdd:FFFFF}",
        Description = $"Institution logo for {dto.InstituteName}",
        ReferenceEntityType = "CRMInstitute",
        ReferenceEntityId = id.ToString(),
        UploadedByUserId = currentUser.UserId.ToString(),
        SystemTags = "InstitutionLogo",

        // Folder properties
        FolderName = $"CRMInstitute_{id}",
        OwnerId = currentUser.UserId.ToString(),

        // Access Log properties
        AccessedByUserId = currentUser.UserId.ToString(),
        AccessDateTime = DateTime.UtcNow,
        Action = "Upload",

        // Tag properties
        DocumentTagName = "Logo,Institution,Image",

        // Version properties
        VersionNumber = 1,
        UploadedBy = currentUser.UserId.ToString(),
        UploadedDate = DateTime.UtcNow
      };

      // Convert DMSDto to JSON string
      string logoDMSJson = JsonConvert.SerializeObject(logoDMSDto);

      // Call DMS service to save file and create all DMS entities
      string logoFilePath = await _serviceManager.Dmsdocuments.SaveFileAndDocumentWithAllDmsAsync( dto.InstitutionLogoFile, logoDMSJson );

      // Update DTO with the returned file path
      if (!string.IsNullOrEmpty(logoFilePath))
      {
        dto.InstitutionLogo = logoFilePath;
      }
    }

    /* ---------- Save Institution Prospectus File via DMS ---------- */
    if (dto.InstitutionProspectusFile != null)
    {
      // Create DMSDto object for Prospectus file
      var prospectusDMSDto = new DMSDto
      {
        // DocumentType properties
        DocumentTypeName = "Institution Prospectus",
        DocumentType = "Prospectus",
        IsMandatory = false,
        AcceptedExtensions = ".pdf,.doc,.docx",
        MaxFileSizeMb = 5,

        // Document properties
        Title = $"Prospectus_{dto.InstituteName}_{DateTime.Now:yyyyMMdd:FFFF}",
        Description = $"Institution prospectus for {dto.InstituteName}",
        ReferenceEntityType = "CRMInstitute",
        ReferenceEntityId = id.ToString(),
        UploadedByUserId = currentUser.UserId.ToString(),
        SystemTags = "InstitutionProspectus",

        // Folder properties
        FolderName = $"CRMInstitute_{id}",
        OwnerId = currentUser.UserId.ToString(),

        // Access Log properties
        AccessedByUserId = currentUser.UserId.ToString(),
        AccessDateTime = DateTime.UtcNow,
        Action = "Upload",

        // Tag properties
        DocumentTagName = "Prospectus,Institution,Document",

        // Version properties
        VersionNumber = 1,
        UploadedBy = currentUser.UserId.ToString(),
        UploadedDate = DateTime.UtcNow
      };

      // Convert DMSDto to JSON string
      string prospectusDMSJson = JsonConvert.SerializeObject(prospectusDMSDto);

      // Call DMS service to save file and create all DMS entities
      string prospectusFilePath = await _serviceManager.Dmsdocuments.SaveFileAndDocumentWithAllDmsAsync(dto.InstitutionProspectusFile, prospectusDMSJson
      );

      // Update DTO with the returned file path
      if (!string.IsNullOrEmpty(prospectusFilePath))
      {
        dto.InstitutionProspectus = prospectusFilePath;
      }
    }
  }

  //// Update Institute Files Method with Version Control Strategy
  //private async Task UpdateInstituteFilesWithVersioningAsync(CrmInstituteDto updatedDto, CrmInstituteDto existingDto, UsersDto currentUser)
  //{
  //  int id = updatedDto.InstituteId;

  //  /* ---------- Update Institution Logo File with Versioning ---------- */
  //  if (updatedDto.InstitutionLogoFile != null)
  //  {
  //    // Get existing document info for versioning
  //    //var existingLogoDocument = await GetExistingDocumentAsync(id.ToString(), "CRMInstitute", "Logo");
  //    //int nextVersionNumber = await GetNextVersionNumberAsync(existingLogoDocument?.DocumentId ?? 0);

  //    // Create new version of logo file
  //    var logoDMSDto = new DMSDto
  //    {
  //      // DocumentType properties
  //      DocumentTypeName = "Institution_Logo",
  //      DocumentType = "Logo",
  //      IsMandatory = true,
  //      AcceptedExtensions = ".png",
  //      MaxFileSizeMb = 1,

  //      // Document properties
  //      Title = $"Logo_{updatedDto.InstituteName}_{DateTime.Now:yyyyMMdd}_v{nextVersionNumber}",
  //      Description = $"Institution logo for {updatedDto.InstituteName} (Version {nextVersionNumber})",
  //      ReferenceEntityType = "CRMInstitute",
  //      ReferenceEntityId = id.ToString(),
  //      UploadedByUserId = currentUser.UserId.ToString(),
  //      SystemTags = "InstitutionLogo,Updated",

  //      // Folder properties
  //      FolderName = $"CRMInstitute_{id}",
  //      OwnerId = currentUser.UserId.ToString(),

  //      // Access Log properties
  //      AccessedByUserId = currentUser.UserId.ToString(),
  //      AccessDateTime = DateTime.UtcNow,
  //      Action = "Update",

  //      // Tag properties
  //      DocumentTagName = "Logo,Institution,Image,Updated",

  //      // Version properties
  //      VersionNumber = nextVersionNumber,
  //      UploadedBy = currentUser.UserId.ToString(),
  //      UploadedDate = DateTime.UtcNow,

  //      // For versioning - reference to existing document
  //      ExistingDocumentId = existingLogoDocument?.DocumentId
  //    };

  //    string logoDMSJson = JsonConvert.SerializeObject(logoDMSDto);
  //    string logoFilePath = await _serviceManager.Dmsdocuments.SaveFileAndDocumentWithVersioningAsync(
  //        updatedDto.InstitutionLogoFile,
  //        logoDMSJson
  //    );

  //    if (!string.IsNullOrEmpty(logoFilePath))
  //    {
  //      updatedDto.InstitutionLogo = logoFilePath;
  //      // Update database with new logo path
  //      await _serviceManager.CRMInstitutes.UpdateLogoPathAsync(id, logoFilePath);

  //      // Create file update history
  //      await CreateFileUpdateHistoryAsync(id.ToString(), "CRMInstitute", "Logo",
  //          existingDto.InstitutionLogo, logoFilePath, nextVersionNumber, currentUser);
  //    }
  //  }

  //  /* ---------- Update Institution Prospectus File with Versioning ---------- */
  //  if (updatedDto.InstitutionProspectusFile != null)
  //  {
  //    // Get existing document info for versioning
  //    var existingProspectusDocument = await GetExistingDocumentAsync(id.ToString(), "CRMInstitute", "Prospectus");
  //    int nextVersionNumber = await GetNextVersionNumberAsync(existingProspectusDocument?.DocumentId ?? 0);

  //    // Create new version of prospectus file
  //    var prospectusDMSDto = new DMSDto
  //    {
  //      // DocumentType properties
  //      DocumentTypeName = "Institution Prospectus",
  //      DocumentType = "Prospectus",
  //      IsMandatory = false,
  //      AcceptedExtensions = ".pdf,.doc,.docx",
  //      MaxFileSizeMb = 5,

  //      // Document properties
  //      Title = $"Prospectus_{updatedDto.InstituteName}_{DateTime.Now:yyyyMMdd}_v{nextVersionNumber}",
  //      Description = $"Institution prospectus for {updatedDto.InstituteName} (Version {nextVersionNumber})",
  //      ReferenceEntityType = "CRMInstitute",
  //      ReferenceEntityId = id.ToString(),
  //      UploadedByUserId = currentUser.UserId.ToString(),
  //      SystemTags = "InstitutionProspectus,Updated",

  //      // Folder properties
  //      FolderName = $"CRMInstitute_{id}",
  //      OwnerId = currentUser.UserId.ToString(),

  //      // Access Log properties
  //      AccessedByUserId = currentUser.UserId.ToString(),
  //      AccessDateTime = DateTime.UtcNow,
  //      Action = "Update",

  //      // Tag properties
  //      DocumentTagName = "Prospectus,Institution,Document,Updated",

  //      // Version properties
  //      VersionNumber = nextVersionNumber,
  //      UploadedBy = currentUser.UserId.ToString(),
  //      UploadedDate = DateTime.UtcNow,

  //      // For versioning - reference to existing document
  //      ExistingDocumentId = existingProspectusDocument?.DocumentId
  //    };

  //    string prospectusDMSJson = JsonConvert.SerializeObject(prospectusDMSDto);
  //    string prospectusFilePath = await _serviceManager.Dmsdocuments.SaveFileAndDocumentWithVersioningAsync(
  //        updatedDto.InstitutionProspectusFile,
  //        prospectusDMSJson
  //    );

  //    if (!string.IsNullOrEmpty(prospectusFilePath))
  //    {
  //      updatedDto.InstitutionProspectus = prospectusFilePath;
  //      // Update database with new prospectus path
  //      await _serviceManager.CRMInstitutes.UpdateProspectusPathAsync(id, prospectusFilePath);

  //      // Create file update history
  //      await CreateFileUpdateHistoryAsync(id.ToString(), "CRMInstitute", "Prospectus",
  //          existingDto.InstitutionProspectus, prospectusFilePath, nextVersionNumber, currentUser);
  //    }
  //  }
  //}




  private async Task SaveInstituteFilesAsync2(CrmInstituteDto dto, int? idOverride = null)
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

