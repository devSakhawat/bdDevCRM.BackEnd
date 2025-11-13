using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmApplicationService
{
  //Task<CrmApplicationDto> GetApplication(int applicationId, bool trackChanges);
  Task<GetApplicationDto> GetApplication(int applicationId, bool trackChanges);
  Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(CRMGridOptions options, int statusId, UsersDto usersDto ,MenuDto menuDto);
  Task<CrmApplicationDto> CreateNewRecordAsync(CrmApplicationDto dto, UsersDto currentUser);
  Task<CrmApplicationDto> UpdateCrmApplicationAsync(int key,CrmApplicationDto dto, UsersDto currentUser);
}