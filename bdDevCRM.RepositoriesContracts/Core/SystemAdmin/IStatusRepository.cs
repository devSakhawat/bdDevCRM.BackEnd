using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

  public interface IStatusRepository : IRepositoryBase<WfState>
  {
  Task<IEnumerable<WfStateRepositoryDto>> StatusByMenuId(int menuId, bool trackChanges);
  Task<IEnumerable<WfActionRepositoryDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges);

  Task<IEnumerable<WfStateRepositoryDto>> GetWFStateByUserPermission(int menuId, int userId);

  Task<IEnumerable<WfStateRepositoryDto>> GetWFStateByMenuNUserPermission(string menuName, int userId);
}
