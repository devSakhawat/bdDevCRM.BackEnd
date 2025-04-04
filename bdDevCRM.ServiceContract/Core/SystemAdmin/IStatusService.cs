using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IStatusService
{
  Task<IEnumerable<WfstateDto>> StatusByMenuId(int menuId, bool trackChanges);
  Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges);
}
