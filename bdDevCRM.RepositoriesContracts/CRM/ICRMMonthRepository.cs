using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmMonthRepository : IRepositoryBase<CrmMonth>
{
  Task<IEnumerable<CrmMonth>> GetActiveMonthAsync(bool trackChanges);
}
