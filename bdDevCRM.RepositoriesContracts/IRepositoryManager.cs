using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts;

public interface IRepositoryManager : IDisposable
{
  ITokenBlacklistRepository TokenBlacklist { get; }
  ICountryRepository Countries { get; }
  ICompanyRepository Companies { get; }
  ISystemSettingsRepository SystemSettings { get; }
  IUsersRepository Users { get; }
  IEmployeeRepository Employees { get; }
  IAuthenticationRepository CustomAuthentication { get; }
  IMenuRepository Menus { get; }
  IModuleRepository Modules { get; }
  IGroupRepository Groups { get; }
  IQueryAnalyzerRepository QueryAnalyzer { get; }
  IStatusRepository WfState { get; }
  IGroupPermissionRepository GroupPermission { get; }
  IAccessControlRepository AccessControl { get; }
  IAccessRestrictionRepository AccessRestriction { get; }

  // Save changes to the database
  Task SaveAsync();
  void Save();
}

