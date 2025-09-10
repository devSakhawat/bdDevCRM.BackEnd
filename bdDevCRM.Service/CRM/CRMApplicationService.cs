using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
    try
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
      ai.TitleText,
      ai.FirstName,
      ai.LastName,
      --(ai.TitleText + ' ' + ai.FirstName + ' ' + ai.LastName) AS ApplicantName,
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
      INNER JOIN CrmApplicantInfo ai ON ca.ApplicationId = ai.ApplicationId
      INNER JOIN CrmApplicantCourse ac ON ai.ApplicantId = ac.ApplicantId
      INNER JOIN CrmInstitute i ON ac.InstituteId = i.InstituteId
      INNER JOIN CrmCountry c ON ac.CountryId = c.CountryId
      LEFT JOIN CrmCurrencyInfo curInfo ON ac.CurrencyId = curInfo.CurrencyId
      LEFT JOIN CrmPresentAddress preAddress ON preAddress.ApplicantId = ai.ApplicantId
      LEFT JOIN CrmCountry preCountry ON preCountry.CountryId = preAddress.CountryId
      LEFT JOIN CrmPermanentAddress perAddress ON perAddress.ApplicantId = ai.ApplicantId
      LEFT JOIN CrmCountry perCountry ON perCountry.CountryId = perAddress.CountryId    

      LEFT JOIN CrmIELTSInformation ieltsInfo ON ca.ApplicationId = ieltsInfo.ApplicantId
      LEFT JOIN CrmTOEFLInformation toeflInfo ON ca.ApplicationId = toeflInfo.ApplicantId
      LEFT JOIN CrmPTEInformation pteInfo ON ca.ApplicationId = pteInfo.ApplicantId
      LEFT JOIN CrmOTHERSInformation othersInfo ON ca.ApplicationId = othersInfo.ApplicantId
      LEFT JOIN CrmStatementOfPurpose sp ON sp.ApplicantId = ai.ApplicantId
      LEFT JOIN CrmAdditionalInfo  ON CrmAdditionalInfo.ApplicantId = ai.ApplicantId
    
      --LEFT JOIN CrmEducationHistory edu ON ai.ApplicantId = edu.ApplicantId      
      --LEFT JOIN CrmWorkExperience workEx ON ca.ApplicationId = workEx.ApplicantId
      --LEFT JOIN CrmApplicantReference ar  ON ar.ApplicantId = ai.ApplicantId
  ");
      string orderBy = " ApplicationDate asc ";
      var gridResult = await _repository.CrmApplications.GridData<CrmApplicationGridDto>(sql, options, orderBy, condition);
      return gridResult;
    }
    catch (DataMappingException ex)
    {
      _logger.LogError($"Grid mapping error: {ex.Message}");
      throw new BadRequestException($"Grid data mapping error. {ex.Message}");
    }
  }

  //public async Task<CrmApplicationDto> GetApplication(int applicationId, bool trackChanges)
  public async Task<GetApplicationDto> GetApplication(int applicationId, bool trackChanges)
  {
    var query = string.Format(@" 
      SELECT
      -- ================================
      -- CrmApplicationDto (Top-level)
      -- ================================
      ca.ApplicationId,
      ca.ApplicationDate,
      ca.StateId,
      ca.CreatedDate AS AppCreatedDate,
      ca.CreatedBy AS AppCreatedBy,
      ca.UpdatedDate AS AppUpdatedDate,
      ca.UpdatedBy AS AppUpdatedBy,

      -- ================================
      -- ApplicantCourseDto
      -- ================================
      ISNULL(ac.ApplicantCourseId, 0) AS ApplicantCourseId,
      ISNULL(ac.CountryId, 0) AS CountryId,
      c.CountryName,
      ISNULL(ac.InstituteId, 0) AS InstituteId,
      i.InstituteName,
      ac.CourseTitle,
      ISNULL(ac.IntakeMonthId, 0) AS IntakeMonthId,
      ac.IntakeMonth,
      ISNULL(ac.IntakeYearId, 0) AS IntakeYearId,
      ac.IntakeYear,
      TRY_CONVERT(decimal(18,2), ac.ApplicationFee) AS ApplicationFee,
      ISNULL(ac.CurrencyId, 0) AS CurrencyId,
      curInfo.CurrencyName,
      ISNULL(ac.PaymentMethodId, 0) AS PaymentMethodId,
      ac.PaymentMethod,
      ac.PaymentReferenceNumber,
      ac.PaymentDate,
      ac.Remarks AS CourseRemarks,
      ISNULL(ac.CreatedDate, GETUTCDATE()) AS CourseCreatedDate,
      ISNULL(ac.CreatedBy, 0) AS CourseCreatedBy,
      ac.UpdatedDate AS CourseUpdatedDate,
      ac.UpdatedBy AS CourseUpdatedBy,
      ac.CourseId,

      -- ================================
      -- ApplicantInfoDto (PersonalDetails)
      -- ================================
      ai.ApplicantId,
      ai.GenderId,
      NULL AS GenderName,
      ai.TitleValue,
      ai.TitleText,
      ai.FirstName,
      ai.LastName,
      (ISNULL(ai.TitleText, '') + CASE WHEN ai.TitleText IS NULL OR ai.TitleText = '' THEN '' ELSE ' ' END
        + ISNULL(ai.FirstName, '') + CASE WHEN ai.FirstName IS NULL OR ai.FirstName = '' THEN '' ELSE ' ' END
        + ISNULL(ai.LastName, '')) AS ApplicantName,
      ai.DateOfBirth,
      ai.MaritalStatusId,
      NULL AS MaritalStatusName,
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

      -- ================================
      -- PermanentAddressDto
      -- ================================
      ISNULL(pAddr.PermanentAddressId, 0) AS PermanentAddressId,
      pAddr.Address AS PermanentAddress,
      pAddr.City AS PermanentCity,
      pAddr.State AS PermanentState,
      ISNULL(pAddr.CountryId, 0) AS PermanentCountryId,
      perCountry.CountryName AS PermanentCountryName,
      pAddr.PostalCode AS PermanentPostalCode,
      ISNULL(pAddr.CreatedDate, GETUTCDATE()) AS PermanentCreatedDate,
      ISNULL(pAddr.CreatedBy, 0) AS PermanentCreatedBy,
      pAddr.UpdatedDate AS PermanentUpdatedDate,
      pAddr.UpdatedBy AS PermanentUpdatedBy,

      -- ================================
      -- PresentAddressDto
      -- ================================
      ISNULL(prAddr.PresentAddressId, 0) AS PresentAddressId,
      ISNULL(prAddr.SameAsPermanentAddress, 0) AS SameAsPermanentAddress,
      prAddr.Address AS PresentAddress,
      prAddr.City AS PresentCity,
      prAddr.State AS PresentState,
      ISNULL(prAddr.CountryId, 0) AS PresentCountryId,
      preCountry.CountryName AS PresentCountryName,
      prAddr.PostalCode AS PresentPostalCode,
      ISNULL(prAddr.CreatedDate, GETUTCDATE()) AS PresentCreatedDate,
      ISNULL(prAddr.CreatedBy, 0) AS PresentCreatedBy,
      prAddr.UpdatedDate AS PresentUpdatedDate,
      prAddr.UpdatedBy AS PresentUpdatedBy,

      -- ================================
      -- IELTSInformationDto
      -- ================================
      ISNULL(ielts.IELTSInformationId, 0) AS IELTSInformationId,
      ISNULL(ielts.ApplicantId, 0) AS IELTS_ApplicantId,
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

      -- ================================
      -- TOEFLInformationDto
      -- ================================
      ISNULL(toefl.TOEFLInformationId, 0) AS TOEFLInformationId,
      ISNULL(toefl.ApplicantId, 0) AS TOEFL_ApplicantId,
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

      -- ================================
      -- PTEInformationDto
      -- ================================
      ISNULL(pte.PTEInformationId, 0) AS PTEInformationId,
      ISNULL(pte.ApplicantId, 0) AS PTE_ApplicantId,
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

      -- ================================
      -- GMATInformationDto
      -- ================================
      ISNULL(gmat.GMATInformationId, 0) AS GMATInformationId,
      ISNULL(gmat.ApplicantId, 0) AS GMAT_ApplicantId,
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

      -- ================================
      -- OTHERSInformationDto
      -- ================================
      ISNULL(others.OTHERSInformationId, 0) AS OthersInformationId,
      ISNULL(others.ApplicantId, 0) AS OTHERS_ApplicantId,
      --others.OTHERSAdditionalInformation,
      others.OTHERSScannedCopyPath,
      others.CreatedDate AS OTHERS_CreatedDate,
      others.CreatedBy AS OTHERS_CreatedBy,
      others.UpdatedDate AS OTHERS_UpdatedDate,
      others.UpdatedBy AS OTHERS_UpdatedBy,

      -- ================================
      -- StatementOfPurposeDto
      -- ================================
      ISNULL(sop.StatementOfPurposeId, 0) AS StatementOfPurposeId,
      ISNULL(sop.ApplicantId, 0) AS SOP_ApplicantId,
      sop.StatementOfPurposeRemarks,
      sop.StatementOfPurposeFilePath,
      sop.CreatedDate AS SOP_CreatedDate,
      sop.CreatedBy AS SOP_CreatedBy,
      sop.UpdatedDate AS SOP_UpdatedDate,
      sop.UpdatedBy AS SOP_UpdatedBy,

      -- ================================
      -- AdditionalInfoDto
      -- ================================
      ISNULL(addInfo.AdditionalInfoId, 0) AS AdditionalInfoId,
      ISNULL(addInfo.ApplicantId, 0) AS AddInfo_ApplicantId,
      addInfo.RequireAccommodation,
      addInfo.HealthNmedicalNeeds,
      addInfo.HealthNmedicalNeedsRemarks,
      addInfo.AdditionalInformationRemarks,

      -- ================================
      -- AdditionalDocument/Info (not available in CrmAdditionalInfo)
      -- returning NULL/defaults to fit DTO
      -- ================================
      CAST(0 AS INT) AS AdditionalDocumentId,
      CAST(addInfo.ApplicantId AS VARCHAR(50)) AS AddDoc_ApplicantId,
      CAST(NULL AS VARCHAR(500)) AS AddInfoUploadFile,
      CAST(NULL AS VARCHAR(200)) AS AddInfoDocumentName,
      CAST(NULL AS VARCHAR(200)) AS AddInfoFileThumbnail,
      CAST(NULL AS VARCHAR(100)) AS AddInfoRecordType,
      addInfo.CreateDate AS AddInfoCreatedDate,
      ISNULL(addInfo.CreateBy, 0) AS AddInfoCreatedBy,
      addInfo.UpdateDate AS AddInfoUpdatedDate,
      addInfo.UpdateBy AS AddInfoUpdatedBy,
      CAST(NULL AS VARCHAR(100)) AS RecordType

    FROM CrmApplication ca
    INNER JOIN CrmApplicantInfo ai ON ca.ApplicationId = ai.ApplicationId
    LEFT JOIN CrmApplicantCourse ac ON ai.ApplicantId = ac.ApplicantId
    LEFT JOIN CrmCountry c ON ac.CountryId = c.CountryId
    LEFT JOIN CrmInstitute i ON ac.InstituteId = i.InstituteId
    LEFT JOIN CrmCurrencyInfo curInfo ON ac.CurrencyId = curInfo.CurrencyId

    LEFT JOIN CrmPermanentAddress pAddr ON ai.ApplicantId = pAddr.ApplicantId
    LEFT JOIN CrmCountry perCountry ON pAddr.CountryId = perCountry.CountryId

    LEFT JOIN CrmPresentAddress prAddr ON ai.ApplicantId = prAddr.ApplicantId
    LEFT JOIN CrmCountry preCountry ON prAddr.CountryId = preCountry.CountryId

    LEFT JOIN CrmIELTSInformation ielts ON ai.ApplicantId = ielts.ApplicantId
    LEFT JOIN CrmTOEFLInformation toefl ON ai.ApplicantId = toefl.ApplicantId
    LEFT JOIN CrmPTEInformation pte ON ai.ApplicantId = pte.ApplicantId
    LEFT JOIN CrmGMATInformation gmat ON ai.ApplicantId = gmat.ApplicantId
    LEFT JOIN CrmOTHERSInformation others ON ai.ApplicantId = others.ApplicantId

    -- Saved using ApplicationId as ApplicantId (per Create flow)
    LEFT JOIN CrmStatementOfPurpose sop ON ca.ApplicationId = sop.ApplicantId
    LEFT JOIN CrmAdditionalInfo addInfo ON ca.ApplicationId = addInfo.ApplicantId
    -- list of data contain entitites are removed from here.They make the data multiple They will be added manually.

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
      return new GetApplicationDto(); // Return empty DTO if no data found
    }

    CrmApplicationDto crmApplicationDto = new CrmApplicationDto();
    crmApplicationDto = MyMapper.JsonClone<GetApplicationDto, CrmApplicationDto>(result);
    // Map the result to ApplicationDto, ApplicantCourseDto, and other nested DTOs using MyMapper
    // Note: MyMapper.JsonClone is assumed to be a method that maps one DTO to another using JSON serialization/deserialization
    // Applicant Personal Info with course information

    crmApplicationDto.CourseInformation.PersonalDetails = MyMapper.JsonClone<GetApplicationDto, ApplicantInfoDto>(result);
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
    //Enumerable<AdditionalDocumentDto> additionalDocumentDto = MyMapper.JsonClone<GetApplicationDto, AdditionalDocumentDto>(result);

    return result;
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

  public async Task<CrmApplicationDto> UpdateCrmApplicationAsync(int key, CrmApplicationDto updateDto, UsersDto currentUser)
  {
    if (updateDto.ApplicationId == 0)
      throw new InvalidCreateOperationException("ApplicationId must be greater than 0 for existing record.");

    if (key != updateDto.ApplicationId) throw new IdMismatchBadRequestException(key.ToString(), "Application Update");
    bool exists = await _repository.CrmApplications.ExistsAsync(x => x.ApplicationId == key);


    try
    {
      // Set audit fields for CrmApplication
      updateDto.UpdatedDate = DateTime.UtcNow;
      updateDto.UpdatedBy = currentUser.UserId ?? 0;

      // 1. Update CrmApplication 
      var crmApplicationEntity = await _repository.CrmApplications.FirstOrDefaultAsync(expression: x => x.ApplicationId == updateDto.ApplicationId, trackChanges: false);

      crmApplicationEntity.StateId = updateDto.StateId;
      crmApplicationEntity.ApplicationDate = (crmApplicationEntity.ApplicationDate > DateTime.MinValue)
                                              ? crmApplicationEntity.ApplicationDate : DateTime.UtcNow;
      crmApplicationEntity.CreatedDate = (crmApplicationEntity.CreatedDate > DateTime.MinValue)
                                   ? crmApplicationEntity.CreatedDate
                                   : DateTime.UtcNow;
      crmApplicationEntity.CreatedBy = (crmApplicationEntity.CreatedBy > 0)
                                ? crmApplicationEntity.CreatedBy
                                : (currentUser.UserId ?? 0);
      crmApplicationEntity.UpdatedDate = DateTime.UtcNow;
      crmApplicationEntity.UpdatedBy = currentUser.UserId ?? 0;


      _repository.CrmApplications.UpdateByState(crmApplicationEntity);
      await _repository.SaveAsync();

      // 2. Save or Update ApplicantInfo
      if (updateDto.CourseInformation?.PersonalDetails != null)
      {
        var applicantInfoDto = updateDto.CourseInformation.PersonalDetails;
        bool applicantExists = await _repository.CrmApplicantInfoes.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.ApplicationId == key);

        if (!applicantExists)
        {
          applicantInfoDto.CreatedDate = DateTime.UtcNow;
          applicantInfoDto.CreatedBy = currentUser.UserId ?? 0;

          CrmApplicantInfo crmApplicantInfo = MyMapper.JsonClone<ApplicantInfoDto, CrmApplicantInfo>(applicantInfoDto);
          updateDto.CourseInformation?.PersonalDetails.ApplicantId = await _repository.CrmApplicantInfoes.CreateAndGetIdAsync(crmApplicantInfo);
          applicantInfoDto.ApplicantId = (int)(updateDto.CourseInformation?.PersonalDetails.ApplicantId);

          // **Set ApplicantId in all nested DTOs that have ApplicantId property**
          SetApplicantIdInAllNestedDtos(updateDto, applicantInfoDto.ApplicantId);
        }
        else
        {
          var applicantInfosDB = await _repository.CrmApplicantInfoes.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.ApplicationId == updateDto.ApplicationId, false);

          applicantInfoDto.ApplicationId = (applicantInfoDto.ApplicationId == 0 || applicantInfoDto.ApplicationId == null) ? applicantInfosDB.ApplicationId : applicantInfoDto.ApplicationId;

          applicantInfoDto.CreatedDate = DateTimeFormatters.IsValidDateTime(applicantInfosDB.CreatedDate)
                                         ? applicantInfosDB.CreatedDate
                                         : DateTime.UtcNow;
          applicantInfoDto.CreatedBy = (applicantInfosDB.CreatedBy > 0)
                                    ? applicantInfosDB.CreatedBy
                                    : (currentUser.UserId ?? 0);

          applicantInfoDto.UpdatedDate = DateTime.UtcNow;
          applicantInfoDto.UpdatedBy = currentUser.UserId ?? 0;
          applicantInfosDB = MyMapper.JsonCloneSafe<ApplicantInfoDto, CrmApplicantInfo>(applicantInfoDto);
          _repository.CrmApplicantInfoes.UpdateByState(applicantInfosDB);
          await _repository.SaveAsync();

          // **Set ApplicantId in all nested DTOs that have ApplicantId property**
          SetApplicantIdInAllNestedDtos(updateDto, applicantInfoDto.ApplicantId);
        }

        // 3. Save all other CRM entities with ApplicantId
        // Save ApplicantCourse
        if (updateDto.CourseInformation?.ApplicantCourse != null)
        {
          var applicantCourseDto = updateDto.CourseInformation.ApplicantCourse;
          bool applicantCourseExists = await _repository.CrmApplicantCourses.ExistsAsync(x => x.ApplicantId == applicantCourseDto.ApplicantId && x.ApplicantCourseId == applicantCourseDto.ApplicantCourseId);

          if (!applicantCourseExists)
          {
            applicantCourseDto.CreatedDate = DateTime.UtcNow;
            applicantCourseDto.CreatedBy = currentUser.UserId ?? 0;

            var applicantCourseEntity = MyMapper.JsonClone<ApplicantCourseDto, CrmApplicantCourse>(applicantCourseDto);
            updateDto.CourseInformation?.ApplicantCourse.ApplicantCourseId = await _repository.CrmApplicantCourses.CreateAndGetIdAsync(applicantCourseEntity);
          }
          else
          {
            var applicantCourseDB = await _repository.CrmApplicantCourses.FirstOrDefaultAsync(x => x.ApplicantId == applicantCourseDto.ApplicantId && x.ApplicantCourseId == applicantCourseDto.ApplicantCourseId, false);

            applicantCourseDto.ApplicantId = (applicantCourseDto.ApplicantId == 0 || applicantCourseDto.ApplicantId == null) ? applicantCourseDB.ApplicantId : applicantCourseDto.ApplicantId;

            applicantCourseDto.CreatedDate = DateTimeFormatters.IsValidDateTime(applicantCourseDB.CreatedDate)
                                           ? applicantCourseDB.CreatedDate
                                           : DateTime.UtcNow;
            applicantInfoDto.CreatedBy = (applicantCourseDB.CreatedBy > 0)
                                      ? applicantCourseDB.CreatedBy
                                      : (currentUser.UserId ?? 0);

            applicantCourseDto.UpdatedDate = DateTime.UtcNow;
            applicantCourseDto.UpdatedBy = currentUser.UserId ?? 0;
            applicantCourseDB = MyMapper.JsonClone<ApplicantCourseDto, CrmApplicantCourse>(applicantCourseDto);
            _repository.CrmApplicantCourses.UpdateByState(applicantCourseDB);
            await _repository.SaveAsync();
          }
        }

        // Save PermanentAddress
        if (updateDto.CourseInformation?.ApplicantAddress?.PermanentAddress != null)
        {
          var permanentAddressDto = updateDto.CourseInformation.ApplicantAddress.PermanentAddress;
          bool permanentAddressExists = await _repository.CrmPermanentAddresses.ExistsAsync(x => x.ApplicantId == permanentAddressDto.ApplicantId && x.PermanentAddressId == permanentAddressDto.PermanentAddressId);

          if (!permanentAddressExists)
          {
            permanentAddressDto.CreatedDate = DateTime.UtcNow;
            permanentAddressDto.CreatedBy = currentUser.UserId ?? 0;

            var permanentAddressEntity = MyMapper.JsonClone<PermanentAddressDto, CrmPermanentAddress>(permanentAddressDto);
            updateDto.CourseInformation?.ApplicantAddress?.PermanentAddress.PermanentAddressId = await _repository.CrmPermanentAddresses.CreateAndGetIdAsync(permanentAddressEntity);
          }
          else
          {
            var permanentAddresseDB = await _repository.CrmPermanentAddresses.FirstOrDefaultAsync(x => x.ApplicantId == permanentAddressDto.ApplicantId && x.PermanentAddressId == permanentAddressDto.PermanentAddressId, false);

            permanentAddressDto.ApplicantId = (permanentAddressDto.ApplicantId == 0 || permanentAddressDto.ApplicantId == null) ? permanentAddresseDB.ApplicantId : permanentAddressDto.ApplicantId;

            permanentAddressDto.CreatedDate = DateTimeFormatters.IsValidDateTime(permanentAddresseDB.CreatedDate) ? permanentAddresseDB.CreatedDate : DateTime.UtcNow;
            applicantInfoDto.CreatedBy = (permanentAddresseDB.CreatedBy > 0)
                                      ? permanentAddresseDB.CreatedBy
                                      : (currentUser.UserId ?? 0);

            permanentAddressDto.UpdatedDate = DateTime.UtcNow;
            permanentAddressDto.UpdatedBy = currentUser.UserId ?? 0;
            permanentAddresseDB = MyMapper.JsonClone<PermanentAddressDto, CrmPermanentAddress>(permanentAddressDto);
            _repository.CrmPermanentAddresses.UpdateByState(permanentAddresseDB);
            await _repository.SaveAsync();
          }
        }

        // Save PresentAddress
        if (updateDto.CourseInformation?.ApplicantAddress?.PresentAddress != null)
        {
          var presentAddressDto = updateDto.CourseInformation.ApplicantAddress.PresentAddress;
          bool presentAddressExists = await _repository.CrmPresentAddresses.ExistsAsync(x => x.ApplicantId == presentAddressDto.ApplicantId);

          if (!presentAddressExists)
          {
            presentAddressDto.CreatedDate = DateTime.UtcNow;
            presentAddressDto.CreatedBy = currentUser.UserId ?? 0;

            var presentAddressEntity = MyMapper.JsonClone<PresentAddressDto, CrmPresentAddress>(presentAddressDto);
            updateDto.CourseInformation?.ApplicantAddress?.PresentAddress.PresentAddressId = await _repository.CrmPresentAddresses.CreateAndGetIdAsync(presentAddressEntity);
          }
          else
          {
            var presentAddresseDB = await _repository.CrmPresentAddresses.FirstOrDefaultAsync(x => x.ApplicantId == presentAddressDto.ApplicantId && x.PresentAddressId == presentAddressDto.PresentAddressId, false);

            presentAddressDto.ApplicantId = (presentAddressDto.ApplicantId == 0 || presentAddressDto.ApplicantId == null) ? presentAddresseDB.ApplicantId : presentAddressDto.ApplicantId;

            presentAddressDto.CreatedDate = DateTimeFormatters.IsValidDateTime(presentAddresseDB.CreatedDate) ? presentAddresseDB.CreatedDate : DateTime.UtcNow;
            applicantInfoDto.CreatedBy = (presentAddresseDB.CreatedBy > 0)
                                      ? presentAddresseDB.CreatedBy
                                      : (currentUser.UserId ?? 0);

            presentAddressDto.UpdatedDate = DateTime.UtcNow;
            presentAddressDto.UpdatedBy = currentUser.UserId ?? 0;
            presentAddresseDB = MyMapper.JsonClone<PresentAddressDto, CrmPresentAddress>(presentAddressDto);
            _repository.CrmPresentAddresses.UpdateByState(presentAddresseDB);
            await _repository.SaveAsync();
          }
        }

        // Save Education History
        if (updateDto.EducationInformation?.EducationDetails?.EducationHistory != null && updateDto.EducationInformation.EducationDetails.EducationHistory.Any())
        {
          foreach (var educationDto in updateDto.EducationInformation.EducationDetails.EducationHistory)
          {
            bool educationExists = await _repository.CrmEducationHistories.ExistsAsync(x => x.ApplicantId == educationDto.ApplicantId && x.EducationHistoryId == educationDto.EducationHistoryId);

            if (!educationExists)
            {
              educationDto.CreatedDate = DateTime.UtcNow;
              educationDto.CreatedBy = currentUser.UserId ?? 0;

              var educationEntity = MyMapper.JsonClone<EducationHistoryDto, CrmEducationHistory>(educationDto);
              educationDto.EducationHistoryId = await _repository.CrmEducationHistories.CreateAndGetIdAsync(educationEntity);
            }
            else
            {
              educationDto.UpdatedDate = DateTime.UtcNow;
              educationDto.UpdatedBy = currentUser.UserId ?? 0;
              var educationEntity = MyMapper.JsonClone<EducationHistoryDto, CrmEducationHistory>(educationDto);
              _repository.CrmEducationHistories.UpdateByState(educationEntity);
              await _repository.SaveAsync();
            }
          }
        }

        // Save IELTS Information
        if (updateDto.EducationInformation?.IELTSInformation != null)
        {
          var ieltsInformationDto = updateDto.EducationInformation?.IELTSInformation;
          bool ieltsInformationExists = await _repository.CrmIELTSInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.IELTSInformationId == ieltsInformationDto.IELTSInformationId);

          if (!ieltsInformationExists)
          {
            ieltsInformationDto.CreatedDate = DateTime.UtcNow;
            ieltsInformationDto.CreatedBy = currentUser.UserId ?? 0;

            var ieltsInformationEntity = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(ieltsInformationDto);
            updateDto.EducationInformation?.IELTSInformation.IELTSInformationId = await _repository.CrmIELTSInformations.CreateAndGetIdAsync(ieltsInformationEntity);
          }
          else
          {
            ieltsInformationDto.UpdatedDate = DateTime.UtcNow;
            ieltsInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            var ieltsInformationEntity = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(ieltsInformationDto);
            _repository.CrmIELTSInformations.UpdateByState(ieltsInformationEntity);
            await _repository.SaveAsync();
          }
        }

        // Save TOEFL Information
        if (updateDto.EducationInformation?.TOEFLInformation != null)
        {
          var toeflInformationDto = updateDto.EducationInformation?.TOEFLInformation;
          bool toeflInformationExists = await _repository.CrmTOEFLInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.TOEFLInformationId == toeflInformationDto.TOEFLInformationId);

          if (!toeflInformationExists)
          {
            toeflInformationDto.CreatedDate = DateTime.UtcNow;
            toeflInformationDto.CreatedBy = currentUser.UserId ?? 0;

            var toeflInformationEntity = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(toeflInformationDto);
            updateDto.EducationInformation?.TOEFLInformation.TOEFLInformationId = await _repository.CrmTOEFLInformations.CreateAndGetIdAsync(toeflInformationEntity);
          }
          else
          {
            toeflInformationDto.UpdatedDate = DateTime.UtcNow;
            toeflInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            var toeflInformationEntity = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(toeflInformationDto);
            _repository.CrmTOEFLInformations.UpdateByState(toeflInformationEntity);
            await _repository.SaveAsync();
          }
        }

        // Save or Update PTE Information
        if (updateDto.EducationInformation?.PTEInformation != null)
        {
          var pteInformationDto = updateDto.EducationInformation?.PTEInformation;
          bool pteInformationExists = await _repository.CrmPTEInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.PTEInformationId == pteInformationDto.PTEInformationId);

          if (!pteInformationExists)
          {
            pteInformationDto.CreatedDate = DateTime.UtcNow;
            pteInformationDto.CreatedBy = currentUser.UserId ?? 0;

            var pteInformationEntity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(pteInformationDto);
            updateDto.EducationInformation?.PTEInformation.PTEInformationId = await _repository.CrmPTEInformations.CreateAndGetIdAsync(pteInformationEntity);
          }
          else
          {
            pteInformationDto.UpdatedDate = DateTime.UtcNow;
            pteInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            var pteInformationEntity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(pteInformationDto);
            _repository.CrmPTEInformations.UpdateByState(pteInformationEntity);
            await _repository.SaveAsync();
          }
        }

        // Save or Update GMAT Information
        if (updateDto.EducationInformation?.GMATInformation != null)
        {
          var gmatInformationDto = updateDto.EducationInformation?.GMATInformation;
          bool gmatInformationExists = await _repository.CrmGMATInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.GMATInformationId == gmatInformationDto.GMATInformationId);

          if (!gmatInformationExists)
          {
            gmatInformationDto.CreatedDate = DateTime.UtcNow;
            gmatInformationDto.CreatedBy = currentUser.UserId ?? 0;

            var gmatInformationEntity = MyMapper.JsonClone<GMATInformationDto, CrmGMATInformation>(gmatInformationDto);
            updateDto.EducationInformation?.GMATInformation.GMATInformationId = await _repository.CrmGMATInformations.CreateAndGetIdAsync(gmatInformationEntity);
          }
          else
          {
            gmatInformationDto.UpdatedDate = DateTime.UtcNow;
            gmatInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            var gmatInformationEntity = MyMapper.JsonClone<GMATInformationDto, CrmGMATInformation>(gmatInformationDto);
            _repository.CrmGMATInformations.UpdateByState(gmatInformationEntity);
            await _repository.SaveAsync();
          }
        }

        // Save OTHERS Information
        if (updateDto.EducationInformation?.OTHERSInformation != null)
        {
          var othersDto = updateDto.EducationInformation?.OTHERSInformation;
          bool othersExists = await _repository.CrmOthersInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.OthersInformationId == othersDto.OthersInformationId);

          if (!othersExists)
          {
            othersDto.CreatedDate = DateTime.UtcNow;
            othersDto.CreatedBy = currentUser.UserId ?? 0;

            var othersEntity = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(othersDto);
            updateDto.EducationInformation?.OTHERSInformation.OthersInformationId = await _repository.CrmOthersInformations.CreateAndGetIdAsync(othersEntity);
          }
          else
          {
            othersDto.UpdatedDate = DateTime.UtcNow;
            othersDto.UpdatedBy = currentUser.UserId ?? 0;
            var othersEntity = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(othersDto);
            _repository.CrmOthersInformations.UpdateByState(othersEntity);
            await _repository.SaveAsync();
          }
        }

        // Save Work Experience
        if (updateDto.EducationInformation?.WorkExperience?.WorkExperienceHistory != null && updateDto.EducationInformation.WorkExperience.WorkExperienceHistory.Any())
        {
          foreach (var workExpDto in updateDto.EducationInformation.WorkExperience.WorkExperienceHistory)
          {
            bool workExpExists = await _repository.CrmWorkExperiences.ExistsAsync(x => x.ApplicantId == workExpDto.ApplicantId && x.WorkExperienceId == workExpDto.WorkExperienceId);

            if (!workExpExists)
            {
              workExpDto.CreatedDate = DateTime.UtcNow;
              workExpDto.CreatedBy = currentUser.UserId ?? 0;

              var workExpEntity = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(workExpDto);
              workExpDto.WorkExperienceId = await _repository.CrmWorkExperiences.CreateAndGetIdAsync(workExpEntity);
            }
            else
            {
              workExpDto.UpdatedDate = DateTime.UtcNow;
              workExpDto.UpdatedBy = currentUser.UserId ?? 0;
              var workExpEntity = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(workExpDto);
              _repository.CrmWorkExperiences.UpdateByState(workExpEntity);
              await _repository.SaveAsync();
            }
          }
        }

        // Save Applicant Reference
        if (updateDto.AdditionalInformation?.ReferenceDetails?.References != null && updateDto.AdditionalInformation.ReferenceDetails.References.Any())
        {
          foreach (var referenceDto in updateDto.AdditionalInformation.ReferenceDetails.References)
          {
            bool referenceExists = await _repository.CrmApplicantReferences.ExistsAsync(x => x.ApplicantId == referenceDto.ApplicantId && x.ApplicantReferenceId == referenceDto.ApplicantReferenceId);

            if (!referenceExists)
            {
              referenceDto.CreatedDate = DateTime.UtcNow;
              referenceDto.CreatedBy = currentUser.UserId ?? 0;

              var referenceEntity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(referenceDto);
              referenceDto.ApplicantReferenceId = await _repository.CrmApplicantReferences.CreateAndGetIdAsync(referenceEntity);
            }
            else
            {
              referenceDto.UpdatedDate = DateTime.UtcNow;
              referenceDto.UpdatedBy = currentUser.UserId ?? 0;
              var referenceEntity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(referenceDto);
              _repository.CrmApplicantReferences.UpdateByState(referenceEntity);
              await _repository.SaveAsync();
            }
          }
        }


        // Save Statement of Purpose
        if (updateDto.AdditionalInformation?.StatementOfPurpose != null)
        {
          var statementDto = updateDto.AdditionalInformation.StatementOfPurpose;
          bool statementExists = await _repository.CrmStatementOfPurposes.ExistsAsync(x => x.ApplicantId == statementDto.ApplicantId && x.StatementOfPurposeId == statementDto.StatementOfPurposeId);

          if (!statementExists)
          {
            statementDto.CreatedDate = DateTime.UtcNow;
            statementDto.CreatedBy = currentUser.UserId ?? 0;

            var statementEntity = MyMapper.JsonClone<StatementOfPurposeDto, CrmStatementOfPurpose>(statementDto);
            updateDto.AdditionalInformation?.StatementOfPurpose.StatementOfPurposeId = await _repository.CrmStatementOfPurposes.CreateAndGetIdAsync(statementEntity);
          }
          else
          {
            statementDto.UpdatedDate = DateTime.UtcNow;
            statementDto.UpdatedBy = currentUser.UserId ?? 0;
            var statementEntity = MyMapper.JsonClone<StatementOfPurposeDto, CrmStatementOfPurpose>(statementDto);
            _repository.CrmStatementOfPurposes.UpdateByState(statementEntity);
            await _repository.SaveAsync();
          }
        }

        // Save Additional Information
        if (updateDto.AdditionalInformation?.AdditionalInformation != null)
        {
          var additionalInfoDto = updateDto.AdditionalInformation.AdditionalInformation;
          bool additionalInfoExists = await _repository.CrmAdditionalInfoes.ExistsAsync(x => x.ApplicantId == additionalInfoDto.ApplicantId && x.AdditionalInfoId == additionalInfoDto.AdditionalInfoId);

          if (!additionalInfoExists)
          {
            additionalInfoDto.CreatedDate = DateTime.UtcNow;
            additionalInfoDto.CreatedBy = currentUser.UserId ?? 0;

            var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(additionalInfoDto);
            updateDto.AdditionalInformation?.AdditionalInformation.AdditionalInfoId = await _repository.CrmAdditionalInfoes.CreateAndGetIdAsync(additionalInfoEntity);
          }
          else
          {
            additionalInfoDto.UpdatedDate = DateTime.UtcNow;
            additionalInfoDto.UpdatedBy = currentUser.UserId ?? 0;
            var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(additionalInfoDto);
            _repository.CrmAdditionalInfoes.UpdateByState(additionalInfoEntity);
            await _repository.SaveAsync();
          }
        }
      }

      // Commit transaction
      //await _repository.CRMApplication.TransactionCommitAsync();
      return updateDto;
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
      if (dto.EducationInformation.EducationDetails?.EducationHistory != null && dto.EducationInformation.EducationDetails?.EducationHistory.Count > 0)
      {
        foreach (var educationDto in dto.EducationInformation.EducationDetails.EducationHistory)
        {
          educationDto.ApplicantId = applicantId;
        }
      }
      else
      {
        dto.EducationInformation.EducationDetails?.EducationHistory = null;
      }

      // IELTS Information
      if (dto.EducationInformation.IELTSInformation != null)
      {
        var total = (dto.EducationInformation.IELTSInformation.IELTSListening + dto.EducationInformation.IELTSInformation.IELTSWriting + dto.EducationInformation.IELTSInformation.IELTSReading + dto.EducationInformation.IELTSInformation.IELTSSpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.IELTSInformation.ApplicantId = applicantId;
        dto.EducationInformation.IELTSInformation = null;
      }

      // TOEFL Information
      if (dto.EducationInformation.TOEFLInformation != null)
      {
        var total = (dto.EducationInformation.TOEFLInformation.TOEFLListening + dto.EducationInformation.TOEFLInformation.TOEFLWriting + dto.EducationInformation.TOEFLInformation.TOEFLReading + dto.EducationInformation.TOEFLInformation.TOEFLSpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.TOEFLInformation.ApplicantId = applicantId;
        dto.EducationInformation.TOEFLInformation = null;
      }

      // PTE Information
      if (dto.EducationInformation.PTEInformation != null)
      {
        var total = (dto.EducationInformation.PTEInformation.PTEListening + dto.EducationInformation.PTEInformation.PTEWriting + dto.EducationInformation.PTEInformation.PTEReading + dto.EducationInformation.PTEInformation.PTESpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.PTEInformation.ApplicantId = applicantId;
        dto.EducationInformation.PTEInformation = null;
      }

      // GMAT Information
      if (dto.EducationInformation.GMATInformation != null)
      {
        var total = (dto.EducationInformation.GMATInformation.GMATListening + dto.EducationInformation.GMATInformation.GMATWriting + dto.EducationInformation.GMATInformation.GMATReading + dto.EducationInformation.GMATInformation.GMATSpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.GMATInformation.ApplicantId = applicantId;
        dto.EducationInformation.GMATInformation = null;
      }

      // OTHERS Information
      if (dto.EducationInformation.OTHERSInformation != null)
      {
        if (dto.EducationInformation.OTHERSInformation.OTHERSScannedCopyFile != null || dto.EducationInformation.OTHERSInformation.OTHERSAdditionalInformation != null)
          dto.EducationInformation.OTHERSInformation.ApplicantId = applicantId;
        dto.EducationInformation.OTHERSInformation = null;
      }

      // Work Experience
      if (dto.EducationInformation.WorkExperience?.WorkExperienceHistory != null && dto.EducationInformation.WorkExperience?.WorkExperienceHistory.Count > 0)
      {
        foreach (var workExpDto in dto.EducationInformation.WorkExperience.WorkExperienceHistory)
        {
          workExpDto.ApplicantId = applicantId;
        }
      }
    }

    // StatementOfPurpose
    if (dto.AdditionalInformation?.StatementOfPurpose != null)
    {
      if (dto.AdditionalInformation?.StatementOfPurpose.StatementOfPurposeFile != null)
        dto.AdditionalInformation?.StatementOfPurpose.ApplicantId = applicantId;
      dto.AdditionalInformation?.StatementOfPurpose = null;
    }


    // AdditionalInformation
    if (dto.AdditionalInformation?.AdditionalInformation != null)
    {
      if (!string.IsNullOrEmpty(dto.AdditionalInformation?.AdditionalInformation.HealthNMedicalNeedsRemarks))
        dto.AdditionalInformation?.AdditionalInformation.ApplicantId = applicantId;
      dto.AdditionalInformation?.AdditionalInformation = null;
    }


    // Additional Information Section - Only for References and Additional Documents
    if (dto.AdditionalInformation != null)
    {
      // Reference Details - These use actual ApplicantId
      if (dto.AdditionalInformation.ReferenceDetails?.References != null && dto.AdditionalInformation.ReferenceDetails?.References.Count> 0)
      {
        foreach (var referenceDto in dto.AdditionalInformation.ReferenceDetails.References)
        {
          referenceDto.ApplicantId = applicantId;
        }
      }

      // Additional Documents - These use actual ApplicantId  
      if (dto.AdditionalInformation.AdditionalDocuments?.Documents != null && dto.AdditionalInformation.AdditionalDocuments?.Documents.Count > 0)
      {
        foreach (var additionalDoc in dto.AdditionalInformation.AdditionalDocuments.Documents)
        {
          additionalDoc.ApplicantId = applicantId;
        }
      }

    }

  }

}