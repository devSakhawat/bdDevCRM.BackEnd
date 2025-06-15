using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.DMS;

public class DmsdocumentFolderRepository : RepositoryBase<DmsdocumentFolder>, IDmsdocumentFolderRepository
{
  public DmsdocumentFolderRepository(CRMContext context) : base(context) { }

  // Get folders by ParentFolderId
  public async Task<IEnumerable<DmsdocumentFolder>> GetFoldersByParentIdAsync(int? parentId, bool trackChanges) =>
      await ListByConditionAsync(x => x.ParentFolderId == parentId, x => x.FolderId, trackChanges);

  // Create new folder
  public void CreateFolder(DmsdocumentFolder folder) => Create(folder);

  // Update folder
  public void UpdateFolder(DmsdocumentFolder folder) => UpdateByState(folder);

  // Delete folder
  public void DeleteFolder(DmsdocumentFolder folder) => Delete(folder);
}
