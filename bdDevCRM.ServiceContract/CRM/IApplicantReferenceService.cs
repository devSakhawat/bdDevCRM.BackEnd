using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IApplicantReferenceService
{
  Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantReferenceDto>> GetActiveApplicantReferencesAsync(bool trackChanges = false);
  Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesAsync(bool trackChanges = false);
  Task<ApplicantReferenceDto> GetApplicantReferenceAsync(int id, bool trackChanges = false);
  Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<ApplicantReferenceDto> CreateNewRecordAsync(ApplicantReferenceDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, ApplicantReferenceDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, ApplicantReferenceDto dto);
  Task<GridEntity<ApplicantReferenceDto>> SummaryGrid(CRMGridOptions options);
}