using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;

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

  public async Task<MenuRepositoryDto> CheckMenuPermission(string rawUrl, Users objUser)
  {
    rawUrl ??= string.Empty;

    //string query = @"
    //  SELECT DISTINCT mnu.* ,mdl.ModuleName
    //  FROM GroupPermission gp
    //  INNER JOIN Menu   mnu ON mnu.MenuId   = gp.ReferenceId
    //  INNER JOIN Module mdl ON mdl.ModuleId = mnu.ModuleId
    //  INNER JOIN GroupMember gm ON gm.GroupId = gp.GroupId
    //  WHERE gp.PermissionTableName = 'Menu'
    //    AND gm.UserId = @UserId
    //    AND mnu.MenuPath LIKE @MenuPath";

    //var parameters = new SqlParameter[]
    //{
    //  new SqlParameter("@UserId",   objUser.UserId),
    //  new SqlParameter("@MenuPath", $"%{rawUrl}%"),
    //};

    string query = string.Format(@"
      SELECT DISTINCT mnu.* ,mdl.ModuleName
      FROM GroupPermission gp
      INNER JOIN Menu   mnu ON mnu.MenuId   = gp.ReferenceId
      INNER JOIN Module mdl ON mdl.ModuleId = mnu.ModuleId
      INNER JOIN GroupMember gm ON gm.GroupId = gp.GroupId
      WHERE gp.PermissionTableName = 'Menu'
        AND gm.UserId = {0}
        AND mnu.MenuPath LIKE '%{1}%'
        AND mnu.ParentMenu = 0
      ", objUser.UserId, rawUrl);

    //var parameters = new SqlParameter[]
    //{
    //  //new SqlParameter("@UserId",   objUser.UserId),
    //  //new SqlParameter("@MenuPath", $"%{rawUrl}%"),
    //};

    MenuRepositoryDto result = await ExecuteSingleData<MenuRepositoryDto>(query);
    return result;
  }

  //   // I will update this query Leter for better optimization.
  public async Task<IEnumerable<GroupPermissionRepositoryDto>> GetAccessPermisionForCurrentUser(int moduleId, int userId)
  {
    // Parameterized SQL to avoid injection + consistent with RepositoryBase ExecuteListQuery
    //    var sql = @"
    //SELECT DISTINCT *
    //FROM GroupPermission gp
    //WHERE gp.PermissionTableName = 'Access'
    //  AND gp.ParentPermission = @ModuleId
    //  AND gp.GroupId IN (
    //      SELECT gm.GroupId
    //      FROM GroupMember gm
    //      WHERE gm.UserId = @UserId
    //      UNION
    //      SELECT gm2.GroupId
    //      FROM GroupMember gm2
    //      WHERE gm2.UserId IN (
    //          SELECT DISTINCT u.UserId
    //          FROM DeligationInfo di
    //          INNER JOIN Users u  ON u.EmployeeId  = di.HrRecordId
    //          INNER JOIN Users dg ON dg.EmployeeId = di.DeligatedHrRecordId
    //          WHERE dg.UserId = @UserId
    //            AND @Now BETWEEN di.FromDate AND di.ToDate
    //            AND di.IsActive = 1
    //      )
    //  );";

    //    var parameters = new[]
    //    {
    //      new SqlParameter("@ModuleId", moduleId),
    //      new SqlParameter("@UserId", userId),
    //      new SqlParameter("@Now", DateTime.Now.ToString("MM/dd/yyyy"))
    //    };

    var sql = string.Format(@"
SELECT DISTINCT *
FROM GroupPermission gp
WHERE gp.PermissionTableName = 'Access'
  AND gp.ParentPermission = {0}
  AND gp.GroupId IN (
      SELECT gm.GroupId
      FROM GroupMember gm
      WHERE gm.UserId = {1}
      UNION
      SELECT gm2.GroupId
      FROM GroupMember gm2
      WHERE gm2.UserId IN (
          SELECT DISTINCT u.UserId
          FROM DeligationInfo di
          INNER JOIN Users u  ON u.EmployeeId  = di.HrRecordId
          INNER JOIN Users dg ON dg.EmployeeId = di.DeligatedHrRecordId
          WHERE dg.UserId = {1}
            AND '{2}' BETWEEN di.FromDate AND di.ToDate
            AND di.IsActive = 1
      )
  )", moduleId, userId, DateTime.Now.ToString("MM/dd/yyyy"));

    //var parameters = new[]
    //{
    //  new SqlParameter("@ModuleId", moduleId),
    //  new SqlParameter("@UserId", userId),
    //  new SqlParameter("@Now", DateTime.Now.ToString("MM/dd/yyyy"))
    //};


    var result = await ExecuteListQuery<GroupPermissionRepositoryDto>(sql);
    return result;
  }
}
