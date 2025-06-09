using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICRMInstituteTypeRepository : IRepositoryBase<CrminstituteType>
{
  Task<IEnumerable<CrminstituteType>> GetInstituteTypesAsync(bool trackChanges);
  Task<CrminstituteType?> GetInstituteTypeAsync(int id, bool trackChanges);
}
