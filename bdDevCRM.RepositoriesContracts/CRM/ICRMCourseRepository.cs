using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRMM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICRMCourseRepository : IRepositoryBase<Crmcourse>
{
  Task<IEnumerable<Crmcourse>> GetActiveCourseAsync(bool trackChanges);
}
