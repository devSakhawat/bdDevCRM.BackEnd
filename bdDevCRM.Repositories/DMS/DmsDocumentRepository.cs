using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;

public sealed class DmsDocumentRepository : RepositoryBase<Dmsdocument>, IDmsDocumentRepository
{
  public DmsDocumentRepository(CRMContext ctx) : base(ctx) { }

  public async Task<IEnumerable<Dmsdocument>> GetActiveAsync(bool t) => await ListByConditionAsync(d => d.FilePath != null, d => d.DocumentId, t);

  public async Task<Dmsdocument?> GetAsync(int id, bool t) =>  await FirstOrDefaultAsync(d => d.DocumentId == id, t);
}


