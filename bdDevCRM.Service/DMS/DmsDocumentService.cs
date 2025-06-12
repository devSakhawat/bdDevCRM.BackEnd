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

internal sealed class DmsDocumentService : IDmsDocumentService
{
  private readonly IRepositoryManager _repo;
  private readonly ILoggerManager _log;
  private readonly IConfiguration _cfg;

  private static readonly string[] _imageExt = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];

  public DmsDocumentService(IRepositoryManager repo, ILoggerManager log, IConfiguration cfg)
  {
    _repo = repo;
    _log = log;
    _cfg = cfg;
  }

  // -------------- DDL ----------------------------------------------
  public async Task<IEnumerable<KeyValuePair<int, string>>> GetDocumentDDLAsync()
  {
    var docs = await _repo.DmsDocuments.GetActiveAsync(false);
    if (!docs.Any()) throw new GenericListNotFoundException("DmsDocument");
    return docs.Select(d => new KeyValuePair<int, string>(d.DocumentId, d.Title));
  }

  // -------------- Grid ---------------------------------------------
  public async Task<GridEntity<DmsDocumentDto>> SummaryGrid(CRMGridOptions opt)
  {
    string sql = "SELECT * FROM DmsDocument";   // 👉 এখানে আপনার নিজস্ব ভিউ/স্টোর্ড-প্রসিজিউর ব্যবহার করতে পারেন
    string orderBy = " UploadDate DESC ";
    return await _repo.DmsDocuments.GridData<DmsDocumentDto>(sql, opt, orderBy, "");
  }

  // -------------- Create -------------------------------------------
  public async Task<string> CreateAsync(DmsDocumentDto dto)
  {
    if (dto.DocumentId != 0) throw new InvalidCreateOperationException("DocumentId must be 0.");

    await ValidateFileRules(dto.FileExtension, dto.FileSize);

    bool dup = await _repo.DmsDocuments.ExistsAsync(d =>
        d.Title == dto.Title && d.ReferenceEntityId == dto.ReferenceEntityId);
    if (dup) throw new DuplicateRecordException("DmsDocument", "Title");

    var entity = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(dto);

    // 👉 ফাইল সেভ করলে path সেট করুন (StorageService ইত্যাদি)
    // entity.FilePath = _fileStore.Save(fileStream, dto.FileName);

    int id = await _repo.DmsDocuments.CreateAndGetIdAsync(entity);
    if (id <= 0) throw new InvalidCreateOperationException();

    _log.LogInfo($"DMS Document created, id={id}");
    return OperationMessage.Success;
  }

  // -------------- Update -------------------------------------------
  public async Task<string> UpdateAsync(int key, DmsDocumentDto dto)
  {
    if (key != dto.DocumentId) throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentDto));

    await ValidateFileRules(dto.FileExtension, dto.FileSize);

    bool exists = await _repo.DmsDocuments.ExistsAsync(x => x.DocumentId == key);
    if (!exists) throw new GenericNotFoundException("DmsDocument", "DocumentId", key.ToString());

    var entity = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(dto);
    _repo.DmsDocuments.Update(entity);
    await _repo.SaveAsync();

    _log.LogInfo($"DMS Document updated, id={key}");
    return OperationMessage.Success;
  }

  // -------------- Delete -------------------------------------------
  public async Task<string> DeleteAsync(int key)
  {
    await _repo.DmsDocuments.DeleteAsync(x => x.DocumentId == key, true);
    await _repo.SaveAsync();

    _log.LogInfo($"DMS Document deleted, id={key}");
    return OperationMessage.Success;
  }

  // ---------------------------------------------------------
  private static Task ValidateFileRules(string ext, long sizeInBytes)
  {
    bool isImage = _imageExt.Contains(ext.ToLower());
    long max = isImage ? 1 * 1024 * 1024 : 5 * 1024 * 1024;

    if (sizeInBytes > max)
      throw new FileSizeExceededException($"File size exceeds limit ({max / 1024 / 1024} MB).");

    return Task.CompletedTask;
  }
}

// If the FileSizeExceededException class is not defined in the bdDevCRM.Entities.Exceptions namespace,
// you need to define it. Below is the definition based on the provided type signature.



