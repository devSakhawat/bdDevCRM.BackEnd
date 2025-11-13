using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class CRMApplicationController : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly ILogger<CRMApplicationController> _logger;
  private readonly IWebHostEnvironment _environment;


  public CRMApplicationController(IServiceManager serviceManager, IMemoryCache cache, ILogger<CRMApplicationController> logger, IWebHostEnvironment environment) : base(serviceManager)
  {
    _cache = cache;
    _logger = logger;
    _environment = environment;
  }




  [HttpGet(RouteConstants.CRMApplicationStatus)]
  public async Task<IActionResult> StatusByMenuNUserId()
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

    //if (!menu.MenuId.HasValue)
    //  throw new GenericBadRequestException("Valid MenuId is required.");

    if (!currentUser.UserId.HasValue)
      throw new GenericBadRequestException("Valid UserId is required.");

    // menu.MenuId and currentUser.UserId are nullable so we use .Value after checking HasValue
    var res = await _serviceManager.WfState.GetWFStateByMenuNUserPermission(rawUrl, currentUser.UserId.Value);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

    return Ok(ResponseHelper.Success(res, "Application retrieved successfully"));
  }


  // --------- Application by ApplicationId ----------------------------------------
  [HttpGet(RouteConstants.CRMApplicationByApplicationId)]
  //[AllowAnonymous]
  public async Task<IActionResult> GetApplication([FromRoute] int applicationId)
  {
    if (applicationId <= 0)
      throw new GenericBadRequestException("Invalid application ID. Application ID must be greater than 0.");

    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      throw new GenericUnauthorizedException("User authentication required.");

    if (!int.TryParse(userIdClaim, out int userId))
      throw new GenericBadRequestException("Invalid user ID format.");

    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
      throw new GenericUnauthorizedException("User session expired.");

    var res = await _serviceManager.CrmApplications.GetApplication(applicationId, trackChanges: false);
    if (res == null)
      return Ok(ResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

    return Ok(ResponseHelper.Success(res, "Application retrieved successfully"));
  }

  // --------- Summary Grid ----------------------------------------
  [HttpPost(RouteConstants.CRMApplicationSummary)]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options, [FromRoute] int statusId)
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

    //MenuDto menuDto = await ManageMenu.GetByMenuPathAsync(this, );

    if (!MenuConstant.TryGetPath("CRMApplication", out var menuPath))
      throw new GenericBadRequestException("Invalid menu name.");

    MenuDto menuDto = await _serviceManager.Groups.CheckMenuPermission($"..{menuPath}", currentUser);

    if (menuDto == null)
      throw new GenericBadRequestException("Menu not found for the current user.");


    var summaryGrid = await _serviceManager.CrmApplications.SummaryGrid(options, statusId, currentUser ,menuDto );
    if (summaryGrid == null || !summaryGrid.Items.Any())
      return Ok(ResponseHelper.NoContent<GridEntity<CrmApplicationGridDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
  }


  #region Helper Methods
  private bool TryGetLoggedInUser(out UsersDto currentUser)
  {
    currentUser = null;
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
      return false;

    var userId = Convert.ToInt32(userIdClaim);
    currentUser = _serviceManager.GetCache<UsersDto>(userId);
    return currentUser != null;
  }
  #endregion Helper Methods

  #region Course Details start
  [HttpGet(RouteConstants.CRMCountryDLL)]
  public async Task<IActionResult> CRMCountryDLL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var groupSummary = await _serviceManager.CrmCountries.GetCountriesDDLAsync(trackChanges: false);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpGet(RouteConstants.CRMInstituteDLLByCountry)]
  public async Task<IActionResult> CRMInstituteDLLByCountry([FromQuery] int countryId)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CrmInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    return Ok(res);
  }

  [HttpGet(RouteConstants.CRMCourseDLLByInstitute)]
  public async Task<IActionResult> CRMCourseDLLByInstitute([FromQuery] int instituteId)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CrmCourses.GetCourseByInstituteIdDDLAsync(instituteId, trackChanges: false);
    return Ok(res);
  }

  [HttpGet(RouteConstants.CRMInstituteTypeDDL)]
  public async Task<IActionResult> CRMInstituteTypeDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CrmInstituteTypes.GetInstituteTypesDDLAsync();
    return Ok(res);
  }
  #endregion Course Details end

  [HttpPost(RouteConstants.CRMApplicationCreate)]
  [RequestSizeLimit(50_000_000)] // Increased limit for multiple files
  public async Task<IActionResult> CreateApplication([FromForm] CrmApplicationDto applicationData)
  {
    if (applicationData == null)
      throw new NullModelBadRequestException(nameof(CrmApplicationDto));

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

    // Validate application data
    var validationResult = ValidateApplicationData(applicationData);
    if (!validationResult.IsValid)
      throw new GenericBadRequestException(validationResult.ErrorMessage);

    // Set default values
    applicationData.ApplicationDate = DateTime.UtcNow;
    applicationData.ApplicationId = 0; // Ensure ApplicationId is 0 for new record
    applicationData.CreatedDate = DateTime.UtcNow;
    applicationData.CreatedBy = userId;
    applicationData.UpdatedDate = null;
    applicationData.UpdatedBy = null;

    // Initialize nested DTOs if null
    InitializeNestedDtos(applicationData);

    // Save CRM application record
    CrmApplicationDto savedDto = await _serviceManager.CrmApplications.CreateNewRecordAsync(applicationData, currentUser);

    // Save attached files using DMS
    await SaveCrmApplicationFilesAsync(savedDto, currentUser);

    if (savedDto.ApplicationId <= 0)
      throw new InvalidCreateOperationException("Failed to create application record.");

    return Ok(ResponseHelper.Created(savedDto, "Application created successfully."));
  }



  /// <summary>
  /// Initialize nested DTOs to avoid null reference exceptions
  /// </summary>
  private void InitializeNestedDtos(CrmApplicationDto applicationData)
  {
    applicationData.CourseInformation ??= new CourseInformationDto();
    applicationData.CourseInformation.PersonalDetails ??= new ApplicantInfoDto();
    applicationData.CourseInformation.ApplicantCourse ??= new ApplicantCourseDto();
    applicationData.CourseInformation.ApplicantAddress ??= new ApplicantAddressDto();
    applicationData.CourseInformation.ApplicantAddress.PermanentAddress ??= new PermanentAddressDto();
    applicationData.CourseInformation.ApplicantAddress.PresentAddress ??= new PresentAddressDto();

    applicationData.EducationInformation ??= new EducationInformationDto();
    applicationData.EducationInformation.EducationDetails ??= new EducationDetailsDto();
    applicationData.EducationInformation.EducationDetails.EducationHistory ??= new List<EducationHistoryDto>();
    applicationData.EducationInformation.IELTSInformation ??= new IELTSInformationDto();
    applicationData.EducationInformation.TOEFLInformation ??= new TOEFLInformationDto();
    applicationData.EducationInformation.PTEInformation ??= new PTEInformationDto();
    applicationData.EducationInformation.GMATInformation ??= new GMATInformationDto();
    applicationData.EducationInformation.OTHERSInformation ??= new OTHERSInformationDto();
    applicationData.EducationInformation.WorkExperience ??= new WorkExperienceDto();
    applicationData.EducationInformation.WorkExperience.WorkExperienceHistory ??= new List<WorkExperienceHistoryDto>();

    applicationData.AdditionalInformation ??= new AdditionalInformationDto();
    applicationData.AdditionalInformation.ReferenceDetails ??= new ReferenceDetailsDto();
    applicationData.AdditionalInformation.ReferenceDetails.References ??= new List<ApplicantReferenceDto>();
    applicationData.AdditionalInformation.StatementOfPurpose ??= new StatementOfPurposeDto();
    applicationData.AdditionalInformation.AdditionalInformation ??= new AdditionalInfoDto();
    applicationData.AdditionalInformation.AdditionalDocuments ??= new AdditionalDocumentsDto();
    applicationData.AdditionalInformation.AdditionalDocuments.Documents ??= new List<AdditionalDocumentDto>();
  }

  /// <summary>
  /// Validates application data
  /// </summary>
  private (bool IsValid, string ErrorMessage) ValidateApplicationData(CrmApplicationDto applicationDto)
  {
    // Personal Details Validation
    if (applicationDto.CourseInformation?.PersonalDetails != null)
    {
      var personalDetails = applicationDto.CourseInformation.PersonalDetails;
      if (string.IsNullOrWhiteSpace(personalDetails.FirstName))
        return (false, "First Name is required in Personal Details");

      if (string.IsNullOrWhiteSpace(personalDetails.LastName))
        return (false, "Last Name is required in Personal Details");

      if (personalDetails.GenderId <= 0)
        return (false, "Gender selection is required in Personal Details");

      if (personalDetails.DateOfBirth == null)
        return (false, "Date of Birth is required in Personal Details");
    }

    // Course Information Validation
    if (applicationDto.CourseInformation?.ApplicantCourse != null)
    {
      var courseDetails = applicationDto.CourseInformation.ApplicantCourse;
      if (courseDetails.CountryId <= 0)
        return (false, "Country selection is required in Course Information");

      if (courseDetails.InstituteId <= 0)
        return (false, "Institute selection is required in Course Information");

      if (courseDetails.CourseId <= 0)
        return (false, "Course selection is required in Course Information");
    }

    return (true, string.Empty);
  }

  [HttpPut(RouteConstants.CRMApplicationUpdate)]
  //[DisableRequestSizeLimit]
  public async Task<IActionResult> UpdateApplication([FromRoute] int key, [FromForm] CrmApplicationDto applicationData)
  {
    if (applicationData == null)
      throw new NullModelBadRequestException(nameof(CrmApplicationDto));

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

    //// Validate application data
    //var validationResult = ValidateApplicationData(applicationData);
    //if (!validationResult.IsValid)
    //  throw new GenericBadRequestException(validationResult.ErrorMessage);

    // Set default values
    applicationData.UpdatedDate = DateTime.UtcNow;
    applicationData.UpdatedBy = userId;


    // Save CRM application record
    CrmApplicationDto savedDto = await _serviceManager.CrmApplications.UpdateCrmApplicationAsync(key,applicationData, currentUser);

    // Save attached files using DMS
    await SaveCrmApplicationFilesAsync(savedDto, currentUser);

    if (savedDto.ApplicationId <= 0)
      throw new InvalidCreateOperationException("Failed to create application record.");

    return Ok(ResponseHelper.Created(savedDto, "Application created successfully."));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteApplication(int id)
  {
    try
    {
      if (id <= 0)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application ID",
          ErrorType = "ValidationError"
        });
      }

      return Ok(new
      {
        IsSuccess = true,
        Message = "Application deleted successfully"
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error deleting CRM Application with ID: {id}");
      return StatusCode(500, new
      {
        IsSuccess = false,
        Message = "An unexpected error occurred while deleting the application",
        ErrorType = "InternalServerError"
      });
    }
  }

  [HttpGet]
  public async Task<IActionResult> GetApplications([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
  {
    try
    {
      return Ok(new
      {
        IsSuccess = true,
        Data = new { Applications = new List<GetApplicationDto>(), TotalCount = 0 }
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error getting CRM Applications");
      return StatusCode(500, new
      {
        IsSuccess = false,
        Message = "An unexpected error occurred while retrieving applications",
        ErrorType = "InternalServerError"
      });
    }
  }


  /// <summary>
  /// Handles all file uploads for the application
  /// </summary>
  private async Task HandleApplicationFileUploads(CrmApplicationDto applicationDto, IFormFileCollection files)
  {
    if (files == null || files.Count == 0) return;

    var uploadsBasePath = Path.Combine(_environment.WebRootPath, "uploads", "applications");
    Directory.CreateDirectory(uploadsBasePath);

    foreach (var file in files)
    {
      if (file.Length > 0)
      {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsBasePath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }

        var relativePath = $"/uploads/applications/{fileName}";

        // Map files to appropriate properties based on file field names
        switch (file.Name)
        {
          case "ApplicantImageFile":
            if (applicationDto.CourseInformation?.PersonalDetails != null)
              applicationDto.CourseInformation.PersonalDetails.ApplicantImagePath = relativePath;
            break;
          case "IELTSScannedCopyFile":
            if (applicationDto.EducationInformation?.IELTSInformation != null)
              applicationDto.EducationInformation.IELTSInformation.IELTSScannedCopyPath = relativePath;
            break;
          case "TOEFLScannedCopyFile":
            if (applicationDto.EducationInformation?.TOEFLInformation != null)
              applicationDto.EducationInformation.TOEFLInformation.TOEFLScannedCopyPath = relativePath;
            break;
          case "PTEScannedCopyFile":
            if (applicationDto.EducationInformation?.PTEInformation != null)
              applicationDto.EducationInformation.PTEInformation.PTEScannedCopyPath = relativePath;
            break;
          case "GMATScannedCopyFile":
            if (applicationDto.EducationInformation?.GMATInformation != null)
              applicationDto.EducationInformation.GMATInformation.GMATScannedCopyPath = relativePath;
            break;
          case "OTHERSScannedCopyFile":
            if (applicationDto.EducationInformation?.OTHERSInformation != null)
              applicationDto.EducationInformation.OTHERSInformation.OTHERSScannedCopyPath = relativePath;
            break;
          case "StatementOfPurposeFile":
            if (applicationDto.AdditionalInformation?.StatementOfPurpose != null)
              applicationDto.AdditionalInformation.StatementOfPurpose.StatementOfPurposeFilePath = relativePath;
            break;
          default:
            // Handle education documents and additional documents
            if (file.Name.StartsWith("EducationDocumentFile_"))
            {
              // Handle education document files
            }
            else if (file.Name.StartsWith("WorkExperienceDocumentFile_"))
            {
              // Handle work experience document files
            }
            else if (file.Name.StartsWith("AdditionalDocumentFile_"))
            {
              // Handle additional document files
            }
            break;
        }
      }
    }
  }

  private (bool IsValid, string ErrorMessage) ValidateFileUploads(IFormFileCollection files)
  {
    return (true, string.Empty);
  }


  /* ========================================
     APPLICATION FILES 
  ======================================== */
  private async Task<CrmApplicationDto> SaveCrmApplicationFilesAsync(CrmApplicationDto dto, UsersDto currentUser)
  {
    // Get application ID - use override or dto's ApplicationId
    int applicantId = dto.CourseInformation.PersonalDetails.ApplicantId;

    /* ========================================
       COURSE INFORMATION SECTION FILES
    ======================================== */

    /* ---------- Save Applicant Image File (ApplicantInfo Entity) ---------- */
    if (dto.CourseInformation?.PersonalDetails?.ApplicantImageFile != null)
    {
      var applicantImageDMSDto = new DMSDto
      {
        // DocumentType properties
        DocumentTypeName = "Applicant_Image",
        DocumentType = "Applicant_Image",
        IsMandatory = false,
        AcceptedExtensions = ".jpg,.jpeg,.png",
        MaxFileSizeMb = 2,

        // Document properties
        Title = $"{dto.CourseInformation.PersonalDetails.FirstName} {dto.CourseInformation.PersonalDetails.LastName} {DateTime.Now:yyyyMMdd:FFFFF}",
        Description = $"Applicant image for {dto.CourseInformation.PersonalDetails.FirstName} {dto.CourseInformation.PersonalDetails.LastName}",
        ReferenceEntityType = "ApplicantInfo", // Entity name
        ReferenceEntityId = applicantId.ToString(), // applicant id (in crm module it is applicant id, hr module it is hr id)
        CurrentEntityId = dto.CourseInformation?.PersonalDetails.ApplicantId,
        UploadedByUserId = currentUser.UserId.ToString(),
        SystemTags = "ApplicantImagePath", // DTO field name

        // Folder properties
        FolderName = $"ApplicantInfo_{applicantId}",
        OwnerId = applicantId.ToString(),

        // Access Log properties when user accesses the document
        AccessedByUserId = currentUser.UserId.ToString(),
        AccessDateTime = DateTime.UtcNow,
        Action = "Upload",

        // Tag properties
        DocumentTagName = "Image,Applicant,Personal",

        // Version properties
        VersionNumber = 1,
        UploadedBy = currentUser.UserId.ToString(),
        UploadedDate = DateTime.UtcNow
      };

      string applicantImageDMSJson = JsonConvert.SerializeObject(applicantImageDMSDto);
      string applicantImagePath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
          dto.CourseInformation.PersonalDetails.ApplicantImageFile, applicantImageDMSJson);

      if (!string.IsNullOrEmpty(applicantImagePath))
      {
        dto.CourseInformation.PersonalDetails.ApplicantImagePath = applicantImagePath;
        var resultMessage = _serviceManager.ApplicantInfos.UpdateRecordAsync(dto.CourseInformation.PersonalDetails.ApplicantId, dto.CourseInformation.PersonalDetails, false);
      }
    }

    /* ========================================
       EDUCATION INFORMATION SECTION FILES
    ======================================== */

    /* ---------- Save Education History Documents (EducationHistory Entity) ---------- */
    if (dto.EducationInformation?.EducationDetails?.EducationHistory != null && dto.EducationInformation?.EducationDetails?.EducationHistory.Count > 0)
    {
      foreach (var educationRecord in dto.EducationInformation.EducationDetails.EducationHistory)
      {
        if (educationRecord.AttachedDocumentFile != null)
        {
          var educationDocumentDMSDto = new DMSDto
          {
            // DocumentType properties
            DocumentTypeName = "Education_Document",
            DocumentType = "Document",
            IsMandatory = false,
            AcceptedExtensions = ".pdf,.doc,.docx,.jpg,.jpeg,.png",
            MaxFileSizeMb = 5,

            // Document properties
            Title = $"EducationDocument_{educationRecord.Institution}_{DateTime.Now:yyyyMMdd:FFFFF}",
            Description = $"Education document for {educationRecord.Institution} - {educationRecord.Qualification} - {educationRecord.PassingYear}",
            ReferenceEntityType = "EducationHistory", // Entity name
            ReferenceEntityId = applicantId.ToString(), // applicant id
            CurrentEntityId = educationRecord.EducationHistoryId,
            UploadedByUserId = currentUser.UserId.ToString(),
            SystemTags = "AttachedDocument", // File path property name

            // Folder properties
            FolderName = $"EducationHistory_{applicantId}",
            OwnerId = currentUser.UserId.ToString(),

            // Access Log properties
            AccessedByUserId = currentUser.UserId.ToString(),
            AccessDateTime = DateTime.UtcNow,
            Action = "Upload",

            // Tag properties
            DocumentTagName = "Document,Education,Academic",

            // Version properties
            VersionNumber = 1,
            UploadedBy = currentUser.UserId.ToString(),
            UploadedDate = DateTime.UtcNow,


            FileExtension = Path.GetExtension(educationRecord.AttachedDocumentFile.FileName),
            FileName = educationRecord.AttachedDocumentFile.FileName,
            FileSize = educationRecord.AttachedDocumentFile.Length,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),

          };

          string educationDocumentDMSJson = JsonConvert.SerializeObject(educationDocumentDMSDto);
          string educationDocumentPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
              educationRecord.AttachedDocumentFile, educationDocumentDMSJson);

          if (!string.IsNullOrEmpty(educationDocumentPath))
          {
            educationRecord.AttachedDocument = educationDocumentPath;
          }
        }
      }
    }

    /* ---------- Save IELTS Scanned Copy (IELTSInformation Entity) ---------- */
    if (dto.EducationInformation?.IELTSInformation?.IELTSScannedCopyFile != null)
    {
      dto.EducationInformation.IELTSInformation.IELTSScannedCopyPath = await SaveLanguageTestDocumentAsync(
          "IELTSInformation", // Entity name
          "IELTSScannedCopyPath", // DTO field name
          "IELTS",
          dto.EducationInformation.IELTSInformation.IELTSScannedCopyFile,
          applicantId,
          currentUser,
          dto.EducationInformation.IELTSInformation.IELTSInformationId
      );
    }

    /* ---------- Save TOEFL Scanned Copy (TOEFLInformation Entity) ---------- */
    if (dto.EducationInformation?.TOEFLInformation?.TOEFLScannedCopyFile != null)
    {
      dto.EducationInformation.TOEFLInformation.TOEFLScannedCopyPath = await SaveLanguageTestDocumentAsync(
          "TOEFLInformation", // Entity name
          "TOEFLScannedCopyPath", // DTO field name
          "TOEFL",
          dto.EducationInformation.TOEFLInformation.TOEFLScannedCopyFile,
          applicantId,
          currentUser,
          dto.EducationInformation.TOEFLInformation.TOEFLInformationId
      );
    }

    /* ---------- Save PTE Scanned Copy (PTEInformation Entity) ---------- */
    if (dto.EducationInformation?.PTEInformation?.PTEScannedCopyFile != null)
    {
      dto.EducationInformation.PTEInformation.PTEScannedCopyPath = await SaveLanguageTestDocumentAsync(
          "PTEInformation", // Entity name
          "PTEScannedCopyPath", // DTO field name
          "PTE",
          dto.EducationInformation.PTEInformation.PTEScannedCopyFile,
          applicantId,
          currentUser,
          dto.EducationInformation.PTEInformation.PTEInformationId
      );
    }

    /* ---------- Save GMAT Scanned Copy (GMATInformation Entity) ---------- */
    if (dto.EducationInformation?.GMATInformation?.GMATScannedCopyFile != null)
    {
      dto.EducationInformation.GMATInformation.GMATScannedCopyPath = await SaveLanguageTestDocumentAsync(
          "GMATInformation", // Entity name
          "GMATScannedCopyPath", // DTO field name
          "GMAT",
          dto.EducationInformation.GMATInformation.GMATScannedCopyFile,
          applicantId,
          currentUser,
          dto.EducationInformation.GMATInformation.GMATInformationId
      );
    }

    /* ---------- Save OTHERS Language Test Scanned Copy (OTHERSInformation Entity) ---------- */
    if (dto.EducationInformation?.OTHERSInformation?.OTHERSScannedCopyFile != null)
    {
      dto.EducationInformation.OTHERSInformation.OTHERSScannedCopyPath = await SaveLanguageTestDocumentAsync(
          "OTHERSInformation", // Entity name
          "OTHERSScannedCopyPath", // DTO field name
          "OTHERS",
          dto.EducationInformation.OTHERSInformation.OTHERSScannedCopyFile,
          applicantId,
          currentUser,
          dto.EducationInformation.OTHERSInformation.OthersInformationId
      );
    }

    /* ---------- Save Work Experience Documents (WorkExperience Entity) ---------- */
    if (dto.EducationInformation?.WorkExperience?.WorkExperienceHistory != null && dto.EducationInformation?.WorkExperience?.WorkExperienceHistory.Count > 0)
    {
      foreach (var workExpRecord in dto.EducationInformation.WorkExperience.WorkExperienceHistory)
      {
        if (workExpRecord.ScannedCopyFile != null)
        {
          var workExperienceDMSDto = new DMSDto
          {
            // DocumentType properties
            ApplicantId = applicantId,
            DocumentTypeName = "Work_Experience_Document",
            DocumentType = "Document",
            IsMandatory = false,
            AcceptedExtensions = ".pdf,.doc,.docx,.jpg,.jpeg,.png",
            MaxFileSizeMb = 5,

            // Document properties
            Title = $"WorkExperience_{workExpRecord.NameOfEmployer}_{DateTime.Now:yyyyMMdd:FFFFF}",
            Description = $"Work experience document for {workExpRecord.NameOfEmployer} - {workExpRecord.Position}",
            ReferenceEntityType = "WorkExperience", // Entity name
            ReferenceEntityId = applicantId.ToString(), // ApplicationID
            CurrentEntityId = workExpRecord.WorkExperienceId,
            UploadedByUserId = currentUser.UserId.ToString(),
            SystemTags = "ScannedCopyPath", // DTO field name

            // Folder properties
            FolderName = $"WorkExperience_{applicantId}",
            OwnerId = currentUser.UserId.ToString(),

            // Access Log properties
            AccessedByUserId = currentUser.UserId.ToString(),
            AccessDateTime = DateTime.UtcNow,
            Action = "Upload",

            // Tag properties
            DocumentTagName = "Document,WorkExperience,Professional",

            // Version properties
            VersionNumber = 1,
            UploadedBy = currentUser.UserId.ToString(),
            UploadedDate = DateTime.UtcNow
          };

          string workExperienceDMSJson = JsonConvert.SerializeObject(workExperienceDMSDto);
          string workExperiencePath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
              workExpRecord.ScannedCopyFile, workExperienceDMSJson);

          if (!string.IsNullOrEmpty(workExperiencePath))
          {
            workExpRecord.ScannedCopyPath = workExperiencePath;
          }
        }
      }
    }

    /* ========================================
       ADDITIONAL INFORMATION SECTION FILES
    ======================================== */

    /* ---------- Save Statement of Purpose File (StatementOfPurpose Entity) ---------- */
    if (dto.AdditionalInformation?.StatementOfPurpose?.StatementOfPurposeFile != null)
    {
      var statementDMSDto = new DMSDto
      {
        // DocumentType properties
        DocumentTypeName = "Statement_Of_Purpose",
        DocumentType = "Document",
        IsMandatory = false,
        AcceptedExtensions = ".pdf,.doc,.docx",
        MaxFileSizeMb = 5,

        // Document properties
        Title = $"StatementOfPurpose_{DateTime.Now:yyyyMMdd:FFFFF}",
        Description = $"Statement of Purpose for application {applicantId}",
        ReferenceEntityType = "StatementOfPurpose", // Entity name
        ReferenceEntityId = applicantId.ToString(), // ApplicationID
        CurrentEntityId = dto.AdditionalInformation?.StatementOfPurpose?.StatementOfPurposeId,
        UploadedByUserId = currentUser.UserId.ToString(),
        SystemTags = "StatementOfPurposeFile", // DTO field name

        // Folder properties
        FolderName = $"StatementOfPurpose_{applicantId}",
        OwnerId = currentUser.UserId.ToString(),

        // Access Log properties
        AccessedByUserId = currentUser.UserId.ToString(),
        AccessDateTime = DateTime.UtcNow,
        Action = "Upload",

        // Tag properties
        DocumentTagName = "Document,StatementOfPurpose,Additional",

        // Version properties
        VersionNumber = 1,
        UploadedBy = currentUser.UserId.ToString(),
        UploadedDate = DateTime.UtcNow
      };

      string statementDMSJson = JsonConvert.SerializeObject(statementDMSDto);
      string statementPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
          dto.AdditionalInformation.StatementOfPurpose.StatementOfPurposeFile, statementDMSJson);

      if (!string.IsNullOrEmpty(statementPath))
      {
        dto.AdditionalInformation.StatementOfPurpose.StatementOfPurposeFilePath = statementPath;
      }
    }


    /* ---------- Save Additional Documents (AdditionalDocument Entity) ---------- */
    if (dto.AdditionalInformation?.AdditionalDocuments?.Documents != null &&
        dto.AdditionalInformation.AdditionalDocuments.Documents.Any())
    {
      foreach (var additionalDoc in dto.AdditionalInformation.AdditionalDocuments.Documents)
      {
        if (additionalDoc.UploadFormFile != null)
        {
          var additionalDocDMSDto = new DMSDto
          {
            // DocumentType properties
            DocumentTypeName = "Additional_Document",
            DocumentType = "Document",
            IsMandatory = false,
            AcceptedExtensions = ".pdf,.doc,.docx,.jpg,.jpeg,.png",
            MaxFileSizeMb = 5,

            // Document properties
            Title = $"AdditionalDocument_{additionalDoc.DocumentTitle}_{DateTime.Now:yyyyMMdd:FFFFF}",
            Description = $"Additional document: {additionalDoc.DocumentTitle} for application {applicantId}",
            ReferenceEntityType = "AdditionalDocument", // Entity name - mapped to AdditionalDocument entity 
            ReferenceEntityId = applicantId.ToString(), // ApplicationID
            CurrentEntityId = additionalDoc.AdditionalDocumentId,
            UploadedByUserId = currentUser.UserId.ToString(),
            SystemTags = "UploadFileFormFile", // DTO field name

            // Folder properties
            FolderName = $"AdditionalDocument_{applicantId}",
            OwnerId = currentUser.UserId.ToString(),

            // Access Log properties
            AccessedByUserId = currentUser.UserId.ToString(),
            AccessDateTime = DateTime.UtcNow,
            Action = "Upload",

            // Tag properties
            DocumentTagName = "Document,Additional,Supplementary",

            // Version properties
            VersionNumber = 1,
            UploadedBy = currentUser.UserId.ToString(),
            UploadedDate = DateTime.UtcNow
          };

          string additionalDocDMSJson = JsonConvert.SerializeObject(additionalDocDMSDto);
          string additionalDocPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
              additionalDoc.UploadFormFile, additionalDocDMSJson);

          if (!string.IsNullOrEmpty(additionalDocPath))
          {
            additionalDoc.DocumentPath = additionalDocPath;
          }
        }
      }
    }

    return dto;
  }

  /* ========================================
     HELPER METHOD FOR LANGUAGE TEST DOCUMENTS
  ======================================== */
  private async Task<string> SaveLanguageTestDocumentAsync(string entityName, string fieldName, string testType, IFormFile file, int applicationId, UsersDto currentUser ,int currentEntityId)
  {
    var languageTestDMSDto = new DMSDto
    {
      // DocumentType properties
      DocumentTypeName = $"{testType}_Test_Document",
      DocumentType = "Document",
      IsMandatory = false,
      AcceptedExtensions = ".pdf,.doc,.docx,.jpg,.jpeg,.png",
      MaxFileSizeMb = 5,

      // Document properties
      Title = $"{testType}TestResult_{DateTime.Now:yyyyMMdd:FFFFF}",
      Description = $"{testType} test result for application {applicationId}",
      ReferenceEntityType = entityName, // Entity name (e.g., "IELTSInformation")
      ReferenceEntityId = applicationId.ToString(), // ApplicationID
      CurrentEntityId = currentEntityId, // ownentitiy id
      UploadedByUserId = currentUser.UserId.ToString(),
      SystemTags = fieldName, // DTO field name (e.g., "IELTSScannedCopyFile")

      // Folder properties
      FolderName = $"{entityName}_{applicationId}",
      OwnerId = currentUser.UserId.ToString(),

      // Access Log properties
      AccessedByUserId = currentUser.UserId.ToString(),
      AccessDateTime = DateTime.UtcNow,
      Action = "Upload",

      // Tag properties
      DocumentTagName = $"Document,{testType},LanguageTest,TestResult",

      // Version properties
      VersionNumber = 1,
      UploadedBy = currentUser.UserId.ToString(),
      UploadedDate = DateTime.UtcNow
    };

    string languageTestDMSJson = JsonConvert.SerializeObject(languageTestDMSDto);
    string languageTestPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(file, languageTestDMSJson);
    return languageTestPath;
  }



}






