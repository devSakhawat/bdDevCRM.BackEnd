using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmGmatinformationService
{
  Task<IEnumerable<GMATInformationDto>> GetGmatinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<GMATInformationDto>> GetActiveGmatinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<GMATInformationDto>> GetGmatinformationsAsync(bool trackChanges = false);
  Task<GMATInformationDto> GetGmatinformationAsync(int id, bool trackChanges = false);
  Task<GMATInformationDto> GetGmatinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<GMATInformationDto> CreateNewRecordAsync(GMATInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, GMATInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, GMATInformationDto dto);
  Task<GridEntity<GMATInformationDto>> SummaryGrid(CRMGridOptions options);
}