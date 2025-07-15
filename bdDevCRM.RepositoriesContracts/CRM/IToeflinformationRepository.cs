using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IToeflinformationRepository : IRepositoryBase<Toeflinformation>
{
  Task<IEnumerable<Toeflinformation>> GetActiveToeflinformationsAsync(bool track);
  Task<IEnumerable<Toeflinformation>> GetToeflinformationsAsync(bool track);
  Task<Toeflinformation?> GetToeflinformationAsync(int id, bool track);
  Task<IEnumerable<Toeflinformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<Toeflinformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track);
}