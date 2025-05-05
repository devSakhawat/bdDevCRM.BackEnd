using bdDevCRM.Entities.Entities;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ISystemSettingsRepository : IRepositoryBase<SystemSettings>
{
  Task<SystemSettings> GetSystemSettingsDataByCompanyId(int companyId);
  Task<AssemblyInfo?> GetAssemblyInfoResult();
}
