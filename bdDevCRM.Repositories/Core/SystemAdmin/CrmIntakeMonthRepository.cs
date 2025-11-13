using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CrmIntakeMonthRepository : RepositoryBase<CrmIntakeMonth>, ICrmIntakeMonthRepository
{
  public CrmIntakeMonthRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmIntakeMonth>> GetActiveIntakeMonthsAsync(bool trackChanges) =>
      await ListByConditionAsync(x => x.IsActive == true, c => c.MonthNumber, trackChanges, descending: false);

  public async Task<CrmIntakeMonth?> GetIntakeMonthByIdAsync(int intakeMonthId, bool trackChanges) =>
      await FirstOrDefaultAsync(x => x.IntakeMonthId.Equals(intakeMonthId), trackChanges);

  public void CreateIntakeMonth(CrmIntakeMonth intakeMonth) => Create(intakeMonth);

  public void UpdateIntakeMonth(CrmIntakeMonth intakeMonth) => UpdateByState(intakeMonth);

  public void DeleteIntakeMonth(CrmIntakeMonth intakeMonth) => Delete(intakeMonth);
}