using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmPTEInformationRepository : IRepositoryBase<CrmPTEInformation>
{
  Task<IEnumerable<CrmPTEInformation>> GetActivePTEInformationsAsync(bool track);
  Task<IEnumerable<CrmPTEInformation>> GetPTEInformationsAsync(bool track);
  Task<CrmPTEInformation?> GetPTEInformationAsync(int id, bool track);
  Task<IEnumerable<CrmPTEInformation>> GetPTEInformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmPTEInformation?> GetPTEInformationByApplicantIdAsync(int applicantId, bool track);
}