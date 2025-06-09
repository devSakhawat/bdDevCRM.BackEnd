using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMMonthRepository : IRepositoryBase<Crmmonth>
{
  Task<IEnumerable<Crmmonth>> GetActiveMonthAsync(bool trackChanges);
}
