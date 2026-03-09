// =========================================================================================================
// 📄 File: CRMApplicationController.WithCancellationToken.Example.cs
// 📝 Description: CancellationToken implementation উদাহরণ - Controller Layer
// 🎯 Purpose: Existing CRMApplicationController কে CancellationToken support সহ refactor করার উদাহরণ
//
// ⚠️ NOTE: এটি একটি EXAMPLE FILE। Production code এ ব্যবহার করার আগে:
//    1. Original CRMApplicationController.cs এ implement করুন
//    2. সব dependencies সঠিকভাবে inject করুন
//    3. Proper testing করুন
//    4. এই example file delete করুন
// =========================================================================================================

using bdDevCRM.Utilities.CRMGrid.GRID;
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

namespace bdDevCRM.Presentation.Controllers.CRM.Examples;

/// <summary>
/// CRMApplicationController with CancellationToken Implementation Example
///
/// এই class টি দেখায় কিভাবে existing controller এ CancellationToken যোগ করতে হয়।
///
/// Key Changes:
/// 1. সব async action methods এ CancellationToken parameter যোগ
/// 2. Service layer calls এ CancellationToken pass করা
/// 3. OperationCanceledException proper handling
/// 4. Logging improvements
/// </summary>
[ApiController]
[Route("api/crm/applications")]
public class CRMApplicationControllerWithCancellationTokenExample : BaseApiController
{
  private readonly IMemoryCache _cache;
  private readonly ILogger<CRMApplicationControllerWithCancellationTokenExample> _logger;
  private readonly IWebHostEnvironment _environment;

  public CRMApplicationControllerWithCancellationTokenExample(
      IServiceManager serviceManager,
      IMemoryCache cache,
      ILogger<CRMApplicationControllerWithCancellationTokenExample> logger,
      IWebHostEnvironment environment) : base(serviceManager)
  {
    _cache = cache;
    _logger = logger;
    _environment = environment;
  }

  /* ========================================
     ✅ EXAMPLE 1: Simple GET endpoint

     Changes:
     - Added CancellationToken parameter
     - Added try-catch for OperationCanceledException
     - Pass token to service layer
  ======================================== */
  [HttpGet(RouteConstants.CRMApplicationStatus)]
  public async Task<IActionResult> StatusByMenuNUserId(
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
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

      if (!currentUser.UserId.HasValue)
        throw new GenericBadRequestException("Valid UserId is required.");

      // ✅ Pass CancellationToken to service
      var res = await _serviceManager.WfState.GetWFStateByMenuNUserPermission(
          rawUrl,
          currentUser.UserId.Value,
          cancellationToken);

      if (res == null)
        return Ok(ApiResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

      return Ok(ApiResponseHelper.Success(res, "Application retrieved successfully"));
    }
    catch (OperationCanceledException)
    {
      // ✅ Handle cancellation gracefully
      _logger.LogInformation("StatusByMenuNUserId request cancelled by client");
      return NoContent(); // Client already gone, no need to send response
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error in StatusByMenuNUserId");
      throw; // Let global exception handler handle it
    }
  }

