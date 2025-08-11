using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentTypeRepository : IRepositoryBase<DmsDocumentType>
{
  Task<IEnumerable<DmsDocumentType>> GetAllDocumentTypesAsync(bool trackChanges);
  void CreateDocumentType(DmsDocumentType type);
  void UpdateDocumentType(DmsDocumentType type);
  void DeleteDocumentType(DmsDocumentType type);
}