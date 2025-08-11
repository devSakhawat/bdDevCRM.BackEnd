using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmIntakeMonthRepository : IRepositoryBase<CrmIntakeMonth>
{
  Task<IEnumerable<CrmIntakeMonth>> GetActiveIntakeMonthsAsync(bool trackChanges);
  Task<CrmIntakeMonth?> GetIntakeMonthByIdAsync(int intakeMonthId, bool trackChanges);
  void CreateIntakeMonth(CrmIntakeMonth intakeMonth);
  void UpdateIntakeMonth(CrmIntakeMonth intakeMonth);
  void DeleteIntakeMonth(CrmIntakeMonth intakeMonth);
}