using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.Authentication;

public class TokenBlacklistRepository : RepositoryBase<TokenBlacklist>, ITokenBlacklistRepository
{
  public TokenBlacklistRepository(CRMContext context) : base(context) { }

  public async Task AddToBlacklistAsync(TokenBlacklist token)
  {
    await AddAsync(token);
  }

  public async Task<bool> IsTokenBlacklistedAsync(string token)
  {
    return await HasAnyAsync(tb => tb.Token == token && tb.ExpiryDate > DateTime.UtcNow);
  }
}
