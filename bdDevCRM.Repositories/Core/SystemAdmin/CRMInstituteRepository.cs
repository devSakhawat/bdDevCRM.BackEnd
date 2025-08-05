using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public sealed class CRMInstituteRepository : RepositoryBase<CrmInstitute>, ICrmInstituteRepository
{
  public CRMInstituteRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmInstitute>> GetActiveInstitutesAsync(bool track) => await ListByConditionAsync(x => x.Status == true, c => c.InstituteId, track);

  public async Task<IEnumerable<CrmInstitute>> GetInstitutesAsync(bool track) => await ListAsync(c => c.InstituteId, track);

  public async Task<CrmInstitute?> GetInstituteAsync(int id, bool track) => await FirstOrDefaultAsync(c => c.InstituteId == id, track);
}

