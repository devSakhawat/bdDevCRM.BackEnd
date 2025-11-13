using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentVersionRepository : IRepositoryBase<DmsDocumentVersion>
{
  Task<IEnumerable<DmsDocumentVersion>> GetVersionsByDocumentIdAsync(int documentId, bool trackChanges);
  void CreateVersion(DmsDocumentVersion version);
}
