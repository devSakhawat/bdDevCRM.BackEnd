using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmIELTSInformationRepository : RepositoryBase<CrmIELTSInformation>, ICrmIELTSInformationRepository
{
  public CrmIELTSInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmIELTSInformation>> GetActiveIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IELTSInformationId, track);

  public async Task<IEnumerable<CrmIELTSInformation>> GetIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IELTSInformationId, track);

  public async Task<CrmIELTSInformation?> GetIeltsinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.IELTSInformationId == id, track);

  public async Task<IEnumerable<CrmIELTSInformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.IELTSInformationId, track);

  public async Task<CrmIELTSInformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}