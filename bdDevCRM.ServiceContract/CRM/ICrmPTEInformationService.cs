using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmPTEInformationService
{
  Task<IEnumerable<PTEInformationDto>> GetPteinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<PTEInformationDto>> GetActivePteinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<PTEInformationDto>> GetPteinformationsAsync(bool trackChanges = false);
  Task<PTEInformationDto> GetPteinformationAsync(int id, bool trackChanges = false);
  Task<PTEInformationDto> GetPteinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<PTEInformationDto> CreateNewRecordAsync(PTEInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, PTEInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, PTEInformationDto dto);
  Task<GridEntity<PTEInformationDto>> SummaryGrid(CRMGridOptions options);
}