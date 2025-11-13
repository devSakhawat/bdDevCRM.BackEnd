using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmMonthRepository : IRepositoryBase<CrmMonth>
{
  Task<IEnumerable<CrmMonth>> GetActiveMonthAsync(bool trackChanges);
}
