using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoriesContracts.CRM;

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

  // HR Part
  IEmployeeRepository Employees { get; }
  IEmployeeTypeRepository EmployeeTypes { get; }
  IBranchRepository Branches { get; }
  IDepartmentRepository departments { get; }
  // instance should be small letter.

  #region CRM
  ICRMInstituteRepository CRMInstitute { get; }
  ICRMInstituteTypeRepository CRMInstituteType { get; }
  ICRMCourseRepository CRMCourse { get; }
  ICRMMonthRepository CRMMonth{ get; }
  ICRMYearRepository CRMYear { get; }
  
  #endregion CRM




  // Save changes to the database
  Task SaveAsync();
  void Save();
}

