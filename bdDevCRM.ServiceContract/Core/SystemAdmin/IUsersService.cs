using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IUsersService
{
  Task<GridEntity<UsersDto>> UsersSummary(bool trackChanges, CRMGridOptions options, int hrrecordId);


  IEnumerable<UsersDto> GetUsers(bool trackChanges);
  UsersDto GetUser(int UsersId, bool trackChanges);
  IEnumerable<UsersDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<UsersDto>> GetUsersAsync(bool trackChanges);
  Task<UsersDto> GetUserAsync(int UsersId, bool trackChanges);
  UsersDto? GetUserByLoginIdAsync(string loginId, bool trackChanges);
  void CreateUser(UsersDto model);
  void UpdateUser(UsersDto model);
  void DeleteUser(UsersDto model);

  Task<UsersDto> CreateUserAsync(UsersDto entityForCreate);
  Task DeleteUserAsync(int userId, bool trackChanges);
  Task UpdateUserAsync(int userId, UsersDto model, bool trackChanges);





  Task<IQueryable<PasswordHistoryDto>> GetPasswordHistory(int userId, int passRestriction);
}

