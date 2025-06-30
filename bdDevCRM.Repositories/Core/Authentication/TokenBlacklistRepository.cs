using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.Authentication;

public class TokenBlacklistRepository : RepositoryBase<TokenBlacklist>, ITokenBlacklistRepository
{
  public TokenBlacklistRepository(CRMContext context) : base(context) { }

  public async Task AddToBlacklistAsync(TokenBlacklist token)
  {
    await CreateAsync(token);
  }

  public async Task<bool> IsTokenBlacklistedAsync(string token)
  {
    return await ExistsAsync(tb => tb.Token == token && tb.ExpiryDate > DateTime.UtcNow);
  }
}
