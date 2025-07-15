using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Principal;
using System.Text.Json;

public class CRMApplicationController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache;

  private readonly ILogger<CRMApplicationController> _logger;
  private readonly IWebHostEnvironment _environment;

  public CRMApplicationController(IServiceManager serviceManager, IMemoryCache cache, ILogger<CRMApplicationController> logger, IWebHostEnvironment environment) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _cache = cache;
    _logger = logger;
    _environment = environment;
  }


  #region Course Details start
  [HttpGet(RouteConstants.CRMCountryDLL)]
  public async Task<IActionResult> CRMCountryDLL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("UserId not found in token.");
    }
    var userId = Convert.ToInt32(userIdClaim);
    // userId : which key is responsible to when cache was created.
    // get user from cache. if cache is not found by key then it will throw Unauthorized exception with 401 status code.
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var groupSummary = await _serviceManager.Countries.GetCountriesDDLAsync(trackChanges: false);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpGet(RouteConstants.CRMInstituteDLLByCountry)]
  public async Task<IActionResult> CRMInstituteDLLByCountry([FromQuery] int countryId)
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("Unauthorized attempt to get data!");
    }
    var userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    //var res = await _serviceManager.CRMInstitutes.CRMInstituteDLLByCountry(countryId, trackChanges: false);

    return Ok(res);
  }

  [HttpGet(RouteConstants.CRMInstituteTypeDDL)]
  public async Task<IActionResult> CRMInstituteTypeDDL()
  {
    var userIdClaim = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userIdClaim))
    {
      return Unauthorized("Unauthorized attempt to get data!");
    }
    var userId = Convert.ToInt32(userIdClaim);
    UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);

    if (currentUser == null)
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstituteTypes.GetInstituteTypesDDLAsync();

    return Ok(res);
  }
  #endregion  Course Details end

  //////////////////////////////////////////////////////


  //////////////////////////////////////////////////////

  /// <summary>
  /// Creates a new CRM Application
  /// </summary>
  [HttpPost(RouteConstants.CRMApplication)]
  public async Task<IActionResult> CreateApplication([FromForm] string ApplicationData, [FromForm] IFormFileCollection files)
  {
    try
    {
      _logger.LogInformation("Creating new CRM Application");

      // Deserialize the complex application data
      var applicationDto = JsonSerializer.Deserialize<CrmApplicationDto>(ApplicationData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

      if (applicationDto == null)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application data",
          ErrorType = "ValidationError"
        });
      }

      // Validate required fields
      var validationResult = ValidateApplicationData(applicationDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = validationResult.ErrorMessage,
          ErrorType = "ValidationError"
        });
      }

      // Handle all file uploads
      await HandleApplicationFileUploads(applicationDto, files);

      // TODO: Add your business logic here
      // Example:
      // var createdApplication = await _applicationService.CreateAsync(applicationDto);

      // For now, return success response with generated ID
      var newApplicationId = 1; // Replace with actual generated ID

      return Ok(new
      {
        IsSuccess = true,
        Message = "Application created successfully",
        Data = new { ApplicationId = newApplicationId }
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error creating CRM Application");
      return StatusCode(500, new
      {
        IsSuccess = false,
        Message = "An unexpected error occurred while creating the application",
        ErrorType = "InternalServerError"
      });
    }
  }

  /// <summary>
  /// Updates an existing CRM Application
  /// </summary>
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateApplication(int id, [FromForm] string ApplicationData, [FromForm] IFormFileCollection files)
  {
    try
    {
      _logger.LogInformation($"Updating CRM Application with ID: {id}");

      if (id <= 0)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application ID",
          ErrorType = "ValidationError"
        });
      }

      // Deserialize the application data
      var applicationDto = JsonSerializer.Deserialize<CrmApplicationDto>(ApplicationData, new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      });

      if (applicationDto == null)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application data",
          ErrorType = "ValidationError"
        });
      }

      // Ensure ID matches
      applicationDto.ApplicationId = id;

      // Validate application data
      var validationResult = ValidateApplicationData(applicationDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = validationResult.ErrorMessage,
          ErrorType = "ValidationError"
        });
      }

      // Handle file uploads
      await HandleApplicationFileUploads(applicationDto, files);

      // TODO: Add your business logic here
      // Example:
      // var updatedApplication = await _applicationService.UpdateAsync(id, applicationDto);

      return Ok(new
      {
        IsSuccess = true,
        Message = "Application updated successfully",
        Data = new { ApplicationId = id }
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error updating CRM Application with ID: {id}");
      return StatusCode(500, new
      {
        IsSuccess = false,
        Message = "An unexpected error occurred while updating the application",
        ErrorType = "InternalServerError"
      });
    }
  }

  /// <summary>
  /// Gets a CRM Application by ID
  /// </summary>
  [HttpGet("{id}")]
  public async Task<IActionResult> GetApplication(int id)
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

      // TODO: Add your business logic here
      // Example:
      // var application = await _applicationService.GetByIdAsync(id);

      // For now, return sample data
      var sampleApplication = new CrmApplicationDto
      {
        ApplicationId = id,
        ApplicationDate = DateTime.UtcNow,
        ApplicationStatus = "Draft"
      };

      return Ok(new
      {
        IsSuccess = true,
        Data = sampleApplication
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error getting CRM Application with ID: {id}");
      return StatusCode(500, new
      {
        IsSuccess = false,
        Message = "An unexpected error occurred while retrieving the application",
        ErrorType = "InternalServerError"
      });
    }
  }

  /// <summary>
  /// Deletes a CRM Application by ID
  /// </summary>
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

      // TODO: Add your business logic here
      // Example:
      // await _applicationService.DeleteAsync(id);

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

  /// <summary>
  /// Gets all CRM Applications with pagination
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> GetApplications([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
  {
    try
    {
      // TODO: Add your business logic here
      // Example:
      // var applications = await _applicationService.GetPagedAsync(page, pageSize);

      return Ok(new
      {
        IsSuccess = true,
        Data = new { Applications = new List<CrmApplicationDto>(), TotalCount = 0 }
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
  /// Validates application data
  /// </summary>
  private (bool IsValid, string ErrorMessage) ValidateApplicationData(CrmApplicationDto applicationDto)
  {
    // Course Information Validation
    if (applicationDto.CourseInformation?.applicantCourse != null)
    {
      var courseDetails = applicationDto.CourseInformation.applicantCourse;
      if (courseDetails.CountryId <= 0)
      {
        return (false, "Country selection is required in Course Information");
      }
    }

    // Personal Details Validation
    if (applicationDto.CourseInformation?.personalDetails != null)
    {
      var personalDetails = applicationDto.CourseInformation.personalDetails;
      if (string.IsNullOrWhiteSpace(personalDetails.FirstName))
      {
        return (false, "First Name is required in Personal Details");
      }
      if (string.IsNullOrWhiteSpace(personalDetails.LastName))
      {
        return (false, "Last Name is required in Personal Details");
      }
      if (personalDetails.GenderId <= 0)
      {
        return (false, "Gender selection is required in Personal Details");
      }
      if (personalDetails.DateOfBirth == null)
      {
        return (false, "Date of Birth is required in Personal Details");
      }
    }

    // Additional validations can be added here for other sections

    return (true, string.Empty);
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
            if (applicationDto.CourseInformation?.personalDetails != null)
              applicationDto.CourseInformation.personalDetails.ApplicantImagePath = relativePath;
            break;
          case "IELTSScannedCopyFile":
            if (applicationDto.EducationInformation?.ieltsInformation != null)
              applicationDto.EducationInformation.ieltsInformation.IELTSScannedCopyPath = relativePath;
            break;
          case "TOEFLScannedCopyFile":
            if (applicationDto.EducationInformation?.toeflInformation != null)
              applicationDto.EducationInformation.toeflInformation.TOEFLScannedCopyPath = relativePath;
            break;
          case "PTEScannedCopyFile":
            if (applicationDto.EducationInformation?.pteInformation != null)
              applicationDto.EducationInformation.pteInformation.PTEScannedCopyPath = relativePath;
            break;
          case "GMATScannedCopyFile":
            if (applicationDto.EducationInformation?.gmatInformation != null)
              applicationDto.EducationInformation.gmatInformation.GMATScannedCopyPath = relativePath;
            break;
          case "OTHERSScannedCopyFile":
            if (applicationDto.EducationInformation?.othersInformation != null)
              applicationDto.EducationInformation.othersInformation.OTHERSScannedCopyPath = relativePath;
            break;
          case "StatementOfPurposeFile":
            if (applicationDto.AdditionalInformation?.statementOfPurpose != null)
              applicationDto.AdditionalInformation.statementOfPurpose.StatementOfPurposeFilePath = relativePath;
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
}

/// <summary>
/// Complete CRM Application Data Transfer Object
/// </summary>
public class CrmApplicationDto
{
  public int ApplicationId { get; set; }
  public DateTime ApplicationDate { get; set; }
  public string ApplicationStatus { get; set; } = "Draft";

  public CourseInformationDto? CourseInformation { get; set; }
  public EducationInformationDto? EducationInformation { get; set; }
  public AdditionalInformationDto? AdditionalInformation { get; set; }
}

/// <summary>
/// Course Information Section DTO
/// </summary>
public class CourseInformationDto
{
  public ApplicantCourseDto? applicantCourse { get; set; }
  public ApplicantInfoDto? personalDetails { get; set; }
  public ApplicantAddressDto? applicantAddress { get; set; }
}


/// <summary>
/// Applicant Course Form Details DTO
/// </summary>
#region Course Form
public class ApplicantCourseDto
{
  public int ApplicantCourseId { get; set; }
  public int CountryId { get; set; }
  public string? CountryName { get; set; }
  public int InstituteId { get; set; }
  public string? InstituteName { get; set; }
  public string? CourseTitle { get; set; }
  public int IntakeMonthId { get; set; }
  public string? IntakeMonth { get; set; }
  public int IntakeYearId { get; set; }
  public string? IntakeYear { get; set; }
  public string? ApplicationFee { get; set; }
  public int CurrencyId { get; set; }
  public string? CurrencyName { get; set; }
  public int PaymentMethodId { get; set; }
  public string? PaymentMethod { get; set; }
  public string? PaymentReferenceNumber { get; set; }
  public DateTime? PaymentDate { get; set; }
  public string? Remarks { get; set; }
}

/// <summary>
/// Personal Details DTO
/// </summary>
public class ApplicantInfoDto
{
  public int ApplicantInfoId { get; set; }
  public int GenderId { get; set; }
  public string? GenderName { get; set; }
  public string? TitleValue { get; set; }
  public string? TitleText { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public int MaritalStatusId { get; set; }
  public string? MaritalStatusName { get; set; }
  public string? Nationality { get; set; }
  public string? HasValidPassport { get; set; }
  public string? PassportNumber { get; set; }
  public DateTime? PassportIssueDate { get; set; }
  public DateTime? PassportExpiryDate { get; set; }
  public string? PhoneCountryCode { get; set; }
  public string? PhoneAreaCode { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Mobile { get; set; }
  public string? EmailAddress { get; set; }
  public string? SkypeId { get; set; }
  public string? ApplicantImagePath { get; set; }
}

/// <summary>
/// Applicant Address DTO
/// </summary>
public class ApplicantAddressDto
{
  public PermanentAddressDto? PermanentAddress { get; set; }
  public PresentAddressDto? PresentAddress { get; set; }
}

public class PermanentAddressDto
{
  public int PermanentAddressId { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public int CountryId { get; set; }
  public string? CountryName { get; set; }
  public string? PostalCode { get; set; }
}

public class PresentAddressDto
{
  public int PresentAddressId { get; set; }
  public bool SameAsPermanentAddress { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public int CountryId { get; set; }
  public string? CountryName { get; set; }
  public string? PostalCode { get; set; }
}

#endregion Course Form

/// <summary>
/// Education And English From Section DTOs
/// </summary>
#region Education and English Language Form

public class EducationInformationDto
{
  public EducationDetailsDto? educationDetails { get; set; }
  public IELTSInformationDto? ieltsInformation { get; set; }
  public TOEFLInformationDto? toeflInformation { get; set; }
  public PTEInformationDto? pteInformation { get; set; }
  public GMATInformationDto? gmatInformation { get; set; }
  public OTHERSInformationDto? othersInformation { get; set; }
  public WorkExperienceDto? workExperience { get; set; }
}

public class EducationDetailsDto
{
  public List<EducationHistoryDto>? EducationHistory { get; set; }
  public int TotalEducationRecords { get; set; }
}

public class EducationHistoryDto
{
  public int EducationHistoryId { get; set; }
  public string? Institution { get; set; }
  public string? Qualification { get; set; }
  public int? PassingYear { get; set; }
  public string? Grade { get; set; }
  public string? AttachedDocument { get; set; }
  public string? DocumentName { get; set; }
  public string? PdfThumbnail { get; set; }
}

public class IELTSInformationDto
{
  public int IELTSInformationId { get; set; }
  public string? IELTSListening { get; set; }
  public string? IELTSReading { get; set; }
  public string? IELTSWriting { get; set; }
  public string? IELTSSpeaking { get; set; }
  public string? IELTSOverallScore { get; set; }
  public DateTime? IELTSDate { get; set; }
  public string? IELTSScannedCopyPath { get; set; }
  public string? IELTSAdditionalInformation { get; set; }
}

public class TOEFLInformationDto
{
  public int TOEFLInformationId { get; set; }
  public string? TOEFLListening { get; set; }
  public string? TOEFLReading { get; set; }
  public string? TOEFLWriting { get; set; }
  public string? TOEFLSpeaking { get; set; }
  public string? TOEFLOverallScore { get; set; }
  public DateTime? TOEFLDate { get; set; }
  public string? TOEFLScannedCopyPath { get; set; }
  public string? TOEFLAdditionalInformation { get; set; }
}

public class PTEInformationDto
{
  public int PTEInformationId { get; set; }
  public string? PTEListening { get; set; }
  public string? PTEReading { get; set; }
  public string? PTEWriting { get; set; }
  public string? PTESpeaking { get; set; }
  public string? PTEOverallScore { get; set; }
  public DateTime? PTEDate { get; set; }
  public string? PTEScannedCopyPath { get; set; }
  public string? PTEAdditionalInformation { get; set; }
}

public class GMATInformationDto
{
  public int PTEInformationId { get; set; }
  public string? GMATListening { get; set; }
  public string? GMATReading { get; set; }
  public string? GMATWriting { get; set; }
  public string? GMATSpeaking { get; set; }
  public string? GMATOverallScore { get; set; }
  public DateTime? GMATDate { get; set; }
  public string? GMATScannedCopyPath { get; set; }
  public string? GMATAdditionalInformation { get; set; }
}

public class OTHERSInformationDto
{
  public int OTHERSInformationId { get; set; }
  public string? OTHERSAdditionalInformation { get; set; }
  public string? OTHERSScannedCopyPath { get; set; }
}

public class WorkExperienceDto
{
  public List<WorkExperienceHistoryDto>? WorkExperienceHistory { get; set; }
  public int TotalWorkExperienceRecords { get; set; }
}

public class WorkExperienceHistoryDto
{
  public int WorkExperienceId { get; set; }
  public string? NameOfEmployer { get; set; }
  public string? Position { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
  public string? Period { get; set; }
  public string? MainResponsibility { get; set; }
  public string? ScannedCopy { get; set; }
  public string? DocumentName { get; set; }
  public string? FileThumbnail { get; set; }
}

#endregion Education and English Language Form

           
/// <summary>
/// Additional Information Section DTOs
/// </summary>
#region Additional Information Form Section
public class AdditionalInformationDto
{
  public ReferenceDetailsDto? referenceDetails { get; set; }
  public StatementOfPurposeDto? statementOfPurpose { get; set; }
  public AdditionalInfoDto? additionalInformation { get; set; }
  public AdditionalDocumentsDto? additionalDocuments { get; set; }
}

public class ReferenceDetailsDto
{
  public List<ReferenceDto>? References { get; set; }
  public int TotalReferenceRecords { get; set; }
}

public class ReferenceDto
{
  public int ApplicantRefferenceId { get; set; }
  public string Name { get; set; }
  public string? Designation { get; set; }
  public string? Institution { get; set; }
  public string? EmailID { get; set; }
  public string? PhoneNo { get; set; }
  public string? FaxNo { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? Country { get; set; }
  public string? PostOrZipCode { get; set; }
}

public class StatementOfPurposeDto
{
  public string? StatementOfPurposeRemarks { get; set; }
  public string? StatementOfPurposeFilePath { get; set; }
}

public class AdditionalInfoDto
{
  public int AdditionalInfo { get; set; }
  public string? RequireAccommodation { get; set; }
  public string? HealthNMedicalNeeds { get; set; }
  public string? HealthNMedicalNeedsRemarks { get; set; }
  public string? AdditionalInformationRemarks { get; set; }
}

public class AdditionalDocumentsDto
{
  public List<AdditionalDocumentDto>? Documents { get; set; }
  public int TotalDocumentRecords { get; set; }
}

public class AdditionalDocumentDto
{
  public int DocumentId { get; set; }
  public string? Title { get; set; }
  public string? UploadFile { get; set; }
  public string? DocumentName { get; set; }
  public string? FileThumbnail { get; set; }
}
#endregion  Additional Information Form Section


