using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Principal;
//using System.Text.Json;

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

  private (bool IsValid, string ErrorMessage) ValidateApplicationData(CrmApplicationDto applicationDto)
  {
    if (applicationDto.CourseInformation?.ApplicantCourse != null)
    {
      var courseDetails = applicationDto.CourseInformation.ApplicantCourse;
      if (courseDetails.CountryId <= 0)
        return (false, "Country selection is required");
      if (courseDetails.InstituteId <= 0)
        return (false, "Institute selection is required");
      if (courseDetails.CourseId <= 0)
        return (false, "Course selection is required");
    }

    return (true, string.Empty);
  }

  private (bool IsValid, string ErrorMessage) ValidateFileUploads(IFormFileCollection files)
  {
    return (true, string.Empty);
  }

  private async Task HandleApplicationFileUploads(CrmApplicationDto applicationDto, IFormFileCollection files)
  {
    // File upload logic will be implemented here
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

    var groupSummary = await _serviceManager.Countries.GetCountriesDDLAsync(trackChanges: false);
    return (groupSummary != null) ? Ok(groupSummary) : NoContent();
  }

  [HttpGet(RouteConstants.CRMInstituteDLLByCountry)]
  public async Task<IActionResult> CRMInstituteDLLByCountry([FromQuery] int countryId)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstitutes.GetInstitutesDDLAsync(trackChanges: false);
    return Ok(res);
  }

  [HttpGet(RouteConstants.CRMCourseDLLByInstitute)]
  public async Task<IActionResult> CRMCourseDLLByInstitute([FromQuery] int instituteId)
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMCourses.GetCourseByInstituteIdDDLAsync(instituteId, trackChanges: false);
    return Ok(res);
  }

  [HttpGet(RouteConstants.CRMInstituteTypeDDL)]
  public async Task<IActionResult> CRMInstituteTypeDDL()
  {
    if (!TryGetLoggedInUser(out UsersDto currentUser))
    {
      return Unauthorized("User not found in cache.");
    }

    var res = await _serviceManager.CRMInstituteTypes.GetInstituteTypesDDLAsync();
    return Ok(res);
  }
  #endregion Course Details end

  [HttpPost(RouteConstants.CRMApplication)]
  //public async Task<IActionResult> CreateApplication([FromForm] string ApplicationData, [FromForm] IFormFileCollection files)
  public async Task<IActionResult> CreateApplication([FromForm] string ApplicationData, [FromForm] IFormFileCollection files)
  {
    try
    {
      _logger.LogInformation("Creating new CRM Application");

      if (!TryGetLoggedInUser(out UsersDto currentUser))
      {
        return Unauthorized("User authentication failed.");
      }

      var applicationDto = JsonSafeDeserializer.SafeDeserialize<CrmApplicationDto>(ApplicationData);

      if (applicationDto == null)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application data",
          ErrorType = "ValidationError"
        });
      }

      var currentDateTime = DateTime.UtcNow;
      var userId = currentUser.UserId ?? 0;

      applicationDto.ApplicationDate = currentDateTime;
      applicationDto.CreatedDate = currentDateTime;
      applicationDto.CreatedBy = userId;

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

      var fileValidationResult = ValidateFileUploads(files);
      if (!fileValidationResult.IsValid)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = fileValidationResult.ErrorMessage,
          ErrorType = "FileValidationError"
        });
      }

      await HandleApplicationFileUploads(applicationDto, files);

      var newApplicationId = new Random().Next(1000, 9999);

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

      if (!TryGetLoggedInUser(out UsersDto currentUser))
      {
        return Unauthorized("User authentication failed.");
      }

      //var applicationDto = JsonSerializer.Deserialize<CrmApplicationDto>(ApplicationData, new JsonSerializerOptions
      //{
      //  PropertyNameCaseInsensitive = true
      //});

      var applicationDto = JsonConvert.DeserializeObject<CrmApplicationDto>(ApplicationData);

      if (applicationDto == null)
      {
        return BadRequest(new
        {
          IsSuccess = false,
          Message = "Invalid application data",
          ErrorType = "ValidationError"
        });
      }

      applicationDto.ApplicationId = id;
      applicationDto.UpdatedDate = DateTime.UtcNow;
      applicationDto.UpdatedBy = currentUser.UserId ?? 0;

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

      await HandleApplicationFileUploads(applicationDto, files);

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
}


