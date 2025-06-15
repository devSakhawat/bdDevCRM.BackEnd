using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;

public class DmsdocumentRepository : RepositoryBase<Dmsdocument>, IDmsdocumentRepository
{
  public DmsdocumentRepository(CRMContext context) : base(context) { }

  // Get all documents ordered by DocumentId
  public async Task<IEnumerable<Dmsdocument>> GetAllDocumentsAsync(bool trackChanges) =>
      await ListAsync(x => x.DocumentId, trackChanges);

  // Get a single document by DocumentId
  public async Task<Dmsdocument> GetDocumentByIdAsync(int documentId, bool trackChanges) =>
      await FirstOrDefaultAsync(x => x.DocumentId == documentId, trackChanges);

  // Add a new document
  public void CreateDocument(Dmsdocument document) => Create(document);

  // Update an existing document
  public void UpdateDocument(Dmsdocument document) => UpdateByState(document);

  // Delete a document
  public void DeleteDocument(Dmsdocument document) => Delete(document);
}



