using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Authentication;

public interface ITokenBlacklistService
{
  Task AddToBlacklistAsync(string token);
  Task<bool> IsTokenBlacklisted(string token);
}
