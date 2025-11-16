using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmApplicantCourseService
{
  Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantCourseDto>> GetActiveApplicantCoursesAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesAsync(bool trackChanges = false);
  Task<ApplicantCourseDto> GetApplicantCourseAsync(int id, bool trackChanges = false);
  Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<ApplicantCourseDto> CreateNewRecordAsync(ApplicantCourseDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, ApplicantCourseDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, ApplicantCourseDto dto);
  Task<ApplicantCourseDto> GetApplicantCourseByApplicantAndCourseIdAsync(int applicantId, int courseId, bool trackChanges = false);
  Task<GridEntity<ApplicantCourseDto>> SummaryGrid(CRMGridOptions options);
}