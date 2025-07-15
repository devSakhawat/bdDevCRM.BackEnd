using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class WorkExperienceRepository : RepositoryBase<WorkExperience>, IWorkExperienceRepository
{
  public WorkExperienceRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<WorkExperience>> GetActiveWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<IEnumerable<WorkExperience>> GetWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<WorkExperience?> GetWorkExperienceAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.WorkExperienceId == id, track);

  public async Task<IEnumerable<WorkExperience>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.WorkExperienceId, track);
}