using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmInstituteTypeRepository : IRepositoryBase<CrmInstituteType>
{
  Task<IEnumerable<CrmInstituteType>> GetInstituteTypesAsync(bool trackChanges);
  Task<CrmInstituteType?> GetInstituteTypeAsync(int id, bool trackChanges);
}
