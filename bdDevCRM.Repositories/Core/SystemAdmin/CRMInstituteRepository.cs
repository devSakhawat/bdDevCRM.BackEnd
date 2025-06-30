using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public sealed class CRMInstituteRepository : RepositoryBase<Crminstitute>, ICRMInstituteRepository
{
  public CRMInstituteRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Crminstitute>> GetActiveInstitutesAsync(bool track) => await ListByConditionAsync(x => x.Status == true, c => c.InstituteId, track);

  public async Task<IEnumerable<Crminstitute>> GetInstitutesAsync(bool track) => await ListAsync(c => c.InstituteId, track);

  public async Task<Crminstitute?> GetInstituteAsync(int id, bool track) => await FirstOrDefaultAsync(c => c.InstituteId == id, track);
}

