using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class IeltsinformationRepository : RepositoryBase<Ieltsinformation>, IIeltsinformationRepository
{
  public IeltsinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Ieltsinformation>> GetActiveIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IeltsinformationId, track);

  public async Task<IEnumerable<Ieltsinformation>> GetIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IeltsinformationId, track);

  public async Task<Ieltsinformation?> GetIeltsinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.IeltsinformationId == id, track);

  public async Task<IEnumerable<Ieltsinformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.IeltsinformationId, track);

  public async Task<Ieltsinformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}