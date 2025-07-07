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


internal sealed class DmsdocumentFolderService : IDmsdocumentFolderService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DmsdocumentFolderService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<DmsdocumentFolderDDL>> GetFoldersDDLAsync(bool trackChanges = false)
  {
    var folders = await _repository.DmsdocumentFolders.ListAsync(trackChanges:trackChanges);

    if (!folders.Any())
      throw new GenericListNotFoundException("DmsdocumentFolder");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<DmsdocumentFolder, DmsdocumentFolderDDL>(folders);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsdocumentFolderDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM DmsdocumentFolder";
    string orderBy = "FolderName asc";

    var gridEntity = await _repository.DmsdocumentFolders.GridData<DmsdocumentFolderDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsdocumentFolderDto modelDto)
  {
    if (modelDto.FolderId != 0)
      throw new InvalidCreateOperationException("FolderId must be 0 when creating a new folder.");

    bool isExist = await _repository.DmsdocumentFolders.ExistsAsync(x => x.FolderName.Trim().ToLower() == modelDto.FolderName.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsdocumentFolder", "FolderName");

    var folder = MyMapper.JsonClone<DmsdocumentFolderDto, DmsdocumentFolder>(modelDto);

    var createdId = await _repository.DmsdocumentFolders.CreateAndGetIdAsync(folder);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document folder created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsdocumentFolderDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.FolderId)
      return "Invalid update attempt: key does not match the FolderId.";

    bool exists = await _repository.DmsdocumentFolders.ExistsAsync(x => x.FolderId == key);
    if (!exists)
      return "Update failed: folder not found.";

    var folder = MyMapper.JsonClone<DmsdocumentFolderDto, DmsdocumentFolder>(modelDto);

    _repository.DmsdocumentFolders.Update(folder);
    await _repository.SaveAsync();
    _logger.LogWarn($"Folder with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsdocumentFolderDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsdocumentFolderDto));

    if (key != modelDto.FolderId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsdocumentFolderDto));

    var folder = await _repository.DmsdocumentFolders.FirstOrDefaultAsync(x => x.FolderId == key, false);

    if (folder == null)
      throw new GenericNotFoundException("DmsdocumentFolder", "FolderId", key.ToString());

    await _repository.DmsdocumentFolders.DeleteAsync(x => x.FolderId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Folder with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsdocumentFolderDto modelDto)
  {
    if (modelDto.FolderId == 0 && key == 0)
    {
      bool isExist = await _repository.DmsdocumentFolders.ExistsAsync(x => x.FolderName.Trim().ToLower() == modelDto.FolderName.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsdocumentFolder", "FolderName");

      var newFolder = MyMapper.JsonClone<DmsdocumentFolderDto, DmsdocumentFolder>(modelDto);

      var createdId = await _repository.DmsdocumentFolders.CreateAndGetIdAsync(newFolder);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document folder created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.FolderId)
    {
      var exists = await _repository.DmsdocumentFolders.ExistsAsync(x => x.FolderId == key);
      if (!exists)
      {
        var updateFolder = MyMapper.JsonClone<DmsdocumentFolderDto, DmsdocumentFolder>(modelDto);
        _repository.DmsdocumentFolders.Update(updateFolder);
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
