using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmMonthService
{
  Task<IEnumerable<CrmMonthDto>> GetMonthsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<CrmMonthDto>> GetActiveMonthsAsync(bool trackChanges = false);
  Task<IEnumerable<CrmMonthDto>> GetMonthsAsync(bool trackChanges = false);
  Task<CrmMonthDto> GetMonthAsync(int id, bool trackChanges = false);
  Task<IEnumerable<CrmMonthDto>> GetMonthsByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<CrmMonthDto> CreateNewRecordAsync(CrmMonthDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, CrmMonthDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmMonthDto dto);
  Task<string> SaveOrUpdate(int key, CrmMonthDto modelDto, UsersDto currentUser);
  Task<CrmMonthDto> CreateMonthAsync(CrmMonthDto entityForCreate);
  Task<GridEntity<CrmMonthDto>> SummaryGrid(CRMGridOptions options);
}
