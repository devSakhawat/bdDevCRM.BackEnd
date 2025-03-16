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

  #region Menu
  public const string SelectMenuByUserPermission = "menus-by-user-permission";
  public const string GetParentMenuByMenu = "parent-Menu-By-Menu";
  public const string CreateMenu = "menu";
  public const string MenuSummary = "menu-summary";
  public const string ReadMenu = "menu/key/{key}";
  public const string UpdateMenu = "menu/{key}";
  public const string DeleteMenu = "menu/{key}";
  #endregion Menu







  // Old code

  #region Country
  public const string CreateCountry = "country";

  public const string ReadCountries = "countries";

  public const string ReadCountryByKey = "country/key/{key}";

  public const string UpdateCountry = "country/{key}";

  public const string DeleteCountry = "country/{key}";
  #endregion

  #region Province
  public const string CreateProvince = "province";

  public const string ReadProvinces = "provinces";

  public const string ReadProvinceByKey = "province/key/{key}";

  public const string ReadProvinceByCountry = "province/country/{key}";

  public const string UpdateProvince = "province/{key}";

  public const string DeleteProvince = "province/{key}";
  #endregion

  #region Facility
  public const string CreateFacility = "facility";

  public const string ReadFacilities = "facilities";

  public const string ReadFacilityByKey = "facility/key/{key}";

  public const string ReadFacilityByDistrict = "facility/district/{key}";

  public const string UpdateFacility = "facility/{key}";

  public const string DeleteFacility = "facility/{key}";
  #endregion

  #region District
  public const string CreateDistrict = "district";

  public const string ReadDistrict = "districts";

  public const string ReadDistrictByKey = "district/key/{key}";

  public const string ReadDistrictByProvince = "district/province/{key}";

  public const string UpdateDistrict = "district/{key}";

  public const string DeleteDistrict = "district/{key}";
  #endregion

  #region Roles
  public const string CreateUserRole = "user-role";

  public const string ReadUserRoles = "user-roles";

  public const string ReadUserRoleByKey = "user-role/key/{key}";

  public const string UpdateUserRole = "user-role/{key}";

  public const string DeleteUserRole = "user-role/{key}";
  #endregion

  #region UserAccounts
  public const string CreateUserAccount = "user-account";

  public const string ReadUserAccounts = "user-accounts";

  public const string ReadUserAccountsByName = "user-accounts/name";

  public const string ReadUserAccountByKey = "user-account/key/{key}";

  public const string ReadUserAccountByRole = "user-account/role/{key}";

  public const string ReadUserAccountByExpert = "user-account/expert";

  public const string UpdateUserAccount = "user-account/{key}";

  public const string DeleteUserAccount = "user-account/{key}";

  public const string IsUniqueUserName = "user-account/unique/{key}";

  public const string UserLogin = "user-account/login";

  public const string ReadUserImage = "user-account/user-image";

  public const string ChangedPassword = "user-account/changepassword";

  public const string RecoveryPassword = "user-account/recovery-password";
  #endregion

  #region Modules
  public const string CreateModule = "module";

  public const string ReadModules = "modules";

  public const string ReadModuleByKey = "module/key/{key}";

  public const string UpdateModule = "module/{key}";

  public const string DeleteModule = "module/{key}";
  #endregion

  #region IncidentCategory
  public const string CreateIncidentCategory = "incident-category";

  public const string ReadIncidentCategorys = "incident-categorys";

  public const string ReadIncidentCategorysTreeView = "incident-categorystreeview";

  public const string ReadIncidentCategorysByKey = "incident-categorys/key/{key}";

  public const string ReadIncidentCategoryByKey = "incident-category/key/{key}";

  public const string UpdateIncidentCategory = "incident-category/{key}";

  public const string DeleteIncidentCategory = "incident-category/{key}";
  #endregion

  #region IncidentPriority
  public const string CreateIncidentPriority = "incident-priority";

  public const string ReadIncidentPriorities = "incident-priorities";

  public const string ReadIncidentPriorityByKey = "incident-priority/key/{key}";

  public const string UpdateIncidentPriority = "incident-priority/{key}";

  public const string DeleteIncidentPriority = "incident-priority/{key}";
  #endregion

  #region Incident
  public const string CreateIncident = "incident";

  public const string ReadIncidents = "incidents";

  public const string ReadIncidentsByStatus = "incidents/status";

  public const string ReadIncidentsByClient = "incidents/client";

  public const string ReadIncidentsByKey = "incidents/key";

  public const string ReadIncidentsByExpert = "incidents/expart";

  public const string ReadIncidentsByExpertLeader = "incidents/expartleader";

  public const string ReadIncidentsByAgent = "incidents/agent/{key}";

  public const string ReadIncidentsBySearch = "incidents/search";

  public const string ReadIncidentByKey = "incident/key/{key}";

  public const string UpdateIncident = "incident/{key}";

  public const string CloseIncident = "incident/close/{key}";

  public const string DeleteIncident = "incident/{key}";
  #endregion

  #region IncidentStatus
  public const string CreateIncidentStatus = "incident-status";

  public const string ReadIncidentStatuses = "incident-statuses";

  public const string ReadIncidentStatusByKey = "incident-status/key/{key}";

  public const string UpdateIncidentStatus = "incident-status/{key}";

  public const string DeleteIncidentStatus = "incident-status/{key}";
  #endregion

  #region System
  public const string CreateSystem = "system";

  public const string ReadSystems = "systems";
  /// <summary>
  /// OID is used as primary key of system entity.
  /// </summary>

  public const string ReadSystemByKey = "system/key/{key}";

  public const string UpdateSystem = "system/{key}";

  public const string DeleteSystem = "system/{key}";
  #endregion

  #region Member
  public const string CreateMember = "member";

  public const string ReadMembers = "members";

  public const string ReadMemberByKey = "member/key/{key}";

  public const string ReadMemberByUser = "member/user/{key}";

  public const string ReadMemberByTeam = "member/team/{key}";

  public const string UpdateMember = "member/{key}";

  public const string DeleteMember = "member/{key}";
  #endregion

  #region Message
  public const string CreateMessage = "message";

  public const string ReadMessages = "messages";

  public const string ReadMessageByKey = "message/key/{key}";

  public const string UpdateMessage = "message/{key}";

  public const string DeleteMessage = "message/{key}";
  #endregion

  #region ProfilePicture
  public const string CreateProfilePicture = "profile-picture";

  public const string ReadProfilePictures = "profile-pictures";

  public const string ReadProfilePictureByKey = "profile-picture/key/{key}";

  public const string UpdateProfilePicture = "profile-picture/{key}";

  public const string DeleteProfilePicture = "profile-picture/{key}";
  #endregion

  #region ModulePermission
  public const string CreateModulePermission = "module-permission";

  public const string ReadModulePermissions = "module-permissions";

  public const string ReadModulePermissionByKey = "module-permission/key/{key}";

  public const string ReadModulePermissionByRole = "module-permission/role/{RoleID}";

  public const string ReadModulePermissionByModule = "module-permission/module/{ModuleID}";

  public const string ReadModulePermission = "module-permission/key";

  public const string UpdateModulePermission = "module-permission/{key}";

  public const string DeleteModulePermission = "module-permission/{key}";
  #endregion

  #region IncidentPermission
  public const string CreateIncidentPermission = "incident-permission";

  public const string ReadIncidentPermissions = "incident-permissions";

  public const string ReadIncidentPermissionByKey = "incident-permission/key/{key}";

  public const string ReadIncidentPermissionByRole = "incident-permission/role/{RoleID}";

  public const string ReadIncidentPermissionByIncidentType = "incident-permission/incidenttype/{IncidentTypeID}";

  public const string ReadIncidentPermission = "incident-permission/key";

  public const string UpdateIncidentPermission = "incident-permission/{key}";

  public const string DeleteIncidentPermission = "incident-permission/{key}";
  #endregion

  #region SystemPermission
  public const string CreateSystemPermission = "system-permission";

  public const string ReadSystemPermissions = "system-permissions";

  public const string ReadSystemPermissionByKey = "system-permission/key/{key}";

  public const string ReadSystemPermissionByUser = "system-permission/user/{UserAccountID}";

  public const string ReadSystemPermissionByProject = "system-permission/system/{SystemID}";

  public const string ReadSystemPermission = "system-permission/key";

  public const string UpdateSystemPermission = "system-permission/{key}";

  public const string DeleteSystemPermission = "system-permission/{key}";
  #endregion

  #region Team
  public const string CreateTeam = "team";

  public const string ReadTeams = "teams";

  public const string ReadTeamByKey = "team/key/{key}";

  public const string UpdateTeam = "team/{key}";

  public const string DeleteTeam = "team/{key}";
  #endregion

  #region ProfilePicture
  public const string CreateScreenshot = "screenshot/{key}";

  public const string ReadScreenshots = "screenshots";

  public const string ReadScreenshotByKey = "screenshot/key/{key}";

  public const string UpdateScreenshot = "screenshot/{key}";

  public const string DeleteScreenshot = "screenshot/{key}";
  #endregion

  #region RecoverRequest
  public const string CreateRecoveryRequest = "recovery-request";

  public const string ReadRecoveryRequests = "recovery-requests";

  public const string ReadRecoveryRequestByKey = "recovery-request/key/{key}";

  public const string UpdateRecoveryRequest = "recovery-request/{key}";

  public const string DeleteRecoveryRequest = "recovery-request/{key}";
  #endregion
}
