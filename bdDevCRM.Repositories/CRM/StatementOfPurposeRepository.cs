using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class StatementOfPurposeRepository : RepositoryBase<StatementOfPurpose>, IStatementOfPurposeRepository
{
  public StatementOfPurposeRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<StatementOfPurpose>> GetActiveStatementOfPurposesAsync(bool track) =>
      await ListAsync(c => c.StatementOfPurposeId, track);

  public async Task<IEnumerable<StatementOfPurpose>> GetStatementOfPurposesAsync(bool track) =>
      await ListAsync(c => c.StatementOfPurposeId, track);

  public async Task<StatementOfPurpose?> GetStatementOfPurposeAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.StatementOfPurposeId == id, track);

  public async Task<StatementOfPurpose?> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);
}