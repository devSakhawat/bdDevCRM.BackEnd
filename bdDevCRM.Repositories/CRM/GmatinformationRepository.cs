using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class GmatinformationRepository : RepositoryBase<Gmatinformation>, IGmatinformationRepository
{
  public GmatinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Gmatinformation>> GetActiveGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GmatinformationId, track);

  public async Task<IEnumerable<Gmatinformation>> GetGmatinformationsAsync(bool track) =>
      await ListAsync(c => c.GmatinformationId, track);

  public async Task<Gmatinformation?> GetGmatinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.GmatinformationId == id, track);

  public async Task<IEnumerable<Gmatinformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.GmatinformationId, track);

  public async Task<Gmatinformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}