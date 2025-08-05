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
  private readonly Lazy<ICrmCountryService> _countryService;
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
  private readonly Lazy<ICrmInstituteService> _crminstitute;
  private readonly Lazy<ICrmInstituteTypeService> _crminstituteType;
  private readonly Lazy<ICrmCourseService> _crmcourse;
  private readonly Lazy<ICrmMonthService> _crmmonth;
  private readonly Lazy<ICrmYearService> _crmyear;

  // Existing Crm services
  private readonly Lazy<ICrmApplicationService> _crmApplicationService;
  private readonly Lazy<ICrmApplicantCourseService> _applicantCourseService;
  private readonly Lazy<ICrmApplicantInfoService> _applicantInfoService;
  private readonly Lazy<ICrmPermanentAddressService> _permanentAddressService;
  private readonly Lazy<ICrmPresentAddressService> _presentAddressService;
  
  // New 10 CRM services
  private readonly Lazy<ICrmEducationHistoryService> _educationHistoryService;
  private readonly Lazy<ICrmIELTSInformationService> _ieltsinformationService;
  private readonly Lazy<ICrmTOEFLInformationService> _toeflinformationService;
  private readonly Lazy<ICrmPTEInformationService> _pteinformationService;
  private readonly Lazy<ICrmGmatinformationService> _gmatinformationService;
  private readonly Lazy<ICrmOthersInformationService> _othersinformationService;
  private readonly Lazy<ICrmWorkExperienceService> _workExperienceService;
  private readonly Lazy<ICrmApplicantReferenceService> _applicantReferenceService;
  private readonly Lazy<ICrmStatementOfPurposeService> _statementOfPurposeService;
  private readonly Lazy<ICrmAdditionalInfoService> _additionalInfoService;
  #endregion CRM

  #region DMS Lazy Fields
  private readonly Lazy<IDmsDocumentService> _dmsdocumentService;
  private readonly Lazy<IDmsDocumentTypeService> _dmsdocumentTypeService;
  private readonly Lazy<IDmsDocumentTagService> _dmsdocumentTagService;
  private readonly Lazy<IDmsDocumentTagMapService> _dmsdocumentTagMapService;
  private readonly Lazy<IDmsDocumentFolderService> _dmsdocumentFolderService;
  private readonly Lazy<IDmsDocumentVersionService> _dmsdocumentVersionService;
  private readonly Lazy<IDmsDocumentAccessLogService> _dmsdocumentAccessLogService;
  #endregion

  public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration, IMemoryCache cache ,IHttpContextAccessor httpContextAccessor)
  {
    _cache = cache;

    _tokenBlackListService = new Lazy<ITokenBlacklistService>(() => new TokenBlacklistService(configuration, repository, logger));
    _countryService = new Lazy<ICrmCountryService>(() => new CrmCountryService(repository, logger, configuration));
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
    _crminstitute = new Lazy<ICrmInstituteService>(() => new CrmInstituteService(repository, logger, configuration, httpContextAccessor));
    _crminstituteType = new Lazy<ICrmInstituteTypeService>(() => new CrmInstituteTypeService(repository, logger, configuration));
    _crmcourse = new Lazy<ICrmCourseService>(() => new CrmCourseService(repository, logger, configuration, httpContextAccessor));
    _crmmonth = new Lazy<ICrmMonthService>(() => new CrmMonthService(repository, logger, configuration));
    _crmyear = new Lazy<ICrmYearService>(() => new CrmYearService(repository, logger, configuration));

    // Existing Crm services initialization
    _crmApplicationService = new Lazy<ICrmApplicationService>(() => new CrmApplicationService(repository, logger, configuration, httpContextAccessor));
    _applicantCourseService = new Lazy<ICrmApplicantCourseService>(() => new CrmApplicantCourseService(repository, logger, configuration, httpContextAccessor));
    _applicantInfoService = new Lazy<ICrmApplicantInfoService>(() => new CrmApplicantInfoService(repository, logger, configuration, httpContextAccessor));
    _permanentAddressService = new Lazy<ICrmPermanentAddressService>(() => new CrmPermanentAddressService(repository, logger, configuration, httpContextAccessor));
    _presentAddressService = new Lazy<ICrmPresentAddressService>(() => new CrmPresentAddressService(repository, logger, configuration, httpContextAccessor));
    
    // New 10 Crm services initialization
    _educationHistoryService = new Lazy<ICrmEducationHistoryService>(() => new CrmEducationHistoryService(repository, logger, configuration, httpContextAccessor));
    _ieltsinformationService = new Lazy<ICrmIELTSInformationService>(() => new CrmIELTSInformationService(repository, logger, configuration, httpContextAccessor));
    _toeflinformationService = new Lazy<ICrmTOEFLInformationService>(() => new CrmTOEFLInformationService(repository, logger, configuration, httpContextAccessor));
    _pteinformationService = new Lazy<ICrmPTEInformationService>(() => new CrmPTEInformationService(repository, logger, configuration, httpContextAccessor));
    _gmatinformationService = new Lazy<ICrmGmatinformationService>(() => new CrmGMATInformationService(repository, logger, configuration, httpContextAccessor));
    _othersinformationService = new Lazy<ICrmOthersInformationService>(() => new CrmOthersInformationService(repository, logger, configuration, httpContextAccessor));
    _workExperienceService = new Lazy<ICrmWorkExperienceService>(() => new CrmWorkExperienceService(repository, logger, configuration, httpContextAccessor));
    _applicantReferenceService = new Lazy<ICrmApplicantReferenceService>(() => new CrmApplicantReferenceService(repository, logger, configuration, httpContextAccessor));
    _statementOfPurposeService = new Lazy<ICrmStatementOfPurposeService>(() => new CrmStatementOfPurposeService(repository, logger, configuration, httpContextAccessor));
    _additionalInfoService = new Lazy<ICrmAdditionalInfoService>(() => new CrmAdditionalInfoService(repository, logger, configuration, httpContextAccessor));
    #endregion Crm

    #region DMS Lazy Initializations
    _dmsdocumentService = new Lazy<IDmsDocumentService>(() => new DmsDocumentService(repository, logger, configuration, httpContextAccessor));
    _dmsdocumentTypeService = new Lazy<IDmsDocumentTypeService>(() => new DmsDocumentTypeService(repository, logger, configuration));
    _dmsdocumentTagService = new Lazy<IDmsDocumentTagService>(() => new DmsDocumentTagService(repository, logger, configuration));
    _dmsdocumentTagMapService = new Lazy<IDmsDocumentTagMapService>(() => new DmsDocumentTagMapService(repository, logger, configuration));
    _dmsdocumentFolderService = new Lazy<IDmsDocumentFolderService>(() => new DmsDocumentFolderService(repository, logger, configuration));
    _dmsdocumentVersionService = new Lazy<IDmsDocumentVersionService>(() => new DmsDocumentVersionService(repository, logger, configuration));
    _dmsdocumentAccessLogService = new Lazy<IDmsDocumentAccessLogService>(() => new DmsDocumentAccessLogService(repository, logger, configuration));
    #endregion
  }

  public ITokenBlacklistService TokenBlacklist => _tokenBlackListService.Value;
  public ICrmCountryService CrmCountries => _countryService.Value;
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

  #region Crm
  public ICrmInstituteService CrmInstitutes => _crminstitute.Value;
  public ICrmInstituteTypeService CrmInstituteTypes => _crminstituteType.Value;
  public ICrmCourseService CrmCourses => _crmcourse.Value;
  public ICrmMonthService CrmMonths => _crmmonth.Value;
  public ICrmYearService CrmYears => _crmyear.Value;

  // Existing Crm service properties
  public ICrmApplicationService CrmApplications => _crmApplicationService.Value;
  public ICrmApplicantCourseService ApplicantCourses => _applicantCourseService.Value;
  public ICrmApplicantInfoService ApplicantInfos => _applicantInfoService.Value;
  public ICrmPermanentAddressService PermanentAddresses => _permanentAddressService.Value;
  public ICrmPresentAddressService PresentAddresses => _presentAddressService.Value;
  
  // New 10 Crm service properties - ALL IMPLEMENTED NOW!
  public ICrmEducationHistoryService EducationHistories => _educationHistoryService.Value;
  public ICrmIELTSInformationService IELTSInformations => _ieltsinformationService.Value;
  public ICrmTOEFLInformationService TOEFLInformations => _toeflinformationService.Value;
  public ICrmPTEInformationService PTEInformations => _pteinformationService.Value;
  public ICrmGmatinformationService GMATInformations => _gmatinformationService.Value;
  public ICrmOthersInformationService OTHERSInformations => _othersinformationService.Value;
  public ICrmWorkExperienceService WorkExperiences => _workExperienceService.Value;
  public ICrmApplicantReferenceService ApplicantReferences => _applicantReferenceService.Value;
  public ICrmStatementOfPurposeService StatementOfPurposes => _statementOfPurposeService.Value;
  public ICrmAdditionalInfoService AdditionalInfos => _additionalInfoService.Value;
  #endregion Crm

  #region DMS Property Exposures
  public IDmsDocumentService DmsDocuments => _dmsdocumentService.Value;
  public IDmsDocumentTypeService DmsDocumentTypes => _dmsdocumentTypeService.Value;
  public IDmsDocumentTagService DmsDocumentTags => _dmsdocumentTagService.Value;
  public IDmsDocumentTagMapService DmsDocumentTagMaps => _dmsdocumentTagMapService.Value;
  public IDmsDocumentFolderService DmsDocumentFolders => _dmsdocumentFolderService.Value;
  public IDmsDocumentVersionService DmsDocumentVersions => _dmsdocumentVersionService.Value;
  public IDmsDocumentAccessLogService DmsDocumentAccessLogs => _dmsdocumentAccessLogService.Value;
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


