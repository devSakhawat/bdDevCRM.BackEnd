using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoryDtos.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmAdditionalDocumentRepository : IRepositoryBase<CrmAdditionalDocument>
{
  Task<IEnumerable<CrmAdditionalDocument>> GetActiveAdditionalDocumentsAsync(bool track);
  Task<IEnumerable<CrmAdditionalDocument>> GetAdditionalDocumentsAsync(bool track);
  Task<CrmAdditionalDocument?> GetAdditionalDocumentAsync(int id, bool track);
  Task<IEnumerable<CrmAdditionalDocument>> GetAdditionalDocumentsByApplicantIdAsync(int applicantId, bool track);

  Task<IEnumerable<AdditionalDocumentRepositoryDto>> AdditionalDocumentsByApplicantId(int applicantId);
}