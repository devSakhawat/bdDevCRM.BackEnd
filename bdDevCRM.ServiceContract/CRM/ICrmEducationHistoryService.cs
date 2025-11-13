using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmEducationHistoryService
{
  Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<EducationHistoryDto>> GetActiveEducationHistoriesAsync(bool trackChanges = false);
  Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesAsync(bool trackChanges = false);
  Task<EducationHistoryDto> GetEducationHistoryAsync(int id, bool trackChanges = false);
  Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<EducationHistoryDto> CreateNewRecordAsync(EducationHistoryDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, EducationHistoryDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, EducationHistoryDto dto);
  Task<EducationHistoryDto> GetEducationHistoryByInstitutionAsync(string institution, bool trackChanges = false);
  Task<GridEntity<EducationHistoryDto>> SummaryGrid(CRMGridOptions options);
}