using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmPTEInformationRepository : RepositoryBase<CrmPTEInformation>, ICrmPTEInformationRepository
{
  public CrmPTEInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmPTEInformation>> GetActivePteinformationsAsync(bool track) =>
      await ListAsync(c => c.PTEInformationId, track);

  public async Task<IEnumerable<CrmPTEInformation>> GetPteinformationsAsync(bool track) =>
      await ListAsync(c => c.PTEInformationId, track);

  public async Task<CrmPTEInformation?> GetPteinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PTEInformationId == id, track);

  public async Task<IEnumerable<CrmPTEInformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.PTEInformationId, track);

  public async Task<CrmPTEInformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}