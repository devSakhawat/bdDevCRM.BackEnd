using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.CRM;
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


  public async Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(CRMGridOptions options ,int statusId ,UsersDto usersDto ,MenuDto menuDto)
  {
    try
    {
      //string condition = "";
      //string condition1 = "";
      if (menuDto.MenuId != null && menuDto.MenuId != 0)
      {
        //IEnumerable<GroupPermissionRepositoryDto> returnResult = await _repository.Groups.GetAccessPermisionForCurrentUser(menuDto.ModuleId.Value, usersDto.UserId.Value);
        //IEnumerable<GroupPermissionDto> result = MyMapper.JsonCloneIEnumerableToIEnumerable<GroupPermissionRepositoryDto, GroupPermissionDto>(returnResult);

        //var isApprover = result.Any(groupPermission => groupPermission.ReferenceID == 4);
        //var isRecomander = result.Any(groupPermission => groupPermission.ReferenceID == 3);
        //var isHr = result.Any(groupPermission => groupPermission.ReferenceID == 22);
        //var onlyApprovalData = result.Any(groupPermission => groupPermission.ReferenceID == 23);
        //var state = wfstateSvc.GetWfStateById(wfStateId);
      }

      IEnumerable<GroupPermissionRepositoryDto> returnResult = await _repository.Groups.GetAccessPermisionForCurrentUser(menuDto.ModuleId.Value, usersDto.UserId.Value);
      IEnumerable<GroupPermissionDto> result = MyMapper.JsonCloneIEnumerableToIEnumerable<GroupPermissionRepositoryDto, GroupPermissionDto>(returnResult);

      var isApprover = result.Any(groupPermission => groupPermission.ReferenceID == 4);
      var isRecomander = result.Any(groupPermission => groupPermission.ReferenceID == 3);
      var isHr = result.Any(groupPermission => groupPermission.ReferenceID == 22);
      var onlyApprovalData = result.Any(groupPermission => groupPermission.ReferenceID == 23);


      string condition = string.Empty;

      string sql = string.Format(
              @"SELECT
      ca.ApplicationId,
      ca.ApplicationDate,
      ca.StateId,
      doc.FilePath as ApplicantImagePath,
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

      LEFT JOIN CrmIELTSInformation ieltsInfo ON ai.ApplicantId = ieltsInfo.ApplicantId
      LEFT JOIN CrmTOEFLInformation toeflInfo ON ai.ApplicantId = toeflInfo.ApplicantId
      LEFT JOIN CrmPTEInformation pteInfo ON ai.ApplicantId = pteInfo.ApplicantId
      LEFT JOIN CrmOTHERSInformation othersInfo ON ai.ApplicantId = othersInfo.ApplicantId
      LEFT JOIN CrmStatementOfPurpose sp ON sp.ApplicantId = ai.ApplicantId
    
      --LEFT JOIN CrmEducationHistory edu ON ai.ApplicantId = edu.ApplicantId      
      --LEFT JOIN CrmWorkExperience workEx ON ca.ApplicationId = workEx.ApplicantId
      --LEFT JOIN CrmApplicantReference ar  ON ar.ApplicantId = ai.ApplicantId
      --LEFT JOIN CrmAdditionalInfo  ON CrmAdditionalInfo.ApplicantId = ai.ApplicantId

      OUTER APPLY (
          SELECT TOP 1 d.FilePath
          FROM DmsDocument d
          WHERE d.ReferenceEntityType = 'ApplicantInfo'
            AND d.ReferenceEntityId = ai.ApplicantId
          ORDER BY d.UploadDate DESC
      ) doc

  ");
      string orderBy = " ApplicationId asc ";

      var gridResult = await _repository.CrmApplications.GridData<CrmApplicationGridDto>(sql, options, orderBy, condition);
      return gridResult;
    }
    catch (DataMappingException ex)
    {
      _logger.LogError($"Grid mapping error: {ex.Message}");
      throw new BadRequestException($"Grid data mapping error. {ex.Message}");
    }
  }

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
      docs.ApplicantImagePath,
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
      ISNULL(ac.CountryId, 0) AS CountryId,
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
      docs.IELTSScannedCopyPath,
      docs.IELTSScannedCopyFileName,
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
      docs.TOEFLScannedCopyPath,
      docs.TOEFLScannedCopyFileName,
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
      docs.PTEScannedCopyPath,
      docs.PTEScannedCopyFileName,
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
      docs.GMATScannedCopyPath,
      gmat.GMATAdditionalInformation,
      docs.GMATScannedCopyFileName,
      gmat.CreatedDate AS GMAT_CreatedDate,
      gmat.CreatedBy AS GMAT_CreatedBy,
      gmat.UpdatedDate AS GMAT_UpdatedDate,
      gmat.UpdatedBy AS GMAT_UpdatedBy,

      -- ================================
      -- OTHERSInformationDto
      -- ================================
      ISNULL(others.OTHERSInformationId, 0) AS OthersInformationId,
      ISNULL(others.ApplicantId, 0) AS OTHERS_ApplicantId,
      others.AdditionalInformation as OTHERSAdditionalInformation,
      docs.OTHERSScannedCopyPath,
      docs.OTHERSScannedCopyFileName,
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
      docs.StatementOfPurposeFilePath,
		  docs.StatementOfPurposeFileName,
      --sop.CreatedDate AS SOP_CreatedDate,
      --sop.CreatedBy AS SOP_CreatedBy,
      --sop.UpdatedDate AS SOP_UpdatedDate,
      --sop.UpdatedBy AS SOP_UpdatedBy

      -- ================================
      -- AdditionalInfoDto
      -- ================================
      addInfo.AdditionalInfoId,
      ISNULL(addInfo.ApplicantId, 0) AS AddInfo_ApplicantId,
      addInfo.RequireAccommodation,
      addInfo.HealthNMedicalNeeds,
      addInfo.HealthNMedicalNeedsRemarks,
      addInfo.AdditionalInformationRemarks
  
      --,addInfo.UpdatedDate AS AddInfo_CreatedDate,
      --ISNULL(addInfo.CreatedBy, 0) AS AddInfo_CreatedBy,
      --addInfo.UpdatedDate AS AddInfo_UpdatedDate,
      --addInfo.UpdatedBy AS AddInfo_UpdatedBy

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
    LEFT JOIN CrmStatementOfPurpose sop ON ai.ApplicantId = sop.ApplicantId
    LEFT JOIN CrmAdditionalInfo addInfo ON ai.ApplicantId = addInfo.ApplicantId

    -- SINGLE OUTER APPLY for all DMS files (including FileName)
    OUTER APPLY (
      SELECT
        MAX(CASE WHEN d.ReferenceEntityType = 'ApplicantInfo'       THEN d.FilePath END) AS ApplicantImagePath,
        MAX(CASE WHEN d.ReferenceEntityType = 'ApplicantInfo'       THEN d.FileName END) AS ApplicantImageFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'IELTSInformation'    THEN d.FilePath END) AS IELTSScannedCopyPath,
        MAX(CASE WHEN d.ReferenceEntityType = 'IELTSInformation'    THEN d.FileName END) AS IELTSScannedCopyFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'TOEFLInformation'    THEN d.FilePath END) AS TOEFLScannedCopyPath,
        MAX(CASE WHEN d.ReferenceEntityType = 'TOEFLInformation'    THEN d.FileName END) AS TOEFLScannedCopyFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'PTEInformation'      THEN d.FilePath END) AS PTEScannedCopyPath,
        MAX(CASE WHEN d.ReferenceEntityType = 'PTEInformation'      THEN d.FileName END) AS PTEScannedCopyFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'GMATInformation'     THEN d.FilePath END) AS GMATScannedCopyPath,
        MAX(CASE WHEN d.ReferenceEntityType = 'GMATInformation'     THEN d.FileName END) AS GMATScannedCopyFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'OTHERSInformation'   THEN d.FilePath END) AS OTHERSScannedCopyPath,
        MAX(CASE WHEN d.ReferenceEntityType = 'OTHERSInformation'   THEN d.FileName END) AS OTHERSScannedCopyFileName,
        MAX(CASE WHEN d.ReferenceEntityType = 'StatementOfPurpose'  THEN d.FilePath END) AS StatementOfPurposeFilePath,
        MAX(CASE WHEN d.ReferenceEntityType = 'StatementOfPurpose'  THEN d.FileName END) AS StatementOfPurposeFileName
      FROM (
        SELECT FilePath, FileName, ReferenceEntityType,
               ROW_NUMBER() OVER (PARTITION BY ReferenceEntityType ORDER BY UploadDate DESC) AS rn
        FROM DmsDocument
        WHERE ReferenceEntityId = CONVERT(nvarchar(50), 4)
          AND ReferenceEntityType IN (
          'ApplicantInfo', 'IELTSInformation', 'TOEFLInformation' , 'PTEInformation', 'GMATInformation'
          ,'OTHERSInformation','StatementOfPurpose'
          )
      ) d
      WHERE d.rn = 1
    ) docs
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

    #region Comment_Code
    //CrmApplicationDto crmApplicationDto = new CrmApplicationDto();
    //crmApplicationDto = MyMapper.JsonClone<GetApplicationDto, CrmApplicationDto>(result);
    // Map the result to ApplicationDto, ApplicantCourseDto, and other nested DTOs using MyMapper
    // Note: MyMapper.JsonClone is assumed to be a method that maps one DTO to another using JSON serialization/deserialization
    // Applicant Personal Info with course information

    //crmApplicationDto.CourseInformation.PersonalDetails = MyMapper.JsonClone<GetApplicationDto, ApplicantInfoDto>(result);
    //crmApplicationDto.CourseInformation.ApplicantCourse = MyMapper.JsonClone<GetApplicationDto, ApplicantCourseDto>(result);
    //crmApplicationDto.CourseInformation.ApplicantAddress.PresentAddress = MyMapper.JsonClone<GetApplicationDto, PresentAddressDto>(result);
    //crmApplicationDto.CourseInformation.ApplicantAddress.PermanentAddress = MyMapper.JsonClone<GetApplicationDto, PermanentAddressDto>(result);

    //// English Language Test Information
    //crmApplicationDto.EducationInformation.IELTSInformation = MyMapper.JsonClone<GetApplicationDto, IELTSInformationDto>(result);
    //crmApplicationDto.EducationInformation.TOEFLInformation = MyMapper.JsonClone<GetApplicationDto, TOEFLInformationDto>(result);
    //crmApplicationDto.EducationInformation.PTEInformation = MyMapper.JsonClone<GetApplicationDto, PTEInformationDto>(result);
    //crmApplicationDto.EducationInformation.GMATInformation = MyMapper.JsonClone<GetApplicationDto, GMATInformationDto>(result);
    //crmApplicationDto.EducationInformation.OTHERSInformation = MyMapper.JsonClone<GetApplicationDto, OTHERSInformationDto>(result);

    //// Education HistoryList and  Work Experience List
    //IEnumerable<CrmEducationHistory> education = await _repository.CrmEducationHistories.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.EducationHistoryId, trackChanges: false);

    //crmApplicationDto.EducationInformation.EducationDetails.EducationHistory
    //  = MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(education);
    //crmApplicationDto.EducationInformation.EducationDetails.TotalEducationRecords = education.Count();

    //IEnumerable<CrmWorkExperience> workExperiences = await _repository.CrmWorkExperiences.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.WorkExperienceId, trackChanges: false);
    //crmApplicationDto.EducationInformation.WorkExperience.WorkExperienceHistory
    //  = MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(workExperiences);
    //crmApplicationDto.EducationInformation.WorkExperience.TotalWorkExperienceRecords = workExperiences.Count();

    //// Additional Information Section DTOs
    //// Applicant Reference List
    //IEnumerable<CrmApplicantReference> applicantReferences = await _repository.CrmApplicantReferences.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.ApplicantReferenceId, trackChanges: false);
    //crmApplicationDto.AdditionalInformation.ReferenceDetails.References
    //  = MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(applicantReferences);
    //crmApplicationDto.AdditionalInformation.ReferenceDetails.TotalReferenceRecords = applicantReferences.Count();

    //// Additional Information Section DTOs
    //crmApplicationDto.AdditionalInformation.StatementOfPurpose = MyMapper.JsonClone<GetApplicationDto, StatementOfPurposeDto>(result);
    //crmApplicationDto.AdditionalInformation.AdditionalInformation = MyMapper.JsonClone<GetApplicationDto, AdditionalInfoDto>(result);

    // If you have AdditionalDocumentDto, you can map it similarly
    // Additional Document List
    //Enumerable<AdditionalDocumentDto> additionalDocumentDto = MyMapper.JsonClone<GetApplicationDto, AdditionalDocumentDto>(result);
    #endregion Comment_Code

    #region Educational_History_Start
    var educationHistories = await _repository.CrmEducationHistories.EducationHistoryByApplicantId(result.ApplicantId);

    if (educationHistories != null && educationHistories.Any())
    {
      result.EducationHistories = MyMapper.JsonCloneIEnumerableToIEnumerable<EducationHistoryRepositoryDto, EducationHistoryDto>(educationHistories);
    }
    #endregion Educational_History_End

    #region Work_Experience_start
    var workExperiences = await _repository.CrmWorkExperiences.WorkExperiencesByApplicantId(result.ApplicantId);

    if (workExperiences != null && workExperiences.Any())
    {
      result.WorkExperienceHistories = MyMapper.JsonCloneIEnumerableToIEnumerable<WorkExperienceHistoryRepositoryDto, WorkExperienceHistoryDto>(workExperiences);
    }
    #endregion Work_Experience_End

    #region References_Start
    var references = await _repository.CrmApplicantReferences.ListByConditionAsync(expression: x => x.ApplicantId == result.ApplicantId, orderBy: x => x.ApplicantReferenceId, trackChanges: false);

    if (references != null && references.Any())
    {
      result.ApplicantReferences = MyMapper.JsonCloneIEnumerableToIEnumerable<CrmApplicantReference, ApplicantReferenceDto>(references);
    }
    #endregion References_End

    #region Additional_Document_Start
    var additionalDocuments = await _repository.CrmAdditionalDocuments.AdditionalDocumentsByApplicantId(result.ApplicantId);

    if (additionalDocuments != null && additionalDocuments.Any())
    {
      result.AdditionalDocuments = MyMapper.JsonCloneIEnumerableToIEnumerable<AdditionalDocumentRepositoryDto, AdditionalDocumentDto>(additionalDocuments);
    }
    #endregion Additional_Document_End

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
        }

        // Save Additional Documents
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
    if (!exists) throw new GenericNotFoundException("Application", "ApplicationId", key.ToString());

    try
    {
      // Set audit fields for CrmApplication
      updateDto.UpdatedDate = DateTime.UtcNow;
      updateDto.UpdatedBy = currentUser.UserId ?? 0;

      // 1. Update CrmApplication 
      var crmApplicationDB = await _repository.CrmApplications.FirstOrDefaultAsync(expression: x => x.ApplicationId == updateDto.ApplicationId, trackChanges: false);

      crmApplicationDB.StateId = updateDto.StateId;
      crmApplicationDB.ApplicationDate = (crmApplicationDB.ApplicationDate > DateTime.MinValue)
                                              ? crmApplicationDB.ApplicationDate : DateTime.UtcNow;
      crmApplicationDB.CreatedDate = (crmApplicationDB.CreatedDate > DateTime.MinValue)
                                   ? crmApplicationDB.CreatedDate
                                   : DateTime.UtcNow;
      crmApplicationDB.CreatedBy = (crmApplicationDB.CreatedBy > 0)
                                ? crmApplicationDB.CreatedBy
                                : (currentUser.UserId ?? 0);
      crmApplicationDB.UpdatedDate = DateTime.UtcNow;
      crmApplicationDB.UpdatedBy = currentUser.UserId ?? 0;


      _repository.CrmApplications.UpdateByState(crmApplicationDB);
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
            applicantInfoDto.CreatedBy = (permanentAddresseDB.CreatedBy > 0) ? permanentAddresseDB.CreatedBy : (currentUser.UserId ?? 0);

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
              var educationHisotryDB = await _repository.CrmEducationHistories.FirstOrDefaultAsync(x => x.ApplicantId == educationDto.ApplicantId && x.EducationHistoryId == educationDto.EducationHistoryId);

              educationDto.CreatedDate = DateTimeFormatters.IsValidDateTime(educationHisotryDB.CreatedDate) ? educationHisotryDB.CreatedDate : DateTime.UtcNow;
              educationDto.CreatedBy = (educationHisotryDB.CreatedBy > 0) ? educationHisotryDB.CreatedBy : (currentUser.UserId ?? 0);

              educationDto.UpdatedDate = DateTime.UtcNow;
              educationDto.UpdatedBy = currentUser.UserId ?? 0;
              educationHisotryDB = MyMapper.JsonCloneSafe<EducationHistoryDto, CrmEducationHistory>(educationDto);
              _repository.CrmEducationHistories.UpdateByState(educationHisotryDB);
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

            var ieltsInformationDB = await _repository.CrmIELTSInformations.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.IELTSInformationId == ieltsInformationDto.IELTSInformationId);

            ieltsInformationDto.CreatedDate = DateTimeFormatters.IsValidDateTime(ieltsInformationDB.CreatedDate)
                                           ? ieltsInformationDB.CreatedDate
                                           : DateTime.UtcNow;
            ieltsInformationDto.CreatedBy = (ieltsInformationDB.CreatedBy > 0)
                                      ? ieltsInformationDB.CreatedBy
                                      : (currentUser.UserId ?? 0);

            ieltsInformationDto.UpdatedDate = DateTime.UtcNow;
            ieltsInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            ieltsInformationDB = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(ieltsInformationDto);
            _repository.CrmIELTSInformations.UpdateByState(ieltsInformationDB);
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
            var toeflInformationDB = await _repository.CrmTOEFLInformations.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.TOEFLInformationId == toeflInformationDto.TOEFLInformationId);
            toeflInformationDto.CreatedDate = DateTimeFormatters.IsValidDateTime(toeflInformationDB.CreatedDate) ? toeflInformationDB.CreatedDate : DateTime.UtcNow;
            toeflInformationDto.CreatedBy = (toeflInformationDB.CreatedBy > 0) ? toeflInformationDB.CreatedBy : (currentUser.UserId ?? 0);

            toeflInformationDto.UpdatedDate = DateTime.UtcNow;
            toeflInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            toeflInformationDB = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(toeflInformationDto);
            _repository.CrmTOEFLInformations.UpdateByState(toeflInformationDB);
            await _repository.SaveAsync();
          }
        }

        // Save or Update PTE Information
        if (updateDto.EducationInformation?.PTEInformation != null)
        {
          var PTEInformationDto = updateDto.EducationInformation?.PTEInformation;
          bool PTEInformationExists = await _repository.CrmPTEInformations.ExistsAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.PTEInformationId == PTEInformationDto.PTEInformationId);

          if (!PTEInformationExists)
          {

            PTEInformationDto.CreatedDate = DateTime.UtcNow;
            PTEInformationDto.CreatedBy = currentUser.UserId ?? 0;

            var PTEInformationEntity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(PTEInformationDto);
            updateDto.EducationInformation?.PTEInformation.PTEInformationId = await _repository.CrmPTEInformations.CreateAndGetIdAsync(PTEInformationEntity);
          }
          else
          {
            var PTEInformationDB = await _repository.CrmPTEInformations.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.PTEInformationId == PTEInformationDto.PTEInformationId);

            PTEInformationDto.CreatedDate = DateTimeFormatters.IsValidDateTime(PTEInformationDB.CreatedDate) ? PTEInformationDB.CreatedDate : DateTime.UtcNow;
            PTEInformationDto.CreatedBy = (PTEInformationDB.CreatedBy > 0) ? PTEInformationDB.CreatedBy : (currentUser.UserId ?? 0);

            PTEInformationDto.UpdatedDate = DateTime.UtcNow;
            PTEInformationDto.UpdatedBy = currentUser.UserId ?? 0;
            PTEInformationDB = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(PTEInformationDto);
            _repository.CrmPTEInformations.UpdateByState(PTEInformationDB);
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
            var gmatInformationDB = await _repository.CrmGMATInformations.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.GMATInformationId == gmatInformationDto.GMATInformationId);

            gmatInformationDto.CreatedDate = DateTimeFormatters.IsValidDateTime(gmatInformationDB.CreatedDate) ? gmatInformationDB.CreatedDate : DateTime.UtcNow;
            gmatInformationDto.CreatedBy = (gmatInformationDB.CreatedBy > 0) ? gmatInformationDB.CreatedBy : (currentUser.UserId ?? 0);

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
            var othersInformationDB = await _repository.CrmOthersInformations.FirstOrDefaultAsync(x => x.ApplicantId == applicantInfoDto.ApplicantId && x.OthersInformationId == othersDto.OthersInformationId);

            othersDto.CreatedDate = DateTimeFormatters.IsValidDateTime(othersInformationDB.CreatedDate) ? othersInformationDB.CreatedDate : DateTime.UtcNow;
            othersDto.CreatedBy = (othersInformationDB.CreatedBy > 0) ? othersInformationDB.CreatedBy : (currentUser.UserId ?? 0);

            othersDto.UpdatedDate = DateTime.UtcNow;
            othersDto.UpdatedBy = currentUser.UserId ?? 0;
            othersInformationDB = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(othersDto);
            _repository.CrmOthersInformations.UpdateByState(othersInformationDB);
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
              var workExperienceDB = await _repository.CrmWorkExperiences.FirstOrDefaultAsync(x => x.ApplicantId == workExpDto.ApplicantId && x.WorkExperienceId == workExpDto.WorkExperienceId);
              workExpDto.CreatedDate = DateTimeFormatters.IsValidDateTime(workExperienceDB.CreatedDate) ? workExperienceDB.CreatedDate : DateTime.UtcNow;
              workExpDto.CreatedBy = (workExperienceDB.CreatedBy > 0) ? workExperienceDB.CreatedBy : (currentUser.UserId ?? 0);

              workExpDto.UpdatedDate = DateTime.UtcNow;
              workExpDto.UpdatedBy = currentUser.UserId ?? 0;
              workExperienceDB = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(workExpDto);
              _repository.CrmWorkExperiences.UpdateByState(workExperienceDB);
              await _repository.SaveAsync();
            }
          }
        }

        // Save Applicant Reference
        if (updateDto.AdditionalInformation?.ReferenceDetails?.References != null && updateDto.AdditionalInformation.ReferenceDetails.References.Any())
        {
          var incomingRefs = updateDto.AdditionalInformation.ReferenceDetails.References;
          var existingRefs = (await _repository.CrmApplicantReferences.GetApplicantReferencesByApplicantIdAsync(applicantInfoDto.ApplicantId, false)).ToList();

          var toDelete = existingRefs.Where(dbRef => !incomingRefs.Any(ir => ir.ApplicantReferenceId == dbRef.ApplicantReferenceId)).ToList();

          foreach (var del in toDelete)
          {
            _repository.CrmApplicantReferences.Delete(del);
          }
          if (toDelete.Any()) await _repository.SaveAsync();

          // Create / Update
          foreach (var referenceDto in incomingRefs)
          {
            bool referenceExists = referenceDto.ApplicantReferenceId > 0 && existingRefs.Any(r => r.ApplicantReferenceId == referenceDto.ApplicantReferenceId);

            if (!referenceExists)
            {
              referenceDto.CreatedDate = DateTime.UtcNow;
              referenceDto.CreatedBy = currentUser.UserId ?? 0;
              var referenceEntity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(referenceDto);
              referenceDto.ApplicantReferenceId = await _repository.CrmApplicantReferences.CreateAndGetIdAsync(referenceEntity);
            }
            else
            {
              var referenceDB = existingRefs.First(r => r.ApplicantReferenceId == referenceDto.ApplicantReferenceId);
              referenceDto.CreatedDate = DateTimeFormatters.IsValidDateTime(referenceDB.CreatedDate) ? referenceDB.CreatedDate : DateTime.UtcNow;
              referenceDto.CreatedBy = (referenceDB.CreatedBy > 0) ? referenceDB.CreatedBy : (currentUser.UserId ?? 0);
              referenceDto.UpdatedDate = DateTime.UtcNow;
              referenceDto.UpdatedBy = currentUser.UserId ?? 0;
              referenceDB = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(referenceDto);
              _repository.CrmApplicantReferences.UpdateByState(referenceDB);
            }
          }

          await _repository.SaveAsync();


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
            var statementDB = await _repository.CrmStatementOfPurposes.FirstOrDefaultAsync(x => x.ApplicantId == statementDto.ApplicantId && x.StatementOfPurposeId == statementDto.StatementOfPurposeId);
            statementDto.CreatedDate = DateTimeFormatters.IsValidDateTime(statementDB.CreatedDate) ? statementDB.CreatedDate : DateTime.UtcNow;
            statementDto.CreatedBy = (statementDB.CreatedBy > 0) ? statementDB.CreatedBy : (currentUser.UserId ?? 0);

            statementDto.UpdatedDate = DateTime.UtcNow;
            statementDto.UpdatedBy = currentUser.UserId ?? 0;
            statementDB = MyMapper.JsonClone<StatementOfPurposeDto, CrmStatementOfPurpose>(statementDto);
            _repository.CrmStatementOfPurposes.UpdateByState(statementDB);
            await _repository.SaveAsync();
          }
        }

        // Save Additional Information
        if (updateDto.AdditionalInformation?.AdditionalInformation != null)
        {
          var additionalInfoDto = updateDto.AdditionalInformation.AdditionalInformation;
          bool additionalInfoExists = await _repository.CrmAdditionalInfoes.ExistsAsync(x => x.ApplicantId == additionalInfoDto.ApplicantId && x.AdditionalInfoId == additionalInfoDto.AdditionalInfoId);

          if (additionalInfoExists == false)
          {
            additionalInfoDto.CreatedDate = DateTime.UtcNow;
            additionalInfoDto.CreatedBy = currentUser.UserId ?? 0;

            var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(additionalInfoDto);
            updateDto.AdditionalInformation?.AdditionalInformation.AdditionalInfoId = await _repository.CrmAdditionalInfoes.CreateAndGetIdAsync(additionalInfoEntity);
          }
          else
          {
            //var additionalInfoDB = await _repository.CrmAdditionalInfoes.FirstOrDefaultAsync(x => x.ApplicantId == additionalInfoDto.ApplicantId && x.AdditionalInfoId == additionalInfoDto.AdditionalInfoId);
            var additionalInfoDB = await _repository.CrmAdditionalInfoes.FirstOrDefaultAsync(x => x.ApplicantId == additionalInfoDto.ApplicantId);
            additionalInfoDto.CreatedDate = DateTimeFormatters.IsValidDateTime(additionalInfoDB.CreatedDate) ? additionalInfoDB.CreatedDate : DateTime.UtcNow;
            additionalInfoDto.CreatedBy = (additionalInfoDB.CreatedBy > 0) ? additionalInfoDB.CreatedBy : (currentUser.UserId ?? 0);

            additionalInfoDto.UpdatedDate = DateTime.UtcNow;
            additionalInfoDto.UpdatedBy = currentUser.UserId ?? 0;
            var additionalInfoEntity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(additionalInfoDto);
            _repository.CrmAdditionalInfoes.UpdateByState(additionalInfoEntity);
            await _repository.SaveAsync();
          }
        }

        // Save Additional Documents
        if (updateDto.AdditionalInformation?.AdditionalDocuments?.Documents != null && updateDto.AdditionalInformation.AdditionalDocuments.Documents.Any())
        {
          var incomingDocs = updateDto.AdditionalInformation.AdditionalDocuments.Documents;
          int docsApplicantId = incomingDocs.FirstOrDefault()?.ApplicantId ?? applicantInfoDto.ApplicantId;

          // Option A: use ListByConditionAsync with a proper expression
          var existingDocs = (await _repository.CrmAdditionalDocuments.ListByConditionAsync(
                                  expression: x => x.ApplicantId == docsApplicantId,
                                  orderBy: x => x.AdditionalDocumentId,
                                  trackChanges: false)).ToList();

          // Delete missing ones
          var toDelete = existingDocs
            .Where(dbDoc => !incomingDocs.Any(inDoc => inDoc.AdditionalDocumentId == dbDoc.AdditionalDocumentId))
            .ToList();

          foreach (var del in toDelete)
          {
            _repository.CrmAdditionalDocuments.Delete(del);
          }
          if (toDelete.Any()) await _repository.SaveAsync();

          // Create/Update
          foreach (var documentDto in incomingDocs)
          {
            bool existsDoc = existingDocs.Any(d => d.AdditionalDocumentId == documentDto.AdditionalDocumentId);

            if (!existsDoc)
            {
              documentDto.CreatedDate = DateTime.UtcNow;
              documentDto.CreatedBy = currentUser.UserId ?? 0;

              var entity = MyMapper.JsonClone<AdditionalDocumentDto, CrmAdditionalDocument>(documentDto);
              documentDto.AdditionalDocumentId = await _repository.CrmAdditionalDocuments.CreateAndGetIdAsync(entity);
            }
            else
            {
              var dbDoc = existingDocs.First(d => d.AdditionalDocumentId == documentDto.AdditionalDocumentId);

              documentDto.CreatedDate = DateTimeFormatters.IsValidDateTime(dbDoc.CreatedDate) ? dbDoc.CreatedDate : DateTime.UtcNow;
              documentDto.CreatedBy = (dbDoc.CreatedBy > 0) ? dbDoc.CreatedBy : (currentUser.UserId ?? 0);
              documentDto.UpdatedDate = DateTime.UtcNow;
              documentDto.UpdatedBy = currentUser.UserId ?? 0;

              var updateEntity = MyMapper.JsonClone<AdditionalDocumentDto, CrmAdditionalDocument>(documentDto);
              _repository.CrmAdditionalDocuments.UpdateByState(updateEntity);
            }
          }

          await _repository.SaveAsync();
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
        else
          dto.EducationInformation.IELTSInformation = null;
      }

      // TOEFL Information
      if (dto.EducationInformation.TOEFLInformation != null)
      {
        var total = (dto.EducationInformation.TOEFLInformation.TOEFLListening + dto.EducationInformation.TOEFLInformation.TOEFLWriting + dto.EducationInformation.TOEFLInformation.TOEFLReading + dto.EducationInformation.TOEFLInformation.TOEFLSpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.TOEFLInformation.ApplicantId = applicantId;
        else
          dto.EducationInformation.TOEFLInformation = null;
      }

      // PTE Information
      if (dto.EducationInformation.PTEInformation != null)
      {
        var total = (dto.EducationInformation.PTEInformation.PTEListening + dto.EducationInformation.PTEInformation.PTEWriting + dto.EducationInformation.PTEInformation.PTEReading + dto.EducationInformation.PTEInformation.PTESpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.PTEInformation.ApplicantId = applicantId;
        else
          dto.EducationInformation.PTEInformation = null;
      }

      // GMAT Information
      if (dto.EducationInformation.GMATInformation != null)
      {
        var total = (dto.EducationInformation.GMATInformation.GMATListening + dto.EducationInformation.GMATInformation.GMATWriting + dto.EducationInformation.GMATInformation.GMATReading + dto.EducationInformation.GMATInformation.GMATSpeaking);
        if (total != null && total > 0)
          dto.EducationInformation.GMATInformation.ApplicantId = applicantId;
        else
          dto.EducationInformation.GMATInformation = null;
      }

      // OTHERS Information
      if (dto.EducationInformation.OTHERSInformation != null)
      {
        if (dto.EducationInformation.OTHERSInformation.OTHERSScannedCopyFile != null || dto.EducationInformation.OTHERSInformation.AdditionalInformation != null)
          dto.EducationInformation.OTHERSInformation.ApplicantId = applicantId;
        else
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
      else
        dto.AdditionalInformation?.StatementOfPurpose = null;
    }


    // AdditionalInformation
    if (dto.AdditionalInformation?.AdditionalInformation != null)
    {
      if (!string.IsNullOrEmpty(dto.AdditionalInformation?.AdditionalInformation.HealthNMedicalNeedsRemarks))
        dto.AdditionalInformation?.AdditionalInformation.ApplicantId = applicantId;
      else
        dto.AdditionalInformation?.AdditionalInformation = null;
    }


    // Additional Information Section - Only for References and Additional Documents
    if (dto.AdditionalInformation != null)
    {
      // Reference Details - These use actual ApplicantId
      if (dto.AdditionalInformation.ReferenceDetails?.References != null && dto.AdditionalInformation.ReferenceDetails?.References.Count > 0)
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