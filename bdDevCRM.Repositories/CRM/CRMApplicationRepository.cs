using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CRMApplicationRepository : RepositoryBase<CrmApplication>, ICRMApplicationRepository
{
  public CRMApplicationRepository(CRMContext context) : base(context) { }

}