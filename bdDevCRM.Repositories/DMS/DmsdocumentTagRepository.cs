using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;

// DmsdocumentTag Repository Implementation
public class DmsdocumentTagRepository : RepositoryBase<DmsdocumentTag>, IDmsdocumentTagRepository
{
  public DmsdocumentTagRepository(CRMContext context) : base(context) { }

  // Get all tags
  public async Task<IEnumerable<DmsdocumentTag>> GetAllTagsAsync(bool trackChanges) =>
      await ListAsync(x => x.TagId, trackChanges);

  // Create new tag
  public void CreateTag(DmsdocumentTag tag) => Create(tag);

  // Delete tag
  public void DeleteTag(DmsdocumentTag tag) => Delete(tag);
}