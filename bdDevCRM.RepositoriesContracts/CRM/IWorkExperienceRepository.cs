using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IWorkExperienceRepository : IRepositoryBase<WorkExperience>
{
  Task<IEnumerable<WorkExperience>> GetActiveWorkExperiencesAsync(bool track);
  Task<IEnumerable<WorkExperience>> GetWorkExperiencesAsync(bool track);
  Task<WorkExperience?> GetWorkExperienceAsync(int id, bool track);
  Task<IEnumerable<WorkExperience>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool track);
}