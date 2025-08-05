using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmIELTSInformationRepository : IRepositoryBase<CrmIELTSInformation>
{
  Task<IEnumerable<CrmIELTSInformation>> GetActiveIeltsinformationsAsync(bool track);
  Task<IEnumerable<CrmIELTSInformation>> GetIeltsinformationsAsync(bool track);
  Task<CrmIELTSInformation?> GetIeltsinformationAsync(int id, bool track);
  Task<IEnumerable<CrmIELTSInformation>> GetIeltsinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmIELTSInformation?> GetIeltsinformationByApplicantIdAsync(int applicantId, bool track);
}