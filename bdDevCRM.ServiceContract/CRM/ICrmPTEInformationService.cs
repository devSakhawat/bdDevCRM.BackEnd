using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmPTEInformationService
{
  Task<IEnumerable<PTEInformationDto>> GetPTEInformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<PTEInformationDto>> GetActivePTEInformationsAsync(bool trackChanges = false);
  Task<IEnumerable<PTEInformationDto>> GetPTEInformationsAsync(bool trackChanges = false);
  Task<PTEInformationDto> GetPTEInformationAsync(int id, bool trackChanges = false);
  Task<PTEInformationDto> GetPTEInformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<PTEInformationDto> CreateNewRecordAsync(PTEInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, PTEInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, PTEInformationDto dto);
  Task<GridEntity<PTEInformationDto>> SummaryGrid(CRMGridOptions options);
}