//using System.Security.Cryptography;

namespace bdDevCRM.Utilities.Constants;

public static class RouteConstants
{
  public const string BaseRoute = "bdDevs-crm";

  #region Authentication
  public const string Login = "login";
  public const string GetUserInfo = "user-info";
  public const string Logout = "logout";
  #endregion Authentication


  #region Module
  public const string ModuleSummary = "module-summary";
  public const string Modules = "modules";
  public const string CreateModule = "module";
  public const string UpdateModule = "module/{key}";
  public const string DeleteModule = "module/{key}";
  #endregion Module

  #region Menu
  public const string SelectMenuByUserPermission = "menus-by-user-permission";
  public const string GetParentMenuByMenu = "parent-Menu-By-Menu";
  public const string CreateMenu = "menu";
  public const string MenuSummary = "menu-summary";
  public const string GetMenus = "menus";
  public const string MenusByModuleId = "menus-by-moduleId";
  public const string ReadMenu = "menu/key/{key}";
  public const string UpdateMenu = "menu/{key}";
  public const string DeleteMenu = "menu/{key}";
  public const string MenuForDDL = "menus-4-ddl";
  #endregion Menu

  #region Companies
  public const string Companies = "companies";
  public const string GetMotherCompany = "mother-companies";
  public const string ReadCompany = "company/key/{key}";
  public const string CreateCompany = "company";
  #endregion Companies

  #region Group
  public const string Groups = "groups";
  public const string GroupById = "group/key/{key}";
  public const string GroupPermisionsbyGroupId = "grouppermission/key/";
  public const string GetAccesses = "getaccess";
  public const string GroupSummary = "group-summary";
  public const string CreateGroup = "group";
  public const string ReadGroup = "group/key/{key}";
  public const string UpdateGroup = "group/{key}";
  public const string DeleteGroup = "group/{key}";
  public const string GroupsByCompany = "groups/companyId";

  public const string GroupMemberByUserId = "groups/group-members-by-userId/";
  #endregion Group

  #region QueryAnalyzer
  public const string QueryAnalyzers = "query-analyzers";
  public const string GetCustomizedReportInfo = "customized-report";
  #endregion QueryAnalyzer

  #region Status
  public const string StatusByMenuId = "status/key/";
  public const string ActionsByStatusIdForGroup = "actions-4-group/status/";
  #endregion Status

  #region WorkFlow
  public const string WorkFlowSummary = "workflow-summary";
  public const string CreateWorkFlow = "workflow";
  public const string CreateAction = "wf-action";
  public const string UpdateAction = "wf-action/{key}";
  public const string DeleteAction = "wf-action/{key}";
  public const string GetNextStatesByMenu = "next-states-by-menu/";
  public const string GetActionSummaryByStatusId = "get-action-summary-by-statusId/";
  //public const string ActionsByStatusIdForGroup = "actions-4-group/status/";
  #endregion WorkFlow

  #region AccessControl
  public const string AccessControlSummary = "access-control-summary";
  public const string ReadAccessControls = "access-control-list";
  public const string CreateAccessControl = "access-control";
  public const string UpdateAccessControl = "access-control/{key}";
  public const string DeleteAccessControl = "access-control/{key}";
  #endregion AccessControl

  #region user
  public const string UserSummary = "user-summary";
  public const string Users = "users";
  public const string SaveUser = "user";
  #endregion user

  #region Employee
  public const string EmployeeTypes = "employeetypes";
  // Indentity means : companyId, branchId, departmentId
  public const string EmployeesByCompanyIdAndBranchIdAndDepartmentId = "employees-by-indentities";
  #endregion Employee

  #region Branch
  public const string BranchByCompanyIdForCombo = "branches-by-compnayId-for-combo/companyId/";
  #endregion Branch

  #region Department
  public const string DepartmentByCompanyIdForCombo = "departments-by-compnayId-for-combo/companyId/";
  #endregion Department

  #region CRMApplication
  public const string CRMApplicationSummary = "crm-application-summary";
  public const string CRMApplication= "crm-Application";
  public const string CRMUpdateApplication = "crm-Application/{key}";
  public const string CRMDeleteApplication = "crm-Application/{key}";
  public const string CRMCountryDLL = "crm-countryddl";
  public const string CRMInstituteDLLByCountry = "crm-instituteddl-country";
  public const string CRMCourseDLLByInstitute = "crm-courseddl-institute";
  public const string CRMMonthDLL = "crm-monthddl";
  public const string CRMYearDLL = "crm-yearddl";
  #endregion CRMApplication
}
