using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentTagMapRepository : IRepositoryBase<DmsDocumentTagMap>
{
  //Task<IEnumerable<DmsDocumentTagMap>> GetDocumentTagsByDocumentIdAsync(int documentId, bool trackChanges = false);
  //Task<IEnumerable<DmsDocumentTagMap>> GetTagsByDocumentIdsAsync(IEnumerable<int> documentIds, bool trackChanges = false);
  //Task<bool> AddTagToDocumentAsync(int documentId, int tagId);
  //Task<bool> RemoveTagFromDocumentAsync(int documentId, int tagId);
}
