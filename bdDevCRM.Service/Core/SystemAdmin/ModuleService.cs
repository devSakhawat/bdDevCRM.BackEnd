using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

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

    public async Task<GridEntity<ModuleDto>> ModuleSummary(bool trackChanges, CRMGridOptions options)
    {
        //var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var menuRepositoryDtos = await _repository.Modules.ModuleSummary(trackChanges);
        var modulesDto = MyMapper.JsonCloneIEnumerableToList<ModuleRepositoryDto, ModuleDto>(menuRepositoryDtos);
        var gridentity = new GridResult<ModuleDto>().Data(modulesDto, modulesDto.Count);

        //var source = modulesDto.AsQueryable();
        //var source2 = source.Skip(options.skip).Take(options.pageSize);
        //var gridentity = new GridEntity<ModuleDto>();
        //gridentity.Items = source2.ToList();
        //gridentity.TotalCount = (int)source.LongCount();

        if (gridentity.Items == null) gridentity.Items = new List<ModuleDto>();

        //stopwatch.Stop();
        //_logger.LogInfo($"Menu query execution time: {stopwatch.ElapsedMilliseconds}ms");

        return gridentity;
    }

    public async Task<List<ModuleDto>> GetModulesAsync(UsersDto currentUser, bool trackChanges)
    {
        var module = await _repository.Modules.GetModulesAsync(trackChanges);
        List<ModuleDto> modulesDto = MyMapper.JsonCloneIEnumerableToList<Module, ModuleDto>(module);
        return modulesDto;
    }

    public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
    {
        try
        {
            if (moduleDto == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());

            bool isModuleExists = await _repository.Modules.ExistsAsync(m => m.ModuleName == moduleDto.ModuleName);
            if (isModuleExists) throw new DuplicateRecordException("Module", "ModuleName");

            Module module = MyMapper.JsonClone<ModuleDto, Module>(moduleDto);
            moduleDto.ModuleId = await _repository.Modules.CreateAndGetIdAsync(module);
        }
        catch (DbUpdateException ex)
        {
            // Log the detailed inner exception
            throw new Exception("Save failed", ex.InnerException);
        }

        //await _repository.SaveAsync();
        return moduleDto;
    }

    public async Task<ModuleDto> UpdateModuleAsync(int key, ModuleDto moduleDto)
    {
        if (moduleDto == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());
        if (key != moduleDto.ModuleId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

        Module moduleData = await _repository.Modules.GetByIdAsync(m => m.ModuleId == moduleDto.ModuleId, trackChanges: false);
        if (moduleDto.ModuleName == moduleData.ModuleName) throw new DuplicateRecordException();

        moduleData = MyMapper.JsonClone<ModuleDto, Module>(moduleDto);
        _repository.Modules.UpdateByState(moduleData);
        await _repository.SaveAsync();
        moduleDto = MyMapper.JsonClone<Module, ModuleDto>(moduleData);
        return moduleDto;
    }

    public async Task DeleteModuleAsync(int key)
    {
        if (key <= 0)
            throw new ArgumentOutOfRangeException(nameof(key), "Module ID must be a positive integer.");

        Module entity = await _repository.Modules.GetByIdAsync(m => m.ModuleId == key, trackChanges: false);
        if (entity == null) throw new NullModelBadRequestException(new ModuleDto().GetType().Name.ToString());
        if (key != entity.ModuleId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

        await _repository.Modules.DeleteAsync(x => x.ModuleId == entity.ModuleId, trackChanges: true);
        await _repository.SaveAsync();
    }


}
