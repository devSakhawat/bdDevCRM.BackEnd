using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IApplicantCourseRepository : IRepositoryBase<ApplicantCourse>
{
  Task<IEnumerable<ApplicantCourse>> GetActiveApplicantCoursesAsync(bool track);
  Task<IEnumerable<ApplicantCourse>> GetApplicantCoursesAsync(bool track);
  Task<ApplicantCourse?> GetApplicantCourseAsync(int id, bool track);
  Task<IEnumerable<ApplicantCourse>> GetApplicantCoursesByApplicantIdAsync(int applicantId, bool track);
  Task<ApplicantCourse?> GetApplicantCourseByApplicantAndCourseIdAsync(int applicantId, int courseId, bool track);
}