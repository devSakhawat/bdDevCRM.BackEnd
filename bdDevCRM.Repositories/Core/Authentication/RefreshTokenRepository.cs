using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.Token;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.Authentication;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
  public RefreshTokenRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<RefreshToken>> GetExpiredTokensAsync()
  {
    return await ListByConditionAsync(rt => rt.ExpiryDate < DateTime.UtcNow || rt.IsRevoked, trackChanges: false);
  }


  public async Task RevokeAllTokensByUserIdAsync(int userId)
  {
    var activeTokens = await GetActiveTokensByUserIdAsync(userId, trackChanges: true);

    if (activeTokens == null || !activeTokens.Any())
      return;

    foreach (var token in activeTokens)
    {
      token.IsRevoked = true;
      token.RevokedDate = DateTime.UtcNow;
    }
  }


  /// <summary>
  /// Get all active tokens by user ID
  /// </summary>
  public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId, bool trackChanges)
  {
    return await ListByConditionAsync(rt => rt.UserId == userId && rt.ExpiryDate > DateTime.UtcNow && !rt.IsRevoked, orderBy: x => x.CreatedDate, trackChanges: trackChanges);
  }

}
