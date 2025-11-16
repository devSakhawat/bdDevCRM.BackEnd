using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.DMS;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Service.DMS;

internal sealed class DmsDocumentTagService : IDmsDocumentTagService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsDocumentTagService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsDocumentTagDDL>> GetTagsDDLAsync(bool trackChanges = false)
  {
    var tags = await _repository.DmsDocumentTags.ListAsync(trackChanges:trackChanges);

    if (!tags.Any())
      throw new GenericListNotFoundException("DmsDocumentTag");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsDocumentTag, DmsDocumentTagDDL>(tags);
    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentTagDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsDocumentTag";
    string orderBy = "Name asc";

    var gridEntity = await _repository.DmsDocumentTags.GridData<DmsDocumentTagDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentTagDto modelDto)
  {
    if (modelDto.TagId != 0)
      throw new InvalidCreateOperationException("TagId must be 0 when creating a new tag.");

    bool isExist = await _repository.DmsDocumentTags.ExistsAsync(x => x.DocumentTagName.Trim().ToLower() == modelDto.Name.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsDocumentTag", "Name");

    var tag = MyMapper.JsonClone<DmsDocumentTagDto, DmsDocumentTag>(modelDto);

    var createdId = await _repository.DmsDocumentTags.CreateAndGetIdAsync(tag);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document tag created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentTagDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.TagId)
      return "Invalid update attempt: key does not match the TagId.";

    bool exists = await _repository.DmsDocumentTags.ExistsAsync(x => x.TagId == key);
    if (!exists)
      return "Update failed: tag not found.";

    var tag = MyMapper.JsonClone<DmsDocumentTagDto, DmsDocumentTag>(modelDto);

    _repository.DmsDocumentTags.Update(tag);
    await _repository.SaveAsync();
    _logger.LogWarn($"Tag with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentTagDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentTagDto));

    if (key != modelDto.TagId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentTagDto));

    var tag = await _repository.DmsDocumentTags.FirstOrDefaultAsync(x => x.TagId == key, false);

    if (tag == null)
      throw new GenericNotFoundException("DmsDocumentTag", "TagId", key.ToString());

    await _repository.DmsDocumentTags.DeleteAsync(x => x.TagId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Tag with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentTagDto modelDto)
  {
    if (modelDto.TagId == 0 && key == 0)
    {
      bool isExist = await _repository.DmsDocumentTags.ExistsAsync(x => x.DocumentTagName.Trim().ToLower() == modelDto.Name.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsDocumentTag", "Name");

      var newTag = MyMapper.JsonClone<DmsDocumentTagDto, DmsDocumentTag>(modelDto);

      var createdId = await _repository.DmsDocumentTags.CreateAndGetIdAsync(newTag);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document tag created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.TagId)
    {
      var exists = await _repository.DmsDocumentTags.ExistsAsync(x => x.TagId == key);
      if (!exists)
      {
        var updateTag = MyMapper.JsonClone<DmsDocumentTagDto, DmsDocumentTag>(modelDto);
        _repository.DmsDocumentTags.Update(updateTag);
        await _repository.SaveAsync();

        _logger.LogWarn($"Tag with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: tag with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and TagId mismatch.";
    }
  }
}