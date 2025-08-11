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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.CRM;
internal sealed class CrmApplicationService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config, IHttpContextAccessor httpContextAccessor) : ICrmApplicationService
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
    ca.ApplicationDate,
    ca.StateId,

    ai.ApplicantId,
    ac.ApplicantCourseId,
    ac.CountryId,
    ac.InstituteId,
    ac.CourseId,
    ai.GenderId,
    ai.MaritalStatusId,
    PermanentAddressId,
    PresentAddressId,

    -- Basic Application Info

    -- Personal Details
    (ai.TitleText + ' ' + ai.FirstName + ' ' + ai.LastName) AS ApplicantName,
    ai.EmailAddress,
    ai.Mobile,
    ai.DateOfBirth,
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
    ac.CurrencyId,
    ac.ApplicationFee,
    curInfo.CurrencyName,
    ac.PaymentMethodId,
    ac.PaymentMethod,
    ac.PaymentDate,
    ac.PaymentReferenceNumber,

    -- Address Information
    perCountry.CountryName AS PermanentCountryName,
    perAddress.City AS PermanentCity,
    preCountry.CountryName AS PresentCountryName,
    preAddress.City AS PresentCity,

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
        FROM CrmStatementOfPurpose sp 
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
    INNER JOIN CrmApplicantInfo ai ON ca.ApplicationId = ai.ApplicantId
    INNER JOIN CrmApplicantCourse ac ON ai.ApplicantId = ac.ApplicantId
    INNER JOIN CrmInstitute i ON ac.InstituteId = i.InstituteId
    INNER JOIN CrmCountry c ON ac.CountryId = c.CountryId
    LEFT JOIN CrmCurrencyInfo curInfo ON ac.CurrencyId = curInfo.CurrencyId
    LEFT JOIN CrmPresentAddress preAddress ON preAddress.ApplicantId = ai.ApplicantId
    LEFT JOIN CrmCountry preCountry ON preCountry.CountryId = preAddress.CountryId
    LEFT JOIN CrmPermanentAddress perAddress ON perAddress.ApplicantId = ai.ApplicantId
    LEFT JOIN CrmCountry perCountry ON perCountry.CountryId = perAddress.CountryId    

    LEFT JOIN CrmEducationHistory edu ON ai.ApplicantId = edu.ApplicantId
    LEFT JOIN CrmIELTSInformation ieltsInfo ON ca.ApplicationId = ieltsInfo.ApplicantId
    LEFT JOIN CrmTOEFLInformation toeflInfo ON ca.ApplicationId = toeflInfo.ApplicantId
    LEFT JOIN CrmPTEInformation pteInfo ON ca.ApplicationId = pteInfo.ApplicantId
    LEFT JOIN CrmOTHERSInformation othersInfo ON ca.ApplicationId = othersInfo.ApplicantId
    LEFT JOIN CrmWorkExperience workEx ON ca.ApplicationId = workEx.ApplicantId
    
    LEFT JOIN CrmApplicantReference ar  ON ar.ApplicantId = ai.ApplicantId
    LEFT JOIN CrmStatementOfPurpose sp ON sp.ApplicantId = ai.ApplicantId
    LEFT JOIN CrmAdditionalInfo  ON CrmAdditionalInfo.ApplicantId = ai.ApplicantId
