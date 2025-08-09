using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmTOEFLInformationRepository : RepositoryBase<CrmTOEFLInformation>, ICrmTOEFLInformationRepository
{
  public CrmTOEFLInformationRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmTOEFLInformation>> GetActiveToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.TOEFLInformationId, track);

  public async Task<IEnumerable<CrmTOEFLInformation>> GetToeflinformationsAsync(bool track) =>
      await ListAsync(c => c.TOEFLInformationId, track);

  public async Task<CrmTOEFLInformation?> GetToeflinformationAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.TOEFLInformationId == id, track);

  public async Task<IEnumerable<CrmTOEFLInformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.TOEFLInformationId, track);

  public async Task<CrmTOEFLInformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}