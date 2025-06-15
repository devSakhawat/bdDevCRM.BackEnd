using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentFolderRepository : IRepositoryBase<DmsdocumentFolder>
{
  Task<IEnumerable<DmsdocumentFolder>> GetFoldersByParentIdAsync(int? parentId, bool trackChanges);
  void CreateFolder(DmsdocumentFolder folder);
  void UpdateFolder(DmsdocumentFolder folder);
  void DeleteFolder(DmsdocumentFolder folder);
}