using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.KendoGrid;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IModuleService
{
  Task<GridEntity<ModuleDto>> ModuleSummary(bool trackChanges, GridOptions options);
  Task<List<ModuleDto>> GetModulesAsync(bool trackChanges);
  Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto);
  Task<ModuleDto> UpdateModuleAsync(int key, ModuleDto moduleDto);
  Task DeleteModuleAsync(int key, ModuleDto moduleDto);
}
