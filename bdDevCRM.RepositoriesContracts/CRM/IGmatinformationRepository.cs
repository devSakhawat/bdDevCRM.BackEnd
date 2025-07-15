using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IGMATInformationRepository : IRepositoryBase<GMATInformation>
{
  Task<IEnumerable<GMATInformation>> GetActiveGmatinformationsAsync(bool track);
  Task<IEnumerable<GMATInformation>> GetGmatinformationsAsync(bool track);
  Task<GMATInformation?> GetGmatinformationAsync(int id, bool track);
  Task<IEnumerable<GMATInformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<GMATInformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track);
}