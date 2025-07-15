using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class EducationHistoryRepository : RepositoryBase<EducationHistory>, IEducationHistoryRepository
{
  public EducationHistoryRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<EducationHistory>> GetActiveEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<IEnumerable<EducationHistory>> GetEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<EducationHistory?> GetEducationHistoryAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.EducationHistoryId == id, track);

  public async Task<IEnumerable<EducationHistory>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.EducationHistoryId, track);

  public async Task<EducationHistory?> GetEducationHistoryByInstitutionAsync(string institution, bool track) =>
      await FirstOrDefaultAsync(c => c.Institution != null && c.Institution.ToLower() == institution.ToLower(), track);
}