using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class OTHERSInformationRepository : RepositoryBase<OTHERSInformation>, IOTHERSInformationRepository
{
  public OTHERSInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<OTHERSInformation>> GetActiveOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OTHERSInformationId, track);

  public async Task<IEnumerable<OTHERSInformation>> GetOthersinformationsAsync(bool track) =>
      await ListAsync(c => c.OTHERSInformationId, track);

  public async Task<OTHERSInformation?> GetOthersinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.OTHERSInformationId == id, track);

  public async Task<IEnumerable<OTHERSInformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.OTHERSInformationId, track);

  public async Task<OTHERSInformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}