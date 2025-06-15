using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;

public class DmsdocumentTypeRepository : RepositoryBase<DmsdocumentType>, IDmsdocumentTypeRepository
{
  public DmsdocumentTypeRepository(CRMContext context) : base(context) { }

  // Get all document types
  public async Task<IEnumerable<DmsdocumentType>> GetAllDocumentTypesAsync(bool trackChanges) =>
      await ListAsync(x => x.DocumentTypeId, trackChanges);

  // Create document type
  public void CreateDocumentType(DmsdocumentType type) => Create(type);

  // Update document type
  public void UpdateDocumentType(DmsdocumentType type) => UpdateByState(type);

  // Delete document type
  public void DeleteDocumentType(DmsdocumentType type) => Delete(type);
}

