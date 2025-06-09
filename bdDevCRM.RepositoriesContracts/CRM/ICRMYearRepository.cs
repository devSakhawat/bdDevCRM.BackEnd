using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMYearRepository : IRepositoryBase<Crmyear>
{
  Task<IEnumerable<Crmyear>> GetActiveYearAsync(bool trackChanges);
}
