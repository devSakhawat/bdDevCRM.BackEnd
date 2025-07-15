using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IApplicantReferenceService
{
  Task<IEnumerable<ReferenceDto>> GetApplicantReferencesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<ReferenceDto>> GetActiveApplicantReferencesAsync(bool trackChanges = false);
  Task<IEnumerable<ReferenceDto>> GetApplicantReferencesAsync(bool trackChanges = false);
  Task<ReferenceDto> GetApplicantReferenceAsync(int id, bool trackChanges = false);
  Task<IEnumerable<ReferenceDto>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<ReferenceDto> CreateNewRecordAsync(ReferenceDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, ReferenceDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, ReferenceDto dto);
  Task<GridEntity<ReferenceDto>> SummaryGrid(CRMGridOptions options);
}