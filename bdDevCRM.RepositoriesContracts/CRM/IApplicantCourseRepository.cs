using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmApplicantCourseRepository : IRepositoryBase<CrmApplicantCourse>
{
  Task<IEnumerable<CrmApplicantCourse>> GetActiveApplicantCoursesAsync(bool track);
  Task<IEnumerable<CrmApplicantCourse>> GetApplicantCoursesAsync(bool track);
  Task<CrmApplicantCourse?> GetApplicantCourseAsync(int id, bool track);
  Task<IEnumerable<CrmApplicantCourse>> GetApplicantCoursesByApplicantIdAsync(int applicantId, bool track);
  Task<CrmApplicantCourse?> GetApplicantCourseByApplicantAndCourseIdAsync(int applicantId, int courseId, bool track);
}