using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.ServicesContract.CRM;
using bdDevCRM.ServiceContract.CRM;

namespace bdDevCRM.ServicesContract;

public interface IServiceManager
{
  IPropertyInspectorService PropertyInspectorService { get; }
  ITokenBlacklistService TokenBlacklist { get; }
  ICrmCountryService CrmCountries { get; }
  ICurrencyService Currencies { get; }
  ICompanyService Companies { get; }
  ISystemSettingsService SystemSettings { get; }
  IUsersService Users { get; }
  IAuthenticationService CustomAuthentication { get; }
  IMenuService Menus { get; }
  IModuleService Modules { get; }
  IGroupService Groups { get; }
  IQueryAnalyzerService QueryAnalyzer { get; }
  IStatusService WfState { get; }
  IAccessControlService AccessControl { get; }

  #region HR
  IEmployeeService Employees { get; }
  IBranchService Branches { get; }
  IDepartmentService departments { get; }
  #endregion HR

  #region Crm
  ICrmInstituteService CrmInstitutes { get; }
  ICrmInstituteTypeService CrmInstituteTypes { get; }
  ICrmCourseService CrmCourses { get; }
  ICrmMonthService CrmMonths { get; }
  ICrmYearService CrmYears { get; }
  
  // Existing Crm services
  ICrmApplicationService CrmApplications { get; }
  ICrmApplicantCourseService ApplicantCourses { get; }
  ICrmApplicantInfoService ApplicantInfos { get; }
  ICrmPermanentAddressService PermanentAddresses { get; }
  ICrmPresentAddressService PresentAddresses { get; }
  
  // New 10 Crm services
  ICrmEducationHistoryService EducationHistories { get; }
  ICrmIELTSInformationService IELTSInformations { get; }
  ICrmTOEFLInformationService TOEFLInformations { get; }
  ICrmPTEInformationService PTEInformations { get; }
  ICrmGmatinformationService GMATInformations { get; }
  ICrmOthersInformationService OTHERSInformations { get; }
  ICrmWorkExperienceService WorkExperiences { get; }
  ICrmApplicantReferenceService ApplicantReferences { get; }
  ICrmStatementOfPurposeService StatementOfPurposes { get; }
  ICrmAdditionalInfoService AdditionalInfos { get; }
  ICrmAdditionalDocumentService AdditionalDocuments { get; }
  
  // New CRM services
  ICrmIntakeMonthService CrmIntakeMonths { get; }
  ICrmIntakeYearService CrmIntakeYears { get; }
  ICrmPaymentMethodService CrmPaymentMethods { get; }
  #endregion Crm

  #region DMS
  IDmsDocumentService DmsDocuments { get; }
  IDmsDocumentTypeService DmsDocumentTypes { get; }
  IDmsDocumentTagService DmsDocumentTags { get; }
  IDmsDocumentTagMapService DmsDocumentTagMaps { get; }
  IDmsDocumentFolderService DmsDocumentFolders { get; }
  IDmsDocumentVersionService DmsDocumentVersions { get; }
  IDmsDocumentAccessLogService DmsDocumentAccessLogs { get; }
  #endregion

  T GetCache<T>(int key);
}

