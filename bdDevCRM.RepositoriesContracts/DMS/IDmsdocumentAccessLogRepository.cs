using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentAccessLogRepository : IRepositoryBase<DmsDocumentAccessLog>
{
  Task<IEnumerable<DmsDocumentAccessLog>> GetLogsByDocumentIdAsync(int documentId, bool trackChanges);
  void CreateLog(DmsDocumentAccessLog log);
}