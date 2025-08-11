using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;


public class DmsDocumentTagMapRepository : RepositoryBase<DmsDocumentTagMap>, IDmsDocumentTagMapRepository
{
  public DmsDocumentTagMapRepository(CRMContext context) : base(context) { }


}
