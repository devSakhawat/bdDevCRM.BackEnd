using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmIntakeYearRepository : IRepositoryBase<CrmIntakeYear>
{
  Task<IEnumerable<CrmIntakeYear>> GetActiveIntakeYearsAsync(bool trackChanges);
  Task<CrmIntakeYear?> GetIntakeYearByIdAsync(int intakeYearId, bool trackChanges);
  void CreateIntakeYear(CrmIntakeYear intakeYear);
  void UpdateIntakeYear(CrmIntakeYear intakeYear);
  void DeleteIntakeYear(CrmIntakeYear intakeYear);
}