  /* ========================================
     ✅ EXAMPLE 2: GET with route parameter

     Changes:
     - Added CancellationToken parameter
     - Pass token through the call chain
  ======================================== */
  [HttpGet(RouteConstants.CRMApplicationByApplicationId)]
  public async Task<IActionResult> GetApplication(
      [FromRoute] int applicationId,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
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

      // ✅ Pass CancellationToken to service
      var res = await _serviceManager.CrmApplications.GetApplication(
          applicationId,
          trackChanges: false,
          cancellationToken);

      if (res == null)
        return Ok(ApiResponseHelper.NoContent<IEnumerable<GetApplicationDto>>("No institutes found for the specified country"));

      return Ok(ApiResponseHelper.Success(res, "Application retrieved successfully"));
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation(
          "GetApplication request cancelled for ApplicationId: {ApplicationId}",
          applicationId);
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error getting application {ApplicationId}", applicationId);
      throw;
    }
  }

  /* ========================================
     ✅ EXAMPLE 3: POST with complex body

     Changes:
     - Added CancellationToken parameter
     - Multiple service calls with token
     - Check cancellation between heavy operations
  ======================================== */
  [HttpPost(RouteConstants.CRMApplicationSummary)]
  public async Task<IActionResult> SummaryGrid(
      [FromBody] CRMGridOptions options,
      [FromRoute] int statusId,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
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

      if (!MenuConstant.TryGetPath("CRMApplication", out var menuPath))
        throw new GenericBadRequestException("Invalid menu name.");

      // ✅ Check cancellation before heavy operation
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to first service call
      MenuDto menuDto = await _serviceManager.Groups.CheckMenuPermission(
          $"..{menuPath}",
          currentUser,
          cancellationToken);

      if (menuDto == null)
        throw new GenericBadRequestException("Menu not found for the current user.");

      // ✅ Check cancellation before next heavy operation
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to main service call
      var summaryGrid = await _serviceManager.CrmApplications.SummaryGrid(
          options,
          statusId,
          currentUser,
          menuDto,
          cancellationToken);

      if (summaryGrid == null || !summaryGrid.Items.Any())
        return Ok(ApiResponseHelper.NoContent<GridEntity<CrmApplicationGridDto>>("No data found"));

      return Ok(ApiResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation(
          "SummaryGrid request cancelled for StatusId: {StatusId}",
          statusId);
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error in SummaryGrid for StatusId: {StatusId}", statusId);
      throw;
    }
  }

  /* ========================================
     ✅ EXAMPLE 4: Helper Methods (DDL endpoints)

     Simple GET endpoints জন্য pattern
  ======================================== */
  [HttpGet(RouteConstants.CRMCountryDLL)]
  public async Task<IActionResult> CRMCountryDLL(
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
    {
      if (!TryGetLoggedInUser(out UsersDto currentUser))
      {
        return Unauthorized("User not found in cache.");
      }

      // ✅ Pass CancellationToken to service
      var groupSummary = await _serviceManager.CrmCountries.GetCountriesDDLAsync(
          trackChanges: false,
          cancellationToken);

      return (groupSummary != null) ? Ok(groupSummary) : NoContent();
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation("CRMCountryDLL request cancelled");
      return NoContent();
    }
  }

  [HttpGet(RouteConstants.CRMInstituteDLLByCountry)]
  public async Task<IActionResult> CRMInstituteDLLByCountry(
      [FromQuery] int countryId,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
    {
      if (!TryGetLoggedInUser(out UsersDto currentUser))
      {
        return Unauthorized("User not found in cache.");
      }

      // ✅ Pass CancellationToken to service
      var res = await _serviceManager.CrmInstitutes.GetInstitutesDDLAsync(
          trackChanges: false,
          cancellationToken);

      return Ok(res);
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation(
          "CRMInstituteDLLByCountry request cancelled for CountryId: {CountryId}",
          countryId);
      return NoContent();
    }
  }

  /* ========================================
     ✅ EXAMPLE 5: POST with FormData (File Upload)

     File upload operations এ CancellationToken especially important
     কারণ large file uploads user cancel করতে পারে
  ======================================== */
  [HttpPost(RouteConstants.CRMApplicationCreate)]
  [RequestSizeLimit(50_000_000)]
  public async Task<IActionResult> CreateApplication(
      [FromForm] CrmApplicationDto applicationData,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
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

      // ✅ Check cancellation before heavy validation
      cancellationToken.ThrowIfCancellationRequested();

      var validationResult = ValidateApplicationData(applicationData);
      if (!validationResult.IsValid)
        throw new GenericBadRequestException(validationResult.ErrorMessage);

      // Set default values
      applicationData.ApplicationDate = DateTime.UtcNow;
      applicationData.ApplicationId = 0;
      applicationData.CreatedDate = DateTime.UtcNow;
      applicationData.CreatedBy = userId;
      applicationData.UpdatedDate = null;
      applicationData.UpdatedBy = null;

      InitializeNestedDtos(applicationData);

      // ✅ Check cancellation before database operation
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to service - This may take time
      CrmApplicationDto savedDto = await _serviceManager.CrmApplications.CreateNewRecordAsync(
          applicationData,
          currentUser,
          cancellationToken);

      // ✅ Check cancellation before file operations
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to file saving - This may take significant time
      await SaveCrmApplicationFilesAsync(savedDto, currentUser, cancellationToken);

      if (savedDto.ApplicationId <= 0)
        throw new InvalidCreateOperationException("Failed to create application record.");

      return Ok(ApiResponseHelper.Created(savedDto, "Application created successfully."));
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation("CreateApplication request cancelled during processing");
      // Important: User cancelled during upload, clean up any partial data
      // TODO: Implement cleanup logic if needed
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error creating application");
      throw;
    }
  }

  /* ========================================
     ✅ EXAMPLE 6: PUT (Update with FormData)
  ======================================== */
  [HttpPut(RouteConstants.CRMApplicationUpdate)]
  public async Task<IActionResult> UpdateApplication(
      [FromRoute] int key,
      [FromForm] CrmApplicationDto applicationData,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    try
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

      applicationData.UpdatedDate = DateTime.UtcNow;
      applicationData.UpdatedBy = userId;

      // ✅ Check cancellation before update
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to service
      CrmApplicationDto savedDto = await _serviceManager.CrmApplications.UpdateCrmApplicationAsync(
          key,
          applicationData,
          currentUser,
          cancellationToken);

      // ✅ Check cancellation before file operations
      cancellationToken.ThrowIfCancellationRequested();

      // ✅ Pass CancellationToken to file operations
      await SaveCrmApplicationFilesAsync(savedDto, currentUser, cancellationToken);

      if (savedDto.ApplicationId <= 0)
        throw new InvalidCreateOperationException("Failed to update application record.");

      return Ok(ApiResponseHelper.Created(savedDto, "Application updated successfully."));
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation(
          "UpdateApplication request cancelled for ApplicationId: {ApplicationId}",
          key);
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error updating application {ApplicationId}", key);
      throw;
    }
  }

  /* ========================================
     ✅ EXAMPLE 7: DELETE endpoint
  ======================================== */
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteApplication(
      int id,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
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

      // ✅ Pass CancellationToken to service (when implemented)
      // await _serviceManager.CrmApplications.DeleteAsync(id, cancellationToken);

      return Ok(new
      {
        IsSuccess = true,
        Message = "Application deleted successfully"
      });
    }
    catch (OperationCanceledException)
    {
      _logger.LogInformation("DeleteApplication request cancelled for Id: {Id}", id);
      return NoContent();
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

  /* ========================================
     Private Helper Methods

     Note: এই methods গুলো synchronous, তাই CancellationToken প্রয়োজন নেই
  ======================================== */
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

  private void InitializeNestedDtos(CrmApplicationDto applicationData)
  {
    applicationData.CourseInformation ??= new CourseInformationDto();
    applicationData.CourseInformation.PersonalDetails ??= new ApplicantInfoDto();
    applicationData.CourseInformation.ApplicantCourse ??= new ApplicantCourseDto();
    // ... rest of initialization
  }

  private (bool IsValid, string ErrorMessage) ValidateApplicationData(CrmApplicationDto applicationDto)
  {
    // Validation logic (synchronous, no cancellation needed)
    if (applicationDto.CourseInformation?.PersonalDetails != null)
    {
      var personalDetails = applicationDto.CourseInformation.PersonalDetails;
      if (string.IsNullOrWhiteSpace(personalDetails.FirstName))
        return (false, "First Name is required in Personal Details");

      if (string.IsNullOrWhiteSpace(personalDetails.LastName))
        return (false, "Last Name is required in Personal Details");
    }

    return (true, string.Empty);
  }

  #endregion

  /* ========================================
     ✅ File Operations with CancellationToken

     File operations can be long-running, especially for large files.
     CancellationToken এখানে বিশেষভাবে গুরুত্বপূর্ণ।
  ======================================== */
  private async Task<CrmApplicationDto> SaveCrmApplicationFilesAsync(
      CrmApplicationDto dto,
      UsersDto currentUser,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    int applicantId = dto.CourseInformation.PersonalDetails.ApplicantId;

    /* Save Applicant Image File */
    if (dto.CourseInformation?.PersonalDetails?.ApplicantImageFile != null)
    {
      // ✅ Check cancellation before file operation
      cancellationToken.ThrowIfCancellationRequested();

      var applicantImageDMSDto = new DMSDto
      {
        DocumentTypeName = "Applicant_Image",
        DocumentType = "Applicant_Image",
        Title = $"{dto.CourseInformation.PersonalDetails.FirstName} {dto.CourseInformation.PersonalDetails.LastName}",
        ReferenceEntityType = "ApplicantInfo",
        ReferenceEntityId = applicantId.ToString(),
        // ... other properties
      };

      string applicantImageDMSJson = JsonConvert.SerializeObject(applicantImageDMSDto);

      // ✅ Pass CancellationToken to file save operation
      string applicantImagePath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
          dto.CourseInformation.PersonalDetails.ApplicantImageFile,
          applicantImageDMSJson,
          cancellationToken);

      if (!string.IsNullOrEmpty(applicantImagePath))
      {
        dto.CourseInformation.PersonalDetails.ApplicantImagePath = applicantImagePath;

        // ✅ Pass CancellationToken to update operation
        var resultMessage = await _serviceManager.ApplicantInfos.UpdateRecordAsync(
            dto.CourseInformation.PersonalDetails.ApplicantId,
            dto.CourseInformation.PersonalDetails,
            false,
            cancellationToken);
      }
    }

    /* Save Education History Documents */
    if (dto.EducationInformation?.EducationDetails?.EducationHistory != null &&
        dto.EducationInformation?.EducationDetails?.EducationHistory.Count > 0)
    {
      foreach (var educationRecord in dto.EducationInformation.EducationDetails.EducationHistory)
      {
        // ✅ Check cancellation for each file in loop
        cancellationToken.ThrowIfCancellationRequested();

        if (educationRecord.AttachedDocumentFile != null)
        {
          var educationDocumentDMSDto = new DMSDto
          {
            DocumentTypeName = "Education_Document",
            Title = $"EducationDocument_{educationRecord.Institution}",
            // ... other properties
          };

          string educationDocumentDMSJson = JsonConvert.SerializeObject(educationDocumentDMSDto);

          // ✅ Pass CancellationToken to file save
          string educationDocumentPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
              educationRecord.AttachedDocumentFile,
              educationDocumentDMSJson,
              cancellationToken);

          if (!string.IsNullOrEmpty(educationDocumentPath))
          {
            educationRecord.AttachedDocument = educationDocumentPath;
          }
        }
      }
    }

    // Similar pattern for other file operations...
    // ✅ Always check cancellation in loops
    // ✅ Always pass token to async operations

    return dto;
  }

  private async Task<string> SaveLanguageTestDocumentAsync(
      string entityName,
      string fieldName,
      string testType,
      IFormFile file,
      int applicationId,
      UsersDto currentUser,
      int currentEntityId,
      CancellationToken cancellationToken) // ✅ Added CancellationToken
  {
    // ✅ Check cancellation
    cancellationToken.ThrowIfCancellationRequested();

    var languageTestDMSDto = new DMSDto
    {
      DocumentTypeName = $"{testType}_Test_Document",
      Title = $"{testType}TestResult_{DateTime.Now:yyyyMMdd:FFFFF}",
      // ... other properties
    };

    string languageTestDMSJson = JsonConvert.SerializeObject(languageTestDMSDto);

    // ✅ Pass CancellationToken to file save
    string languageTestPath = await _serviceManager.DmsDocuments.SaveFileAndDocumentWithAllDmsAsync(
        file,
        languageTestDMSJson,
        cancellationToken);

    return languageTestPath;
  }
}

/* ========================================
   📝 IMPLEMENTATION CHECKLIST

   Controller Layer এ CancellationToken implement করার জন্য:

   ✅ 1. সব async action methods এ `CancellationToken cancellationToken` parameter যোগ করুন

   ✅ 2. সব service calls এ token pass করুন:
        await _serviceManager.ServiceName.MethodAsync(..., cancellationToken);

   ✅ 3. Long operations এর আগে/পরে check করুন:
        cancellationToken.ThrowIfCancellationRequested();

   ✅ 4. Loop এ প্রতিটি iteration এ check করুন (especially file operations)

   ✅ 5. OperationCanceledException handle করুন:
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation cancelled");
            return NoContent();
        }

   ✅ 6. File upload/download operations এ অবশ্যই token pass করুন

   ✅ 7. Helper methods গুলো review করুন - async হলে token যোগ করুন

   ⚠️ 8. Original controller এ implement করার পরে এই example file delete করুন

======================================== */
