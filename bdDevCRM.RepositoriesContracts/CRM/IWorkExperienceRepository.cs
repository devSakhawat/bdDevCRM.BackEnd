using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmWorkExperienceRepository : IRepositoryBase<CrmWorkExperience>
{
  Task<IEnumerable<CrmWorkExperience>> GetActiveWorkExperiencesAsync(bool track);
  Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesAsync(bool track);
  Task<CrmWorkExperience?> GetWorkExperienceAsync(int id, bool track);
  Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool track);
}