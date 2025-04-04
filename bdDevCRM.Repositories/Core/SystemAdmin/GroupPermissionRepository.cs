using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class GroupPermissionRepository : RepositoryBase<GroupPermission>, IGroupPermissionRepository
{

  public GroupPermissionRepository(CRMContext context) : base(context) { }



}
