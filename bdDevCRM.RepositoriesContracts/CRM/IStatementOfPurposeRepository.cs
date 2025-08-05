using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmStatementOfPurposeRepository : IRepositoryBase<CrmStatementOfPurpose>
{
  Task<IEnumerable<CrmStatementOfPurpose>> GetActiveStatementOfPurposesAsync(bool track);
  Task<IEnumerable<CrmStatementOfPurpose>> GetStatementOfPurposesAsync(bool track);
  Task<CrmStatementOfPurpose?> GetStatementOfPurposeAsync(int id, bool track);
  Task<CrmStatementOfPurpose?> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool track);
}