using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IUsersService
{
  Task<GridEntity<UsersDto>> UsersSummary(int companyId ,bool trackChanges, CRMGridOptions options, UsersDto user);


  IEnumerable<UsersDto> GetUsers(bool trackChanges);
  UsersDto GetUser(int UsersId, bool trackChanges);
  IEnumerable<UsersDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<UsersDto>> GetUsersAsync(bool trackChanges);
  Task<UsersDto> GetUserAsync(int UsersId, bool trackChanges);
  UsersDto? GetUserByLoginIdRaw(string loginId, bool trackChanges);
  void CreateUser(UsersDto model);
  void UpdateUser(UsersDto model);
  void DeleteUser(UsersDto model);

  Task<UsersDto> CreateUserAsync(UsersDto entityForCreate);
  Task DeleteUserAsync(int userId, bool trackChanges);
  Task UpdateUserAsync(int userId, UsersDto model, bool trackChanges);
  Task<UsersDto> SaveUser(UsersDto usersDto);




  Task<IQueryable<PasswordHistoryDto>> GetPasswordHistory(int userId, int passRestriction);
}

