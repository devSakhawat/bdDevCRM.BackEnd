using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmTOEFLInformationRepository : IRepositoryBase<CrmTOEFLInformation>
{
  Task<IEnumerable<CrmTOEFLInformation>> GetActiveToeflinformationsAsync(bool track);
  Task<IEnumerable<CrmTOEFLInformation>> GetToeflinformationsAsync(bool track);
  Task<CrmTOEFLInformation?> GetToeflinformationAsync(int id, bool track);
  Task<IEnumerable<CrmTOEFLInformation>> GetToeflinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<CrmTOEFLInformation?> GetToeflinformationByApplicantIdAsync(int applicantId, bool track);
}