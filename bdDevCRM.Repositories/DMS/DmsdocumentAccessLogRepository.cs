using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;

public class DmsdocumentAccessLogRepository : RepositoryBase<DmsdocumentAccessLog>, IDmsdocumentAccessLogRepository
{
  public DmsdocumentAccessLogRepository(CRMContext context) : base(context) { }

  // Get access logs by DocumentId
  public async Task<IEnumerable<DmsdocumentAccessLog>> GetLogsByDocumentIdAsync(int documentId, bool trackChanges) =>
      await ListByConditionAsync(x => x.DocumentId == documentId, x => x.AccessDateTime, trackChanges);

  // Create new log
  public void CreateLog(DmsdocumentAccessLog log) => Create(log);
}
