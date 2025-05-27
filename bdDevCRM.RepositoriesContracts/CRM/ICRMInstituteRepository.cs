using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMInstituteRepository : IRepositoryBase<Crminstitute>
{
  Task<IEnumerable<Crminstitute>> GetActiveInstituteAsync(bool trackChanges);
}
