using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmYearRepository : IRepositoryBase<CrmYear>
{
  Task<IEnumerable<CrmYear>> GetActiveYearAsync(bool trackChanges);
}
