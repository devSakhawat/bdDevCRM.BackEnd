using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class IELTSInformationRepository : RepositoryBase<IELTSInformation>, IIELTSInformationRepository
{
  public IELTSInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<IELTSInformation>> GetActiveIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IELTSInformationId, track);

  public async Task<IEnumerable<IELTSInformation>> GetIeltsinformationsAsync(bool track) =>
      await ListAsync(c => c.IELTSInformationId, track);

  public async Task<IELTSInformation?> GetIeltsinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.IELTSInformationId == id, track);

  public async Task<IEnumerable<IELTSInformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.IELTSInformationId, track);

  public async Task<IELTSInformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}