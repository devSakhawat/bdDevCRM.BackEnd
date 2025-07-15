using bdDevCRM.RepositoriesContracts;
using bdDevCRM.Service.Authentication;
using bdDevCRM.Service.Core.HR;
using bdDevCRM.Service.Core.SystemAdmin;
using bdDevCRM.Service.CRM;
using bdDevCRM.Service.DMS;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Services.Core.SystemAdmin;
using bdDevCRM.ServicesContract;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.ServicesContract.CRM;
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services;

public sealed class ServiceManager : IServiceManager
{
  private readonly IMemoryCache _cache;

  private readonly Lazy<ITokenBlacklistService> _tokenBlackListService;
  private readonly Lazy<ICountryService> _countryService;
  private readonly Lazy<ICurrencyService> _currencyService;
  private readonly Lazy<ICompanyService> _companyService;
  private readonly Lazy<ISystemSettingsService> _systemSettingsService;
  private readonly Lazy<IUsersService> _userService;
  private readonly Lazy<IAuthenticationService> _authenticationService;
  private readonly Lazy<IMenuService> _menuService;
  private readonly Lazy<IModuleService> _moduleService;
  private readonly Lazy<IGroupService> _groupService;
  private readonly Lazy<IQueryAnalyzerService> _queryAnalyzerService;
  private readonly Lazy<IStatusService> _statusService;
  private readonly Lazy<IAccessControlService> _accessControlService;

  #region HR Area
  private readonly Lazy<IEmployeeService> _employeeService;
  private readonly Lazy<IBranchService> _branchService;
  private readonly Lazy<IDepartmentService> _departmentService;
  #endregion HR Area

  #region CRM
  private readonly Lazy<ICRMInstituteService> _crminstitute;
  private readonly Lazy<ICRMInstituteTypeService> _crminstituteType;
  private readonly Lazy<ICRMCourseService> _crmcourse;
  private readonly Lazy<ICRMMonthService> _crmmonth;
  private readonly Lazy<ICRMYearService> _crmyear;

  // Existing CRM services
  private readonly Lazy<IApplicantCourseService> _applicantCourseService;
  private readonly Lazy<IApplicantInfoService> _applicantInfoService;
  private readonly Lazy<IPermanentAddressService> _permanentAddressService;
  private readonly Lazy<IPresentAddressService> _presentAddressService;
  
  // New 10 CRM services
  private readonly Lazy<IEducationHistoryService> _educationHistoryService;
  private readonly Lazy<IIELTSInformationService> _ieltsinformationService;
  private readonly Lazy<ITOEFLInformationService> _toeflinformationService;
  private readonly Lazy<IPTEInformationService> _pteinformationService;
  private readonly Lazy<IGMATInformationService> _gmatinformationService;
  private readonly Lazy<IOTHERSInformationService> _othersinformationService;
  private readonly Lazy<IWorkExperienceService> _workExperienceService;
  private readonly Lazy<IApplicantReferenceService> _applicantReferenceService;
  private readonly Lazy<IStatementOfPurposeService> _statementOfPurposeService;
  private readonly Lazy<IAdditionalInfoService> _additionalInfoService;
  #endregion CRM

  #region DMS Lazy Fields
  private readonly Lazy<IDmsdocumentService> _dmsdocumentService;
  private readonly Lazy<IDmsdocumentTypeService> _dmsdocumentTypeService;
  private readonly Lazy<IDmsdocumentTagService> _dmsdocumentTagService;
  private readonly Lazy<IDmsdocumentTagMapService> _dmsdocumentTagMapService;
  private readonly Lazy<IDmsdocumentFolderService> _dmsdocumentFolderService;
  private readonly Lazy<IDmsdocumentVersionService> _dmsdocumentVersionService;
  private readonly Lazy<IDmsdocumentAccessLogService> _dmsdocumentAccessLogService;
  #endregion

