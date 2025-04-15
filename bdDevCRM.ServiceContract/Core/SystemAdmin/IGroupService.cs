using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IGroupService
{
  Task<GroupDto> CreateAsync(GroupDto modelDto);
  Task<GroupDto> UpdateAsync(int key, GroupDto modelDto);

  Task<GridEntity<GroupSummaryDto>> GroupSummary(bool trackChanges, CRMGridOptions options);

  Task<IEnumerable<GroupPermissionDto>> GroupPermisionsbyGroupId(int groupId);

  Task<IEnumerable<AccessControlDto>> GetAccesses();
  Task<IEnumerable<GroupPermissionDto>> GetAccessPermisionForCurrentUser(int moduleId, int userId);

  // from user settings
  Task<IEnumerable<GroupForUserSettings>> GetGroups(bool trackChanges);
}
