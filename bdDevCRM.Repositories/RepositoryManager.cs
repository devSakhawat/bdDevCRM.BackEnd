using bdDevCRM.Repositories.Core.Authentication;
using bdDevCRM.Repositories.Core.SystemAdmin;
using bdDevCRM.Repositories.HR;
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
  private readonly Lazy<IEmployeeRepository> _employeeRepository;
  private readonly Lazy<IAuthenticationRepository> _authenticationRepository;
  private readonly Lazy<IMenuRepository> _menuRepository;
  private readonly Lazy<ITokenBlacklistRepository> _tokenBlacklistRepository;
  private readonly Lazy<IModuleRepository> _moduleRepository;

  public RepositoryManager(CRMContext repositoryContext)
  {
    _repositoryContext = repositoryContext;
    _countries = new Lazy<ICountryRepository>(() => new CountryRepository(_repositoryContext));
    _companies = new Lazy<ICompanyRepository>(() => new CompanyRepository(_repositoryContext));
    _systemRepository = new Lazy<ISystemSettingsRepository>(() => new SystemSettingsRepository(_repositoryContext));
    _usersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(_repositoryContext));
    _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
    _authenticationRepository = new Lazy<IAuthenticationRepository>(() => new AuthenticationRepository(_repositoryContext));
    _menuRepository = new Lazy<IMenuRepository>(() => new MenuRepository(_repositoryContext));
    _tokenBlacklistRepository = new Lazy<ITokenBlacklistRepository>(() => new TokenBlacklistRepository(_repositoryContext));
    _moduleRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(_repositoryContext));
  }

  public ICountryRepository Countries => _countries.Value;
  public ICompanyRepository Companies => _companies.Value;
  public ISystemSettingsRepository SystemSettings => _systemRepository.Value;
  public IUsersRepository Users => _usersRepository.Value;
  public IEmployeeRepository Employees => _employeeRepository.Value;
  public IAuthenticationRepository CustomAuthentication => _authenticationRepository.Value;
  public IMenuRepository Menus => _menuRepository.Value;
  public ITokenBlacklistRepository TokenBlacklist => _tokenBlacklistRepository.Value;
  public IModuleRepository Modules => _moduleRepository.Value;




  public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
  public void Save() => _repositoryContext.SaveChanges();
  public void Dispose() => _repositoryContext.Dispose();
}

