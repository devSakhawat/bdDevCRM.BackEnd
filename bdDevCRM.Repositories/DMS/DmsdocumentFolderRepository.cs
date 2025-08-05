using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;

public class DmsDocumentFolderRepository : RepositoryBase<DmsDocumentFolder>, IDmsDocumentFolderRepository
{
  public DmsDocumentFolderRepository(CRMContext context) : base(context) { }

  // Get folders by ParentFolderId
  public async Task<IEnumerable<DmsDocumentFolder>> GetFoldersByParentIdAsync(int? parentId, bool trackChanges) =>
      await ListByConditionAsync(x => x.ParentFolderId == parentId, x => x.FolderId, trackChanges);

  // Create new folder
  public void CreateFolder(DmsDocumentFolder folder) => Create(folder);

  // Update folder
  public void UpdateFolder(DmsDocumentFolder folder) => UpdateByState(folder);

  // Delete folder
  public void DeleteFolder(DmsDocumentFolder folder) => Delete(folder);
}
