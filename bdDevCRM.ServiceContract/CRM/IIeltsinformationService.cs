using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IIeltsinformationService
{
  Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<IELTSInformationDto>> GetActiveIeltsinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsAsync(bool trackChanges = false);
  Task<IELTSInformationDto> GetIeltsinformationAsync(int id, bool trackChanges = false);
  Task<IELTSInformationDto> GetIeltsinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<IELTSInformationDto> CreateNewRecordAsync(IELTSInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, IELTSInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, IELTSInformationDto dto);
  Task<GridEntity<IELTSInformationDto>> SummaryGrid(CRMGridOptions options);
}