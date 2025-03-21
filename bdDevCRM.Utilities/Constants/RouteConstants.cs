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



}
