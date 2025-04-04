using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class AccessRestrictionRepository : RepositoryBase<AccessRestriction>, IAccessRestrictionRepository
{
  public AccessRestrictionRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<GroupsRepositoryDto>> AccessRestrictionGroupsByHrrecordId(int hrRecordId)
  {
    string query = string.Format(@"Select GroupId from GroupMember
inner join Users on Users.UserId = GroupMember.UserId
inner join Employment on Employment.HRRecordId = Users.EmployeeId
where HRRecordId = {0}", hrRecordId);
    IEnumerable<GroupsRepositoryDto> groups = await ExecuteListQuery<GroupsRepositoryDto>(query);
    return groups;
  }


}
