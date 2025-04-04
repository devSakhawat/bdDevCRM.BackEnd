using bdDevCRM.RepositoriesContracts;
using bdDevCRM.Service.Authentication;
using bdDevCRM.Service.Core.SystemAdmin;
using bdDevCRM.Service.HR;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.ServiceContract.HR;
using bdDevCRM.Services.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services;

public sealed class ServiceManager : IServiceManager
{
  private readonly IMemoryCache _cache;

  private readonly Lazy<ITokenBlacklistService> _tokenBlackListService;
  private readonly Lazy<ICountryService> _countryService;
  private readonly Lazy<ICompanyService> _companyService;
  private readonly Lazy<ISystemSettingsService> _systemSettingsService;
  private readonly Lazy<IUsersService> _userService;
  private readonly Lazy<IEmployeeService> _employeeService;
  private readonly Lazy<IAuthenticationService> _authenticationService;
  private readonly Lazy<IMenuService> _menuService;
  private readonly Lazy<IModuleService> _moduleService;
  private readonly Lazy<IGroupService> _groupService;
  private readonly Lazy<IQueryAnalyzerService> _queryAnalyzerService;
  private readonly Lazy<IStatusService> _statusService;
  private readonly Lazy<IAccessControlService> _accessControlService;

  public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration ,IMemoryCache cache)
  {
    _cache = cache;

    _tokenBlackListService = new Lazy<ITokenBlacklistService>(() => new TokenBlacklistService(configuration, repository, logger));
    _countryService = new Lazy<ICountryService>(() => new CountryService(repository, logger));
    _companyService = new Lazy<ICompanyService>(() => new CompanyService(repository, logger));
    _systemSettingsService = new Lazy<ISystemSettingsService>(() => new SystemSettingsService(repository, logger));
    _userService = new Lazy<IUsersService>(() => new UsersService(repository, logger));
    _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repository, logger));
    _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(repository, logger, configuration));
    _menuService = new Lazy<IMenuService>(() => new MenuService(repository, logger, configuration));
    _moduleService = new Lazy<IModuleService>(() => new ModuleService(repository, logger, configuration));
    _groupService = new Lazy<IGroupService>(() => new GroupService(repository, logger, configuration));
    _queryAnalyzerService = new Lazy<IQueryAnalyzerService>(() => new QueryAnalyzerService(repository, logger, configuration));
    _statusService = new Lazy<IStatusService>(() => new StatusService(repository, logger, configuration));
    _accessControlService = new Lazy<IAccessControlService>(() => new AccessControlService(repository, logger, configuration));
  }

  public ITokenBlacklistService TokenBlacklist => _tokenBlackListService.Value;
  public ICountryService Countries => _countryService.Value;
  public ICompanyService Companies => _companyService.Value;
  public ISystemSettingsService SystemSettings => _systemSettingsService.Value;
  public IUsersService Users => _userService.Value;
  public IEmployeeService Employees => _employeeService.Value;
  public IAuthenticationService CustomAuthentication => _authenticationService.Value;
  public IMenuService Menus => _menuService.Value;
  public IModuleService Modules => _moduleService.Value;
  public IGroupService Groups => _groupService.Value;
  public IQueryAnalyzerService QueryAnalyzer => _queryAnalyzerService.Value;
  public IStatusService WfState => _statusService.Value;
  public IAccessControlService AccessControl => _accessControlService.Value;


  // Get Cache // generic function for getting cache from memory cache all of them.
  // This method retrieves an object from the cache using the provided key.
  // If the object is found, it returns the object; otherwise, it returns the default value for the type T.
  // example: var user = _serviceManager.GetCache<UsersDto>($"User_{userId}");
  public T GetCache<T>(string key)
  {
    if (_cache.TryGetValue(key, out T value))
    {
      return value;
    }
    return default;
  }
}
