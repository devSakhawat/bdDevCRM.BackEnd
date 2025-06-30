using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class GroupMemberRepository : RepositoryBase<GroupMember>, IGroupMemberRepository
{
  private const string SELECT_GROUPMEMBER_BY_USERID = "Select * from GroupMember where UserId = {0}";

  public GroupMemberRepository(CRMContext context) : base(context) { }

  // Group Member by UserId for user settings.
  public async Task<IEnumerable<GroupMemberRepositoryDto>> GroupMemberByUserId(int userId, bool trackChanges)
  {
    string groupMemberQuery = string.Format(SELECT_GROUPMEMBER_BY_USERID, userId);
    IEnumerable<GroupMemberRepositoryDto> groupMembersByUser = await ExecuteListQuery<GroupMemberRepositoryDto>(groupMemberQuery);
    return groupMembersByUser.ToList();
  }

  //public async Task<IEnumerable<GroupMemberPermissionRepositoryDto>> GroupMemberPermisionsbyGroupMemberId(int GroupMemberId)
  //{
  //  string query = string.Format(SELECT_GroupMemberPERMISSION_BY_GroupMemberID, GroupMemberId);
  //  IEnumerable<GroupMemberPermissionRepositoryDto> GroupMemberPermissions = await ExecuteListQuery<GroupMemberPermissionRepositoryDto>(query);
  //  return GroupMemberPermissions.AsQueryable();
  //}

  //public async Task<IEnumerable<AccessControlRepositoryDto>> GetAccesses()
  //{
  //  string query = string.Format(SELECT_ALL_ACCESS_CONTROL);
  //  IEnumerable<AccessControlRepositoryDto> GroupMemberPermissions = await ExecuteListQuery<AccessControlRepositoryDto>(query);
  //  return GroupMemberPermissions;
  //}







}
