using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IAccessRestrictionRepository : IRepositoryBase<AccessRestriction>
{
  Task<IEnumerable<GroupsRepositoryDto>> AccessRestrictionGroupsByHrrecordId(int hrRecordId);

}
