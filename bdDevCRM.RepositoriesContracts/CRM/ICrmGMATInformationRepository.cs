using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmGMATInformationRepository : IRepositoryBase<CrmGMATInformation>
{
  Task<IEnumerable<CrmGMATInformation>> GetActiveGmatinformationsAsync(bool track);
  Task<IEnumerable<CrmGMATInformation>> GetGmatinformationsAsync(bool track);
  Task<CrmGMATInformation?> GetGmatinformationAsync(int id, bool track);
  Task<IEnumerable<CrmGMATInformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmGMATInformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track);
}