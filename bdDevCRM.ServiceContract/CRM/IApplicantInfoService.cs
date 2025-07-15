using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IApplicantInfoService
{
  Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosDDLAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantInfoDto>> GetActiveApplicantInfosAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosAsync(bool trackChanges = false);
  Task<ApplicantInfoDto> GetApplicantInfoAsync(int id, bool trackChanges = false);
  Task<ApplicantInfoDto> GetApplicantInfoByApplicationIdAsync(int applicationId, bool trackChanges = false);
  Task<ApplicantInfoDto> CreateNewRecordAsync(ApplicantInfoDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, ApplicantInfoDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, ApplicantInfoDto dto);
  Task<ApplicantInfoDto> GetApplicantInfoByEmailAsync(string email, bool trackChanges = false);
  Task<GridEntity<ApplicantInfoDto>> SummaryGrid(CRMGridOptions options);
}