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
  ITokenBlacklistService TokenBlacklist { get; }
  ICountryService Countries { get; }
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

  #region CRM
  ICRMInstituteService CRMInstitutes { get; }
  ICRMInstituteTypeService CRMInstituteTypes { get; }
  ICRMCourseService CRMCourses { get; }
  ICRMMonthService CRMMonths { get; }
  ICRMYearService CRMYears { get; }
  
  // Existing CRM services
  IApplicantCourseService ApplicantCourses { get; }
  IApplicantInfoService ApplicantInfos { get; }
  IPermanentAddressService PermanentAddresses { get; }
  IPresentAddressService PresentAddresses { get; }
  
  // New 10 CRM services
  IEducationHistoryService EducationHistories { get; }
  IIELTSInformationService IELTSInformations { get; }
  ITOEFLInformationService TOEFLInformations { get; }
  IPTEInformationService PTEInformations { get; }
  IGMATInformationService GMATInformations { get; }
  IOTHERSInformationService OTHERSInformations { get; }
  IWorkExperienceService WorkExperiences { get; }
  IApplicantReferenceService ApplicantReferences { get; }
  IStatementOfPurposeService StatementOfPurposes { get; }
  IAdditionalInfoService AdditionalInfos { get; }
  #endregion CRM

  #region DMS
  IDmsdocumentService Dmsdocuments { get; }
  IDmsdocumentTypeService DmsdocumentTypes { get; }
  IDmsdocumentTagService DmsdocumentTags { get; }
  IDmsdocumentTagMapService DmsdocumentTagMaps { get; }
  IDmsdocumentFolderService DmsdocumentFolders { get; }
  IDmsdocumentVersionService DmsdocumentVersions { get; }
  IDmsdocumentAccessLogService DmsdocumentAccessLogs { get; }
  #endregion

  T GetCache<T>(int key);
}

