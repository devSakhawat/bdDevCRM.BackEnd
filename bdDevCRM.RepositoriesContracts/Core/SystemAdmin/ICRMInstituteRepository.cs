using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;


public interface ICRMInstituteRepository : IRepositoryBase<Crminstitute>
{
  Task<IEnumerable<Crminstitute>> GetActiveInstitutesAsync(bool trackChanges);
  Task<IEnumerable<Crminstitute>> GetInstitutesAsync(bool trackChanges);
  Task<Crminstitute?> GetInstituteAsync(int id, bool trackChanges);
}

