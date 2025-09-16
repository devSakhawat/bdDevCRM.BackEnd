using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoryDtos.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmEducationHistoryRepository : IRepositoryBase<CrmEducationHistory>
{
  Task<IEnumerable<CrmEducationHistory>> GetActiveEducationHistoriesAsync(bool track);
  Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesAsync(bool track);
  Task<CrmEducationHistory?> GetEducationHistoryAsync(int id, bool track);
  Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool track);
  Task<CrmEducationHistory?> GetEducationHistoryByInstitutionAsync(string institution, bool track);
  Task<IEnumerable<EducationHistoryRepositoryDto>> EducationHistoryByApplicantId(int applicantId);
}