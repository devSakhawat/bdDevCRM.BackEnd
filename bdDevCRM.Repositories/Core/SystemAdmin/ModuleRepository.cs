using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
{
  public ModuleRepository(CRMContext context) : base(context) { }

  private const string SELECT_ALL_Module_BY_MODULEID = "Select * from Module where ModuleId = {0} order by SorOrder,ModuleName asc";

  public async Task<IEnumerable<ModuleRepositoryDto>> SelectAllModuleByModuleId(int moduleId, bool trackChanges)
  {
    string quary = string.Format(SELECT_ALL_Module_BY_MODULEID, moduleId);
    IEnumerable<ModuleRepositoryDto> ModuleRepositoryDto = await ExecuteListQuery<ModuleRepositoryDto>(quary);

    return ModuleRepositoryDto.AsEnumerable();
  }


  public async Task<IEnumerable<Module>> GetAllModules(bool trackChanges)
  {
    IEnumerable<Module> modules = await ListAsync(m => m.ModuleId, trackChanges);
    return modules;
  }



  public Module GetModule(int ModuleId, bool trackChanges) => FirstOrDefault(c => c.ModuleId.Equals(ModuleId), trackChanges);

  public IEnumerable<Module> GetByIds(IEnumerable<int> ids, bool trackChanges) => GetListByIds(x => ids.Contains(x.ModuleId), trackChanges);



  // Get all Modules
  public async Task<IEnumerable<Module>> GetModulesAsync(bool trackChanges) => await ListAsync(m => m.ModuleId ,trackChanges);

  // Get a single Module by ID
  public async Task<Module> GetModuleAsync(int ModuleId, bool trackChanges) => await FirstOrDefaultAsync(c => c.ModuleId.Equals(ModuleId), trackChanges);

  public async Task<Module?> ModuleByModuleIdWithAdditionalCondition(int ModuleId, string additionalCondition)
  {
    var quary = string.Format("Select * from Module where ModuleId = {0} {1}", ModuleId, additionalCondition);
    Module? objModule = await ExecuteSingleData<Module>(quary);
    return objModule;
  }



  // Add a new Module
  public void CreateModule(Module Module) => Create(Module);

  // Update an existing Module
  public void UpdateModule(Module Module) => UpdateByState(Module);

  // Delete a Module by ID
  public void DeleteModule(Module Module) => Delete(Module);



  public async Task<List<ModuleRepositoryDto>> ModuleSummary(bool trackChanges)
  {
    string moduleSummaryQuery = $"select ModuleId, ModuleName from Module order by ModuleId";
    IEnumerable<ModuleRepositoryDto> modulesDto = await ExecuteListQuery<ModuleRepositoryDto>(moduleSummaryQuery);
    return modulesDto.ToList();
  }
}
