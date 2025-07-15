using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class TOEFLInformationRepository : RepositoryBase<TOEFLInformation>, ITOEFLInformationRepository
{
  public TOEFLInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<TOEFLInformation>> GetActiveToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.TOEFLInformationId, track);

  public async Task<IEnumerable<TOEFLInformation>> GetToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.TOEFLInformationId, track);

  public async Task<TOEFLInformation?> GetToeflinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.TOEFLInformationId == id, track);

  public async Task<IEnumerable<TOEFLInformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.TOEFLInformationId, track);

  public async Task<TOEFLInformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}