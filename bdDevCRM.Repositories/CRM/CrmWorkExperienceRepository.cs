using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmWorkExperienceRepository : RepositoryBase<CrmWorkExperience>, ICrmWorkExperienceRepository
{
  public CrmWorkExperienceRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmWorkExperience>> GetActiveWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<CrmWorkExperience?> GetWorkExperienceAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.WorkExperienceId == id, track);

  public async Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.WorkExperienceId, track);
}