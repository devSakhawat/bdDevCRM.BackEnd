using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ITOEFLInformationRepository : IRepositoryBase<TOEFLInformation>
{
  Task<IEnumerable<TOEFLInformation>> GetActiveToeflinformationsAsync(bool track);
  Task<IEnumerable<TOEFLInformation>> GetToeflinformationsAsync(bool track);
  Task<TOEFLInformation?> GetToeflinformationAsync(int id, bool track);
  Task<IEnumerable<TOEFLInformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<TOEFLInformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track);
}