using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.DMS;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.DMS;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace bdDevCRM.Service.DMS;


internal sealed class DmsdocumentService : IDmsdocumentService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DmsdocumentService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<DmsdocumentDDL>> GetDocumentsDDLAsync(bool trackChanges = false)
  {
    var documents = await _repository.Dmsdocuments.ListAsync(trackChanges: trackChanges);

    if (!documents.Any())
      throw new GenericListNotFoundException("DmsDocument");

    var ddlDtos = MyMapper.JsonCloneIEnumerableToList<Dmsdocument, DmsdocumentDDL>(documents);

    return ddlDtos;
  }

  public async Task<GridEntity<DmsDocumentDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "SELECT * FROM Dmsdocument";  // Adjust if needed
    string orderBy = "Title asc";

    var gridEntity = await _repository.Dmsdocuments.GridData<DmsDocumentDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(DmsDocumentDto modelDto)
  {
    if (modelDto.DocumentId != 0)
      throw new InvalidCreateOperationException("DocumentId must be 0 when creating a new document.");

    bool isExist = await _repository.Dmsdocuments.ExistsAsync(x => x.Title.Trim().ToLower() == modelDto.Title.Trim().ToLower());
    if (isExist) throw new DuplicateRecordException("DmsDocument", "Title");

    var document = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(modelDto);

    var createdId = await _repository.Dmsdocuments.CreateAndGetIdAsync(document);
    if (createdId == 0)
      throw new InvalidCreateOperationException();

    await _repository.SaveAsync();
    _logger.LogWarn($"New document created with Id: {createdId}");

    return OperationMessage.Success;
  }

  public async Task<string> UpdateNewRecordAsync(int key, DmsDocumentDto modelDto, bool trackChanges)
  {
    if (key <= 0 || key != modelDto.DocumentId)
      return "Invalid update attempt: key does not match the DocumentId.";

    bool exists = await _repository.Dmsdocuments.ExistsAsync(x => x.DocumentId == key);
    if (!exists)
      return "Update failed: document not found.";

    var document = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(modelDto);

    _repository.Dmsdocuments.Update(document);
    await _repository.SaveAsync();

    _logger.LogWarn($"Document with Id: {key} updated.");

    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, DmsDocumentDto modelDto)
  {
    if (modelDto == null)
      throw new NullModelBadRequestException(nameof(DmsDocumentDto));

    if (key != modelDto.DocumentId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(DmsDocumentDto));

    var document = await _repository.Dmsdocuments.FirstOrDefaultAsync(x => x.DocumentId == key, false);

    if (document == null)
      throw new GenericNotFoundException("DmsDocument", "DocumentId", key.ToString());

    await _repository.Dmsdocuments.DeleteAsync(x => x.DocumentId == key, true);
    await _repository.SaveAsync();

    _logger.LogWarn($"Document with Id: {key} deleted.");

    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, DmsDocumentDto modelDto)
  {
    if (modelDto.DocumentId == 0 && key == 0)
    {
      bool isExist = await _repository.Dmsdocuments.ExistsAsync(x => x.Title.Trim().ToLower() == modelDto.Title.Trim().ToLower());
      if (isExist) throw new DuplicateRecordException("DmsDocument", "Title");

      var newDoc = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(modelDto);

      var createdId = await _repository.Dmsdocuments.CreateAndGetIdAsync(newDoc);
      if (createdId == 0)
        throw new InvalidCreateOperationException();

      await _repository.SaveAsync();
      _logger.LogWarn($"New document created with Id: {createdId}");
      return OperationMessage.Success;
    }
    else if (key > 0 && key == modelDto.DocumentId)
    {
      var exists = await _repository.Dmsdocuments.ExistsAsync(x => x.DocumentId == key);
      if (!exists)
      {
        var updateDoc = MyMapper.JsonClone<DmsDocumentDto, Dmsdocument>(modelDto);
        _repository.Dmsdocuments.Update(updateDoc);
        await _repository.SaveAsync();

        _logger.LogWarn($"Document with Id: {key} updated.");
        return OperationMessage.Success;
      }
      else
      {
        return "Update failed: document with this Id already exists.";
      }
    }
    else
    {
      return "Invalid key and DocumentId mismatch.";
    }
  }

  public async Task<string> SaveFileAndDocumentWithAllDmsAsync(IFormFile file, string allAboutDMS)
  {
    if (file == null || file.Length == 0) return null;

    var dmsDto = JsonConvert.DeserializeObject<DMSDto>(allAboutDMS);
    if (dmsDto == null)
      throw new ArgumentException("DMS data are not deserialize");

    // Check Validation
    ValidateDMSData(dmsDto, file);
    using var transaction = _repository.Dmsdocuments.TransactionBeginAsync();
    try
    {

      // 1. DocumentType Check and Create
      var documentType = await CreateOrGetDocumentType(dmsDto);

      // 2. Folder Structure Create
      var folder = await CreateFolderStructure(dmsDto);


      // 3. File Save
      var fileInfo = await SaveFileToSystem(file, dmsDto);

      // 4. Document Create
      var document = await CreateDocument(dmsDto, documentType, folder, fileInfo);

      // 5. Document Version Create
      var version = await CreateDocumentVersion(document, fileInfo, dmsDto);

      // 6. Tag Mapping Create
      await CreateTagMapping(document.DocumentId, dmsDto);

      // 7. access log create
      await CreateAccessLog(document.DocumentId, dmsDto, "Upload");

      //await _repository.SaveAsync();
      await _repository.Dmsdocuments.TransactionCommitAsync();

      _logger.LogInfo($"DMS document created successfylly - DocumentId: {document.DocumentId}");

      return document.FilePath; // Return the file path or any other relevant information
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error in DMS save data. Error message: {ex.Message}");
      await _repository.Dmsdocuments.TransactionRollbackAsync();
      await _repository.Dmsdocuments.TransactionDisposeAsync();
      throw;
    }
    finally
    {
      await _repository.Dmsdocuments.TransactionDisposeAsync();
    }
  }

  #region Helper Methods

  // Validation Method
  private void ValidateDMSData(DMSDto dmsDto, IFormFile file)
  {
    // Check File size.
    var maxFileSize = dmsDto.MaxFileSizeMb ?? 10;
    if (file.Length > maxFileSize * 1024 * 1024)
      throw new ArgumentException($"File size cannot exceed {maxFileSize} MB.");

    // check file extension
    var fileExtension = Path.GetExtension(file.FileName).ToLower();
    var acceptedExtensions = dmsDto.AcceptedExtensions?.Split(',').Select(x => x.Trim().ToLower()).ToList()
                           ?? new List<string> { ".pdf", ".jpg", ".png", ".docx" };

    if (!acceptedExtensions.Contains(fileExtension))
      throw new ArgumentException($"File type '{fileExtension}' is not allowed. Accepted types are: {string.Join(", ", acceptedExtensions)}.");

    // Required fields check
    if (string.IsNullOrWhiteSpace(dmsDto.ReferenceEntityType))
      throw new ArgumentException("Reference entity type is required.");

    if (string.IsNullOrWhiteSpace(dmsDto.ReferenceEntityId))
      throw new ArgumentException("Reference entity ID is required.");
  }


  // 01. create document version with duplicate check
  private async Task<DmsdocumentType> CreateOrGetDocumentType(DMSDto dmsDto)
  {
    var documentType = await _repository.DmsdocumentTypes.FirstOrDefaultAsync(dt => dt.Name.ToLower().Trim() == dmsDto.DocumentType.ToLower().Trim()
                                && dt.DocumentType.ToLower().Trim() == dmsDto.ReferenceEntityType.ToLower().Trim());

    if (documentType == null)
    {
      documentType = new DmsdocumentType
      {
        Name = dmsDto.DocumentTypeName ?? "Default Document Type",
        DocumentType = dmsDto.DocumentType,
        IsMandatory = dmsDto.IsMandatory,
        AcceptedExtensions = dmsDto.AcceptedExtensions ?? ".pdf, .docx, .jpg, .png, .jpeg",
        MaxFileSizeMb = dmsDto.MaxFileSizeMb ?? ((dmsDto.AcceptedExtensions != null
        && (dmsDto.AcceptedExtensions.Contains(".jpg")
        || dmsDto.AcceptedExtensions.Contains(".png")
        || dmsDto.AcceptedExtensions.Contains(".jpeg"))) ? 1 : 5)
        //MaxFileSizeMb = (!dmsDto.AcceptedExtensions.Contains(".pdf")) ? 1 : dmsDto.MaxFileSizeMb ?? 10
      };

      documentType.DocumentTypeId = await _repository.DmsdocumentTypes.CreateAndGetIdAsync(documentType);
      //await _repository.SaveAsync();
    }

    return documentType;
  }

  //02.
  /// <summary>
  /// Create folder structure based on ReferenceEntityType and ReferenceEntityId.
  /// </summary>
  /// <param name="dmsDto">  </param>
  /// <returns>  </returns>
  private async Task<DmsdocumentFolder> CreateFolderStructure(DMSDto dmsDto)
  {
    // Check Parent (For Entity Type)
    var parentFolder = await _repository.DmsdocumentFolders
        .FirstOrDefaultAsync(f => f.FolderName.ToLower().Trim() == dmsDto.ReferenceEntityType.ToLower().Trim() && f.ParentFolderId == null);

    if (parentFolder == null)
    {
      parentFolder = new DmsdocumentFolder
      {
        FolderName = dmsDto.ReferenceEntityType,
        ParentFolderId = null,
        OwnerId = null,
        ReferenceEntityType = dmsDto.ReferenceEntityType,
        ReferenceEntityId = null
      };

      parentFolder.FolderId = await _repository.DmsdocumentFolders.CreateAndGetIdAsync(parentFolder);
    }

    var entityFolder = await _repository.DmsdocumentFolders
        .FirstOrDefaultAsync(f => f.ParentFolderId == parentFolder.FolderId
                                && f.ReferenceEntityId == dmsDto.ReferenceEntityId);

    if (entityFolder == null)
    {
      entityFolder = new DmsdocumentFolder
      {
        FolderName = $"{dmsDto.ReferenceEntityType}_{dmsDto.ReferenceEntityId}",
        ParentFolderId = parentFolder.FolderId,
        OwnerId = null, // may be letter. now entity recordId Or dmsDto.OwnerId.ToString(),
        ReferenceEntityType = dmsDto.ReferenceEntityType,
        ReferenceEntityId = dmsDto.ReferenceEntityId
      };

      entityFolder.FolderId = await _repository.DmsdocumentFolders.CreateAndGetIdAsync(entityFolder);
    }

    return entityFolder;
  }

  // Save file to the system
  private async Task<FileInfoDto> SaveFileToSystem(IFormFile file, DMSDto dmsDto)
  {
    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    string folderPath = Path.Combine(rootPath, "Uploads", dmsDto.ReferenceEntityType.Trim(), dmsDto.ReferenceEntityId, dmsDto.DocumentType.Trim());

    if (!Directory.Exists(folderPath))
      Directory.CreateDirectory(folderPath);

    // unique file name generation
    //string fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N")[..8]}{Path.GetExtension(file.FileName)}";
    string fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(file.FileName)}";
    string fullPath = Path.Combine(folderPath, fileName);
    string relativePath = $"/Uploads/{dmsDto.ReferenceEntityType}/{dmsDto.ReferenceEntityId}/{dmsDto.DocumentType}/{fileName}";

    using (var stream = new FileStream(fullPath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    return new FileInfoDto
    {
      FileName = fileName,
      FullPath = fullPath,
      RelativePath = relativePath,
      FileSize = file.Length,
      FileExtension = Path.GetExtension(file.FileName)
    };
  }

  // generate document with all information
  private async Task<Dmsdocument> CreateDocument(DMSDto dmsDto, DmsdocumentType documentType, DmsdocumentFolder folder, FileInfoDto fileInfo)
  {
    var document = await _repository.Dmsdocuments.FirstOrDefaultAsync(d =>
        d.ReferenceEntityId == dmsDto.ReferenceEntityId &&
        d.FolderId == folder.FolderId &&
        d.FilePath == fileInfo.RelativePath &&
        d.SystemTag.ToLower().Trim() == dmsDto.SystemTags.ToLower().Trim()
        );

    if (document != null)
    {
      document = new Dmsdocument
      {
        Title = dmsDto.Title ?? Path.GetFileNameWithoutExtension(fileInfo.FileName),
        Description = dmsDto.Description,
        FileName = fileInfo.FileName,
        FileExtension = fileInfo.FileExtension,
        FileSize = fileInfo.FileSize,
        FilePath = fileInfo.RelativePath,
        UploadDate = DateTime.Now,
        UploadedByUserId = dmsDto.UploadedByUserId?.ToString(),
        DocumentTypeId = documentType.DocumentTypeId,
        ReferenceEntityType = dmsDto.ReferenceEntityType,
        ReferenceEntityId = dmsDto.ReferenceEntityId,
        FolderId = folder.FolderId,
        SystemTag = dmsDto.SystemTags ?? string.Empty
      };
    }
    document.DocumentId = await _repository.Dmsdocuments.CreateAndGetIdAsync(document);
    if (document.DocumentId == 0)
      throw new InvalidCreateOperationException("Failed to create document.");

    //await _repository.SaveAsync();
    return document;
  }

  // generate document version
  private async Task<DmsdocumentVersion> CreateDocumentVersion(Dmsdocument document, FileInfoDto fileInfo, DMSDto dmsDto)
  {
    var latestVersion = await _repository.DmsdocumentVersions.FirstOrDefaultWithOrderByDescAsync(expression: x => x.DocumentId == document.DocumentId, orderBy: x => x.VersionNumber == dmsDto.VersionNumber);

    var versionNumber = (latestVersion?.VersionNumber ?? 0) + 1;

    var version = new DmsdocumentVersion
    {
      DocumentId = document.DocumentId,
      VersionNumber = versionNumber,
      FileName = fileInfo.FileName,
      FilePath = fileInfo.RelativePath,
      UploadedDate = DateTime.UtcNow,
      UploadedBy = dmsDto.UploadedByUserId?.ToString(),
      IsCurrentVersion = true,
      VersionNotes = dmsDto.VersionNotes,
      PreviousVersionId = versionNumber,
      FileSize = fileInfo.FileSize,
    };

    version.VersionId = await _repository.DmsdocumentVersions.CreateAndGetIdAsync(version);
    await _repository.SaveAsync();

    return version;
  }

  // letter for robust dms project.
  // Create tag mapping for the document
  private async Task CreateTagMapping(int documentId, DMSDto dmsDto)
  {
    if (string.IsNullOrWhiteSpace(dmsDto.DocumentTagName)) return;

    var tagNames = dmsDto.DocumentTagName.Split(',')
        .Select(t => t.Trim())
        .Where(t => !string.IsNullOrEmpty(t))
        .Distinct();

    foreach (var tagName in tagNames)
    {

      var tag = await _repository.DmsdocumentTags
          .FirstOrDefaultAsync(t => t.DocumentTagName.ToLower().Trim() == tagName.ToLower().Trim());

      if (tag == null)
      {
        tag = new DmsdocumentTag
        {
          DocumentTagName = tagName
        };
        tag.TagId = await _repository.DmsdocumentTags.CreateAndGetIdAsync(tag);
        await _repository.SaveAsync();
      }


      var existingMapping = await _repository.DmsdocumentTagMaps
          .FirstOrDefaultAsync(tm => tm.DocumentId == documentId && tm.TagId == tag.TagId);

      if (existingMapping == null)
      {
        var tagMap = new DmsdocumentTagMap
        {
          DocumentId = documentId,
          TagId = tag.TagId
        };

        await _repository.DmsdocumentTagMaps.CreateAsync(tagMap);
      }
    }

    await _repository.SaveAsync();
  }

  // letter for robust dms project.
  private async Task CreateAccessLog(int documentId, DMSDto dmsDto, string action)
  {
    string ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
    string userAgent = _httpContextAccessor?.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";

    string macAddress = "Unavailable";
    try
    {
      macAddress = NetworkInterface.GetAllNetworkInterfaces()
          .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
          .Select(n => n.GetPhysicalAddress().ToString())
          .FirstOrDefault() ?? "Unavailable";
    }
    catch
    {
      
    }

    var accessLog = new DmsdocumentAccessLog
    {
      DocumentId = documentId,
      AccessedByUserId = dmsDto.UploadedByUserId?.ToString() ?? "System",
      AccessDateTime = DateTime.UtcNow,
      Action = action,
      IpAddress = ipAddress,
      DeviceInfo = userAgent,
      MacAddress = macAddress,
      Notes = $"Action: {action}, User: {dmsDto.UploadedByUserId}, IP: {ipAddress}"
    };

    await _repository.DmsdocumentAccessLogs.CreateAsync(accessLog);
  }


  #endregion
  // Enhanced DMS Service Method for Version Control
  public async Task<string> SaveFileAndDocumentWithVersioningAsync(IFormFile file, string allAboutDMS)
  {
    if (file == null || file.Length == 0) return null;

    var dmsDto = JsonConvert.DeserializeObject<DMSDto>(allAboutDMS);
    if (dmsDto == null)
      throw new ArgumentException("DMS data are not deserialize");

    // Check Validation
    ValidateDMSData(dmsDto, file);

    using var transaction = _repository.Dmsdocuments.TransactionBeginAsync();
    try
    {
      Dmsdocument document = new Dmsdocument();
      FileInfoDto fileInfo = new FileInfoDto();
      // Check if this is an update to existing document
      if (dmsDto.ExistingDocumentId.HasValue && dmsDto.ExistingDocumentId > 0)
      {
        // Get existing document
        var existingDocument = await _repository.Dmsdocuments.GetByIdAsync(x => x.DocumentId == dmsDto.ExistingDocumentId);
        if (existingDocument == null)
          throw new ArgumentException("Existing document not found for versioning");

        // Update existing document with new file info
        document = await UpdateExistingDocumentWithNewVersion(existingDocument, dmsDto, file);
      }
      else
      {
        // Create new document (first time)
        var documentType = await CreateOrGetDocumentType(dmsDto);
        var folder = await CreateFolderStructure(dmsDto);
        fileInfo = await SaveFileToSystemWithVersioning(file, dmsDto);
        //document = await CreateDocumentWithVersioning(dmsDto, documentType, folder, fileInfo);
      }

      // Create new version record
      fileInfo = await SaveFileToSystemWithVersioning(file, dmsDto);
      var version = await CreateDocumentVersionWithVersioning(document, fileInfo, dmsDto);

      // Update tag mapping
      await UpdateTagMappingForVersioning(document.DocumentId, dmsDto);

      // Create access log
      await CreateAccessLogForVersioning(document.DocumentId, dmsDto, "Update");

      // Mark previous version as not current (if exists)
      await MarkPreviousVersionAsNotCurrent(document.DocumentId, version.VersionNumber);

      await _repository.Dmsdocuments.TransactionCommitAsync();

      _logger.LogInfo($"DMS document updated with versioning - DocumentId: {document.DocumentId}, Version: {version.VersionNumber}");

      return document.FilePath;
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error in DMS version control save data. Error message: {ex.Message}");
      await _repository.Dmsdocuments.TransactionRollbackAsync();
      throw;
    }
    finally
    {
      await _repository.Dmsdocuments.TransactionDisposeAsync();
    }
  }

  // Update existing document with new version info
  private async Task<Dmsdocument> UpdateExistingDocumentWithNewVersion(Dmsdocument existingDocument, DMSDto dmsDto, IFormFile file)
  {
    var fileInfo = await SaveFileToSystemWithVersioning(file, dmsDto);

    // Update main document record with latest file info
    existingDocument.FileName = fileInfo.FileName;
    existingDocument.FilePath = fileInfo.RelativePath;
    existingDocument.FileExtension = fileInfo.FileExtension;
    existingDocument.FileSize = fileInfo.FileSize;
    existingDocument.Title = dmsDto.Title ?? Path.GetFileNameWithoutExtension(fileInfo.FileName);
    existingDocument.Description = dmsDto.Description;
    existingDocument.UploadDate = DateTime.UtcNow;
    existingDocument.UploadedByUserId = dmsDto.UploadedByUserId?.ToString();

    _repository.Dmsdocuments.Update(existingDocument);
    await _repository.SaveAsync();

    return existingDocument;
  }

  // Enhanced file save with versioning
  private async Task<FileInfoDto> SaveFileToSystemWithVersioning(IFormFile file, DMSDto dmsDto)
  {
    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    string folderPath = Path.Combine(rootPath, "Uploads", dmsDto.ReferenceEntityType.Trim(),
        dmsDto.ReferenceEntityId, dmsDto.DocumentType.Trim(), "Versions");

    if (!Directory.Exists(folderPath))
      Directory.CreateDirectory(folderPath);

    // Version-aware file naming
    string baseFileName = Path.GetFileNameWithoutExtension(file.FileName);
    string extension = Path.GetExtension(file.FileName);
    string fileName = $"{baseFileName}_v{dmsDto.VersionNumber}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
    string fullPath = Path.Combine(folderPath, fileName);
    string relativePath = $"/Uploads/{dmsDto.ReferenceEntityType}/{dmsDto.ReferenceEntityId}/{dmsDto.DocumentType}/Versions/{fileName}";

    using (var stream = new FileStream(fullPath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    return new FileInfoDto
    {
      FileName = fileName,
      FullPath = fullPath,
      RelativePath = relativePath,
      FileSize = file.Length,
      FileExtension = extension
    };
  }

  // Enhanced document version creation
  private async Task<DmsdocumentVersion> CreateDocumentVersionWithVersioning(Dmsdocument document, FileInfoDto fileInfo, DMSDto dmsDto)
  {
    var version = new DmsdocumentVersion
    {
      DocumentId = document.DocumentId,
      VersionNumber = dmsDto.VersionNumber,
      FileName = fileInfo.FileName,
      FilePath = fileInfo.RelativePath,
      FileSize = fileInfo.FileSize,
      UploadedBy = dmsDto.UploadedByUserId?.ToString(),
      UploadedDate = DateTime.UtcNow,
      IsCurrentVersion = true, // Mark as current version
      VersionNotes = $"Updated on {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
      PreviousVersionId = await GetPreviousVersionIdAsync(document.DocumentId)
    };

    version.VersionId = await _repository.DmsdocumentVersions.CreateAndGetIdAsync(version);
    await _repository.SaveAsync();

    return version;
  }

  #region Helper Mehtod: Update documents with versioning

  private async Task<Dmsdocument> GetExistingDocumentAsync(string entityId, string entityType, string documentType)
  {
    return await _repository.Dmsdocuments.FirstOrDefaultAsync(d =>
        d.ReferenceEntityId == entityId &&
        d.ReferenceEntityType == entityType &&
        d.DocumentType.DocumentType == documentType);
  }

  // Get next version number for a document
  private async Task<int> GetNextVersionNumberAsync(int documentId)
  {
    if (documentId == 0) return 1;

    var latestVersion = await _repository.DmsdocumentVersions
        .FirstOrDefaultWithOrderByDescAsync(
            expression: v => v.DocumentId == documentId,
            orderBy: v => v.VersionNumber);

    return (latestVersion?.VersionNumber ?? 0) + 1;
  }

  // Get previous version ID for linking
  private async Task<int?> GetPreviousVersionIdAsync(int documentId)
  {
    var previousVersion = await _repository.DmsdocumentVersions
        .FirstOrDefaultWithOrderByDescAsync(
            expression: v => v.DocumentId == documentId && v.IsCurrentVersion == true,
            orderBy: v => v.VersionNumber);

    return previousVersion?.VersionId;
  }


  // Mark previous version as not current
  private async Task MarkPreviousVersionAsNotCurrent(int documentId, int currentVersionNumber)
  {
    var previousVersions = await _repository.DmsdocumentVersions
        .ListByConditionAsync(v => v.DocumentId == documentId && v.VersionNumber < currentVersionNumber);

    foreach (var version in previousVersions)
    {
      version.IsCurrentVersion = false;
      _repository.DmsdocumentVersions.Update(version);
    }

    await _repository.SaveAsync();
  }


  // Create file update history
  private async Task CreateFileUpdateHistoryAsync(string entityId, string entityType, string documentType,
      string oldFilePath, string newFilePath, int versionNumber, UsersDto currentUser)
  {
    var updateHistory = new DmsFileUpdateHistory
    {
      EntityId = entityId,
      EntityType = entityType,
      DocumentType = documentType,
      OldFilePath = oldFilePath,
      NewFilePath = newFilePath,
      VersionNumber = versionNumber,
      UpdatedBy = currentUser.UserId.ToString(),
      UpdatedDate = DateTime.UtcNow,
      UpdateReason = "Manual Update",
      Notes = $"File updated from version {versionNumber - 1} to {versionNumber}"
    };

    await _repository.IDmsFileUpdateHistories.CreateAsync(updateHistory);
    await _repository.SaveAsync();
  }

  // Enhanced access log for versioning
  private async Task CreateAccessLogForVersioning(int documentId, DMSDto dmsDto, string action)
  {
    string ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
    string userAgent = _httpContextAccessor?.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";

    var accessLog = new DmsdocumentAccessLog
    {
      DocumentId = documentId,
      AccessedByUserId = dmsDto.UploadedByUserId?.ToString() ?? "System",
      AccessDateTime = DateTime.UtcNow,
      Action = action,
      IpAddress = ipAddress,
      DeviceInfo = userAgent,
      Notes = $"Version Control - {action}: Version {dmsDto.VersionNumber}, User: {dmsDto.UploadedByUserId}"
    };

    await _repository.DmsdocumentAccessLogs.CreateAsync(accessLog);
  }

  // Update tag mapping for versioning
  private async Task UpdateTagMappingForVersioning(int documentId, DMSDto dmsDto)
  {
    if (string.IsNullOrWhiteSpace(dmsDto.DocumentTagName)) return;

    // Add version-specific tags
    var versionTags = $"{dmsDto.DocumentTagName},Version{dmsDto.VersionNumber},Updated";

    var tagNames = versionTags.Split(',')
        .Select(t => t.Trim())
        .Where(t => !string.IsNullOrEmpty(t))
        .Distinct();

    foreach (var tagName in tagNames)
    {
      var tag = await _repository.DmsdocumentTags
          .FirstOrDefaultAsync(t => t.DocumentTagName.ToLower().Trim() == tagName.ToLower().Trim());

      if (tag == null)
      {
        tag = new DmsdocumentTag
        {
          DocumentTagName = tagName
        };
        tag.TagId = await _repository.DmsdocumentTags.CreateAndGetIdAsync(tag);
        await _repository.SaveAsync();
      }

      var existingMapping = await _repository.DmsdocumentTagMaps
          .FirstOrDefaultAsync(tm => tm.DocumentId == documentId && tm.TagId == tag.TagId);

      if (existingMapping == null)
      {
        var tagMap = new DmsdocumentTagMap
        {
          DocumentId = documentId,
          TagId = tag.TagId
        };

        await _repository.DmsdocumentTagMaps.CreateAsync(tagMap);
      }
    }

    await _repository.SaveAsync();
  }

  #endregion

  #region DTOs

  public class FileInfoDto
  {
    public string FileName { get; set; }
    public string FullPath { get; set; }
    public string RelativePath { get; set; }
    public long FileSize { get; set; }
    public string FileExtension { get; set; }
  }

  public class DMSResponseDto
  {
    public bool Success { get; set; }
    public int DocumentId { get; set; }
    public string FilePath { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
  }

  #endregion


}
