using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class GroupPermissionRepository : RepositoryBase<GroupPermission>, IGroupPermissionRepository
{
  private const string SELECT_ACCESSPERMISSION_BYMODULE_AND_USER =
            @"Select distinct * 
from GroupPermission
where  PermissionTableName = 'Access' and ParentPermission = {0} and
GroupId in (Select GroupId from GroupMember where UserId = {1}
union
Select GroupId 
from GroupMember where UserId in (
Select distinct Users.UserId from DeligationInfo  
inner join Users on Users.EmployeeId = DeligationInfo.HrRecordId  
inner join Users Dg on Dg.EmployeeId = DeligationInfo.DeligatedHrRecordId 
where Dg.UserId = {1} and '{2}' between FromDate and ToDate and DeligationInfo.IsActive = 1))";

  public GroupPermissionRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<GroupPermissionRepositoryDto>> GetAccessPermisionForCurrentUser(int moduleId, int userId)
  {
    string sql = string.Format(SELECT_ACCESSPERMISSION_BYMODULE_AND_USER, moduleId, userId, DateTime.Now.ToString("MM/dd/yyyy"));
    IEnumerable<GroupPermissionRepositoryDto> groupPermissionRepositoriesDto = await ExecuteListQuery<GroupPermissionRepositoryDto>(sql);
    return groupPermissionRepositoriesDto;
  }


}
