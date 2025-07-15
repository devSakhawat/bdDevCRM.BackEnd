using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class ToeflinformationRepository : RepositoryBase<Toeflinformation>, IToeflinformationRepository
{
  public ToeflinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Toeflinformation>> GetActiveToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.ToeflinformationId, track);

  public async Task<IEnumerable<Toeflinformation>> GetToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.ToeflinformationId, track);

  public async Task<Toeflinformation?> GetToeflinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ToeflinformationId == id, track);

  public async Task<IEnumerable<Toeflinformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.ToeflinformationId, track);

  public async Task<Toeflinformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}