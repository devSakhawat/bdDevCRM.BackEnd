using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.Authentication;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.Authentication;

public class AuthenticationRepository : RepositoryBase<Users>, IAuthenticationRepository
{
  public AuthenticationRepository(CRMContext context) : base(context) { }


  public async Task<Users> AuthenticateByLoginId(string loginId)
  {
    if (string.IsNullOrEmpty(loginId)) return null;

    Users user = FirstOrDefault(u => u.LoginId == loginId, true);

    if (user == null)
      return null;

    return user;
  }


  public async Task<Users> AuthenticateByPassword(string loginId, string password)
  {
    if (string.IsNullOrEmpty(loginId) || string.IsNullOrEmpty(password))
      return null;


    Users user = FirstOrDefault(u => u.LoginId == loginId, true);
    return user;
  }
}
