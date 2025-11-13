using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;

public class DmsDocumentAccessLogRepository : RepositoryBase<DmsDocumentAccessLog>, IDmsDocumentAccessLogRepository
{
  public DmsDocumentAccessLogRepository(CRMContext context) : base(context) { }

  // Get access logs by DocumentId
  public async Task<IEnumerable<DmsDocumentAccessLog>> GetLogsByDocumentIdAsync(int documentId, bool trackChanges) =>
      await ListByConditionAsync(x => x.DocumentId == documentId, x => x.AccessDateTime, trackChanges);

  // Create new log
  public void CreateLog(DmsDocumentAccessLog log) => Create(log);
}
