using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.KendoGrid;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class ModuleService : IModuleService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public ModuleService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<GridEntity<ModuleDto>> ModuleSummary(bool trackChanges, GridOptions options)
  {
    //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    var menuRepositoryDtos = await _repository.Modules.ModuleSummary(trackChanges);
    var modulesDto = MyMapper.JsonCloneIEnumerableToList<ModuleRepositoryDto, ModuleDto>(menuRepositoryDtos);
    var gridData = GridResult<ModuleDto>.Data(modulesDto, options);

    if (gridData.Items == null) gridData.Items = new List<ModuleDto>();

    //stopwatch.Stop();
    //_logger.LogInfo($"Menu query execution time: {stopwatch.ElapsedMilliseconds}ms");

    return gridData;
  }

  public async Task<List<ModuleDto>> GetModulesAsync(bool trackChanges)
  {
    var module = await _repository.Modules.GetModulesAsync(trackChanges);
    List<ModuleDto> modulesDto = MyMapper.JsonCloneIEnumerableToList<Module, ModuleDto>(module);
    return modulesDto;
  }

  public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
  {
    if (moduleDto == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());

    bool isModuleExists = await _repository.Modules.HasAnyAsync(m => m.ModuleName == moduleDto.ModuleName);
    if (isModuleExists) throw new DuplicateRecordException("Module", "ModuleName");

    Module module = MyMapper.JsonClone<ModuleDto, Module>(moduleDto);

    await _repository.Modules.CreateAsync(module);
    await _repository.SaveAsync();
    return moduleDto;
  }

  public async Task<ModuleDto> UpdateModuleAsync(int key, ModuleDto moduleDto)
  {
    if (moduleDto == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());
    if (key != moduleDto.ModuleId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

    Module moduleData = await _repository.Modules.GetByIdAsync(m => m.ModuleId == moduleDto.ModuleId, trackChanges: false);
    if(moduleDto.ModuleName == moduleData.ModuleName) throw new DuplicateRecordException();

    moduleData = MyMapper.JsonClone<ModuleDto, Module>(moduleDto);
    _repository.Modules.UpdateAsync(moduleData);
    await _repository.SaveAsync();
    moduleDto = MyMapper.JsonClone<Module, ModuleDto>(moduleData);
    return moduleDto;
  }

  public async Task DeleteModuleAsync(int key, ModuleDto moduleDto)
  {
    if (moduleDto == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());
    if (key != moduleDto.ModuleId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

    Module moduleData = await _repository.Modules.GetByIdAsync(m => m.ModuleId == key, trackChanges: false);
    if (moduleData == null) throw new GenericNotFoundException("Module", "ModuleId", key.ToString());

    await _repository.Modules.DeleteAsync(x => x.ModuleId == moduleData.ModuleId ,trackChanges:true);
    await _repository.SaveAsync();
  }


}
