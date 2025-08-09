using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmApplicationRepository : RepositoryBase<CrmApplication>, ICrmApplicationRepository
{
  public CrmApplicationRepository(CRMContext context) : base(context) { }


}