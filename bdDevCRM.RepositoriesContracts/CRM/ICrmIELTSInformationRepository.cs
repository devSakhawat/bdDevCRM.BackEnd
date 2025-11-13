using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmIELTSInformationRepository : IRepositoryBase<CrmIELTSInformation>
{
  Task<IEnumerable<CrmIELTSInformation>> GetActiveIELTSinformationsAsync(bool track);
  Task<IEnumerable<CrmIELTSInformation>> GetIELTSinformationsAsync(bool track);
  Task<CrmIELTSInformation?> GetIELTSinformationAsync(int id, bool track);
  Task<IEnumerable<CrmIELTSInformation>> GetIELTSinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmIELTSInformation?> GetIELTSinformationByApplicantIdAsync(int applicantId, bool track);
}