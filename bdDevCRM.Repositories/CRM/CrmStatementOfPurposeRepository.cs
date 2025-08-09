using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmStatementOfPurposeRepository : RepositoryBase<CrmStatementOfPurpose>, ICrmStatementOfPurposeRepository
{
  public CrmStatementOfPurposeRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmStatementOfPurpose>> GetActiveStatementOfPurposesAsync(bool track) =>
      await ListAsync(c => c.StatementOfPurposeId, track);

  public async Task<IEnumerable<CrmStatementOfPurpose>> GetStatementOfPurposesAsync(bool track) =>
      await ListAsync(c => c.StatementOfPurposeId, track);

  public async Task<CrmStatementOfPurpose?> GetStatementOfPurposeAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.StatementOfPurposeId == id, track);

  public async Task<CrmStatementOfPurpose?> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}