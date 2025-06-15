using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Service.DMS;



internal sealed class DmsdocumentAccessLogService : IDmsdocumentAccessLogService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsdocumentAccessLogService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsdocumentAccessLogDDL>> GetAccessLogsDDLAsync(bool trackChanges = false)
  {
    var logs = await _repository.DmsdocumentAccessLogs.ListAsync(trackChanges:trackChanges);

    if (!logs.Any())
      throw new GenericListNotFoundException("DmsdocumentAccessLog");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsdocumentAccessLog, DmsdocumentAccessLogDDL>(logs);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsdocumentAccessLogDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsdocumentAccessLog";  // adjust if needed
    string orderBy = "AccessDateTime desc";

    var gridEntity = await _repository.DmsdocumentAccessLogs.GridData<DmsdocumentAccessLogDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsdocumentAccessLogDto modelDto)
  {
    if (modelDto.LogId != 0)
      throw new InvalidCreateOperationException("LogId must be 0 when creating a new log.");

    var log = MyMapper.JsonClone<DmsdocumentAccessLogDto, DmsdocumentAccessLog>(modelDto);

    var createdId = await _repository.DmsdocumentAccessLogs.CreateAndGetIdAsync(log);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New access log created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsdocumentAccessLogDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.LogId)
      return "Invalid update attempt: key does not match the LogId.";

    bool exists = await _repository.DmsdocumentAccessLogs.ExistsAsync(x => x.LogId == key);
    if (!exists)
      return "Update failed: log not found.";

    var log = MyMapper.JsonClone<DmsdocumentAccessLogDto, DmsdocumentAccessLog>(modelDto);

    _repository.DmsdocumentAccessLogs.Update(log);
    await _repository.SaveAsync();
    _logger.LogWarn($"Access log with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsdocumentAccessLogDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsdocumentAccessLogDto));

    if (key != modelDto.LogId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsdocumentAccessLogDto));

    var log = await _repository.DmsdocumentAccessLogs.FirstOrDefaultAsync(x => x.LogId == key, false);

    if (log == null)
      throw new GenericNotFoundException("DmsdocumentAccessLog", "LogId", key.ToString());

    await _repository.DmsdocumentAccessLogs.DeleteAsync(x => x.LogId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Access log with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsdocumentAccessLogDto modelDto)
  {
    if (modelDto.LogId == 0 && key == 0)
    {
      var newLog = MyMapper.JsonClone<DmsdocumentAccessLogDto, DmsdocumentAccessLog>(modelDto);

      var createdId = await _repository.DmsdocumentAccessLogs.CreateAndGetIdAsync(newLog);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New access log created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.LogId)
    {
      var exists = await _repository.DmsdocumentAccessLogs.ExistsAsync(x => x.LogId == key);
      if (!exists)
      {
        var updateLog = MyMapper.JsonClone<DmsdocumentAccessLogDto, DmsdocumentAccessLog>(modelDto);
        _repository.DmsdocumentAccessLogs.Update(updateLog);
        await _repository.SaveAsync();

        _logger.LogWarn($"Access log with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: access log with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and LogId mismatch.";
    }
  }
}
