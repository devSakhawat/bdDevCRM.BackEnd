using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmAdditionalInfoRepository : IRepositoryBase<CrmAdditionalInfo>
{
  Task<IEnumerable<CrmAdditionalInfo>> GetActiveAdditionalInfosAsync(bool track);
  Task<IEnumerable<CrmAdditionalInfo>> GetAdditionalInfosAsync(bool track);
  Task<CrmAdditionalInfo?> GetAdditionalInfoAsync(int id, bool track);
  Task<IEnumerable<CrmAdditionalInfo>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool track);
}