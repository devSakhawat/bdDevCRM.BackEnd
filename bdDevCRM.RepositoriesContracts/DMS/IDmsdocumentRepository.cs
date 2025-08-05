using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsDocumentRepository : IRepositoryBase<DmsDocument>
{
  Task<IEnumerable<DmsDocument>> GetAllDocumentsAsync(bool trackChanges);
  Task<DmsDocument> GetDocumentByIdAsync(int documentId, bool trackChanges);
  void CreateDocument(DmsDocument document);
  void UpdateDocument(DmsDocument document);
  void DeleteDocument(DmsDocument document);
}

