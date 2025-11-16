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

internal sealed class DmsDocumentTypeService : IDmsDocumentTypeService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsDocumentTypeService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsDocumentTypeDDL>> GetTypesDDLAsync(bool trackChanges = false)
  {
    var types = await _repository.DmsDocumentTypes.ListAsync(trackChanges:trackChanges);

    if (!types.Any())
      throw new GenericListNotFoundException("DmsDocumentType");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsDocumentType, DmsDocumentTypeDDL>(types);
    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentTypeDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsDocumentType";
    string orderBy = "Name asc";

    var gridEntity = await _repository.DmsDocumentTypes.GridData<DmsDocumentTypeDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentTypeDto modelDto)
  {
    if (modelDto.DocumentTypeId != 0)
      throw new InvalidCreateOperationException("DocumentTypeId must be 0 when creating a new document type.");

    bool isExist = await _repository.DmsDocumentTypes.ExistsAsync(x => x.Name.Trim().ToLower() == modelDto.Name.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsDocumentType", "Name");

    var type = MyMapper.JsonClone<DmsDocumentTypeDto, DmsDocumentType>(modelDto);

    var createdId = await _repository.DmsDocumentTypes.CreateAndGetIdAsync(type);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document type created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentTypeDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.DocumentTypeId)
      return "Invalid update attempt: key does not match the DocumentTypeId.";

    bool exists = await _repository.DmsDocumentTypes.ExistsAsync(x => x.DocumentTypeId == key);
    if (!exists)
      return "Update failed: document type not found.";

    var type = MyMapper.JsonClone<DmsDocumentTypeDto, DmsDocumentType>(modelDto);

    _repository.DmsDocumentTypes.Update(type);
    await _repository.SaveAsync();
    _logger.LogWarn($"document type with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentTypeDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentTypeDto));

    if (key != modelDto.DocumentTypeId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentTypeDto));

    var type = await _repository.DmsDocumentTypes.FirstOrDefaultAsync(x => x.DocumentTypeId == key, false);

    if (type == null)
      throw new GenericNotFoundException("DmsDocumentType", "DocumentTypeId", key.ToString());

    await _repository.DmsDocumentTypes.DeleteAsync(x => x.DocumentTypeId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"document type with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentTypeDto modelDto)
  {
    if (modelDto.DocumentTypeId == 0 && key == 0)
    {
      bool isExist = await _repository.DmsDocumentTypes.ExistsAsync(x => x.Name.Trim().ToLower() == modelDto.Name.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsDocumentType", "Name");

      var newType = MyMapper.JsonClone<DmsDocumentTypeDto, DmsDocumentType>(modelDto);

      var createdId = await _repository.DmsDocumentTypes.CreateAndGetIdAsync(newType);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document type created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.DocumentTypeId)
    {
      var exists = await _repository.DmsDocumentTypes.ExistsAsync(x => x.DocumentTypeId == key);
      if (!exists)
      {
        var updateType = MyMapper.JsonClone<DmsDocumentTypeDto, DmsDocumentType>(modelDto);
        _repository.DmsDocumentTypes.Update(updateType);
        await _repository.SaveAsync();

        _logger.LogWarn($"document type with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: document type with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and DocumentTypeId mismatch.";
    }
  }
}