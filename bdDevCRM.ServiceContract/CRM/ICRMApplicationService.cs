using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICRMApplicationService
{
  Task<CrmApplicationDto> CreateNewRecordAsync(CrmApplicationDto dto, UsersDto currentUser);
}