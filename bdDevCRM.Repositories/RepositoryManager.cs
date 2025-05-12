using bdDevCRM.Repositories.Core.Authentication;
using bdDevCRM.Repositories.Core.HR;
using bdDevCRM.Repositories.Core.SystemAdmin;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories;

public class RepositoryManager : IRepositoryManager
{
  private readonly CRMContext _repositoryContext;

  private readonly Lazy<ICountryRepository> _countries;
  private readonly Lazy<ICompanyRepository> _companies;
  private readonly Lazy<ISystemSettingsRepository> _systemRepository;
  private readonly Lazy<IUsersRepository> _usersRepository;
  private readonly Lazy<IAuthenticationRepository> _authenticationRepository;
  private readonly Lazy<IMenuRepository> _menuRepository;
  private readonly Lazy<ITokenBlacklistRepository> _tokenBlacklistRepository;
  private readonly Lazy<IModuleRepository> _moduleRepository;
  private readonly Lazy<IGroupRepository> _groupsRepository;
  private readonly Lazy<IGroupMemberRepository> _groupMembersRepository;
  private readonly Lazy<IQueryAnalyzerRepository> _queryAnalyzerRepository;
  private readonly Lazy<IStatusRepository> _statusRepository;
  private readonly Lazy<IWFActionRepository> _wfActionRepository;
  private readonly Lazy<IWorkFlowSettingsRepository> _workFlowSettingsRepository;
  private readonly Lazy<IGroupPermissionRepository> _groupPermissionRepository;
  private readonly Lazy<IAccessControlRepository> _accessControlRepository;
  private readonly Lazy<IAccessRestrictionRepository> _accessRestrictionRepository;

  // HR area start  
  private readonly Lazy<IEmployeeRepository> _employeeRepository;
  private readonly Lazy<IEmployeeTypeRepository> _employeetypeRepository;
  private readonly Lazy<IBranchRepository> _branchRepository;
  private readonly Lazy<IDepartmentRepository> _departmentRepository;
  // HR area end  

  public RepositoryManager(CRMContext repositoryContext)
  {
    _repositoryContext = repositoryContext;

    _countries = new Lazy<ICountryRepository>(() => new CountryRepository(_repositoryContext));
    _companies = new Lazy<ICompanyRepository>(() => new CompanyRepository(_repositoryContext));
    _systemRepository = new Lazy<ISystemSettingsRepository>(() => new SystemSettingsRepository(_repositoryContext));
    _usersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(_repositoryContext));
    _authenticationRepository = new Lazy<IAuthenticationRepository>(() => new AuthenticationRepository(_repositoryContext));
    _menuRepository = new Lazy<IMenuRepository>(() => new MenuRepository(_repositoryContext));
    _tokenBlacklistRepository = new Lazy<ITokenBlacklistRepository>(() => new TokenBlacklistRepository(_repositoryContext));
    _moduleRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(_repositoryContext));
    _groupsRepository = new Lazy<IGroupRepository>(() => new GroupRepository(_repositoryContext));
    _groupMembersRepository = new Lazy<IGroupMemberRepository>(() => new GroupMemberRepository(_repositoryContext));
    _queryAnalyzerRepository = new Lazy<IQueryAnalyzerRepository>(() => new QueryAnalyzerRepository(_repositoryContext));
    _statusRepository = new Lazy<IStatusRepository>(() => new StatusRepository(_repositoryContext));
    _wfActionRepository = new Lazy<IWFActionRepository>(() => new WFActionRepository(_repositoryContext));
    _workFlowSettingsRepository = new Lazy<IWorkFlowSettingsRepository>(() => new WorkFlowSettingsRepository(_repositoryContext));
    _groupPermissionRepository = new Lazy<IGroupPermissionRepository>(() => new GroupPermissionRepository(_repositoryContext));
    _accessControlRepository = new Lazy<IAccessControlRepository>(() => new AccessControlRepository(_repositoryContext));
    _accessRestrictionRepository = new Lazy<IAccessRestrictionRepository>(() => new AccessRestrictionRepository(_repositoryContext));

    // HR area start  
    _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
    _employeetypeRepository = new Lazy<IEmployeeTypeRepository>(() => new EmployeeTypeRepository(_repositoryContext));
    _branchRepository = new Lazy<IBranchRepository>(() => new BranchRepository(_repositoryContext));
    _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
    // HR area end

  }

  #region SystemAdmin
  public ICountryRepository Countries => _countries.Value;
  public ICompanyRepository Companies => _companies.Value;
  public ISystemSettingsRepository SystemSettings => _systemRepository.Value;
  public IUsersRepository Users => _usersRepository.Value;
  public IAuthenticationRepository CustomAuthentication => _authenticationRepository.Value;
  public IMenuRepository Menus => _menuRepository.Value;
  public ITokenBlacklistRepository TokenBlacklist => _tokenBlacklistRepository.Value;
  public IModuleRepository Modules => _moduleRepository.Value;
  public IGroupRepository Groups => _groupsRepository.Value;
  public IGroupMemberRepository GroupMembers => _groupMembersRepository.Value;
  public IQueryAnalyzerRepository QueryAnalyzer => _queryAnalyzerRepository.Value;
  public IStatusRepository WfState => _statusRepository.Value;
  public IWFActionRepository WfAction => _wfActionRepository.Value;
  public IWorkFlowSettingsRepository Workflow => _workFlowSettingsRepository.Value;
  public IGroupPermissionRepository GroupPermission => _groupPermissionRepository.Value;
  public IAccessControlRepository AccessControl => _accessControlRepository.Value;
  public IAccessRestrictionRepository AccessRestriction => _accessRestrictionRepository.Value;
  #endregion SystemAdmin

  #region HR area
  public IEmployeeRepository Employees => _employeeRepository.Value;
  public IEmployeeTypeRepository EmployeeTypes => _employeetypeRepository.Value;
  public IBranchRepository Branches => _branchRepository.Value;
  public IDepartmentRepository departments => _departmentRepository.Value;
  #endregion HR area




  // Save changes to the database
  public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
  public void Save() => _repositoryContext.SaveChanges();
  public void Dispose() => _repositoryContext.Dispose();
}