  public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration, IMemoryCache cache ,IHttpContextAccessor httpContextAccessor)
  {
    _cache = cache;

    _tokenBlackListService = new Lazy<ITokenBlacklistService>(() => new TokenBlacklistService(configuration, repository, logger));
    _countryService = new Lazy<ICountryService>(() => new CountryService(repository, logger, configuration));
    _currencyService = new Lazy<ICurrencyService>(() => new CurrencyService(repository, logger, configuration));
    _companyService = new Lazy<ICompanyService>(() => new CompanyService(repository, logger, configuration));
    _systemSettingsService = new Lazy<ISystemSettingsService>(() => new SystemSettingsService(repository, logger, configuration));
    _userService = new Lazy<IUsersService>(() => new UsersService(repository, logger, configuration));
    _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(repository, logger, configuration));
    _menuService = new Lazy<IMenuService>(() => new MenuService(repository, logger, configuration));
    _moduleService = new Lazy<IModuleService>(() => new ModuleService(repository, logger, configuration));
    _groupService = new Lazy<IGroupService>(() => new GroupService(repository, logger, configuration));
    _queryAnalyzerService = new Lazy<IQueryAnalyzerService>(() => new QueryAnalyzerService(repository, logger, configuration));
    _statusService = new Lazy<IStatusService>(() => new StatusService(repository, logger, configuration));
    _accessControlService = new Lazy<IAccessControlService>(() => new AccessControlService(repository, logger, configuration));

    // HR Area
    _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repository, logger, configuration));
    _branchService = new Lazy<IBranchService>(() => new BranchService(repository, logger, configuration));
    //_departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repository, logger, configuration));

    #region CRM
    _crminstitute = new Lazy<ICRMInstituteService>(() => new CRMInstituteService(repository, logger, configuration, httpContextAccessor));
    _crminstituteType = new Lazy<ICRMInstituteTypeService>(() => new CRMInstituteTypeService(repository, logger, configuration));
    _crmcourse = new Lazy<ICRMCourseService>(() => new CRMCourseService(repository, logger, configuration, httpContextAccessor));
    _crmmonth = new Lazy<ICRMMonthService>(() => new CRMMonthService(repository, logger, configuration));
    _crmyear = new Lazy<ICRMYearService>(() => new CRMYearService(repository, logger, configuration));

    // Existing CRM services initialization
    _applicantCourseService = new Lazy<IApplicantCourseService>(() => new ApplicantCourseService(repository, logger, configuration, httpContextAccessor));
    _applicantInfoService = new Lazy<IApplicantInfoService>(() => new ApplicantInfoService(repository, logger, configuration, httpContextAccessor));
    _permanentAddressService = new Lazy<IPermanentAddressService>(() => new PermanentAddressService(repository, logger, configuration, httpContextAccessor));
    _presentAddressService = new Lazy<IPresentAddressService>(() => new PresentAddressService(repository, logger, configuration, httpContextAccessor));
    
    // New 10 CRM services initialization
    _educationHistoryService = new Lazy<IEducationHistoryService>(() => new EducationHistoryService(repository, logger, configuration, httpContextAccessor));
    _ieltsinformationService = new Lazy<IIELTSInformationService>(() => new IELTSInformationService(repository, logger, configuration, httpContextAccessor));
    _toeflinformationService = new Lazy<ITOEFLInformationService>(() => new TOEFLInformationService(repository, logger, configuration, httpContextAccessor));
    _pteinformationService = new Lazy<IPTEInformationService>(() => new PTEInformationService(repository, logger, configuration, httpContextAccessor));
    _gmatinformationService = new Lazy<IGMATInformationService>(() => new GMATInformationService(repository, logger, configuration, httpContextAccessor));
    _othersinformationService = new Lazy<IOTHERSInformationService>(() => new OTHERSInformationService(repository, logger, configuration, httpContextAccessor));
    _workExperienceService = new Lazy<IWorkExperienceService>(() => new WorkExperienceService(repository, logger, configuration, httpContextAccessor));
    _applicantReferenceService = new Lazy<IApplicantReferenceService>(() => new ApplicantReferenceService(repository, logger, configuration, httpContextAccessor));
    _statementOfPurposeService = new Lazy<IStatementOfPurposeService>(() => new StatementOfPurposeService(repository, logger, configuration, httpContextAccessor));
    _additionalInfoService = new Lazy<IAdditionalInfoService>(() => new AdditionalInfoService(repository, logger, configuration, httpContextAccessor));
    #endregion CRM

    #region DMS Lazy Initializations
    _dmsdocumentService = new Lazy<IDmsdocumentService>(() => new DmsdocumentService(repository, logger, configuration, httpContextAccessor));
    _dmsdocumentTypeService = new Lazy<IDmsdocumentTypeService>(() => new DmsdocumentTypeService(repository, logger, configuration));
    _dmsdocumentTagService = new Lazy<IDmsdocumentTagService>(() => new DmsdocumentTagService(repository, logger, configuration));
    _dmsdocumentTagMapService = new Lazy<IDmsdocumentTagMapService>(() => new DmsdocumentTagMapService(repository, logger, configuration));
    _dmsdocumentFolderService = new Lazy<IDmsdocumentFolderService>(() => new DmsdocumentFolderService(repository, logger, configuration));
    _dmsdocumentVersionService = new Lazy<IDmsdocumentVersionService>(() => new DmsdocumentVersionService(repository, logger, configuration));
    _dmsdocumentAccessLogService = new Lazy<IDmsdocumentAccessLogService>(() => new DmsdocumentAccessLogService(repository, logger, configuration));
    #endregion
  }

  public ITokenBlacklistService TokenBlacklist => _tokenBlackListService.Value;
  public ICountryService Countries => _countryService.Value;
  public ICurrencyService Currencies => _currencyService.Value;
  public ICompanyService Companies => _companyService.Value;
  public ISystemSettingsService SystemSettings => _systemSettingsService.Value;
  public IUsersService Users => _userService.Value;
  public IAuthenticationService CustomAuthentication => _authenticationService.Value;
  public IMenuService Menus => _menuService.Value;
  public IModuleService Modules => _moduleService.Value;
  public IGroupService Groups => _groupService.Value;
  public IQueryAnalyzerService QueryAnalyzer => _queryAnalyzerService.Value;
  public IStatusService WfState => _statusService.Value;
  public IAccessControlService AccessControl => _accessControlService.Value;

  #region HR Area
  public IEmployeeService Employees => _employeeService.Value;
  public IBranchService Branches => _branchService.Value;
  public IDepartmentService departments => _departmentService.Value;
  #endregion HR Area

  #region CRM
  public ICRMInstituteService CRMInstitutes => _crminstitute.Value;
  public ICRMInstituteTypeService CRMInstituteTypes => _crminstituteType.Value;
  public ICRMCourseService CRMCourses => _crmcourse.Value;
  public ICRMMonthService CRMMonths => _crmmonth.Value;
  public ICRMYearService CRMYears => _crmyear.Value;

  // Existing CRM service properties
  public IApplicantCourseService ApplicantCourses => _applicantCourseService.Value;
  public IApplicantInfoService ApplicantInfos => _applicantInfoService.Value;
  public IPermanentAddressService PermanentAddresses => _permanentAddressService.Value;
  public IPresentAddressService PresentAddresses => _presentAddressService.Value;
  
  // New 10 CRM service properties - ALL IMPLEMENTED NOW!
  public IEducationHistoryService EducationHistories => _educationHistoryService.Value;
  public IIELTSInformationService IELTSInformations => _ieltsinformationService.Value;
  public ITOEFLInformationService TOEFLInformations => _toeflinformationService.Value;
  public IPTEInformationService PTEInformations => _pteinformationService.Value;
  public IGMATInformationService GMATInformations => _gmatinformationService.Value;
  public IOTHERSInformationService OTHERSInformations => _othersinformationService.Value;
  public IWorkExperienceService WorkExperiences => _workExperienceService.Value;
  public IApplicantReferenceService ApplicantReferences => _applicantReferenceService.Value;
  public IStatementOfPurposeService StatementOfPurposes => _statementOfPurposeService.Value;
  public IAdditionalInfoService AdditionalInfos => _additionalInfoService.Value;
  #endregion CRM

  #region DMS Property Exposures
  public IDmsdocumentService Dmsdocuments => _dmsdocumentService.Value;
  public IDmsdocumentTypeService DmsdocumentTypes => _dmsdocumentTypeService.Value;
  public IDmsdocumentTagService DmsdocumentTags => _dmsdocumentTagService.Value;
  public IDmsdocumentTagMapService DmsdocumentTagMaps => _dmsdocumentTagMapService.Value;
  public IDmsdocumentFolderService DmsdocumentFolders => _dmsdocumentFolderService.Value;
  public IDmsdocumentVersionService DmsdocumentVersions => _dmsdocumentVersionService.Value;
  public IDmsdocumentAccessLogService DmsdocumentAccessLogs => _dmsdocumentAccessLogService.Value;
  #endregion


  // Get Cache // generic function for getting cache from memory cache all of them.
  // This method retrieves an object from the cache using the provided key.
  // If the object is found, it returns the object; otherwise, it throws an exception with 401 status code.
  public T GetCache<T>(int key)
  {
    var cacheKey = $"User_{key}";
    if (_cache.TryGetValue(cacheKey, out T value))
    {
      return value;
    }
    // If not found in cache, throw an exception
    throw new UnauthorizedAccessCRMException("User session has expired or is invalid. Please login again.");
  }



}


