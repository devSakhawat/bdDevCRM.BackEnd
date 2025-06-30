using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentTypeRepository : IRepositoryBase<DmsdocumentType>
{
  Task<IEnumerable<DmsdocumentType>> GetAllDocumentTypesAsync(bool trackChanges);
  void CreateDocumentType(DmsdocumentType type);
  void UpdateDocumentType(DmsdocumentType type);
  void DeleteDocumentType(DmsdocumentType type);
}