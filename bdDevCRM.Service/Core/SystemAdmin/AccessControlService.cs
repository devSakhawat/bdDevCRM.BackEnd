//using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
//using bdDevCRM.Utilities.KendoGrid;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class  AccessControlService : IAccessControlService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public AccessControlService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<AccessControlDto> CreateAsync(AccessControlDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new MenuDto().GetType().Name.ToString());
    bool isModuleExists = await _repository.AccessControls.ExistsAsync(m => m.AccessName == modelDto.AccessName);
    if (isModuleExists) throw new DuplicateRecordException("AccessControl", "AccessName");

    AccessControl entity = MyMapper.JsonClone<AccessControlDto, AccessControl>(modelDto);
    await _repository.AccessControls.CreateAsync(entity);
    modelDto.AccessId = await _repository.AccessControls.CreateAndGetIdAsync(entity);
    return modelDto;
  }


  public async Task<AccessControlDto> UpdateAsync(int key, AccessControlDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new GroupDto().GetType().Name.ToString());
    if (key != modelDto.AccessId) throw new IdMismatchBadRequestException(key.ToString(), new GroupDto().GetType().Name.ToString());
    bool isRecordExists = await _repository.AccessControls.ExistsAsync(m => m.AccessName == modelDto.AccessName);

    if (!isRecordExists)
    {
      AccessControl entity = await _repository.AccessControls.GetByIdAsync(m => m.AccessId == modelDto.AccessId, trackChanges: false);
      entity = MyMapper.JsonClone<AccessControlDto, AccessControl>(modelDto);
      _repository.AccessControls.UpdateByState(entity);
    }

    await _repository.SaveAsync();
    return modelDto;
  }

  public async Task<GridEntity<AccessControlDto>> AccessControlSummary(bool trackChanges, CRMGridOptions options)
  {
    string query = "Select AccessId,AccessName from AccessControl";
    string orderBy = "AccessName asc";
    var gridEntity = await _repository.AccessControls.GridData<AccessControlDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> DeleteRecordAsync(int key)
  {
    if (key == null || key == 0)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(AccessControlDto));

    await _repository.AccessControls.DeleteAsync(x => x.AccessId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"AccessControl deleted, id={key}");
    return OperationMessage.Success;
  }

}
