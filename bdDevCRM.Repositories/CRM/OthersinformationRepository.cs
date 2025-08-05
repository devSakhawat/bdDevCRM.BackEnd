using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmOthersInformationRepository : RepositoryBase<CrmOthersInformation>, ICrmOthersInformationRepository
{
  public CrmOthersInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmOthersInformation>> GetActiveOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OthersInformationId, track);

  public async Task<IEnumerable<CrmOthersInformation>> GetOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OthersInformationId, track);

  public async Task<CrmOthersInformation?> GetOthersinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.OthersInformationId == id, track);

  public async Task<IEnumerable<CrmOthersInformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.OthersInformationId, track);

  public async Task<CrmOthersInformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}