using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract;

public interface IServiceManager
{
  ICountryService Countries { get; }
  ICompanyService Companies { get; }
  ISystemSettingsService SystemSettings { get; }
  IUsersService Users { get; }
  IAuthenticationService CustomAuthentication { get; }
  IMenuService Menus { get; }
  ITokenBlacklistService TokenBlacklist { get; }

}

