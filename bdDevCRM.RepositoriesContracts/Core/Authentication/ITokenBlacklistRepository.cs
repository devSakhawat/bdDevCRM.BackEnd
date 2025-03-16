using bdDevCRM.Entities.Entities;

namespace bdDevCRM.RepositoriesContracts.Core.Authentication;

public interface ITokenBlacklistRepository : IRepositoryBase<TokenBlacklist>
{
  Task AddToBlacklistAsync(TokenBlacklist token);
  Task<bool> IsTokenBlacklistedAsync(string token);

}