using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class PteinformationRepository : RepositoryBase<PTEInformation>, IPTEInformationRepository
{
  public PteinformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<PTEInformation>> GetActivePteinformationsAsync(bool track) =>
      await ListAsync(c => c.PTEInformationId, track);

  public async Task<IEnumerable<PTEInformation>> GetPteinformationsAsync(bool track) =>
      await ListAsync(c => c.PTEInformationId, track);

  public async Task<PTEInformation?> GetPteinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PTEInformationId == id, track);

  public async Task<IEnumerable<PTEInformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.PTEInformationId, track);

  public async Task<PTEInformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}