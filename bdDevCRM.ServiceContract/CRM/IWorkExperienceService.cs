using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IWorkExperienceService
{
  Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<WorkExperienceHistoryDto>> GetActiveWorkExperiencesAsync(bool trackChanges = false);
  Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesAsync(bool trackChanges = false);
  Task<WorkExperienceHistoryDto> GetWorkExperienceAsync(int id, bool trackChanges = false);
  Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<WorkExperienceHistoryDto> CreateNewRecordAsync(WorkExperienceHistoryDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, WorkExperienceHistoryDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, WorkExperienceHistoryDto dto);
  Task<GridEntity<WorkExperienceHistoryDto>> SummaryGrid(CRMGridOptions options);
}