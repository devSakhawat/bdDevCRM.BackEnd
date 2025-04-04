using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IUsersRepository : IRepositoryBase<Users>
{
  IEnumerable<Users> GetUsers(bool trackChanges);

  Users GetUser(int UsersId, bool trackChanges);

  IEnumerable<Users> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<Users>> GetUsersAsync(bool trackChanges);

  Task<Users> GetUserAsync(int usersId, bool trackChanges);

  UsersRepositoryDto? GetUserByLoginIdAsync(string loginId, bool trackChanges);

  Users? GetUserByLoginId(string loginId, bool trackChanges);

  void CreateUser(Users Users);

  void UpdateUser(Users Users);

  void DeleteUser(Users Users);

  ////////////////////////////

  Task<IQueryable<PasswordHistoryRepositoryDto>> GetPasswordHistory(int userId, int passRestriction);

  string UpdateUser(Users users, PasswordHistory passwordHistory);
}
