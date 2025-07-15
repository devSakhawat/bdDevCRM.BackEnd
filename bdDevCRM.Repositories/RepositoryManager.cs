using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Repositories.Core.Authentication;
using bdDevCRM.Repositories.Core.HR;
using bdDevCRM.Repositories.Core.SystemAdmin;
using bdDevCRM.Repositories.CRM;
using bdDevCRM.Repositories.DMS;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories;

public class RepositoryManager : IRepositoryManager
{
  private readonly CRMContext _repositoryContext;

  private readonly Lazy<ICountryRepository> _countries;
  private readonly Lazy<ICRMInstituteTypeRepository> _crmInstituteTypeRepository;
  private readonly Lazy<ICRMInstituteRepository> _crmInstituteRepository;
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
  private readonly Lazy<ICurrencyRepository> _currencyRepository;

  // HR area start  
  private readonly Lazy<IEmployeeRepository> _employeeRepository;
  private readonly Lazy<IEmployeeTypeRepository> _employeetypeRepository;
  private readonly Lazy<IBranchRepository> _branchRepository;
  //private readonly Lazy<IDepartmentRepository> _departmentRepository;
  // HR area end  

  #region CRM
  private readonly Lazy<ICRMInstituteTypeRepository> _crminstituteTypeRepository;
  private readonly Lazy<ICRMCourseRepository> _crmcourseRepository;
  private readonly Lazy<ICRMMonthRepository> _crmmonthRepository;
  private readonly Lazy<ICRMYearRepository> _crmyearRepository;

  // New CRM repositories
  private readonly Lazy<IApplicantCourseRepository> _applicantCourseRepository;
  private readonly Lazy<IApplicantInfoRepository> _applicantInfoRepository;
  private readonly Lazy<IPermanentAddressRepository> _permanentAddressRepository;
  private readonly Lazy<IPresentAddressRepository> _presentAddressRepository;
  #endregion CRM

  #region DMS - Private Lazy Fields
  private readonly Lazy<IDmsdocumentRepository> _dmsdocumentRepository;
  private readonly Lazy<IDmsdocumentTypeRepository> _dmsdocumentTypeRepository;
  private readonly Lazy<IDmsdocumentTagRepository> _dmsdocumentTagRepository;
  private readonly Lazy<IDmsdocumentTagMapRepository> _dmsdocumentTagMapRepository;
  private readonly Lazy<IDmsdocumentFolderRepository> _dmsdocumentFolderRepository;
  private readonly Lazy<IDmsdocumentVersionRepository> _dmsdocumentVersionRepository;
  private readonly Lazy<IDmsdocumentAccessLogRepository> _dmsdocumentAccessLogRepository;
  private readonly Lazy<IDmsFileUpdateHistoryRepository> _dmsFileUpdateHistoryRepository;
  #endregion

