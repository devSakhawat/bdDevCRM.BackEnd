using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IAdditionalInfoRepository : IRepositoryBase<AdditionalInfo>
{
  Task<IEnumerable<AdditionalInfo>> GetActiveAdditionalInfosAsync(bool track);
  Task<IEnumerable<AdditionalInfo>> GetAdditionalInfosAsync(bool track);
  Task<AdditionalInfo?> GetAdditionalInfoAsync(int id, bool track);
  Task<IEnumerable<AdditionalInfo>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool track);
}