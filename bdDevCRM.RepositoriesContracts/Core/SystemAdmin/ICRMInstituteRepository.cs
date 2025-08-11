using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;


public interface ICrmInstituteRepository : IRepositoryBase<CrmInstitute>
{
  Task<IEnumerable<CrmInstitute>> GetActiveInstitutesAsync(bool trackChanges);
  Task<IEnumerable<CrmInstitute>> GetInstitutesAsync(bool trackChanges);
  Task<CrmInstitute?> GetInstituteAsync(int id, bool trackChanges);
}

