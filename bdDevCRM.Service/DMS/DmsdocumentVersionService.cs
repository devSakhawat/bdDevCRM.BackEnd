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

internal sealed class DmsdocumentVersionService : IDmsdocumentVersionService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsdocumentVersionService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsdocumentVersionDDL>> GetVersionsDDLAsync(bool trackChanges = false)
  {
    var versions = await _repository.DmsdocumentVersions.ListAsync(trackChanges:trackChanges);

    if (!versions.Any())
      throw new GenericListNotFoundException("DmsdocumentVersion");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsdocumentVersion, DmsdocumentVersionDDL>(versions);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsdocumentVersionDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsdocumentVersion";
    string orderBy = "VersionNumber desc";

    var gridEntity = await _repository.DmsdocumentVersions.GridData<DmsdocumentVersionDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsdocumentVersionDto modelDto)
  {
    if (modelDto.VersionId != 0)
      throw new InvalidCreateOperationException("VersionId must be 0 when creating a new document version.");

    var version = MyMapper.JsonClone<DmsdocumentVersionDto, DmsdocumentVersion>(modelDto);

    var createdId = await _repository.DmsdocumentVersions.CreateAndGetIdAsync(version);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document version created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsdocumentVersionDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.VersionId)
      return "Invalid update attempt: key does not match the VersionId.";

    bool exists = await _repository.DmsdocumentVersions.ExistsAsync(x => x.VersionId == key);
    if (!exists)
      return "Update failed: document version not found.";

    var version = MyMapper.JsonClone<DmsdocumentVersionDto, DmsdocumentVersion>(modelDto);

    _repository.DmsdocumentVersions.Update(version);
    await _repository.SaveAsync();
    _logger.LogWarn($"document version with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsdocumentVersionDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsdocumentVersionDto));

    if (key != modelDto.VersionId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsdocumentVersionDto));

    var version = await _repository.DmsdocumentVersions.FirstOrDefaultAsync(x => x.VersionId == key, false);

    if (version == null)
      throw new GenericNotFoundException("DmsdocumentVersion", "VersionId", key.ToString());

    await _repository.DmsdocumentVersions.DeleteAsync(x => x.VersionId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"document version with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsdocumentVersionDto modelDto)
  {
    if (modelDto.VersionId == 0 && key == 0)
    {
      var newVersion = MyMapper.JsonClone<DmsdocumentVersionDto, DmsdocumentVersion>(modelDto);

      var createdId = await _repository.DmsdocumentVersions.CreateAndGetIdAsync(newVersion);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document version created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.VersionId)
    {
      var exists = await _repository.DmsdocumentVersions.ExistsAsync(x => x.VersionId == key);
      if (!exists)
      {
        var updateVersion = MyMapper.JsonClone<DmsdocumentVersionDto, DmsdocumentVersion>(modelDto);
        _repository.DmsdocumentVersions.Update(updateVersion);
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