  public RepositoryManager(CRMContext repositoryContext)
  {
    _repositoryContext = repositoryContext;
    #region System
    _countries = new Lazy<ICountryRepository>(() => new CountryRepository(_repositoryContext));
    _crmInstituteTypeRepository = new Lazy<ICRMInstituteTypeRepository>( () => new CRMInstituteTypeRepository(_repositoryContext));
    _crmInstituteRepository = new Lazy<ICRMInstituteRepository>(() => new CRMInstituteRepository(_repositoryContext));

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

    _currencyRepository = new Lazy<ICurrencyRepository>(() => new CurrencyRepository(_repositoryContext));
    #endregion System

    // HR area start  
    _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
    _employeetypeRepository = new Lazy<IEmployeeTypeRepository>(() => new EmployeeTypeRepository(_repositoryContext));
    _branchRepository = new Lazy<IBranchRepository>(() => new BranchRepository(_repositoryContext));
    //_departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
    // HR area end

    #region CRM
    _crminstituteTypeRepository = new Lazy<ICRMInstituteTypeRepository>(() => new CRMInstituteTypeRepository(_repositoryContext));
    _crmcourseRepository = new Lazy<ICRMCourseRepository>(() => new CRMCourseRepository(_repositoryContext));
    _crmmonthRepository = new Lazy<ICRMMonthRepository>(() => new CRMMonthRepository(_repositoryContext));
    _crmyearRepository = new Lazy<ICRMYearRepository>(() => new CRMYearRepository(_repositoryContext));

    // New CRM repositories initialization
    _applicantCourseRepository = new Lazy<IApplicantCourseRepository>(() => new ApplicantCourseRepository(_repositoryContext));
    _applicantInfoRepository = new Lazy<IApplicantInfoRepository>(() => new ApplicantInfoRepository(_repositoryContext));
    _permanentAddressRepository = new Lazy<IPermanentAddressRepository>(() => new PermanentAddressRepository(_repositoryContext));
    _presentAddressRepository = new Lazy<IPresentAddressRepository>(() => new PresentAddressRepository(_repositoryContext));
    #endregion CRM

    #region DMS - Lazy Initialization
    _dmsdocumentRepository = new Lazy<IDmsdocumentRepository>(() => new DmsdocumentRepository(_repositoryContext));
    _dmsdocumentTypeRepository = new Lazy<IDmsdocumentTypeRepository>(() => new DmsdocumentTypeRepository(_repositoryContext));
    _dmsdocumentTagRepository = new Lazy<IDmsdocumentTagRepository>(() => new DmsdocumentTagRepository(_repositoryContext));
    _dmsdocumentTagMapRepository = new Lazy<IDmsdocumentTagMapRepository>(() => new DmsdocumentTagMapRepository(_repositoryContext));
    _dmsdocumentFolderRepository = new Lazy<IDmsdocumentFolderRepository>(() => new DmsdocumentFolderRepository(_repositoryContext));
    _dmsdocumentVersionRepository = new Lazy<IDmsdocumentVersionRepository>(() => new DmsdocumentVersionRepository(_repositoryContext));
    _dmsdocumentAccessLogRepository = new Lazy<IDmsdocumentAccessLogRepository>(() => new DmsdocumentAccessLogRepository(_repositoryContext));
    _dmsFileUpdateHistoryRepository = new Lazy<IDmsFileUpdateHistoryRepository>(() => new DmsFileUpdateHistoryRepository(_repositoryContext));
    #endregion

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
  public ICurrencyRepository Currency => _currencyRepository.Value;
  #endregion SystemAdmin

  #region HR area
  public IEmployeeRepository Employees => _employeeRepository.Value;
  public IEmployeeTypeRepository EmployeeTypes => _employeetypeRepository.Value;
  public IBranchRepository Branches => _branchRepository.Value;
  //public IDepartmentRepository departments => _departmentRepository.Value;
  #endregion HR area

  #region CRM
  public ICRMInstituteRepository CRMInstitutes => _crmInstituteRepository.Value;
  public ICRMInstituteTypeRepository CRMInstituteTypes => _crminstituteTypeRepository.Value;
  public ICRMCourseRepository CRMCourse => _crmcourseRepository.Value;
  public ICRMMonthRepository CRMMonth => _crmmonthRepository.Value;
  public ICRMYearRepository CRMYear => _crmyearRepository.Value;

  // New CRM repository properties
  public IApplicantCourseRepository ApplicantCourse => _applicantCourseRepository.Value;
  public IApplicantInfoRepository ApplicantInfo => _applicantInfoRepository.Value;
  public IPermanentAddressRepository PermanentAddress => _permanentAddressRepository.Value;
  public IPresentAddressRepository PresentAddress => _presentAddressRepository.Value;
  #endregion CRM

  #region DMS - Repository Properties
  public IDmsdocumentRepository Dmsdocuments => _dmsdocumentRepository.Value;
  public IDmsdocumentTypeRepository DmsdocumentTypes => _dmsdocumentTypeRepository.Value;
  public IDmsdocumentTagRepository DmsdocumentTags => _dmsdocumentTagRepository.Value;
  public IDmsdocumentTagMapRepository DmsdocumentTagMaps => _dmsdocumentTagMapRepository.Value;
  public IDmsdocumentFolderRepository DmsdocumentFolders => _dmsdocumentFolderRepository.Value;
  public IDmsdocumentVersionRepository DmsdocumentVersions => _dmsdocumentVersionRepository.Value;
  public IDmsdocumentAccessLogRepository DmsdocumentAccessLogs => _dmsdocumentAccessLogRepository.Value;
  public IDmsFileUpdateHistoryRepository IDmsFileUpdateHistories => _dmsFileUpdateHistoryRepository.Value;
  #endregion

  // Save changes to the database
  public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
  public void Save() => _repositoryContext.SaveChanges();
  public void Dispose() => _repositoryContext.Dispose();
}

