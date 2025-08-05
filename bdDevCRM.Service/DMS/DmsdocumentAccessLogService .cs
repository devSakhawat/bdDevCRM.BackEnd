using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.DMS;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Service.DMS;



internal sealed class DmsDocumentAccessLogService : IDmsDocumentAccessLogService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsDocumentAccessLogService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsDocumentAccessLogDDL>> GetAccessLogsDDLAsync(bool trackChanges = false)
  {
    var logs = await _repository.DmsDocumentAccessLogs.ListAsync(trackChanges:trackChanges);

    if (!logs.Any())
      throw new GenericListNotFoundException("DmsDocumentAccessLog");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsDocumentAccessLog, DmsDocumentAccessLogDDL>(logs);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentAccessLogDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsDocumentAccessLog";  // adjust if needed
    string orderBy = "AccessDateTime desc";

    var gridEntity = await _repository.DmsDocumentAccessLogs.GridData<DmsDocumentAccessLogDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentAccessLogDto modelDto)
  {
    if (modelDto.LogId != 0)
      throw new InvalidCreateOperationException("LogId must be 0 when creating a new log.");

    var log = MyMapper.JsonClone<DmsDocumentAccessLogDto, DmsDocumentAccessLog>(modelDto);

    var createdId = await _repository.DmsDocumentAccessLogs.CreateAndGetIdAsync(log);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New access log created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentAccessLogDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.LogId)
      return "Invalid update attempt: key does not match the LogId.";

    bool exists = await _repository.DmsDocumentAccessLogs.ExistsAsync(x => x.LogId == key);
    if (!exists)
      return "Update failed: log not found.";

    var log = MyMapper.JsonClone<DmsDocumentAccessLogDto, DmsDocumentAccessLog>(modelDto);

    _repository.DmsDocumentAccessLogs.Update(log);
    await _repository.SaveAsync();
    _logger.LogWarn($"Access log with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentAccessLogDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentAccessLogDto));

    if (key != modelDto.LogId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentAccessLogDto));

    var log = await _repository.DmsDocumentAccessLogs.FirstOrDefaultAsync(x => x.LogId == key, false);

    if (log == null)
      throw new GenericNotFoundException("DmsDocumentAccessLog", "LogId", key.ToString());

    await _repository.DmsDocumentAccessLogs.DeleteAsync(x => x.LogId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Access log with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentAccessLogDto modelDto)
  {
    if (modelDto.LogId == 0 && key == 0)
    {
      var newLog = MyMapper.JsonClone<DmsDocumentAccessLogDto, DmsDocumentAccessLog>(modelDto);

      var createdId = await _repository.DmsDocumentAccessLogs.CreateAndGetIdAsync(newLog);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New access log created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.LogId)
    {
      var exists = await _repository.DmsDocumentAccessLogs.ExistsAsync(x => x.LogId == key);
      if (!exists)
      {
        var updateLog = MyMapper.JsonClone<DmsDocumentAccessLogDto, DmsDocumentAccessLog>(modelDto);
        _repository.DmsDocumentAccessLogs.Update(updateLog);
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
