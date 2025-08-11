using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentFolderRepository : IRepositoryBase<DmsDocumentFolder>
{
  Task<IEnumerable<DmsDocumentFolder>> GetFoldersByParentIdAsync(int? parentId, bool trackChanges);
  void CreateFolder(DmsDocumentFolder folder);
  void UpdateFolder(DmsDocumentFolder folder);
  void DeleteFolder(DmsDocumentFolder folder);
}