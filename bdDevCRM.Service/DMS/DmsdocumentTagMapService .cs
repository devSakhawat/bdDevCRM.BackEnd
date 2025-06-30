using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Service.DMS;

internal sealed class DmsdocumentTagMapService : IDmsdocumentTagMapService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsdocumentTagMapService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  //public async Task<IEnumerable<DmsdocumentTagMapDDL>> GetTagMapsDDLAsync(bool trackChanges = false)
  //{
  //  var tagMaps = await _repository.DmsdocumentTagMaps.GetAllAsync(trackChanges);

  //  if (!tagMaps.Any())
  //    throw new GenericListNotFoundException("DmsdocumentTagMap");

  //  var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsdocumentTagMap, DmsdocumentTagMapDDL>(tagMaps);

  //  return ddlDtos;
  //}

  //public async Task<GridEntity<DmsdocumentTagMapDto>> SummaryGrid(CRMGridOptions options)
  //{
  //  string query = "SELECT * FROM DmsdocumentTagMap";
  //  string orderBy = "Id asc";

  //  var gridEntity = await _repository.DmsdocumentTagMaps.GridData<DmsdocumentTagMapDto>(query, options, orderBy, "");

  //  return gridEntity;
  //}

  //public async Task<string> CreateNewRecordAsync(DmsdocumentTagMapDto modelDto)
  //{
  //  if (modelDto.Id != 0)
  //    throw new InvalidCreateOperationException("Id must be 0 when creating a new tag map.");

  //  // Optional: check for duplicates, e.g. same documentId & TagId combo
  //  bool isExist = await _repository.DmsdocumentTagMaps.ExistsAsync(x =>
  //      x.documentId == modelDto.documentId && x.TagId == modelDto.TagId);
  //  if (isExist) throw new DuplicateRecordException("DmsdocumentTagMap", "documentId+TagId");

  //  var tagMap = MyMapper.JsonClone<DmsdocumentTagMapDto, DmsdocumentTagMap>(modelDto);

  //  var createdId = await _repository.DmsdocumentTagMaps.CreateAndGetIdAsync(tagMap);
  //  if (createdId == 0)
  //    throw new InvalidCreateOperationException();

  //  await _repository.SaveAsync();
  //  _logger.LogWarn($"New document tag map created with Id: {createdId}");

  //  return OperationMessage.Success;
  //}

  //public async Task<string> UpdateNewRecordAsync(int key, DmsdocumentTagMapDto modelDto, bool trackChanges)
  //{
  //  if (key <= 0 || key != modelDto.Id)
  //    return "Invalid update attempt: key does not match the Id.";

  //  bool exists = await _repository.DmsdocumentTagMaps.ExistsAsync(x => x.Id == key);
  //  if (!exists)
  //    return "Update failed: document tag map not found.";

  //  var tagMap = MyMapper.JsonClone<DmsdocumentTagMapDto, DmsdocumentTagMap>(modelDto);

  //  _repository.DmsdocumentTagMaps.Update(tagMap);
  //  await _repository.SaveAsync();

  //  _logger.LogWarn($"document tag map with Id: {key} updated.");

  //  return OperationMessage.Success;
  //}

  //public async Task<string> DeleteRecordAsync(int key, DmsdocumentTagMapDto modelDto)
  //{
  //  if (modelDto == null)
  //    throw new NullModelBadRequestException(nameof(DmsdocumentTagMapDto));

  //  if (key != modelDto.Id)
  //    throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsdocumentTagMapDto));

  //  var tagMap = await _repository.DmsdocumentTagMaps.FirstOrDefaultAsync(x => x.Id == key, false);

  //  if (tagMap == null)
  //    throw new GenericNotFoundException("DmsdocumentTagMap", "Id", key.ToString());

  //  await _repository.DmsdocumentTagMaps.DeleteAsync(x => x.Id == key, true);
  //  await _repository.SaveAsync();

  //  _logger.LogWarn($"document tag map with Id: {key} deleted.");

  //  return OperationMessage.Success;
  //}

  //public async Task<string> SaveOrUpdate(int key, DmsdocumentTagMapDto modelDto)
  //{
  //  if (modelDto.Id == 0 && key == 0)
  //  {
  //    bool isExist = await _repository.DmsdocumentTagMaps.ExistsAsync(x =>
  //        x.documentId == modelDto.documentId && x.TagId == modelDto.TagId);
  //    if (isExist) throw new DuplicateRecordException("DmsdocumentTagMap", "documentId+TagId");

  //    var newTagMap = MyMapper.JsonClone<DmsdocumentTagMapDto, DmsdocumentTagMap>(modelDto);

  //    var createdId = await _repository.DmsdocumentTagMaps.CreateAndGetIdAsync(newTagMap);
  //    if (createdId == 0)
  //      throw new InvalidCreateOperationException();

  //    await _repository.SaveAsync();
  //    _logger.LogWarn($"New document tag map created with Id: {createdId}");
  //    return OperationMessage.Success;
  //  }
  //  else if (key > 0 && key == modelDto.Id)
  //  {
  //    bool exists = await _repository.DmsdocumentTagMaps.ExistsAsync(x => x.Id == key);
  //    if (!exists)
  //    {
  //      var updateTagMap = MyMapper.JsonClone<DmsdocumentTagMapDto, DmsdocumentTagMap>(modelDto);
  //      _repository.DmsdocumentTagMaps.Update(updateTagMap);
  //      await _repository.SaveAsync();

  //      _logger.LogWarn($"document tag map with Id: {key} updated.");
  //      return OperationMessage.Success;
  //    }
  //    else
  //    {
  //      return "Update failed: document tag map with this Id already exists.";
  //    }
  //  }
  //  else
  //  {
  //    return "Invalid key and Id mismatch.";
  //  }
  //}
}