using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICRMCourseRepository : IRepositoryBase<Crmcourse>
{
  Task<IEnumerable<Crmcourse>> GetActiveCoursesAsync(bool track);
  Task<IEnumerable<Crmcourse>> GetCoursesAsync(bool track);

  Task<Crmcourse?> GetCourseAsync(int id, bool track);
}
