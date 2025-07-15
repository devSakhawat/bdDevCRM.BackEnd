using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IAdditionalInfoService
{
  Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosDDLAsync(bool trackChanges = false);
  Task<IEnumerable<AdditionalInfoDto>> GetActiveAdditionalInfosAsync(bool trackChanges = false);
  Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosAsync(bool trackChanges = false);
  Task<AdditionalInfoDto> GetAdditionalInfoAsync(int id, bool trackChanges = false);
  Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<AdditionalInfoDto> CreateNewRecordAsync(AdditionalInfoDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, AdditionalInfoDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, AdditionalInfoDto dto);
  Task<GridEntity<AdditionalInfoDto>> SummaryGrid(CRMGridOptions options);
}