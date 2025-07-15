using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class GMATInformationRepository : RepositoryBase<GMATInformation>, IGMATInformationRepository
{
  public GMATInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<GMATInformation>> GetActiveGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GMATInformationId, track);

  public async Task<IEnumerable<GMATInformation>> GetGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GMATInformationId, track);

  public async Task<GMATInformation?> GetGmatinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.GMATInformationId == id, track);

  public async Task<IEnumerable<GMATInformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.GMATInformationId, track);

  public async Task<GMATInformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}