using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmTOEFLInformationService
{
  Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<TOEFLInformationDto>> GetActiveToeflinformationsAsync(bool trackChanges = false);
  Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsAsync(bool trackChanges = false);
  Task<TOEFLInformationDto> GetToeflinformationAsync(int id, bool trackChanges = false);
  Task<TOEFLInformationDto> GetToeflinformationByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<TOEFLInformationDto> CreateNewRecordAsync(TOEFLInformationDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, TOEFLInformationDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, TOEFLInformationDto dto);
  Task<GridEntity<TOEFLInformationDto>> SummaryGrid(CRMGridOptions options);
}