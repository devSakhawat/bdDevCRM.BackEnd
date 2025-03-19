using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.KendoGrid;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

    if(gridData.Items == null) gridData.Items = new List<ModuleDto>();

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
}
