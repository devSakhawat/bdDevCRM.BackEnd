using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class ApplicantInfoRepository : RepositoryBase<ApplicantInfo>, IApplicantInfoRepository
{
  public ApplicantInfoRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<ApplicantInfo>> GetActiveApplicantInfosAsync(bool track) =>
      await ListAsync(c => c.ApplicantId, track);

  public async Task<IEnumerable<ApplicantInfo>> GetApplicantInfosAsync(bool track) =>
      await ListAsync(c => c.ApplicantId, track);

  public async Task<ApplicantInfo?> GetApplicantInfoAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == id, track);

  public async Task<ApplicantInfo?> GetApplicantInfoByApplicationIdAsync(int applicationId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicationId == applicationId, track);

  public async Task<ApplicantInfo?> GetApplicantInfoByEmailAsync(string email, bool track) =>
      await FirstOrDefaultAsync(c => c.EmailAddress != null && c.EmailAddress.ToLower() == email.ToLower(), track);
}