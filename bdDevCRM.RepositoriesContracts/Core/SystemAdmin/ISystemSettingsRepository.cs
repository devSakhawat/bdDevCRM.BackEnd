using bdDevCRM.Entities.Entities;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ISystemSettingsRepository
{
  Task<SystemSettings?> GetSystemSettingsDataByCompanyId(int companyId);
  Task<AssemblyInfo?> GetAssemblyInfoResult();
}
