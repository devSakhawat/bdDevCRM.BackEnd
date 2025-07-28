using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.CRM;
internal sealed class CRMApplicationService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config, IHttpContextAccessor httpContextAccessor) : ICRMApplicationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


  public async Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(CRMGridOptions options)
  {
    string condition = string.Empty;
    string sql = string.Format(
            @"SELECT
    ca.ApplicationId,
    ai.ApplicantId,
    ac.ApplicantCourseId,
    ac.CountryId,
    ac.InstituteId,
    ac.CourseId,
    ac.CurrencyId,
    ac.PaymentMethodId,
    ai.GenderId,
    ai.MaritalStatusId,
    PermanentAddressId,
    PresentAddressId,

    -- Basic Application Info
    ca.ApplicationDate,
    ca.ApplicationStatus,

    -- Personal Details
    (ai.TitleText + ' ' + ai.FirstName + ' ' + ai.LastName) AS ApplicantName,
    ai.EmailAddress,
    ai.Mobile,
    ai.DateOfBirth,
    ai.GenderName,
    ai.Nationality,

    -- Passport Information
    ai.HasValidPassport,
    ai.PassportNumber,
    ai.PassportExpiryDate,

    -- Course Information
    c.CountryName,
    i.InstituteName,
    ac.CourseTitle,
    ac.IntakeMonth,
    ac.IntakeYear,

    -- Financial Information
    ac.ApplicationFee,
    curInfo.CurrencyName,
    ac.PaymentMethod,
    ac.PaymentDate,
    ac.PaymentReferenceNumber,

    -- Address Information
    perCountry.CountryName AS PermanentCountryName,
    PermanentAddress.City AS PermanentCity,
    preCountry.CountryName AS PresentCountryName,
    PresentAddress.City AS PresentCity,

    -- English Language Tests (Summary)
    ieltsInfo.IELTSOverallScore as IELTSOverallBand,
    toeflInfo.TOEFLOverallScore,
    pteInfo.PTEOverallScore,

    -- Education Summary
    '' as HighestEducationLevel,
    '' as EducationGPA,

    -- Work Experience
    '' as TotalWorkExperience,

    -- Additional Information
   CASE 
    WHEN EXISTS (
        SELECT 1 
        FROM StatementOfPurpose sp 
        WHERE sp.ApplicantId = ai.ApplicantId
    ) THEN CAST(1 AS BIT)
    ELSE CAST(0 AS BIT)
END AS HasStatementOfPurpose,

--(
--    SELECT COUNT(*) 
--    FROM AdditionalDocument ad 
--    WHERE ad.ApplicantId = ai.ApplicantId
--) AS AdditionalDocumentsCount,


    -- Photo
    ai.ApplicantImagePath,
    -- Remarks
    ac.Remarks

FROM
    CrmApplication ca
    INNER JOIN ApplicantInfo ai ON ca.ApplicationId = ai.ApplicantId
    INNER JOIN ApplicantCourse ac ON ai.ApplicantId = ac.ApplicantId
    INNER JOIN CRMInstitute i ON ac.InstituteId = i.InstituteId
    INNER JOIN Country c ON ac.CountryId = c.CountryId
    LEFT JOIN CurrencyInfo curInfo ON ac.CurrencyId = curInfo.CurrencyId
    LEFT JOIN PresentAddress ON PresentAddress.ApplicantId = ai.ApplicantId
    LEFT JOIN Country preCountry ON preCountry.CountryId = PresentAddress.CountryId
    LEFT JOIN PermanentAddress ON PermanentAddress.ApplicantId = ai.ApplicantId
    LEFT JOIN Country perCountry ON perCountry.CountryId = PermanentAddress.CountryId    

    LEFT JOIN EducationHistory edu ON ai.ApplicantId = edu.ApplicantId
    LEFT JOIN IELTSInformation ieltsInfo ON ca.ApplicationId = ieltsInfo.ApplicantId
    LEFT JOIN TOEFLInformation toeflInfo ON ca.ApplicationId = toeflInfo.ApplicantId
    LEFT JOIN PTEInformation pteInfo ON ca.ApplicationId = pteInfo.ApplicantId
    LEFT JOIN OTHERSInformation othersInfo ON ca.ApplicationId = othersInfo.ApplicantId
    LEFT JOIN WorkExperience workEx ON ca.ApplicationId = workEx.ApplicantId
    
    LEFT JOIN ApplicantReference  ON ApplicantReference.ApplicantId = ai.ApplicantId
    LEFT JOIN StatementOfPurpose  ON StatementOfPurpose.ApplicantId = ai.ApplicantId
    LEFT JOIN AdditionalInfo  ON AdditionalInfo.ApplicantId = ai.ApplicantId
" );
    string orderBy = " InstituteName asc ";
    return await _repository.CRMApplication.GridData<CrmApplicationGridDto>(sql, options, orderBy, condition);
  }





  public async Task<CrmApplicationDto> CreateNewRecordAsync(CrmApplicationDto dto, UsersDto currentUser)
  {
    if (dto.ApplicationId != 0)
      throw new InvalidCreateOperationException("ApplicationId must be 0 for new record.");

    // Begin Transaction
    //await _repository.CRMApplication.TransactionBeginAsync();

    try
    {
      // Set audit fields for CrmApplication
      dto.CreatedDate = DateTime.UtcNow;
      dto.CreatedBy = currentUser.UserId ?? 0;
      dto.UpdatedDate = null;
      dto.UpdatedBy = null;
      dto.ApplicationStatus = "Draft";

      // 1. Save CrmApplication first to get ApplicationId
      var crmApplicationEntity = MyMapper.JsonClone<CrmApplicationDto, CrmApplication>(dto);
      int applicationId = await _repository.CRMApplication.CreateAndGetIdAsync(crmApplicationEntity);
      dto.ApplicationId = applicationId;

      // 2. Save ApplicantInfo with ApplicationId to get ApplicantId
      if (dto.CourseInformation?.PersonalDetails != null)
      {
        var applicantInfoDto = dto.CourseInformation.PersonalDetails;
        applicantInfoDto.ApplicationId = applicationId;
        applicantInfoDto.CreatedDate = DateTime.UtcNow;
        applicantInfoDto.CreatedBy = currentUser.UserId ?? 0;

        var applicantInfoEntity = MyMapper.JsonClone<ApplicantInfoDto, ApplicantInfo>(applicantInfoDto);
        int applicantId = await _repository.ApplicantInfo.CreateAndGetIdAsync(applicantInfoEntity);
        applicantInfoDto.ApplicantId = applicantId;

        // **Set ApplicantId in all nested DTOs that have ApplicantId property**
        SetApplicantIdInAllNestedDtos(dto, applicantId);

        // 3. Save all other CRM entities with ApplicantId

        // Save ApplicantCourse
        if (dto.CourseInformation?.ApplicantCourse != null)
        {
          var applicantCourseDto = dto.CourseInformation.ApplicantCourse;
          applicantCourseDto.CreatedDate = DateTime.UtcNow;
          applicantCourseDto.CreatedBy = currentUser.UserId ?? 0;

          var applicantCourseEntity = MyMapper.JsonClone<ApplicantCourseDto, ApplicantCourse>(applicantCourseDto);
          //await _repository.ApplicantCourse.CreateAsync(applicantCourseEntity);
          var applicantCourseId = await _repository.ApplicantCourse.CreateAndGetIdAsync(applicantCourseEntity);
        }

        // Save PermanentAddress
        if (dto.CourseInformation?.ApplicantAddress?.PermanentAddress != null)
        {
          var permanentAddressDto = dto.CourseInformation.ApplicantAddress.PermanentAddress;
          permanentAddressDto.CreatedDate = DateTime.UtcNow;
          permanentAddressDto.CreatedBy = currentUser.UserId ?? 0;

          var permanentAddressEntity = MyMapper.JsonClone<PermanentAddressDto, PermanentAddress>(permanentAddressDto);
          //await _repository.PermanentAddress.CreateAsync(permanentAddressEntity);

          var prmanentAddressId = await _repository.PermanentAddress.CreateAndGetIdAsync(permanentAddressEntity);
        }

        // Save PresentAddress
        if (dto.CourseInformation?.ApplicantAddress?.PresentAddress != null)
        {
          var presentAddressDto = dto.CourseInformation.ApplicantAddress.PresentAddress;
          presentAddressDto.CreatedDate = DateTime.UtcNow;
          presentAddressDto.CreatedBy = currentUser.UserId ?? 0;

          var presentAddressEntity = MyMapper.JsonClone<PresentAddressDto, PresentAddress>(presentAddressDto);
          //await _repository.PresentAddress.CreateAsync(presentAddressEntity);
          var presentAddressId = await _repository.PresentAddress.CreateAndGetIdAsync(presentAddressEntity);
        }

        // Save Education History
        if (dto.EducationInformation?.EducationDetails?.EducationHistory != null && dto.EducationInformation.EducationDetails.EducationHistory.Any())
        {
          foreach (var educationDto in dto.EducationInformation.EducationDetails.EducationHistory)
          {
            educationDto.CreatedDate = DateTime.UtcNow;
            educationDto.CreatedBy = currentUser.UserId ?? 0;

            var educationEntity = MyMapper.JsonClone<EducationHistoryDto, EducationHistory>(educationDto);
            //await _repository.EducationHistory.CreateAsync(educationEntity);
            educationEntity.PdfThumbnail = null;
            var educationHistoryId = await _repository.EducationHistory.CreateAndGetIdAsync(educationEntity);
          }
        }

        // Save IELTS Information
        if (dto.EducationInformation?.IELTSInformation != null)
        {
          var ieltsDto = dto.EducationInformation.IELTSInformation;
          ieltsDto.CreatedDate = DateTime.UtcNow;
          ieltsDto.CreatedBy = currentUser.UserId ?? 0;

          var ieltsEntity = MyMapper.JsonClone<IELTSInformationDto, IELTSInformation>(ieltsDto);
          var ieltsEntityId = await _repository.IELTSInformation.CreateAndGetIdAsync(ieltsEntity);
          //await _repository.IELTSInformation.CreateAsync(ieltsEntity);
        }

        // Save TOEFL Information
        if (dto.EducationInformation?.TOEFLInformation != null)
        {
          var toeflDto = dto.EducationInformation.TOEFLInformation;
          toeflDto.CreatedDate = DateTime.UtcNow;
          toeflDto.CreatedBy = currentUser.UserId ?? 0;

          var toeflEntity = MyMapper.JsonClone<TOEFLInformationDto, TOEFLInformation>(toeflDto);
          var toeflEntityId = await _repository.TOEFLInformation.CreateAndGetIdAsync(toeflEntity);
          //await _repository.TOEFLInformation.CreateAsync(toeflEntity);
        }

        // Save PTE Information
        if (dto.EducationInformation?.PTEInformation != null)
        {
          var pteDto = dto.EducationInformation.PTEInformation;
          pteDto.CreatedDate = DateTime.UtcNow;
          pteDto.CreatedBy = currentUser.UserId ?? 0;

          var pteEntity = MyMapper.JsonClone<PTEInformationDto, PTEInformation>(pteDto);
          var pteEntityId = await _repository.PTEInformation.CreateAndGetIdAsync(pteEntity);
          //await _repository.PTEInformation.CreateAsync(pteEntity);
        }

        // Save GMAT Information
        if (dto.EducationInformation?.GMATInformation != null)
        {
          var gmatDto = dto.EducationInformation.GMATInformation;
          gmatDto.CreatedDate = DateTime.UtcNow;
          gmatDto.CreatedBy = currentUser.UserId ?? 0;

          var gmatEntity = MyMapper.JsonClone<GMATInformationDto, GMATInformation>(gmatDto);
          var gmatEntityId = await _repository.GMATInformation.CreateAndGetIdAsync(gmatEntity);
          //await _repository.GMATInformation.CreateAsync(gmatEntity);
        }

        // Save OTHERS Information
        if (dto.EducationInformation?.OTHERSInformation != null)
        {
          var othersDto = dto.EducationInformation.OTHERSInformation;
          othersDto.CreatedDate = DateTime.UtcNow;
          othersDto.CreatedBy = currentUser.UserId ?? 0;

          var othersEntity = MyMapper.JsonClone<OTHERSInformationDto, OTHERSInformation>(othersDto);
          var othersEntityId = await _repository.OTHERSInformation.CreateAndGetIdAsync(othersEntity);
          //await _repository.OTHERSInformation.CreateAsync(othersEntity);
        }

        // Save Work Experience
        if (dto.EducationInformation?.WorkExperience?.WorkExperienceHistory != null && dto.EducationInformation.WorkExperience.WorkExperienceHistory.Any())
        {
          foreach (var workExpDto in dto.EducationInformation.WorkExperience.WorkExperienceHistory)
          {
            workExpDto.CreatedDate = DateTime.UtcNow;
            workExpDto.CreatedBy = currentUser.UserId ?? 0;

            var workExpEntity = MyMapper.JsonClone<WorkExperienceHistoryDto, WorkExperience>(workExpDto);
            workExpEntity.FileThumbnail = null;
            var workExpEntityId = await _repository.WorkExperience.CreateAndGetIdAsync(workExpEntity);
            //await _repository.WorkExperience.CreateAsync(workExpEntity);
          }
        }

        // Save Applicant Reference
        if (dto.AdditionalInformation?.ReferenceDetails?.References != null && dto.AdditionalInformation.ReferenceDetails.References.Any())
        {
          foreach (var referenceDto in dto.AdditionalInformation.ReferenceDetails.References)
          {
            referenceDto.CreatedDate = DateTime.UtcNow;
            referenceDto.CreatedBy = currentUser.UserId ?? 0;

            var referenceEntity = MyMapper.JsonClone<ApplicantReferenceDto, ApplicantReference>(referenceDto);
            var referenceEntityId = await _repository.ApplicantReference.CreateAndGetIdAsync(referenceEntity);
            //await _repository.ApplicantReference.CreateAsync(referenceEntity);
          }
        }

        // Save Statement of Purpose - FIX: Use ApplicationId instead of ApplicantId
        if (dto.AdditionalInformation?.StatementOfPurpose != null)
        {
          var statementDto = dto.AdditionalInformation.StatementOfPurpose;
          statementDto.CreatedDate = DateTime.UtcNow;
          statementDto.CreatedBy = currentUser.UserId ?? 0;

          // FIX: Set ApplicantId to ApplicationId for database constraint
          statementDto.ApplicantId = applicationId; // This maps to ApplicationId in database

          var statementEntity = MyMapper.JsonClone<StatementOfPurposeDto, StatementOfPurpose>(statementDto);
          var statementEntityId = await _repository.StatementOfPurpose.CreateAndGetIdAsync(statementEntity);
          //await _repository.StatementOfPurpose.CreateAsync(statementEntity);
        }

        // Save Additional Information - FIX: Use ApplicationId instead of ApplicantId
        if (dto.AdditionalInformation?.AdditionalInformation != null)
        {
          var additionalInfoDto = dto.AdditionalInformation.AdditionalInformation;
          additionalInfoDto.CreatedDate = DateTime.UtcNow;
          additionalInfoDto.CreatedBy = currentUser.UserId ?? 0;

          // FIX: Set ApplicantId to ApplicationId for database constraint
          additionalInfoDto.ApplicantId = applicationId; // This maps to ApplicationId in database

          var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, AdditionalInfo>(additionalInfoDto);
          additionalInfoEntity.AdditionalInfoId = 0;
          additionalInfoEntity.FileThumbnail = null;
          var additionalInfoEntityId = await _repository.AdditionalInfo.CreateAndGetIdAsync(additionalInfoEntity);
          //await _repository.AdditionalInfo.CreateAsync(additionalInfoEntity);
        }
      }

      // Commit transaction
      //await _repository.CRMApplication.TransactionCommitAsync();
      return dto;
    }
    catch (Exception ex)
    {
      // Rollback transaction in case of error
      //await _repository.CRMApplication.TransactionRollbackAsync();
      _logger.LogError($"Error creating CRM Application: {ex.Message}");
      throw;
    }
    finally
    {
      // Dispose transaction
      //await _repository.CRMApplication.TransactionDisposeAsync();
    }
  }

  /// <summary>
  /// Sets ApplicantId in all nested DTOs that have ApplicantId property
  /// </summary>
  private void SetApplicantIdInAllNestedDtos(CrmApplicationDto dto, int applicantId)
  {
    // Course Information Section
    if (dto.CourseInformation != null)
    {
      // ApplicantCourse
      if (dto.CourseInformation.ApplicantCourse != null)
      {
        dto.CourseInformation.ApplicantCourse.ApplicantId = applicantId;
      }

      // ApplicantAddress
      if (dto.CourseInformation.ApplicantAddress != null)
      {
        // PermanentAddress
        if (dto.CourseInformation.ApplicantAddress.PermanentAddress != null)
        {
          dto.CourseInformation.ApplicantAddress.PermanentAddress.ApplicantId = applicantId;
        }

        // PresentAddress
        if (dto.CourseInformation.ApplicantAddress.PresentAddress != null)
        {
          dto.CourseInformation.ApplicantAddress.PresentAddress.ApplicantId = applicantId;
        }
      }
    }

    // Education Information Section
    if (dto.EducationInformation != null)
    {
      // Education History
      if (dto.EducationInformation.EducationDetails?.EducationHistory != null)
      {
        foreach (var educationDto in dto.EducationInformation.EducationDetails.EducationHistory)
        {
          educationDto.ApplicantId = applicantId;
        }
      }

      // IELTS Information
      if (dto.EducationInformation.IELTSInformation != null)
      {
        dto.EducationInformation.IELTSInformation.ApplicantId = applicantId;
      }

      // TOEFL Information
      if (dto.EducationInformation.TOEFLInformation != null)
      {
        dto.EducationInformation.TOEFLInformation.ApplicantId = applicantId;
      }

      // PTE Information
      if (dto.EducationInformation.PTEInformation != null)
      {
        dto.EducationInformation.PTEInformation.ApplicantId = applicantId;
      }

      // GMAT Information
      if (dto.EducationInformation.GMATInformation != null)
      {
        dto.EducationInformation.GMATInformation.ApplicantId = applicantId;
      }

      // OTHERS Information
      if (dto.EducationInformation.OTHERSInformation != null)
      {
        dto.EducationInformation.OTHERSInformation.ApplicantId = applicantId;
      }

      // Work Experience
      if (dto.EducationInformation.WorkExperience?.WorkExperienceHistory != null)
      {
        foreach (var workExpDto in dto.EducationInformation.WorkExperience.WorkExperienceHistory)
        {
          workExpDto.ApplicantId = applicantId;
        }
      }
    }

    // Additional Information Section - Only for References and Additional Documents
    if (dto.AdditionalInformation != null)
    {
      // Reference Details - These use actual ApplicantId
      if (dto.AdditionalInformation.ReferenceDetails?.References != null)
      {
        foreach (var referenceDto in dto.AdditionalInformation.ReferenceDetails.References)
        {
          referenceDto.ApplicantId = applicantId;
        }
      }

      // Additional Documents - These use actual ApplicantId  
      if (dto.AdditionalInformation.AdditionalDocuments?.Documents != null)
      {
        foreach (var additionalDoc in dto.AdditionalInformation.AdditionalDocuments.Documents)
        {
          additionalDoc.ApplicantId = applicantId;
        }
      }

    }
  }

}