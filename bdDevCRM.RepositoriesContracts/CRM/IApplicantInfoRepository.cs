using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IApplicantInfoRepository : IRepositoryBase<ApplicantInfo>
{
  Task<IEnumerable<ApplicantInfo>> GetActiveApplicantInfosAsync(bool track);
  Task<IEnumerable<ApplicantInfo>> GetApplicantInfosAsync(bool track);
  Task<ApplicantInfo?> GetApplicantInfoAsync(int id, bool track);
  Task<ApplicantInfo?> GetApplicantInfoByApplicationIdAsync(int applicationId, bool track);
  Task<ApplicantInfo?> GetApplicantInfoByEmailAsync(string email, bool track);
}