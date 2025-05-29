using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMInstituteTypeRepository : IRepositoryBase<CRMInstituteType>
{
  Task<IEnumerable<CRMInstituteType>> GetInstituteTypeAsync();
}
