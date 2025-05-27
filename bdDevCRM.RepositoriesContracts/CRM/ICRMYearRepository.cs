using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRMM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMYearRepository : IRepositoryBase<Crmyear>
{
  Task<IEnumerable<Crmyear>> GetActiveYearAsync(bool trackChanges);
}
