using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IIELTSInformationRepository : IRepositoryBase<IELTSInformation>
{
  Task<IEnumerable<IELTSInformation>> GetActiveIeltsinformationsAsync(bool track);
  Task<IEnumerable<IELTSInformation>> GetIeltsinformationsAsync(bool track);
  Task<IELTSInformation?> GetIeltsinformationAsync(int id, bool track);
  Task<IEnumerable<IELTSInformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<IELTSInformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track);
}