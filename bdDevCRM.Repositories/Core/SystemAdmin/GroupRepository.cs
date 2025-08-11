using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class GroupRepository : RepositoryBase<Groups>, IGroupRepository
{
  private const string SELECT_GROUPPERMISSION_BY_GROUPID = "Select * from GROUPPERMISSION where GROUPID = {0}";
  private const string SELECT_ALL_ACCESS_CONTROL = "Select * from ACCESSCONTROL";

  public GroupRepository(CRMContext context) : base(context) { }

  // summary data must be returned in a specific format like this.
  public async Task<List<GroupsRepositoryDto>> GroupSummary(bool trackChanges)
  {
    string menuSummaryQuery = $"Select GroupId,Group.ModuleId, GroupName, GroupPath, ISNULL(ParentGroup, 0) as ParentGroup ,ModuleName,ToDo,SORORDER\r\n,(Select GroupName from Groups mn where mn.GroupId = menu.ParentGroup) as ParentGroupName \r\nfrom Group \r\nleft outer join Module on module.ModuleId = menu.ModuleId\r\norder by ModuleName asc,ParentGroup asc, GroupName";
    IEnumerable<GroupsRepositoryDto> menusDto = await ExecuteListQuery<GroupsRepositoryDto>(menuSummaryQuery);
    return menusDto.ToList();
  }

  public async Task<IEnumerable<GroupPermissionRepositoryDto>> GroupPermisionsbyGroupId(int groupId)
  {
    string query = string.Format(SELECT_GROUPPERMISSION_BY_GROUPID, groupId);
    IEnumerable<GroupPermissionRepositoryDto> groupPermissions = await ExecuteListQuery<GroupPermissionRepositoryDto>(query);
    return groupPermissions.AsQueryable();
  }

  public async Task<IEnumerable<AccessControlRepositoryDto>> GetAccesses()
  {
    string query = string.Format(SELECT_ALL_ACCESS_CONTROL);
    IEnumerable<AccessControlRepositoryDto> groupPermissions = await ExecuteListQuery<AccessControlRepositoryDto>(query);
    return groupPermissions;
  }







}
