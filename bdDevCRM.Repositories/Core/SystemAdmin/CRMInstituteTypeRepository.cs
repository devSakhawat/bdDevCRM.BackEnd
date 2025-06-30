using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public sealed class CRMInstituteTypeRepository : RepositoryBase<CrminstituteType>, ICRMInstituteTypeRepository
{
  public CRMInstituteTypeRepository(CRMContext context) : base(context) { }

  public Task<IEnumerable<CrminstituteType>> GetInstituteTypesAsync(bool trackChanges) => ListAsync(c => c.InstituteTypeId, trackChanges);

  public Task<CrminstituteType?> GetInstituteTypeAsync(int id, bool trackChanges) => FirstOrDefaultAsync(x => x.InstituteTypeId == id, trackChanges);
}

