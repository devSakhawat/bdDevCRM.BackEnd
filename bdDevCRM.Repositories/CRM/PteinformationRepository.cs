using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class PteinformationRepository : RepositoryBase<Pteinformation>, IPteinformationRepository
{
  public PteinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<Pteinformation>> GetActivePteinformationsAsync(bool track) =>
      await ListAsync(c => c.PteinformationId, track);

  public async Task<IEnumerable<Pteinformation>> GetPteinformationsAsync(bool track) =>
      await ListAsync(c => c.PteinformationId, track);

  public async Task<Pteinformation?> GetPteinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PteinformationId == id, track);

  public async Task<IEnumerable<Pteinformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.PteinformationId, track);

  public async Task<Pteinformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}