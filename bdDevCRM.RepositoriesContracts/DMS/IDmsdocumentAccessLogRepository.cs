using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentAccessLogRepository : IRepositoryBase<DmsdocumentAccessLog>
{
  Task<IEnumerable<DmsdocumentAccessLog>> GetLogsByDocumentIdAsync(int documentId, bool trackChanges);
  void CreateLog(DmsdocumentAccessLog log);
}