using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IIeltsinformationRepository : IRepositoryBase<Ieltsinformation>
{
  Task<IEnumerable<Ieltsinformation>> GetActiveIeltsinformationsAsync(bool track);
  Task<IEnumerable<Ieltsinformation>> GetIeltsinformationsAsync(bool track);
  Task<Ieltsinformation?> GetIeltsinformationAsync(int id, bool track);
  Task<IEnumerable<Ieltsinformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<Ieltsinformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track);
}