using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmPTEInformationRepository : IRepositoryBase<CrmPTEInformation>
{
  Task<IEnumerable<CrmPTEInformation>> GetActivePteinformationsAsync(bool track);
  Task<IEnumerable<CrmPTEInformation>> GetPteinformationsAsync(bool track);
  Task<CrmPTEInformation?> GetPteinformationAsync(int id, bool track);
  Task<IEnumerable<CrmPTEInformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmPTEInformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track);
}