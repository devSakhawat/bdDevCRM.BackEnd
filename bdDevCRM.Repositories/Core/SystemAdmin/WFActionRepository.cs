using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class WFActionRepository : RepositoryBase<Wfaction>, IWFActionRepository
{
  public WFActionRepository(CRMContext context) : base(context) { }


}
