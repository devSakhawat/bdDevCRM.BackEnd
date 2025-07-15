using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class OthersinformationRepository : RepositoryBase<Othersinformation>, IOthersinformationRepository
{
  public OthersinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Othersinformation>> GetActiveOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OthersinformationId, track);

  public async Task<IEnumerable<Othersinformation>> GetOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OthersinformationId, track);

  public async Task<Othersinformation?> GetOthersinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.OthersinformationId == id, track);

  public async Task<IEnumerable<Othersinformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.OthersinformationId, track);

  public async Task<Othersinformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}