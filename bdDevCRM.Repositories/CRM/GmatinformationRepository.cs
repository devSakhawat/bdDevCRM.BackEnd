using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmGMATInformationRepository : RepositoryBase<CrmGMATInformation>, ICrmGMATInformationRepository
{
  public CrmGMATInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmGMATInformation>> GetActiveGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GMATInformationId, track);

  public async Task<IEnumerable<CrmGMATInformation>> GetGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GMATInformationId, track);

  public async Task<CrmGMATInformation?> GetGmatinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.GMATInformationId == id, track);

  public async Task<IEnumerable<CrmGMATInformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.GMATInformationId, track);

  public async Task<CrmGMATInformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}