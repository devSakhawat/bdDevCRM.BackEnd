using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class ApplicantCourseRepository : RepositoryBase<ApplicantCourse>, IApplicantCourseRepository
{
  public ApplicantCourseRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<ApplicantCourse>> GetActiveApplicantCoursesAsync(bool track) =>
      await ListAsync(c => c.ApplicantCourseId, track);

  public async Task<IEnumerable<ApplicantCourse>> GetApplicantCoursesAsync(bool track) =>
      await ListAsync(c => c.ApplicantCourseId, track);

  public async Task<ApplicantCourse?> GetApplicantCourseAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantCourseId == id, track);

  public async Task<IEnumerable<ApplicantCourse>> GetApplicantCoursesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.ApplicantCourseId, track);

  public async Task<ApplicantCourse?> GetApplicantCourseByApplicantAndCourseIdAsync(int applicantId, int courseId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId && c.CourseId == courseId, track);
}