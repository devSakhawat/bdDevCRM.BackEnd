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

  private readonly Lazy<ICrmCountryRepository> _countries;
  private readonly Lazy<ICrmInstituteTypeRepository> _crmInstituteTypeRepository;
  private readonly Lazy<ICrmInstituteRepository> _crmInstituteRepository;
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
  private readonly Lazy<ICrmCourseRepository> _crmcourseRepository;
  private readonly Lazy<ICrmMonthRepository> _crmmonthRepository;
  private readonly Lazy<ICrmYearRepository> _crmyearRepository;
  private readonly Lazy<ICrmInstituteTypeRepository> _crminstituteTypeRepository;


  // Existing CRM repositories
  private readonly Lazy<ICrmApplicationRepository> _crmApplicationRepository;
  private readonly Lazy<ICrmApplicantCourseRepository> _applicantCourseRepository;
  private readonly Lazy<ICrmApplicantInfoRepository> _applicantInfoRepository;
  private readonly Lazy<ICrmPermanentAddressRepository> _permanentAddressRepository;
  private readonly Lazy<ICrmPresentAddressRepository> _presentAddressRepository;

  // New 10 CRM repositories
  private readonly Lazy<ICrmEducationHistoryRepository> _educationHistoryRepository;
  private readonly Lazy<ICrmIELTSInformationRepository> _ieltsinformationRepository;
  private readonly Lazy<ICrmTOEFLInformationRepository> _toeflinformationRepository;
  private readonly Lazy<ICrmPTEInformationRepository> _pteinformationRepository;
  private readonly Lazy<ICrmGMATInformationRepository> _gmatinformationRepository;
  private readonly Lazy<ICrmOthersInformationRepository> _othersinformationRepository;
  private readonly Lazy<ICrmWorkExperienceRepository> _workExperienceRepository;
  private readonly Lazy<ICrmApplicantReferenceRepository> _applicantReferenceRepository;
  private readonly Lazy<ICrmStatementOfPurposeRepository> _statementOfPurposeRepository;
  private readonly Lazy<ICrmAdditionalInfoRepository> _additionalInfoRepository;
  #endregion CRM

  #region DMS - Private Lazy Fields
  private readonly Lazy<IDmsDocumentRepository> _dmsdocumentRepository;
  private readonly Lazy<IDmsDocumentTypeRepository> _dmsdocumentTypeRepository;
  private readonly Lazy<IDmsDocumentTagRepository> _dmsdocumentTagRepository;
  private readonly Lazy<IDmsDocumentTagMapRepository> _dmsdocumentTagMapRepository;
  private readonly Lazy<IDmsDocumentFolderRepository> _dmsdocumentFolderRepository;
  private readonly Lazy<IDmsDocumentVersionRepository> _dmsdocumentVersionRepository;
  private readonly Lazy<IDmsDocumentAccessLogRepository> _dmsdocumentAccessLogRepository;
  private readonly Lazy<IDmsFileUpdateHistoryRepository> _dmsFileUpdateHistoryRepository;
  #endregion

  public RepositoryManager(CRMContext repositoryContext)
  {
    _repositoryContext = repositoryContext;
    #region System
    _countries = new Lazy<ICrmCountryRepository>(() => new CrmCountryRepository(_repositoryContext));
    _crmInstituteTypeRepository = new Lazy<ICrmInstituteTypeRepository>( () => new CRMInstituteTypeRepository(_repositoryContext));
    _crmInstituteRepository = new Lazy<ICrmInstituteRepository>(() => new CRMInstituteRepository(_repositoryContext));

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
    _crmApplicationRepository = new Lazy<ICrmApplicationRepository>(() => new CrmApplicationRepository(_repositoryContext));
    _crminstituteTypeRepository = new Lazy<ICrmInstituteTypeRepository>(() => new CRMInstituteTypeRepository(_repositoryContext));
    _crmcourseRepository = new Lazy<ICrmCourseRepository>(() => new CrmCourseRepository(_repositoryContext));
    _crmmonthRepository = new Lazy<ICrmMonthRepository>(() => new CrmMonthRepository(_repositoryContext));
    _crmyearRepository = new Lazy<ICrmYearRepository>(() => new CrmYearRepository(_repositoryContext));

    // Existing CRM repositories initialization
    _applicantCourseRepository = new Lazy<ICrmApplicantCourseRepository>(() => new CrmApplicantCourseRepository(_repositoryContext));
    _applicantInfoRepository = new Lazy<ICrmApplicantInfoRepository>(() => new CrmApplicantInfoRepository(_repositoryContext));
    _permanentAddressRepository = new Lazy<ICrmPermanentAddressRepository>(() => new CrmPermanentAddressRepository(_repositoryContext));
    _presentAddressRepository = new Lazy<ICrmPresentAddressRepository>(() => new CrmPresentAddressRepository(_repositoryContext));

    // New 10 CRM repositories initialization
    _educationHistoryRepository = new Lazy<ICrmEducationHistoryRepository>(() => new CrmEducationHistoryRepository(_repositoryContext));
    _ieltsinformationRepository = new Lazy<ICrmIELTSInformationRepository>(() => new CrmIELTSInformationRepository(_repositoryContext));
    _toeflinformationRepository = new Lazy<ICrmTOEFLInformationRepository>(() => new CrmTOEFLInformationRepository(_repositoryContext));
    _pteinformationRepository = new Lazy<ICrmPTEInformationRepository>(() => new CrmPTEInformationRepository(_repositoryContext));
    _gmatinformationRepository = new Lazy<ICrmGMATInformationRepository>(() => new CrmGMATInformationRepository(_repositoryContext));
    _othersinformationRepository = new Lazy<ICrmOthersInformationRepository>(() => new CrmOthersInformationRepository(_repositoryContext));
    _workExperienceRepository = new Lazy<ICrmWorkExperienceRepository>(() => new CrmWorkExperienceRepository(_repositoryContext));
    _applicantReferenceRepository = new Lazy<ICrmApplicantReferenceRepository>(() => new CrmApplicantReferenceRepository(_repositoryContext));
    _statementOfPurposeRepository = new Lazy<ICrmStatementOfPurposeRepository>(() => new CrmStatementOfPurposeRepository(_repositoryContext));
    _additionalInfoRepository = new Lazy<ICrmAdditionalInfoRepository>(() => new CrmAdditionalInfoRepository(_repositoryContext));
    #endregion CRM

    #region DMS - Lazy Initialization
    _dmsdocumentRepository = new Lazy<IDmsDocumentRepository>(() => new DmsDocumentRepository(_repositoryContext));
    _dmsdocumentTypeRepository = new Lazy<IDmsDocumentTypeRepository>(() => new DmsDocumentTypeRepository(_repositoryContext));
    _dmsdocumentTagRepository = new Lazy<IDmsDocumentTagRepository>(() => new DmsDocumentTagRepository(_repositoryContext));
    _dmsdocumentTagMapRepository = new Lazy<IDmsDocumentTagMapRepository>(() => new DmsDocumentTagMapRepository(_repositoryContext));
    _dmsdocumentFolderRepository = new Lazy<IDmsDocumentFolderRepository>(() => new DmsDocumentFolderRepository(_repositoryContext));
    _dmsdocumentVersionRepository = new Lazy<IDmsDocumentVersionRepository>(() => new DmsDocumentVersionRepository(_repositoryContext));
    _dmsdocumentAccessLogRepository = new Lazy<IDmsDocumentAccessLogRepository>(() => new DmsDocumentAccessLogRepository(_repositoryContext));
    _dmsFileUpdateHistoryRepository = new Lazy<IDmsFileUpdateHistoryRepository>(() => new DmsFileUpdateHistoryRepository(_repositoryContext));
    #endregion

  }

  #region SystemAdmin
  public ITokenBlacklistRepository TokenBlacklists => _tokenBlacklistRepository.Value;
  public ICrmCountryRepository Countries => _countries.Value;
  public ICompanyRepository Companies => _companies.Value;
  public ISystemSettingsRepository SystemSettings => _systemRepository.Value;
  public IUsersRepository Users => _usersRepository.Value;
  public IAuthenticationRepository CustomAuthentication => _authenticationRepository.Value;
  public IMenuRepository Menus => _menuRepository.Value;
  public IModuleRepository Modules => _moduleRepository.Value;
  public IGroupRepository Groups => _groupsRepository.Value;
  public IGroupMemberRepository GroupMembers => _groupMembersRepository.Value;
  public IQueryAnalyzerRepository QueryAnalyzers => _queryAnalyzerRepository.Value;
  public IStatusRepository WfStates => _statusRepository.Value;
  public IWFActionRepository WfActions => _wfActionRepository.Value;
  public IWorkFlowSettingsRepository Workflowes => _workFlowSettingsRepository.Value;
  public IGroupPermissionRepository GroupPermissiones => _groupPermissionRepository.Value;
  public IAccessControlRepository AccessControls => _accessControlRepository.Value;
  public IAccessRestrictionRepository AccessRestrictions => _accessRestrictionRepository.Value;
  public ICurrencyRepository Currencies => _currencyRepository.Value;
  #endregion SystemAdmin

  #region HR area
  public IEmployeeRepository Employees => _employeeRepository.Value;
  public IEmployeeTypeRepository EmployeeTypes => _employeetypeRepository.Value;
  public IBranchRepository Branches => _branchRepository.Value;
  //public IDepartmentRepository departments => _departmentRepository.Value;
  #endregion HR area

  #region Crm
  public ICrmCourseRepository CrmCourses => _crmcourseRepository.Value;
  public ICrmMonthRepository CrmMonths => _crmmonthRepository.Value;
  public ICrmYearRepository CrmYears => _crmyearRepository.Value;
  public ICrmInstituteRepository CrmInstitutes => _crmInstituteRepository.Value;
  public ICrmInstituteTypeRepository CrmInstituteTypes => _crminstituteTypeRepository.Value;


  // Existing Crm repository properties
  public ICrmApplicationRepository CrmApplications => _crmApplicationRepository.Value;
  public ICrmApplicantCourseRepository CrmApplicantCourses => _applicantCourseRepository.Value;
  public ICrmApplicantInfoRepository CrmApplicantInfoes => _applicantInfoRepository.Value;
  public ICrmPermanentAddressRepository CrmPermanentAddresses => _permanentAddressRepository.Value;
  public ICrmPresentAddressRepository CrmPresentAddresses => _presentAddressRepository.Value;

  // New 10 Crm repository properties
  public ICrmEducationHistoryRepository CrmEducationHistories => _educationHistoryRepository.Value;
  public ICrmIELTSInformationRepository CrmIELTSInformations => _ieltsinformationRepository.Value;
  public ICrmTOEFLInformationRepository CrmTOEFLInformations => _toeflinformationRepository.Value;
  public ICrmPTEInformationRepository CrmPTEInformations => _pteinformationRepository.Value;
  public ICrmGMATInformationRepository CrmGMATInformations => _gmatinformationRepository.Value;
  public ICrmOthersInformationRepository CrmOthersInformations => _othersinformationRepository.Value;
  public ICrmWorkExperienceRepository CrmWorkExperiences => _workExperienceRepository.Value;
  public ICrmApplicantReferenceRepository CrmApplicantReferences => _applicantReferenceRepository.Value;
  public ICrmStatementOfPurposeRepository CrmStatementOfPurposes => _statementOfPurposeRepository.Value;
  public ICrmAdditionalInfoRepository CrmAdditionalInfoes => _additionalInfoRepository.Value;
  #endregion CRM

  #region DMS - Repository Properties
  public IDmsDocumentRepository DmsDocuments => _dmsdocumentRepository.Value;
  public IDmsDocumentTypeRepository DmsDocumentTypes => _dmsdocumentTypeRepository.Value;
  public IDmsDocumentTagRepository DmsDocumentTags => _dmsdocumentTagRepository.Value;
  public IDmsDocumentTagMapRepository DmsDocumentTagMaps => _dmsdocumentTagMapRepository.Value;
  public IDmsDocumentFolderRepository DmsDocumentFolders => _dmsdocumentFolderRepository.Value;
  public IDmsDocumentVersionRepository DmsDocumentVersions => _dmsdocumentVersionRepository.Value;
  public IDmsDocumentAccessLogRepository DmsDocumentAccessLogs => _dmsdocumentAccessLogRepository.Value;
  public IDmsFileUpdateHistoryRepository DmsFileUpdateHistories => _dmsFileUpdateHistoryRepository.Value;
  #endregion

  // Save changes to the database
  public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
  public void Save() => _repositoryContext.SaveChanges();
  public void Dispose() => _repositoryContext.Dispose();

}

