using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.Token;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.Authentication;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
  public RefreshTokenRepository(CRMContext context) : base(context) { }


  /// <summary>
  /// Get all expired tokens
  /// যেসব token expire হয়ে গেছে সেগুলো খুঁজে বের করা
  /// 
  /// Logic:
  /// 1. ExpiryDate < DateTime.UtcNow (expire হয়ে গেছে)
  /// 2.  অথবা IsRevoked = true (manually revoke করা হয়েছে)
  /// </summary>
  public async Task<IEnumerable<RefreshToken>> GetExpiredTokensAsync()
  {
    return await ListByConditionAsync(rt => rt.ExpiryDate < DateTime.UtcNow || rt.IsRevoked, trackChanges: false);
  }


  public async Task RevokeAllTokensByUserIdAsync(int userId)
  {
    var activeTokens = await GetActiveTokensByUserIdAsync(userId, trackChanges: true);

    if (activeTokens == null || !activeTokens.Any())
      return;

    // সব active tokens কে revoke করা
    foreach (var token in activeTokens)
    {
      token.IsRevoked = true;
      token.RevokedDate = DateTime.UtcNow;
    }

    // EF Core automatically track করে, তাই Update() call করার দরকার নেই
    // শুধু SaveAsync() call করলেই হবে
  }


  /// <summary>
  /// Get all active tokens by user ID
  /// একটা user এর active (valid) tokens
  /// 
  /// Logic:
  /// 1. UserId match করতে হবে
  /// 2. ExpiryDate > DateTime.UtcNow (এখনো valid)
  /// 3. IsRevoked = false (revoke করা হয়নি)
  /// </summary>
  public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId, bool trackChanges)
  {
    return await ListByConditionAsync(rt => rt.UserId == userId && rt.ExpiryDate > DateTime.UtcNow && !rt.IsRevoked, orderBy: x => x.CreatedDate, trackChanges: trackChanges);
  }

}







///// <summary>
///// Refresh token repository implementation
///// রিফ্রেশ টোকেন repository এর implementation
///// </summary>
//public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
//{
//  public RefreshTokenRepository(CRMContext repositoryContext) : base(repositoryContext)
//  {
//  }

//  /// <summary>
//  /// Get refresh token by token value
//  /// Token value দিয়ে refresh token খুঁজে বের করা
//  /// </summary>
//  public async Task<RefreshToken> GetByTokenAsync(string token, bool trackChanges)
//  {
//    return await FindByCondition(
//      rt => rt.Token == token,
//      trackChanges
//    ).FirstOrDefaultAsync();
//  }

//  /// <summary>
//  /// Get all refresh tokens by user ID
//  /// একটা user এর সব refresh tokens
//  /// </summary>
//  public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId, bool trackChanges)
//  {
//    return await FindByCondition(
//      rt => rt.UserId == userId,
//      trackChanges
//    )
//    .OrderByDescending(rt => rt.CreatedDate)
//    .ToListAsync();
//  }

//  /// <summary>
//  /// Get all expired tokens
//  /// যেসব token expire হয়ে গেছে সেগুলো খুঁজে বের করা
//  /// 
//  /// Logic:
//  /// 1. ExpiryDate < DateTime.UtcNow (expire হয়ে গেছে)
//  /// 2.  অথবা IsRevoked = true (manually revoke করা হয়েছে)
//  /// </summary>
//  public async Task<IEnumerable<RefreshToken>> GetExpiredTokensAsync()
//  {
//    return await FindByCondition(
//      rt => rt.ExpiryDate < DateTime.UtcNow || rt.IsRevoked,
//      trackChanges: false
//    )
//    .ToListAsync();
//  }

//  /// <summary>
//  /// Get all active tokens by user ID
//  /// একটা user এর active (valid) tokens
//  /// 
//  /// Logic:
//  /// 1. UserId match করতে হবে
//  /// 2. ExpiryDate > DateTime.UtcNow (এখনো valid)
//  /// 3. IsRevoked = false (revoke করা হয়নি)
//  /// </summary>
//  public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId, bool trackChanges)
//  {
//    return await FindByCondition(
//      rt => rt.UserId == userId &&
//            rt.ExpiryDate > DateTime.UtcNow &&
//            !rt.IsRevoked,
//      trackChanges
//    )
//    .OrderByDescending(rt => rt.CreatedDate)
//    .ToListAsync();
//  }

//  /// <summary>
//  /// Create new refresh token
//  /// নতুন refresh token database এ save করা
//  /// </summary>
//  public void Create(RefreshToken refreshToken)
//  {
//    // Base class এর Create method call করা
//    Create(refreshToken);
//  }

//  /// <summary>
//  /// Update existing refresh token
//  /// Existing refresh token update করা
//  /// </summary>
//  public void Update(RefreshToken refreshToken)
//  {
//    // Base class এর Update method call করা
//    Update(refreshToken);
//  }

//  /// <summary>
//  /// Delete refresh token
//  /// Single refresh token delete করা
//  /// </summary>
//  public void Delete(RefreshToken refreshToken)
//  {
//    // Base class এর Delete method call করা
//    Delete(refreshToken);
//  }

//  /// <summary>
//  /// Bulk delete multiple refresh tokens
//  /// একসাথে অনেকগুলো refresh token delete করা
//  /// 
//  /// Use case: 
//  /// - Cleanup expired tokens
//  /// - Remove all tokens when user logs out from all devices
//  /// </summary>
//  public void BulkDelete(IEnumerable<RefreshToken> refreshTokens)
//  {
//    if (refreshTokens == null || !refreshTokens.Any())
//      return;

//    // RemoveRange - EF Core এর bulk delete method
//    RepositoryContext.RefreshTokens.RemoveRange(refreshTokens);
//  }

//  /// <summary>
//  /// Revoke all tokens for a user
//  /// একটা user এর সব tokens revoke করে দেওয়া
//  /// 
//  /// Use case:
//  /// - User password change করলে
//  /// - Security breach হলে
//  /// - Admin manually revoke করলে
//  /// </summary>
//  public async Task RevokeAllTokensByUserIdAsync(int userId)
//  {
//    var activeTokens = await GetActiveTokensByUserIdAsync(userId, trackChanges: true);

//    if (activeTokens == null || !activeTokens.Any())
//      return;

//    // সব active tokens কে revoke করা
//    foreach (var token in activeTokens)
//    {
//      token.IsRevoked = true;
//      token.RevokedDate = DateTime.UtcNow;
//    }

//    // EF Core automatically track করে, তাই Update() call করার দরকার নেই
//    // শুধু SaveAsync() call করলেই হবে
//  }
//}