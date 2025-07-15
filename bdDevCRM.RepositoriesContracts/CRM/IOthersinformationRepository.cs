using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IOTHERSInformationRepository : IRepositoryBase<OTHERSInformation>
{
  Task<IEnumerable<OTHERSInformation>> GetActiveOthersinformationsAsync(bool track);
  Task<IEnumerable<OTHERSInformation>> GetOthersinformationsAsync(bool track);
  Task<OTHERSInformation?> GetOthersinformationAsync(int id, bool track);
  Task<IEnumerable<OTHERSInformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<OTHERSInformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track);
}