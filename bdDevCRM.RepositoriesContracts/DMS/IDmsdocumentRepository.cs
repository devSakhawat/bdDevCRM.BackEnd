using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentRepository : IRepositoryBase<Dmsdocument>
{
  Task<IEnumerable<Dmsdocument>> GetAllDocumentsAsync(bool trackChanges);
  Task<Dmsdocument> GetDocumentByIdAsync(int documentId, bool trackChanges);
  void CreateDocument(Dmsdocument document);
  void UpdateDocument(Dmsdocument document);
  void DeleteDocument(Dmsdocument document);
}

