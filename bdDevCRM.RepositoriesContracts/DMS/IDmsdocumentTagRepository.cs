using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.DMS;

public interface IDmsdocumentTagRepository : IRepositoryBase<DmsdocumentTag>
{
  Task<IEnumerable<DmsdocumentTag>> GetAllTagsAsync(bool trackChanges);
  void CreateTag(DmsdocumentTag tag);
  void DeleteTag(DmsdocumentTag tag);
}
