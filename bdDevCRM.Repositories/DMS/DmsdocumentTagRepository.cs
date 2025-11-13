using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;

// DmsDocumentTag Repository Implementation
public class DmsDocumentTagRepository : RepositoryBase<DmsDocumentTag>, IDmsDocumentTagRepository
{
  public DmsDocumentTagRepository(CRMContext context) : base(context) { }

  // Get all tags
  public async Task<IEnumerable<DmsDocumentTag>> GetAllTagsAsync(bool trackChanges) =>
      await ListAsync(x => x.TagId, trackChanges);

  // Create new tag
  public void CreateTag(DmsDocumentTag tag) => Create(tag);

  // Delete tag
  public void DeleteTag(DmsDocumentTag tag) => Delete(tag);
}