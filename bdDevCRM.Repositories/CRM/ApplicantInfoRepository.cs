using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmApplicantInfoRepository : RepositoryBase<CrmApplicantInfo>, ICrmApplicantInfoRepository
{
  public CrmApplicantInfoRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmApplicantInfo>> GetActiveApplicantInfosAsync(bool track) =>
      await ListAsync(c => c.ApplicantId, track);

  public async Task<IEnumerable<CrmApplicantInfo>> GetApplicantInfosAsync(bool track) =>
      await ListAsync(c => c.ApplicantId, track);

  public async Task<CrmApplicantInfo?> GetApplicantInfoAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == id, track);

  public async Task<CrmApplicantInfo?> GetApplicantInfoByApplicationIdAsync(int applicationId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicationId == applicationId, track);

  public async Task<CrmApplicantInfo?> GetApplicantInfoByEmailAsync(string email, bool track) =>
      await FirstOrDefaultAsync(c => c.EmailAddress != null && c.EmailAddress.ToLower() == email.ToLower(), track);
}