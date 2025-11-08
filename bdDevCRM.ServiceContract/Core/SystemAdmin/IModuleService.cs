using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IModuleService
{
    Task<GridEntity<ModuleDto>> ModuleSummary(bool trackChanges, CRMGridOptions options);
    Task<List<ModuleDto>> GetModulesAsync(UsersDto currentUser, bool trackChanges);
    Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto);
    Task<ModuleDto> UpdateModuleAsync(int key, ModuleDto moduleDto);
    Task DeleteModuleAsync(int key, ModuleDto moduleDto);
}
