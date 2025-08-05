using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmOthersInformationService
{
  Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<OTHERSInformationDto>> GetActiveOthersinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsAsync(bool trackChanges = false);
  Task<OTHERSInformationDto> GetOthersinformationAsync(int id, bool trackChanges = false);
  Task<OTHERSInformationDto> GetOthersinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<OTHERSInformationDto> CreateNewRecordAsync(OTHERSInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, OTHERSInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, OTHERSInformationDto dto);
  Task<GridEntity<OTHERSInformationDto>> SummaryGrid(CRMGridOptions options);
}