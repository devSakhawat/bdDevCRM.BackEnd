using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IGmatinformationRepository : IRepositoryBase<Gmatinformation>
{
  Task<IEnumerable<Gmatinformation>> GetActiveGmatinformationsAsync(bool track);
  Task<IEnumerable<Gmatinformation>> GetGmatinformationsAsync(bool track);
  Task<Gmatinformation?> GetGmatinformationAsync(int id, bool track);
  Task<IEnumerable<Gmatinformation>> GetGmatinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<Gmatinformation?> GetGmatinformationByApplicantIdAsync(int applicantId, bool track);
}