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

internal sealed class DmsdocumentTagService : IDmsdocumentTagService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsdocumentTagService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsdocumentTagDDL>> GetTagsDDLAsync(bool trackChanges = false)
  {
    var tags = await _repository.DmsdocumentTags.ListAsync(trackChanges:trackChanges);

    if (!tags.Any())
      throw new GenericListNotFoundException("DmsdocumentTag");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsdocumentTag, DmsdocumentTagDDL>(tags);
    return ddlDtos;
  }

  public async Task<GridEntity<DmsdocumentTagDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsdocumentTag";
    string orderBy = "Name asc";

    var gridEntity = await _repository.DmsdocumentTags.GridData<DmsdocumentTagDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsdocumentTagDto modelDto)
  {
    if (modelDto.TagId != 0)
      throw new InvalidCreateOperationException("TagId must be 0 when creating a new tag.");

    bool isExist = await _repository.DmsdocumentTags.ExistsAsync(x => x.DocumentTagName.Trim().ToLower() == modelDto.Name.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsdocumentTag", "Name");

    var tag = MyMapper.JsonClone<DmsdocumentTagDto, DmsdocumentTag>(modelDto);

    var createdId = await _repository.DmsdocumentTags.CreateAndGetIdAsync(tag);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document tag created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsdocumentTagDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.TagId)
      return "Invalid update attempt: key does not match the TagId.";

    bool exists = await _repository.DmsdocumentTags.ExistsAsync(x => x.TagId == key);
    if (!exists)
      return "Update failed: tag not found.";

    var tag = MyMapper.JsonClone<DmsdocumentTagDto, DmsdocumentTag>(modelDto);

    _repository.DmsdocumentTags.Update(tag);
    await _repository.SaveAsync();
    _logger.LogWarn($"Tag with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsdocumentTagDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsdocumentTagDto));

    if (key != modelDto.TagId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsdocumentTagDto));

    var tag = await _repository.DmsdocumentTags.FirstOrDefaultAsync(x => x.TagId == key, false);

    if (tag == null)
      throw new GenericNotFoundException("DmsdocumentTag", "TagId", key.ToString());

    await _repository.DmsdocumentTags.DeleteAsync(x => x.TagId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Tag with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsdocumentTagDto modelDto)
  {
    if (modelDto.TagId == 0 && key == 0)
    {
      bool isExist = await _repository.DmsdocumentTags.ExistsAsync(x => x.DocumentTagName.Trim().ToLower() == modelDto.Name.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsdocumentTag", "Name");

      var newTag = MyMapper.JsonClone<DmsdocumentTagDto, DmsdocumentTag>(modelDto);

      var createdId = await _repository.DmsdocumentTags.CreateAndGetIdAsync(newTag);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document tag created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.TagId)
    {
      var exists = await _repository.DmsdocumentTags.ExistsAsync(x => x.TagId == key);
      if (!exists)
      {
        var updateTag = MyMapper.JsonClone<DmsdocumentTagDto, DmsdocumentTag>(modelDto);
        _repository.DmsdocumentTags.Update(updateTag);
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