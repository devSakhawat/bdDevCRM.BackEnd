using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IEducationHistoryRepository : IRepositoryBase<EducationHistory>
{
  Task<IEnumerable<EducationHistory>> GetActiveEducationHistoriesAsync(bool track);
  Task<IEnumerable<EducationHistory>> GetEducationHistoriesAsync(bool track);
  Task<EducationHistory?> GetEducationHistoryAsync(int id, bool track);
  Task<IEnumerable<EducationHistory>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool track);
  Task<EducationHistory?> GetEducationHistoryByInstitutionAsync(string institution, bool track);
}