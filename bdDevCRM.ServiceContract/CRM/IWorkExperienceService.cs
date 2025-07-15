using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IWorkExperienceService
{
  Task<IEnumerable<WorkExperienceDto>> GetWorkExperiencesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<WorkExperienceDto>> GetActiveWorkExperiencesAsync(bool trackChanges = false);
  Task<IEnumerable<WorkExperienceDto>> GetWorkExperiencesAsync(bool trackChanges = false);
  Task<WorkExperienceDto> GetWorkExperienceAsync(int id, bool trackChanges = false);
  Task<IEnumerable<WorkExperienceDto>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<WorkExperienceHistoryDto> CreateNewRecordAsync(WorkExperienceHistoryDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, WorkExperienceHistoryDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, WorkExperienceHistoryDto dto);
  Task<GridEntity<WorkExperienceHistoryDto>> SummaryGrid(CRMGridOptions options);
}