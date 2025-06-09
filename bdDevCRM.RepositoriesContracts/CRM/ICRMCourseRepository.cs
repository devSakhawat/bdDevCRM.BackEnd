using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMCourseRepository : IRepositoryBase<Crmcourse>
{
  Task<IEnumerable<Crmcourse>> GetActiveCourseAsync(bool trackChanges);
}
