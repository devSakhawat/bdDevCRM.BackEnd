using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmOthersInformationRepository : IRepositoryBase<CrmOthersInformation>
{
  Task<IEnumerable<CrmOthersInformation>> GetActiveOthersinformationsAsync(bool track);
  Task<IEnumerable<CrmOthersInformation>> GetOthersinformationsAsync(bool track);
  Task<CrmOthersInformation?> GetOthersinformationAsync(int id, bool track);
  Task<IEnumerable<CrmOthersInformation>> GetOthersinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmOthersInformation?> GetOthersinformationByApplicantIdAsync(int applicantId, bool track);
}