");
    string orderBy = " InstituteName asc ";
    return await _repository.CrmApplications.GridData<CrmApplicationGridDto>(sql, options, orderBy, condition);
  }

  public async Task<CrmApplicationDto> GetApplication(int applicationId, bool trackChanges)
  {
    var query = string.Format(@" 
      SELECT
        -- CrmApplicationDto
        ca.ApplicationId,
        ca.ApplicationDate,
        ca.ApplicationStatus,
        ca.CreatedDate AS AppCreatedDate,
        ca.CreatedBy AS AppCreatedBy,
        ca.UpdatedDate AS AppUpdatedDate,
        ca.UpdatedBy AS AppUpdatedBy,

        -- ApplicantCourseDto (CourseInformation -> ApplicantCourse)
        ac.ApplicantCourseId,
        ac.CountryId,
        c.CountryName,
        ac.InstituteId,
        i.InstituteName,
        ac.CourseTitle,
        ac.IntakeMonthId,
        ac.IntakeMonth,
        ac.IntakeYearId,
        ac.IntakeYear,
        ac.ApplicationFee,
        ac.CurrencyId,
        curInfo.CurrencyName,
        ac.PaymentMethodId,
        ac.PaymentMethod,
        ac.PaymentReferenceNumber,
        ac.PaymentDate,
        ac.Remarks AS CourseRemarks,
        ac.CreatedDate AS CourseCreatedDate,
        ac.CreatedBy AS CourseCreatedBy,
        ac.UpdatedDate AS CourseUpdatedDate,
        ac.UpdatedBy AS CourseUpdatedBy,
        ac.CourseId,

        -- ApplicantInfoDto (CourseInformation -> PersonalDetails)
        ai.ApplicantId,
        ai.GenderId,
        ai.GenderName,
        ai.TitleValue,
        ai.TitleText,
        ai.FirstName,
        ai.LastName,
        (ai.TitleText + ' ' + ai.FirstName + ' ' + ai.LastName) AS ApplicantName,
        ai.DateOfBirth,
        ai.MaritalStatusId,
        ai.MaritalStatusName,
        ai.Nationality,
        ai.HasValidPassport,
        ai.PassportNumber,
        ai.PassportIssueDate,
        ai.PassportExpiryDate,
        ai.PhoneCountryCode,
        ai.PhoneAreaCode,
        ai.PhoneNumber,
        ai.Mobile,
        ai.EmailAddress,
        ai.SkypeId,
        ai.ApplicantImagePath,
        ai.CreatedDate AS ApplicantCreatedDate,
        ai.CreatedBy AS ApplicantCreatedBy,
        ai.UpdatedDate AS ApplicantUpdatedDate,
        ai.UpdatedBy AS ApplicantUpdatedBy,

        -- PermanentAddressDto
        pAddr.PermanentAddressId,
        pAddr.Address AS PermanentAddress,
        pAddr.City AS PermanentCity,
        pAddr.State AS PermanentState,
        pAddr.CountryId AS PermanentCountryId,
        perCountry.CountryName AS PermanentCountryName,
        pAddr.PostalCode AS PermanentPostalCode,
        pAddr.CreatedDate AS PermanentCreatedDate,
        pAddr.CreatedBy AS PermanentCreatedBy,
        pAddr.UpdatedDate AS PermanentUpdatedDate,
        pAddr.UpdatedBy AS PermanentUpdatedBy,

        -- PresentAddressDto
        prAddr.PresentAddressId,
        prAddr.SameAsPermanentAddress,
        prAddr.Address AS PresentAddress,
        prAddr.City AS PresentCity,
        prAddr.State AS PresentState,
        prAddr.CountryId AS PresentCountryId,
        preCountry.CountryName AS PresentCountryName,
        prAddr.PostalCode AS PresentPostalCode,
        prAddr.CreatedDate AS PresentCreatedDate,
        prAddr.CreatedBy AS PresentCreatedBy,
        prAddr.UpdatedDate AS PresentUpdatedDate,
        prAddr.UpdatedBy AS PresentUpdatedBy,

        -- EducationHistoryDto
        edu.EducationHistoryId,
        edu.ApplicantId AS Education_ApplicantId,
        edu.Institution as EducationInstitution,
        edu.Qualification,
        edu.PassingYear,
        edu.Grade,
        edu.DocumentName AS EducationDocumentName,
        edu.AttachedDocument,
        edu.PdfThumbnail,
        edu.CreatedDate AS EducationCreatedDate,
        edu.CreatedBy AS EducationCreatedBy,
        edu.UpdatedDate AS EducationUpdatedDate,
        edu.UpdatedBy AS EducationUpdatedBy,

        -- IELTSInformationDto
        ielts.IELTSInformationId,
        ielts.ApplicantId AS IELTS_ApplicantId,
        ielts.IELTSListening,
        ielts.IELTSReading,
        ielts.IELTSWriting,
        ielts.IELTSSpeaking,
        ielts.IELTSOverallScore,
        ielts.IELTSDate,
        ielts.IELTSScannedCopyPath,
        ielts.IELTSAdditionalInformation,
        ielts.CreatedDate AS IELTS_CreatedDate,
        ielts.CreatedBy AS IELTS_CreatedBy,
        ielts.UpdatedDate AS IELTS_UpdatedDate,
        ielts.UpdatedBy AS IELTS_UpdatedBy,

        -- TOEFLInformationDto
        toefl.TOEFLInformationId,
        toefl.ApplicantId AS TOEFL_ApplicantId,
        toefl.TOEFLListening,
        toefl.TOEFLReading,
        toefl.TOEFLWriting,
        toefl.TOEFLSpeaking,
        toefl.TOEFLOverallScore,
        toefl.TOEFLDate,
        toefl.TOEFLScannedCopyPath,
        toefl.TOEFLAdditionalInformation,
        toefl.CreatedDate AS TOEFL_CreatedDate,
        toefl.CreatedBy AS TOEFL_CreatedBy,
        toefl.UpdatedDate AS TOEFL_UpdatedDate,
        toefl.UpdatedBy AS TOEFL_UpdatedBy,

        -- PTEInformationDto
        pte.PTEInformationId,
        pte.ApplicantId AS PTE_ApplicantId,
        pte.PTEListening,
        pte.PTEReading,
        pte.PTEWriting,
        pte.PTESpeaking,
        pte.PTEOverallScore,
        pte.PTEDate,
        pte.PTEScannedCopyPath,
        pte.PTEAdditionalInformation,
        pte.CreatedDate AS PTE_CreatedDate,
        pte.CreatedBy AS PTE_CreatedBy,
        pte.UpdatedDate AS PTE_UpdatedDate,
        pte.UpdatedBy AS PTE_UpdatedBy,

        -- GMATInformationDto
        gmat.GMATInformationId,
        gmat.ApplicantId AS GMAT_ApplicantId,
        gmat.GMATListening,
        gmat.GMATReading,
        gmat.GMATWriting,
        gmat.GMATSpeaking,
        gmat.GMATOverallScore,
        gmat.GMATDate,
        gmat.GMATScannedCopyPath,
        gmat.GMATAdditionalInformation,
        gmat.CreatedDate AS GMAT_CreatedDate,
        gmat.CreatedBy AS GMAT_CreatedBy,
        gmat.UpdatedDate AS GMAT_UpdatedDate,
        gmat.UpdatedBy AS GMAT_UpdatedBy,

        -- OTHERSInformationDto
        others.OTHERSInformationId,
        others.ApplicantId AS OTHERS_ApplicantId,
        others.OTHERSAdditionalInformation,
        others.OTHERSScannedCopyPath,
        others.CreatedDate AS OTHERS_CreatedDate,
        others.CreatedBy AS OTHERS_CreatedBy,
        others.UpdatedDate AS OTHERS_UpdatedDate,
        others.UpdatedBy AS OTHERS_UpdatedBy,

        -- WorkExperienceHistoryDto
        work.WorkExperienceId,
        work.ApplicantId AS Work_ApplicantId,
        work.NameOfEmployer,
        work.Position,
        work.StartDate,
        work.EndDate,
        work.Period,
        work.MainResponsibility,
        work.DocumentName AS WorkDocumentName,
        work.FileThumbnail,
        work.CreatedDate AS WorkCreatedDate,
        work.CreatedBy AS WorkCreatedBy,
        work.UpdatedDate AS WorkUpdatedDate,
        work.UpdatedBy AS WorkUpdatedBy,

        -- ApplicantReferenceDto
        ref.ApplicantReferenceId,
        ref.ApplicantId AS Ref_ApplicantId,
        ref.Name AS ReferenceName,
        ref.Designation,
        ref.Institution,
        ref.EmailID,
        ref.PhoneNo,
        ref.FaxNo,
        ref.Address AS RefAddress,
        ref.City AS RefCity,
        ref.State AS RefState,
        ref.Country AS RefCountry,
        ref.PostOrZipCode,
        ref.CreatedDate AS RefCreatedDate,
        ref.CreatedBy AS RefCreatedBy,
        ref.UpdatedDate AS RefUpdatedDate,
        ref.UpdatedBy AS RefUpdatedBy,

        -- StatementOfPurposeDto
        sop.StatementOfPurposeId,
        sop.ApplicantId AS SOP_ApplicantId,
        sop.StatementOfPurposeRemarks,
        sop.StatementOfPurposeFilePath,
        sop.CreatedDate AS SOP_CreatedDate,
        sop.CreatedBy AS SOP_CreatedBy,
        sop.UpdatedDate AS SOP_UpdatedDate,
        sop.UpdatedBy AS SOP_UpdatedBy,

        -- AdditionalInfoDto
        addInfo.AdditionalInfoId,
        addInfo.ApplicantId AS AddInfo_ApplicantId,
        addInfo.RequireAccommodation,
        addInfo.HealthNMedicalNeeds,
        addInfo.HealthNMedicalNeedsRemarks,
        addInfo.AdditionalInformationRemarks,
        addInfo.DocumentTitle AS AddInfoDocTitle,
        addInfo.UploadFile AS AddInfoUploadFile,
        addInfo.DocumentName AS AddInfoDocumentName,
        addInfo.FileThumbnail AS AddInfoFileThumbnail,
        addInfo.RecordType AS AddInfoRecordType,
        addInfo.CreatedDate AS AddInfoCreatedDate,
        addInfo.CreatedBy AS AddInfoCreatedBy,
        addInfo.UpdatedDate AS AddInfoUpdatedDate,
        addInfo.UpdatedBy AS AddInfoUpdatedBy

    FROM CrmApplication ca
    INNER JOIN ApplicantInfo ai ON ca.ApplicationId = ai.ApplicationId
    INNER JOIN ApplicantCourse ac ON ai.ApplicantId = ac.ApplicantId
    INNER JOIN Country c ON ac.CountryId = c.CountryId
    INNER JOIN CRMInstitute i ON ac.InstituteId = i.InstituteId
    LEFT JOIN CrmCurrencyInfo curInfo ON ac.CurrencyId = curInfo.CurrencyId
    LEFT JOIN PermanentAddress pAddr ON ai.ApplicantId = pAddr.ApplicantId
    LEFT JOIN Country perCountry ON pAddr.CountryId = perCountry.CountryId
    LEFT JOIN PresentAddress prAddr ON ai.ApplicantId = prAddr.ApplicantId
    LEFT JOIN Country preCountry ON prAddr.CountryId = preCountry.CountryId
    LEFT JOIN EducationHistory edu ON ai.ApplicantId = edu.ApplicantId
    LEFT JOIN IELTSInformation ielts ON ai.ApplicantId = ielts.ApplicantId
    LEFT JOIN TOEFLInformation toefl ON ai.ApplicantId = toefl.ApplicantId
    LEFT JOIN PTEInformation pte ON ai.ApplicantId = pte.ApplicantId
    LEFT JOIN GMATInformation gmat ON ai.ApplicantId = gmat.ApplicantId
    LEFT JOIN OTHERSInformation others ON ai.ApplicantId = others.ApplicantId
    LEFT JOIN WorkExperience work ON ai.ApplicantId = work.ApplicantId
    LEFT JOIN ApplicantReference ref ON ai.ApplicantId = ref.ApplicantId
    LEFT JOIN StatementOfPurpose sop ON ai.ApplicantId = sop.ApplicantId
    LEFT JOIN AdditionalInfo addInfo ON ai.ApplicantId = addInfo.ApplicantId

    WHERE ca.ApplicationId = @ApplicationId
    ", applicationId);

    // Execute the query using RepositoryBase method
    var parameters = new SqlParameter[]
    {
        new SqlParameter("@ApplicationId", applicationId)
    };

    GetApplicationDto result = await _repository.CrmApplications.ExecuteSingleData<GetApplicationDto>(query, parameters);

    // If no data found, return an empty DTO
    if (result == null)
    {
      _logger.LogWarn($"No application found with ApplicationId: {applicationId}");
      return new CrmApplicationDto(); // Return empty DTO if no data found
    }

    CrmApplicationDto crmApplicationDto = new CrmApplicationDto();
    crmApplicationDto = MyMapper.JsonClone<GetApplicationDto, CrmApplicationDto>(result);
    // Map the result to ApplicationDto, ApplicantCourseDto, and other nested DTOs using MyMapper
    // Note: MyMapper.JsonClone is assumed to be a method that maps one DTO to another using JSON serialization/deserialization
    // Applicant Personal Info with course information
    
    crmApplicationDto.CourseInformation.PersonalDetails = MyMapper.JsonClone<GetApplicationDto, ApplicantInfoDto>(result); ;
    crmApplicationDto.CourseInformation.ApplicantCourse = MyMapper.JsonClone<GetApplicationDto, ApplicantCourseDto>(result);
    crmApplicationDto.CourseInformation.ApplicantAddress.PresentAddress = MyMapper.JsonClone<GetApplicationDto, PresentAddressDto>(result);
    crmApplicationDto.CourseInformation.ApplicantAddress.PermanentAddress = MyMapper.JsonClone<GetApplicationDto, PermanentAddressDto>(result);

    // English Language Test Information
    crmApplicationDto.EducationInformation.IELTSInformation = MyMapper.JsonClone<GetApplicationDto, IELTSInformationDto>(result);
    crmApplicationDto.EducationInformation.TOEFLInformation = MyMapper.JsonClone<GetApplicationDto, TOEFLInformationDto>(result);
    crmApplicationDto.EducationInformation.PTEInformation = MyMapper.JsonClone<GetApplicationDto, PTEInformationDto>(result);
    crmApplicationDto.EducationInformation.GMATInformation = MyMapper.JsonClone<GetApplicationDto, GMATInformationDto>(result);
    crmApplicationDto.EducationInformation.OTHERSInformation = MyMapper.JsonClone<GetApplicationDto, OTHERSInformationDto>(result);

    // Education HistoryList and  Work Experience List
    IEnumerable<CrmEducationHistory> education = await _repository.CrmEducationHistories.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.EducationHistoryId, trackChanges: false);
    crmApplicationDto.EducationInformation.EducationDetails.EducationHistory 
      = MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(education);
    crmApplicationDto.EducationInformation.EducationDetails.TotalEducationRecords = education.Count();


    IEnumerable<CrmWorkExperience> workExperiences = await _repository.CrmWorkExperiences.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.WorkExperienceId, trackChanges: false);
    crmApplicationDto.EducationInformation.WorkExperience.WorkExperienceHistory 
      = MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(workExperiences);
    crmApplicationDto.EducationInformation.WorkExperience.TotalWorkExperienceRecords = workExperiences.Count();


    // Additional Information Section DTOs
    // Applicant Reference List
    IEnumerable<CrmApplicantReference> applicantReferences = await _repository.CrmApplicantReferences.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.ApplicantReferenceId, trackChanges: false);
    crmApplicationDto.AdditionalInformation.ReferenceDetails.References
      = MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(applicantReferences);
    crmApplicationDto.AdditionalInformation.ReferenceDetails.TotalReferenceRecords = applicantReferences.Count();

    // Additional Information Section DTOs
    crmApplicationDto.AdditionalInformation.StatementOfPurpose = MyMapper.JsonClone<GetApplicationDto, StatementOfPurposeDto>(result);
    crmApplicationDto.AdditionalInformation.AdditionalInformation = MyMapper.JsonClone<GetApplicationDto, AdditionalInfoDto>(result);

    // If you have AdditionalDocumentDto, you can map it similarly
    // Additional Document List
    //AdditionalDocumentDto additionalDocumentDto = MyMapper.JsonClone<GetApplicationDto, AdditionalDocumentDto>(result);

    return crmApplicationDto;
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

      // 1. Save CrmApplication first to get ApplicationId
      var crmApplicationEntity = MyMapper.JsonClone<CrmApplicationDto, CrmApplication>(dto);
      int applicationId = await _repository.CrmApplications.CreateAndGetIdAsync(crmApplicationEntity);
      dto.ApplicationId = applicationId;

      // 2. Save ApplicantInfo with ApplicationId to get ApplicantId
      if (dto.CourseInformation?.PersonalDetails != null)
      {
        var applicantInfoDto = dto.CourseInformation.PersonalDetails;
        applicantInfoDto.ApplicationId = applicationId;
        applicantInfoDto.CreatedDate = DateTime.UtcNow;
        applicantInfoDto.CreatedBy = currentUser.UserId ?? 0;

        var applicantInfoEntity = MyMapper.JsonClone<ApplicantInfoDto, CrmApplicantInfo>(applicantInfoDto);
        int applicantId = await _repository.CrmApplicantInfoes.CreateAndGetIdAsync(applicantInfoEntity);
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

          var applicantCourseEntity = MyMapper.JsonClone<ApplicantCourseDto, CrmApplicantCourse>(applicantCourseDto);
          //await _repository.ApplicantCourse.CreateAsync(applicantCourseEntity);
          var applicantCourseId = await _repository.CrmApplicantCourses.CreateAndGetIdAsync(applicantCourseEntity);
        }

        // Save PermanentAddress
        if (dto.CourseInformation?.ApplicantAddress?.PermanentAddress != null)
        {
          var permanentAddressDto = dto.CourseInformation.ApplicantAddress.PermanentAddress;
          permanentAddressDto.CreatedDate = DateTime.UtcNow;
          permanentAddressDto.CreatedBy = currentUser.UserId ?? 0;

          var permanentAddressEntity = MyMapper.JsonClone<PermanentAddressDto, CrmPermanentAddress>(permanentAddressDto);
          //await _repository.PermanentAddress.CreateAsync(permanentAddressEntity);

          var prmanentAddressId = await _repository.CrmPermanentAddresses.CreateAndGetIdAsync(permanentAddressEntity);
        }

        // Save PresentAddress
        if (dto.CourseInformation?.ApplicantAddress?.PresentAddress != null)
        {
          var presentAddressDto = dto.CourseInformation.ApplicantAddress.PresentAddress;
          presentAddressDto.CreatedDate = DateTime.UtcNow;
          presentAddressDto.CreatedBy = currentUser.UserId ?? 0;

          var presentAddressEntity = MyMapper.JsonClone<PresentAddressDto, CrmPresentAddress>(presentAddressDto);
          //await _repository.PresentAddress.CreateAsync(presentAddressEntity);
          var presentAddressId = await _repository.CrmPresentAddresses.CreateAndGetIdAsync(presentAddressEntity);
        }

        // Save Education History
        if (dto.EducationInformation?.EducationDetails?.EducationHistory != null && dto.EducationInformation.EducationDetails.EducationHistory.Any())
        {
          foreach (var educationDto in dto.EducationInformation.EducationDetails.EducationHistory)
          {
            educationDto.CreatedDate = DateTime.UtcNow;
            educationDto.CreatedBy = currentUser.UserId ?? 0;

            var educationEntity = MyMapper.JsonClone<EducationHistoryDto, CrmEducationHistory>(educationDto);
            //await _repository.CrmEducationHistories.CreateAsync(educationEntity);
            var educationHistoryId = await _repository.CrmEducationHistories.CreateAndGetIdAsync(educationEntity);
          }
        }

        // Save IELTS Information
        if (dto.EducationInformation?.IELTSInformation != null)
        {
          var ieltsDto = dto.EducationInformation.IELTSInformation;
          ieltsDto.CreatedDate = DateTime.UtcNow;
          ieltsDto.CreatedBy = currentUser.UserId ?? 0;

          var ieltsEntity = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(ieltsDto);
          var ieltsEntityId = await _repository.CrmIELTSInformations.CreateAndGetIdAsync(ieltsEntity);
          //await _repository.IELTSInformation.CreateAsync(ieltsEntity);
        }

        // Save TOEFL Information
        if (dto.EducationInformation?.TOEFLInformation != null)
        {
          var toeflDto = dto.EducationInformation.TOEFLInformation;
          toeflDto.CreatedDate = DateTime.UtcNow;
          toeflDto.CreatedBy = currentUser.UserId ?? 0;

          var toeflEntity = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(toeflDto);
          var toeflEntityId = await _repository.CrmTOEFLInformations.CreateAndGetIdAsync(toeflEntity);
          //await _repository.TOEFLInformation.CreateAsync(toeflEntity);
        }

        // Save PTE Information
        if (dto.EducationInformation?.PTEInformation != null)
        {
          var pteDto = dto.EducationInformation.PTEInformation;
          pteDto.CreatedDate = DateTime.UtcNow;
          pteDto.CreatedBy = currentUser.UserId ?? 0;

          var pteEntity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(pteDto);
          var pteEntityId = await _repository.CrmPTEInformations.CreateAndGetIdAsync(pteEntity);
          //await _repository.PTEInformation.CreateAsync(pteEntity);
        }

        // Save GMAT Information
        if (dto.EducationInformation?.GMATInformation != null)
        {
          var gmatDto = dto.EducationInformation.GMATInformation;
          gmatDto.CreatedDate = DateTime.UtcNow;
          gmatDto.CreatedBy = currentUser.UserId ?? 0;

          var gmatEntity = MyMapper.JsonClone<GMATInformationDto, CrmGMATInformation>(gmatDto);
          var gmatEntityId = await _repository.CrmGMATInformations.CreateAndGetIdAsync(gmatEntity);
          //await _repository.GMATInformation.CreateAsync(gmatEntity);
        }

        // Save OTHERS Information
        if (dto.EducationInformation?.OTHERSInformation != null)
        {
          var othersDto = dto.EducationInformation.OTHERSInformation;
          othersDto.CreatedDate = DateTime.UtcNow;
          othersDto.CreatedBy = currentUser.UserId ?? 0;

          var othersEntity = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(othersDto);
          var othersEntityId = await _repository.CrmOthersInformations.CreateAndGetIdAsync(othersEntity);
          //await _repository.OTHERSInformation.CreateAsync(othersEntity);
        }

        // Save Work Experience
        if (dto.EducationInformation?.WorkExperience?.WorkExperienceHistory != null && dto.EducationInformation.WorkExperience.WorkExperienceHistory.Any())
        {
          foreach (var workExpDto in dto.EducationInformation.WorkExperience.WorkExperienceHistory)
          {
            workExpDto.CreatedDate = DateTime.UtcNow;
            workExpDto.CreatedBy = currentUser.UserId ?? 0;

            var workExpEntity = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(workExpDto);
            var workExpEntityId = await _repository.CrmWorkExperiences.CreateAndGetIdAsync(workExpEntity);
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

            var referenceEntity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(referenceDto);
            var referenceEntityId = await _repository.CrmApplicantReferences.CreateAndGetIdAsync(referenceEntity);
            //await _repository.CrmApplicantReferences.CreateAsync(referenceEntity);
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

          var statementEntity = MyMapper.JsonClone<StatementOfPurposeDto, CrmStatementOfPurpose>(statementDto);
          var statementEntityId = await _repository.CrmStatementOfPurposes.CreateAndGetIdAsync(statementEntity);
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

          var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(additionalInfoDto);
          additionalInfoEntity.AdditionalInfoId = 0;
          var additionalInfoEntityId = await _repository.CrmAdditionalInfoes.CreateAndGetIdAsync(additionalInfoEntity);
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