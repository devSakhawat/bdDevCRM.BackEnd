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

internal sealed class DmsDocumentVersionService : IDmsDocumentVersionService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsDocumentVersionService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsDocumentVersionDDL>> GetVersionsDDLAsync(bool trackChanges = false)
  {
    var versions = await _repository.DmsDocumentVersions.ListAsync(trackChanges:trackChanges);

    if (!versions.Any())
      throw new GenericListNotFoundException("DmsDocumentVersion");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsDocumentVersion, DmsDocumentVersionDDL>(versions);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentVersionDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsDocumentVersion";
    string orderBy = "VersionNumber desc";

    var gridEntity = await _repository.DmsDocumentVersions.GridData<DmsDocumentVersionDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentVersionDto modelDto)
  {
    if (modelDto.VersionId != 0)
      throw new InvalidCreateOperationException("VersionId must be 0 when creating a new document version.");

    var version = MyMapper.JsonClone<DmsDocumentVersionDto, DmsDocumentVersion>(modelDto);

    var createdId = await _repository.DmsDocumentVersions.CreateAndGetIdAsync(version);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document version created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentVersionDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.VersionId)
      return "Invalid update attempt: key does not match the VersionId.";

    bool exists = await _repository.DmsDocumentVersions.ExistsAsync(x => x.VersionId == key);
    if (!exists)
      return "Update failed: document version not found.";

    var version = MyMapper.JsonClone<DmsDocumentVersionDto, DmsDocumentVersion>(modelDto);

    _repository.DmsDocumentVersions.Update(version);
    await _repository.SaveAsync();
    _logger.LogWarn($"document version with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentVersionDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentVersionDto));

    if (key != modelDto.VersionId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentVersionDto));

    var version = await _repository.DmsDocumentVersions.FirstOrDefaultAsync(x => x.VersionId == key, false);

    if (version == null)
      throw new GenericNotFoundException("DmsDocumentVersion", "VersionId", key.ToString());

    await _repository.DmsDocumentVersions.DeleteAsync(x => x.VersionId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"document version with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentVersionDto modelDto)
  {
    if (modelDto.VersionId == 0 && key == 0)
    {
      var newVersion = MyMapper.JsonClone<DmsDocumentVersionDto, DmsDocumentVersion>(modelDto);

      var createdId = await _repository.DmsDocumentVersions.CreateAndGetIdAsync(newVersion);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document version created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.VersionId)
    {
      var exists = await _repository.DmsDocumentVersions.ExistsAsync(x => x.VersionId == key);
      if (!exists)
      {
        var updateVersion = MyMapper.JsonClone<DmsDocumentVersionDto, DmsDocumentVersion>(modelDto);
        _repository.DmsDocumentVersions.Update(updateVersion);
        await _repository.SaveAsync();

        _logger.LogWarn($"document version with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: document version with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and VersionId mismatch.";
    }
  }
}
