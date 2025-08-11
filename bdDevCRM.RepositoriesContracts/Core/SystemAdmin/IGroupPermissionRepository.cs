using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IGroupPermissionRepository : IRepositoryBase<GroupPermission>
{
  Task<IEnumerable<GroupPermissionRepositoryDto>> GetAccessPermisionForCurrentUser(int moduleId, int userId);
}
