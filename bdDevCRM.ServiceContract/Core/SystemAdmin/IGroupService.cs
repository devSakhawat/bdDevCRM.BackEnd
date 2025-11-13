using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
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
  Task<IEnumerable<GroupForUserSettings>> GetGroupsByUserId(int userId, bool trackChanges);
  Task<IEnumerable<GroupMemberDto>> GroupMemberByUserId(int userId, bool trackChanges);

  // get menu permission from controller.
  Task<MenuDto> CheckMenuPermission(string rawPath, UsersDto objUser);
}
