using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IStatementOfPurposeRepository : IRepositoryBase<StatementOfPurpose>
{
  Task<IEnumerable<StatementOfPurpose>> GetActiveStatementOfPurposesAsync(bool track);
  Task<IEnumerable<StatementOfPurpose>> GetStatementOfPurposesAsync(bool track);
  Task<StatementOfPurpose?> GetStatementOfPurposeAsync(int id, bool track);
  Task<StatementOfPurpose?> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool track);
}