using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System.Data;
using System.Text.RegularExpressions;
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

  public async Task<IEnumerable<AccessRestrictionRepositoryDto>> AccessRestrictionByHrRecordId(int hrRecordId, string groupCondition)
  {
    var query = string.Format(@"select Distinct ReferenceId,ReferenceType,ParentReference,ChiledParentReference from AccessRestriction where (HrRecordId = {0} {1})", hrRecordId, groupCondition);

    IEnumerable<AccessRestrictionRepositoryDto> accessRestrictionData = await ExecuteListQuery<AccessRestrictionRepositoryDto>(query);
    return accessRestrictionData;
  }

  public async Task<IEnumerable<GroupsRepositoryDto>> GetGroupInfo(int hrRecordId)
  {
    string query = string.Format(@"Select GroupId from GroupMember
inner join Users on Users.UserId = GroupMember.UserId
inner join Employment on Employment.HRRecordId = Users.EmployeeId
where HRRecordId = {0}", hrRecordId);

    IEnumerable<GroupsRepositoryDto> accessRestrictionData = await ExecuteListQuery<GroupsRepositoryDto>(query);
    return accessRestrictionData;
  }

  public async Task<IEnumerable<AccessRestrictionRepositoryDto>> GenerateAccessRestrictionConditionListForCompany(int hrRecordId, int type, string gpcondition)
  {
    var query = string.Format(@"select Distinct ReferenceId,ReferenceType,ParentReference,ChiledParentReference from AccessRestriction where (HrRecordId = {0} {2}) and ReferenceType={1}", hrRecordId, type, gpcondition);

    var data = await ExecuteListQuery<AccessRestrictionRepositoryDto>(query);
    return data;
  }

  public async Task<string> GenerateAccessRestrictionConditionForCompany(int hrRecordId)
  {
    var condition = string.Empty;
    var objGroups = await GetGroupInfo(hrRecordId);
    var groupCondition = string.Empty;

    if (objGroups != null && objGroups.Any())
    {
      var gids = string.Join(",", objGroups.Select(g => g.GroupId));
      if (!string.IsNullOrEmpty(gids))
      {
        groupCondition = $" or GroupId in ({gids})";
      }
    }

    IEnumerable<AccessRestrictionRepositoryDto> objAccessData = await GenerateAccessRestrictionConditionListForCompany(hrRecordId, 1, groupCondition);
    if (objAccessData != null && objAccessData.Any())
    {
      var ids = string.Join(",", objAccessData.Select(access => access.ReferenceId));
      condition = $"CompanyId in ({ids})";
    }

    return condition;
  }



}
