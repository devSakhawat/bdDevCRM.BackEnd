using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentTagRepository : IRepositoryBase<DmsDocumentTag>
{
  Task<IEnumerable<DmsDocumentTag>> GetAllTagsAsync(bool trackChanges);
  void CreateTag(DmsDocumentTag tag);
  void DeleteTag(DmsDocumentTag tag);
}
