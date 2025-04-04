using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IModuleRepository : IRepositoryBase<Module>
{
  Task<IEnumerable<Module>> GetAllModules(bool trackChanges);

  Module GetModule(int ModuleId, bool trackChanges);

  void CreateModule(Module Module);

  IEnumerable<Module> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<Module>> GetModulesAsync(bool trackChanges);

  Task<Module> GetModuleAsync(int ModuleId, bool trackChanges);

  Task<Module?> ModuleByModuleIdWithAdditionalCondition(int ModuleId, string additionalCondition);

  void UpdateModule(Module Module);

  void DeleteModule(Module Module);

  Task<List<ModuleRepositoryDto>> ModuleSummary(bool trackChanges);
}
