using System.Security.Cryptography;

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
  #endregion Menu

  #region Group
  public const string Groups = "groups";
  public const string GroupById = "group/key/{key}";
  public const string GroupPermisionsbyGroupId = "grouppermission/key/";
  public const string GetAccesses = "getaccess";

  public const string GroupSummary = "group-summary";
  //public const string GroupsByModuleId = "groups-by-moduleId";
  public const string CreateGroup = "group";
  public const string ReadGroup = "group/key/{key}";
  public const string UpdateGroup = "group/{key}";
  public const string DeleteGroup = "group/{key}";
  #endregion Group

  #region QueryAnalyzer
  public const string QueryAnalyzers = "query-analyzers";
  public const string GetCustomizedReportInfo = "customized-report";

  #endregion QueryAnalyzer

  #region Status
  public const string StatusByMenuId = "status/key/";
  // actions by status for group 
  // to get actions which is responsible? after / sign status. so status is responsible for getting actions
  public const string ActionsByStatusIdForGroup = "actions-4-group/status/";

  #endregion Status


  #region AccessControl
  public const string AccessControlSummary = "access-control-summary";
  public const string ReadAccessControls = "access-control-list";
  public const string CreateAccessControl = "access-control";
  public const string UpdateAccessControl = "access-control/{key}";
  public const string DeleteAccessControl = "access-control/{key}";

  #endregion AccessControl



  #region Group
  //public const string GroupById = "group/key/{key}";
  //public const string GroupPermisionsbyGroupId = "grouppermission/key/";
  //public const string GetAccesses = "getaccess";

  //public const string GroupsByModuleId = "groups-by-moduleId";
  //public const string CreateGroup = "group";
  //public const string ReadGroup = "group/key/{key}";
  //public const string UpdateGroup = "group/{key}";
  //public const string DeleteGroup = "group/{key}";

  public const string Users = "users";
  public const string UserSummary = "user-summary";
  #endregion Group


}
