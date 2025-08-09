using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoriesContracts.DMS;

namespace bdDevCRM.RepositoriesContracts;

public interface IRepositoryManager : IDisposable
{
  // SystemAdmin Part
  ITokenBlacklistRepository TokenBlacklists { get; }
  ICrmCountryRepository Countries { get; }
  ICompanyRepository Companies { get; }
  ISystemSettingsRepository SystemSettings { get; }
  IUsersRepository Users { get; }
  IAuthenticationRepository CustomAuthentication { get; }
  IMenuRepository Menus { get; }
  IModuleRepository Modules { get; }
  IGroupRepository Groups { get; }
  IGroupMemberRepository GroupMembers { get; }
  IQueryAnalyzerRepository QueryAnalyzers { get; }
  IStatusRepository WfStates { get; }
  IWFActionRepository WfActions { get; }
  IWorkFlowSettingsRepository Workflowes { get; }
  IGroupPermissionRepository GroupPermissiones { get; }
  IAccessControlRepository AccessControls { get; }
  IAccessRestrictionRepository AccessRestrictions { get; }
  ICurrencyRepository Currencies { get; }

  // HR Part
  IEmployeeRepository Employees { get; }
  IEmployeeTypeRepository EmployeeTypes { get; }
  IBranchRepository Branches { get; }
  IDepartmentRepository departments { get; }
  // instance should be small letter.

  #region CRM
  ICrmCourseRepository CrmCourses { get; }
  ICrmMonthRepository CrmMonths { get; }
  ICrmYearRepository CrmYears { get; }
  ICrmInstituteRepository CrmInstitutes { get; }
  ICrmInstituteTypeRepository CrmInstituteTypes { get; }


  // Existing CRM repositories
  ICrmApplicationRepository CrmApplications { get; }
  ICrmApplicantCourseRepository CrmApplicantCourses { get; }
  ICrmApplicantInfoRepository CrmApplicantInfoes { get; }
  ICrmPermanentAddressRepository CrmPermanentAddresses { get; }
  ICrmPresentAddressRepository CrmPresentAddresses { get; }
  
  // New CRM repositories for 10 entities
  ICrmEducationHistoryRepository CrmEducationHistories { get; }
  ICrmIELTSInformationRepository CrmIELTSInformations { get; }
  ICrmTOEFLInformationRepository CrmTOEFLInformations { get; }
  ICrmPTEInformationRepository CrmPTEInformations { get; }
  ICrmGMATInformationRepository CrmGMATInformations { get; }
  ICrmOthersInformationRepository CrmOthersInformations { get; }
  ICrmWorkExperienceRepository CrmWorkExperiences { get; }
  ICrmApplicantReferenceRepository CrmApplicantReferences { get; }
  ICrmStatementOfPurposeRepository CrmStatementOfPurposes { get; }
  ICrmAdditionalInfoRepository CrmAdditionalInfoes { get; }
  #endregion CRM

  #region DMS
  IDmsDocumentRepository DmsDocuments { get; }
  IDmsDocumentTypeRepository DmsDocumentTypes { get; }
  IDmsDocumentTagRepository DmsDocumentTags { get; }
  IDmsDocumentTagMapRepository DmsDocumentTagMaps { get; }
  IDmsDocumentFolderRepository DmsDocumentFolders { get; }
  IDmsDocumentVersionRepository DmsDocumentVersions { get; }
  IDmsDocumentAccessLogRepository DmsDocumentAccessLogs { get; }
  IDmsFileUpdateHistoryRepository DmsFileUpdateHistories { get; }
  #endregion

  // Save changes to the database
  Task SaveAsync();
  void Save();
}

