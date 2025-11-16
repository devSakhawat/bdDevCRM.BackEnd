using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.CRMGrid.GRID;
namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmCourseService
{
    Task<IEnumerable<CrmCourseDto>> GetCoursesDDLAsync(bool trackChanges = false);
    Task<IEnumerable<CRMCourseDDLDto>> GetCourseByInstituteIdDDLAsync(int instituteId, bool trackChanges = false);
    Task<IEnumerable<CrmCourseDto>> GetActiveCoursesAsync(bool trackChanges = false);
    Task<IEnumerable<CrmCourseDto>> GetCoursesAsync(bool trackChanges = false);
    Task<CrmCourseDto> GetCourseAsync(int id, bool trackChanges = false);
    Task<CrmCourseDto> CreateNewRecordAsync(CrmCourseDto dto, UsersDto currentUser);
    Task<string> UpdateRecordAsync(int key, CrmCourseDto dto, bool trackChanges);
    Task<string> DeleteRecordAsync(int key, CrmCourseDto dto);
    Task<CrmCourseDto> GetCourseByTitleAsync(string title, bool trackChanges = false);

    Task<GridEntity<CrmCourseDto>> SummaryGrid(CRMGridOptions options);
}