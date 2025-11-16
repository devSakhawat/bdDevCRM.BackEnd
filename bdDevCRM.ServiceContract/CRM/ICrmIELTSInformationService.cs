using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmIELTSInformationService
{
  Task<IEnumerable<IELTSInformationDto>> GetIELTSinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<IELTSInformationDto>> GetActiveIELTSinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<IELTSInformationDto>> GetIELTSinformationsAsync(bool trackChanges = false);
  Task<IELTSInformationDto> GetIELTSinformationAsync(int id, bool trackChanges = false);
  Task<IELTSInformationDto> GetIELTSinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<IELTSInformationDto> CreateNewRecordAsync(IELTSInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, IELTSInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, IELTSInformationDto dto);
  Task<GridEntity<IELTSInformationDto>> SummaryGrid(CRMGridOptions options);
}