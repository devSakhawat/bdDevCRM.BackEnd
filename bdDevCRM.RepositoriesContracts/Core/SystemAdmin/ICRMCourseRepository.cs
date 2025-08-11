using bdDevCRM.Entities.Entities.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmCourseRepository : IRepositoryBase<CrmCourse>
{
  Task<IEnumerable<CrmCourse>> GetActiveCoursesAsync(bool track);
  Task<IEnumerable<CrmCourse>> GetCoursesAsync(bool track);

  Task<CrmCourse?> GetCourseAsync(int id, bool track);
}
