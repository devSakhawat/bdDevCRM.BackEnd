using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.ServicesContract.CRM;

namespace bdDevCRM.ServicesContract;

public interface IServiceManager
{
  ITokenBlacklistService TokenBlacklist { get; }
  ICountryService Countries { get; }
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
  ICRMCourseService CRMCourses { get; }
  ICRMMonthService CRMMonths { get; }
  ICRMYearService CRMYears { get; }
  #endregion CRM

  T GetCache<T>(int key);
}

