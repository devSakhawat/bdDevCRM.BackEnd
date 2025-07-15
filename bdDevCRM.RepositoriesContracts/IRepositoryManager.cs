using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoriesContracts.DMS;

namespace bdDevCRM.RepositoriesContracts;

public interface IRepositoryManager : IDisposable
{
  // SystemAdmin Part
  ITokenBlacklistRepository TokenBlacklist { get; }
  ICountryRepository Countries { get; }
  ICompanyRepository Companies { get; }
  ISystemSettingsRepository SystemSettings { get; }
  IUsersRepository Users { get; }
  IAuthenticationRepository CustomAuthentication { get; }
  IMenuRepository Menus { get; }
  IModuleRepository Modules { get; }
  IGroupRepository Groups { get; }
  IGroupMemberRepository GroupMembers { get; }
  IQueryAnalyzerRepository QueryAnalyzer { get; }
  IStatusRepository WfState { get; }
  IWFActionRepository WfAction { get; }
  IWorkFlowSettingsRepository Workflow { get; }
  IGroupPermissionRepository GroupPermission { get; }
  IAccessControlRepository AccessControl { get; }
  IAccessRestrictionRepository AccessRestriction { get; }
  ICurrencyRepository Currency { get; }

  // HR Part
  IEmployeeRepository Employees { get; }
  IEmployeeTypeRepository EmployeeTypes { get; }
  IBranchRepository Branches { get; }
  //IDepartmentRepository departments { get; }
  // instance should be small letter.

  #region CRM
  ICRMInstituteRepository CRMInstitutes { get; }
  ICRMInstituteTypeRepository CRMInstituteTypes { get; }
  ICRMCourseRepository CRMCourse { get; }
  ICRMMonthRepository CRMMonth{ get; }
  ICRMYearRepository CRMYear { get; }
  
  // New CRM repositories
  IApplicantCourseRepository ApplicantCourse { get; }
  IApplicantInfoRepository ApplicantInfo { get; }
  IPermanentAddressRepository PermanentAddress { get; }
  IPresentAddressRepository PresentAddress { get; }
  #endregion CRM

  #region DMS
  IDmsdocumentRepository Dmsdocuments { get; }
  IDmsdocumentTypeRepository DmsdocumentTypes { get; }
  IDmsdocumentTagRepository DmsdocumentTags { get; }
  IDmsdocumentTagMapRepository DmsdocumentTagMaps { get; }
  IDmsdocumentFolderRepository DmsdocumentFolders { get; }
  IDmsdocumentVersionRepository DmsdocumentVersions { get; }
  IDmsdocumentAccessLogRepository DmsdocumentAccessLogs { get; }
  IDmsFileUpdateHistoryRepository IDmsFileUpdateHistories { get; }
  #endregion

  // Save changes to the database
  Task SaveAsync();
  void Save();
}

