using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IOthersinformationRepository : IRepositoryBase<Othersinformation>
{
  Task<IEnumerable<Othersinformation>> GetActiveOthersinformationsAsync(bool track);
  Task<IEnumerable<Othersinformation>> GetOthersinformationsAsync(bool track);
  Task<Othersinformation?> GetOthersinformationAsync(int id, bool track);
  Task<IEnumerable<Othersinformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<Othersinformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track);
}