using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IAccessRestrictionRepository : IRepositoryBase<AccessRestriction>
{
  Task<IEnumerable<GroupsRepositoryDto>> AccessRestrictionGroupsByHrrecordId(int hrRecordId);
  Task<IEnumerable<AccessRestrictionRepositoryDto>> AccessRestrictionByHrRecordId(int hrRecordId, string groupCondition);
  Task<IEnumerable<GroupsRepositoryDto>> GetGroupInfo(int hrRecordId);
  Task<string> GenerateAccessRestrictionConditionForCompany(int hrRecordId);

  Task<IEnumerable<AccessRestrictionRepositoryDto>> GenerateAccessRestrictionConditionListForCompany(int hrRecordId, int type, string gpcondition);
}
