using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public sealed class CrmCourseRepository : RepositoryBase<CrmCourse>, ICrmCourseRepository
{

  public CrmCourseRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmCourse>> GetActiveCoursesAsync(bool track) =>
      await ListByConditionAsync(x => x.Status == true, c => c.CourseId, track);

  public async Task<IEnumerable<CrmCourse>> GetCoursesAsync(bool track) =>
      await ListAsync(c => c.CourseId, track);

  public async Task<CrmCourse?> GetCourseAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.CourseId == id, track);
}
