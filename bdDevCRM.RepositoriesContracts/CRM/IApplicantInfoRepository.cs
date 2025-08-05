using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmApplicantInfoRepository : IRepositoryBase<CrmApplicantInfo>
{
  Task<IEnumerable<CrmApplicantInfo>> GetActiveApplicantInfosAsync(bool track);
  Task<IEnumerable<CrmApplicantInfo>> GetApplicantInfosAsync(bool track);
  Task<CrmApplicantInfo?> GetApplicantInfoAsync(int id, bool track);
  Task<CrmApplicantInfo?> GetApplicantInfoByApplicationIdAsync(int applicationId, bool track);
  Task<CrmApplicantInfo?> GetApplicantInfoByEmailAsync(string email, bool track);
}