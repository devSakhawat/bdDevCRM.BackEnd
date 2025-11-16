using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmAdditionalDocumentService
{
  Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsDDLAsync(bool trackChanges = false);
  Task<IEnumerable<AdditionalDocumentDto>> GetActiveAdditionalDocumentsAsync(bool trackChanges = false);
  Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsAsync(bool trackChanges = false);
  Task<AdditionalDocumentDto> GetAdditionalDocumentAsync(int id, bool trackChanges = false);
  Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<AdditionalDocumentDto> CreateNewRecordAsync(AdditionalDocumentDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, AdditionalDocumentDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, AdditionalDocumentDto dto);
  Task<GridEntity<AdditionalDocumentDto>> SummaryGrid(CRMGridOptions options);

  //Task<IEnumerable<AdditionalDocumentDto>> AdditionalDocumentsByApplicantId(int applicantId);
}