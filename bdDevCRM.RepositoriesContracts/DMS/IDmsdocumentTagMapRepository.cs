using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentTagMapRepository : IRepositoryBase<DmsdocumentTagMap>
{
  //Task<IEnumerable<DmsdocumentTagMap>> GetDocumentTagsByDocumentIdAsync(int documentId, bool trackChanges = false);
  //Task<IEnumerable<DmsdocumentTagMap>> GetTagsByDocumentIdsAsync(IEnumerable<int> documentIds, bool trackChanges = false);
  //Task<bool> AddTagToDocumentAsync(int documentId, int tagId);
  //Task<bool> RemoveTagFromDocumentAsync(int documentId, int tagId);
}
