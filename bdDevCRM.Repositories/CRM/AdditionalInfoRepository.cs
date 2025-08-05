using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmAdditionalInfoRepository : RepositoryBase<CrmAdditionalInfo>, ICrmAdditionalInfoRepository
{
  public CrmAdditionalInfoRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmAdditionalInfo>> GetActiveAdditionalInfosAsync(bool track) =>
      await ListAsync(c => c.AdditionalInfoId, track);

  public async Task<IEnumerable<CrmAdditionalInfo>> GetAdditionalInfosAsync(bool track) =>
      await ListAsync(c => c.AdditionalInfoId, track);

  public async Task<CrmAdditionalInfo?> GetAdditionalInfoAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.AdditionalInfoId == id, track);

  public async Task<IEnumerable<CrmAdditionalInfo>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.AdditionalInfoId, track);
}