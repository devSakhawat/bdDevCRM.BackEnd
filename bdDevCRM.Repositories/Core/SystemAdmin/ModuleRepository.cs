using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
{
  public ModuleRepository(CRMContext context) : base(context) { }

  private const string SELECT_ALL_Module_BY_MODULEID = "Select * from Module where ModuleId = {0} order by SorOrder,ModuleName asc";

  public async Task<IEnumerable<ModuleRepositoryDto>> SelectAllModuleByModuleId(int moduleId, bool trackChanges)
  {
    string quary = string.Format(SELECT_ALL_Module_BY_MODULEID, moduleId);
    IEnumerable<ModuleRepositoryDto> ModuleRepositoryDto = await GetListOfDataByQuery<ModuleRepositoryDto>(quary);

    return ModuleRepositoryDto.AsQueryable();
  }


  public IEnumerable<Module> GetAllModules(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.ModuleName).ToList();

  public Module GetModule(int ModuleId, bool trackChanges) => FindByCondition(c => c.ModuleId.Equals(ModuleId), trackChanges).SingleOrDefault();

  public IEnumerable<Module> GetByIds(IEnumerable<int> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.ModuleId), trackChanges).ToList();



  // Get all Modules
  public async Task<IEnumerable<Module>> GetModulesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.ModuleId).ToListAsync();

  // Get a single Module by ID
  public async Task<Module> GetModuleAsync(int ModuleId, bool trackChanges) => await FindByCondition(c => c.ModuleId.Equals(ModuleId), trackChanges).FirstOrDefaultAsync();

  public async Task<Module?> ModuleByModuleIdWithAdditionalCondition(int ModuleId, string additionalCondition)
  {
    var quary = string.Format("Select * from Module where ModuleId = {0} {1}", ModuleId, additionalCondition);
    Module? objModule = await GetSingleGenericResultByQuery<Module>(quary);
    return objModule;
  }



  // Add a new Module
  public void CreateModule(Module Module) => Create(Module);

  // Update an existing Module
  public void UpdateModule(Module Module) => UpdateAsync(Module);

  // Delete a Module by ID
  public void DeleteModule(Module Module) => Delete(Module);



  public async Task<List<ModuleRepositoryDto>> ModuleSummary(bool trackChanges)
  {
    string moduleSummaryQuery = $"select ModuleId, ModuleName from Module order by ModuleId";
    List<ModuleRepositoryDto> modulesDto = await ExecuteQueryAsync<ModuleRepositoryDto>(moduleSummaryQuery);
    return modulesDto;
  }


}
