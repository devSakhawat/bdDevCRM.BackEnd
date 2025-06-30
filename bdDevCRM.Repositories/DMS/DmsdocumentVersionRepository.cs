using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;

public class DmsdocumentVersionRepository : RepositoryBase<DmsdocumentVersion>, IDmsdocumentVersionRepository
{
  public DmsdocumentVersionRepository(CRMContext context) : base(context) { }

  // Get versions by DocumentId
  public async Task<IEnumerable<DmsdocumentVersion>> GetVersionsByDocumentIdAsync(int documentId, bool trackChanges) =>
      await ListByConditionAsync(x => x.DocumentId == documentId, x => x.VersionNumber, trackChanges);

  // Create new version
  public void CreateVersion(DmsdocumentVersion version) => Create(version);
}
