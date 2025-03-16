using bdDevCRM.Entities.Entities;
using bdDevCRM.Shared.DataTransferObjects.Authentication;

namespace bdDevCRM.ServiceContract.Authentication;

public interface IAuthenticationService
{
  //Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
  bool ValidateUser(UserForAuthenticationDto userForAuth);
  Task<string> CreateToken(UserForAuthenticationDto users);

  string GenerateToken(Users user);
}
