using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmApplicationService
{
  Task<CrmApplicationDto> GetApplication(int applicationId, bool trackChanges);
  Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(CRMGridOptions options);
  Task<CrmApplicationDto> CreateNewRecordAsync(CrmApplicationDto dto, UsersDto currentUser);
}