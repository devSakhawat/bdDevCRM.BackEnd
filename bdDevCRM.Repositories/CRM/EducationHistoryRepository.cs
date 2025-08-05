using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmEducationHistoryRepository : RepositoryBase<CrmEducationHistory>, ICrmEducationHistoryRepository
{
  public CrmEducationHistoryRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmEducationHistory>> GetActiveEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<CrmEducationHistory?> GetEducationHistoryAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.EducationHistoryId == id, track);

  public async Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.EducationHistoryId, track);

  public async Task<CrmEducationHistory?> GetEducationHistoryByInstitutionAsync(string institution, bool track) =>
      await FirstOrDefaultAsync(c => c.Institution != null && c.Institution.ToLower() == institution.ToLower(), track);
}