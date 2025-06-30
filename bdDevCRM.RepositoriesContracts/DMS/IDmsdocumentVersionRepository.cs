using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentVersionRepository : IRepositoryBase<DmsdocumentVersion>
{
  Task<IEnumerable<DmsdocumentVersion>> GetVersionsByDocumentIdAsync(int documentId, bool trackChanges);
  void CreateVersion(DmsdocumentVersion version);
}
