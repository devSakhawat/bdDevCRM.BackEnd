using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentRepository : IRepositoryBase<Dmsdocument>
{
  Task<IEnumerable<Dmsdocument>> GetActiveAsync(bool track);
  Task<Dmsdocument?> GetAsync(int id, bool track);
}
