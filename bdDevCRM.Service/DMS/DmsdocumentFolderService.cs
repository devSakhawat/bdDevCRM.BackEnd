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


internal sealed class DmsDocumentFolderService : IDmsDocumentFolderService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsDocumentFolderService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsDocumentFolderDDL>> GetFoldersDDLAsync(bool trackChanges = false)
  {
    var folders = await _repository.DmsDocumentFolders.ListAsync(trackChanges:trackChanges);

    if (!folders.Any())
      throw new GenericListNotFoundException("DmsDocumentFolder");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsDocumentFolder, DmsDocumentFolderDDL>(folders);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentFolderDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsDocumentFolder";
    string orderBy = "FolderName asc";

    var gridEntity = await _repository.DmsDocumentFolders.GridData<DmsDocumentFolderDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentFolderDto modelDto)
  {
    if (modelDto.FolderId != 0)
      throw new InvalidCreateOperationException("FolderId must be 0 when creating a new folder.");

    bool isExist = await _repository.DmsDocumentFolders.ExistsAsync(x => x.FolderName.Trim().ToLower() == modelDto.FolderName.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsDocumentFolder", "FolderName");

    var folder = MyMapper.JsonClone<DmsDocumentFolderDto, DmsDocumentFolder>(modelDto);

    var createdId = await _repository.DmsDocumentFolders.CreateAndGetIdAsync(folder);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document folder created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentFolderDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.FolderId)
      return "Invalid update attempt: key does not match the FolderId.";

    bool exists = await _repository.DmsDocumentFolders.ExistsAsync(x => x.FolderId == key);
    if (!exists)
      return "Update failed: folder not found.";

    var folder = MyMapper.JsonClone<DmsDocumentFolderDto, DmsDocumentFolder>(modelDto);

    _repository.DmsDocumentFolders.Update(folder);
    await _repository.SaveAsync();
    _logger.LogWarn($"Folder with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentFolderDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentFolderDto));

    if (key != modelDto.FolderId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentFolderDto));

    var folder = await _repository.DmsDocumentFolders.FirstOrDefaultAsync(x => x.FolderId == key, false);

    if (folder == null)
      throw new GenericNotFoundException("DmsDocumentFolder", "FolderId", key.ToString());

    await _repository.DmsDocumentFolders.DeleteAsync(x => x.FolderId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Folder with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentFolderDto modelDto)
  {
    if (modelDto.FolderId == 0 && key == 0)
    {
      bool isExist = await _repository.DmsDocumentFolders.ExistsAsync(x => x.FolderName.Trim().ToLower() == modelDto.FolderName.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsDocumentFolder", "FolderName");

      var newFolder = MyMapper.JsonClone<DmsDocumentFolderDto, DmsDocumentFolder>(modelDto);

      var createdId = await _repository.DmsDocumentFolders.CreateAndGetIdAsync(newFolder);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document folder created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.FolderId)
    {
      var exists = await _repository.DmsDocumentFolders.ExistsAsync(x => x.FolderId == key);
      if (!exists)
      {
        var updateFolder = MyMapper.JsonClone<DmsDocumentFolderDto, DmsDocumentFolder>(modelDto);
        _repository.DmsDocumentFolders.Update(updateFolder);
        await _repository.SaveAsync();

        _logger.LogWarn($"Folder with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: folder with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and FolderId mismatch.";
    }
  }
}
