using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CrmIntakeYearRepository : RepositoryBase<CrmIntakeYear>, ICrmIntakeYearRepository
{
  public CrmIntakeYearRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmIntakeYear>> GetActiveIntakeYearsAsync(bool trackChanges) =>
      await ListByConditionAsync(x => x.IsActive == true, c => c.YearValue, trackChanges, descending: false);

  public async Task<CrmIntakeYear?> GetIntakeYearByIdAsync(int intakeYearId, bool trackChanges) =>
      await FirstOrDefaultAsync(x => x.IntakeYearId.Equals(intakeYearId), trackChanges);

  public void CreateIntakeYear(CrmIntakeYear intakeYear) => Create(intakeYear);

  public void UpdateIntakeYear(CrmIntakeYear intakeYear) => UpdateByState(intakeYear);

  public void DeleteIntakeYear(CrmIntakeYear intakeYear) => Delete(intakeYear);
}