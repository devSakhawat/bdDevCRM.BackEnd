using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public sealed class CRMCourseRepository : RepositoryBase<Crmcourse>, ICRMCourseRepository
{

  public CRMCourseRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Crmcourse>> GetActiveCoursesAsync(bool track) =>
      await ListByConditionAsync(x => x.Status == true, c => c.CourseId, track);

  public async Task<IEnumerable<Crmcourse>> GetCoursesAsync(bool track) =>
      await ListAsync(c => c.CourseId, track);

  public async Task<Crmcourse?> GetCourseAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.CourseId == id, track);
}
