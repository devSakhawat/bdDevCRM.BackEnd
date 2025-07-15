using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class AdditionalInfoRepository : RepositoryBase<AdditionalInfo>, IAdditionalInfoRepository
{
  public AdditionalInfoRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<AdditionalInfo>> GetActiveAdditionalInfosAsync(bool track) =>
      await ListAsync(c => c.AdditionalInfoId, track);

  public async Task<IEnumerable<AdditionalInfo>> GetAdditionalInfosAsync(bool track) =>
      await ListAsync(c => c.AdditionalInfoId, track);

  public async Task<AdditionalInfo?> GetAdditionalInfoAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.AdditionalInfoId == id, track);

  public async Task<IEnumerable<AdditionalInfo>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.AdditionalInfoId, track);
}