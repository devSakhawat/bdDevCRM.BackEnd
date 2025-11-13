//using bdDevCRM.Shared.DataTransferObjects.DMS;
//using Microsoft.AspNetCore.Http;

//namespace bdDevCRM.Utilities.OthersLibrary;

//public static class FileUploadHelper
//{
//  public static async Task<DMSDto> SaveFileAsync(IFormFile file, string subFolder ,DMSDto dto)
//  {
//    if (file == null || file.Length == 0) return null;

//    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
//    string folderPath = Path.Combine(rootPath, "Uploads", subFolder);
//    if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }


//    //var fileName = Path.GetFileNameWithoutExtension(file.FileName);
//    //var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
//    //var fileSize = file.Length;
//    //var fileType = file.ContentType;
//    var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
//    var allowedDocumentExtensions = new[] { ".pdf", ".docx", ".xlsx" };
//    var allowedImageContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
//    var allowedDocumentContentTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };

//    var documentMaxFileSize = 5 * 1024 * 1024; // 5 MB
//    var imageMaxFileSize = 2 * 1024 * 1024; // 2 MB

//    bool isValidImage = allowedImageExtensions.Contains(fileExtension) && allowedImageContentTypes.Contains(fileType);
//    bool isValidDocument = allowedDocumentExtensions.Contains(fileExtension) && allowedDocumentContentTypes.Contains(fileType);

//    if (isValidImage)
//    {
//      if (fileSize > imageMaxFileSize) return null;
//    }
//    else if (isValidDocument)
//    {
//      if (fileSize > documentMaxFileSize) return null;
//    }
//    else
//    {
//      return null;
//    }

//    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
//    string fullPath = Path.Combine(folderPath, uniqueFileName);

//    using (var stream = new FileStream(fullPath, FileMode.Create))
//    {
//      await file.CopyToAsync(stream);
//    }

//    var filePath = Path.Combine("Uploads", subFolder, uniqueFileName).Replace("\\", "/");


//    var dms = new DMSDto
//    {
//      // DmsDocumentTypeDto -----------------------
//      DocumentTypeName = string.Empty,
//      EntityType = string.Empty,
//      IsMandatory = false,
//      AcceptedExtensions = null,
//      MaxFileSizeMb = null,
//      // ---------- DmsDocumentDto--------------------
//      DocumentId = 0,
//      Title = (!string.IsNullOrEmpty(dto.Title) ? dto.Title : Path.GetFileNameWithoutExtension(file.FileName)),
//      Description = (!string.IsNullOrEmpty(dto.Description) ? dto.Description : Path.GetFileNameWithoutExtension(file.FileName)),
//      FileName = (!string.IsNullOrEmpty(dto.FileName) ? dto.FileName : Path.GetFileNameWithoutExtension(file.FileName)),
//      FileExtension = Path.GetExtension(file.FileName).ToLowerInvariant(),
//      FileSize = file.Length,
//      FilePath = "",
//      UploadDate = DateTime.UtcNow,
//      UploadedByUserId = string.Empty,
//      DocumentTypeId = 0,
//      ReferenceEntityType = string.Empty,
//      ReferenceEntityId = string.Empty,
//      AccessedByUserId = 0,
//      AccessDateTime = DateTime.Now,
//      Action = string.Empty,
//      ParentFolderId = 0,
//      FolderName = string.Empty,
//      OwnerUserId = 0,
//      TagId = 0,
//      DocumentTagName = string.Empty,
      
//      VersionId = 0,
//      VersionNumber = 0,
//      UploadedDate = DateTime.UtcNow,
//      UploadedBy = 0,
//    };

    

    

//    // Populate DmsDocumentDto with available information and default values for others
//    var dto2 = new DMSDto
//    {
//      DocumentId = 0,
//      Title = fileName,
//      Description = null,
//      FileName = uniqueFileName,
//      FileExtension = fileExtension,
//      FileSize = fileSize,
//      FilePath = filePath,
//      UploadDate = DateTime.UtcNow,
//      UploadedByUserId = null,
//      DocumentTypeId = 0,
//      ReferenceEntityType = string.Empty,
//      ReferenceEntityId = string.Empty,
//      AccessedByUserId = 0,
//      AccessDateTime = DateTime.UtcNow,
//      Action = string.Empty,
//      ParentFolderId = null,
//      FolderName = string.Empty,
//      OwnerUserId = 0,
//      TagId = 0,
//      DocumentTagName = string.Empty,
//      DocumentTypeName = string.Empty,
//      EntityType = string.Empty,
//      IsMandatory = false,
//      AcceptedExtensions = null,
//      MaxFileSizeMb = null,
//      VersionId = 0,
//      VersionNumber = 0,
//      UploadedDate = DateTime.UtcNow,
//      UploadedBy = 0
//    };

//    return dms;
//  }


//  public async Task<List<int>> SaveInstituteFilesToDMSAsync(CrmInstituteDto dto, int instituteId, int userId)
//  {
//    var savedDocumentIds = new List<int>();

//    if (dto.InstitutionLogoFile != null)
//    {
//      var docId = await SaveFileWithAllDMSEntitiesAsync(dto.InstitutionLogoFile, "InstituteLogo", "Logo of the institute", "Institute", instituteId, userId);
//      savedDocumentIds.Add(docId);
//    }

//    if (dto.InstitutionProspectusFile != null)
//    {
//      var docId = await SaveFileWithAllDMSEntitiesAsync(dto.InstitutionProspectusFile, "InstituteProspectus", "Prospectus file", "Institute", instituteId, userId);
//      savedDocumentIds.Add(docId);
//    }

//    return savedDocumentIds;
//  }




//  public async Task<int> SaveFileWithAllDMSEntitiesAsync(IFormFile file, string docTypeName, string description, string entityType, int entityId, int userId)
//  {
//    // 1. Validate file extension, size
//    var extension = Path.GetExtension(file.FileName).ToLower();
//    var fileSizeMB = file.Length / (1024.0 * 1024.0);

//    var docType = await _repository.DocumentType.GetByNameAsync(docTypeName);
//    if (docType == null)
//    {
//      throw new Exception("DocumentType not configured.");
//    }

//    if (!docType.AcceptedExtensions.Contains(extension))
//      throw new Exception("File extension not allowed.");

//    if (fileSizeMB > docType.MaxFileSizeMB)
//      throw new Exception("File size exceeded.");

//    // 2. Save file to physical location
//    var uploadsRoot = Path.Combine("wwwroot", "Uploads", entityType.ToLower(), entityId.ToString());
//    Directory.CreateDirectory(uploadsRoot);

//    var uniqueFileName = $"{Guid.NewGuid()}{extension}";
//    var filePath = Path.Combine(uploadsRoot, uniqueFileName);
//    using (var stream = new FileStream(filePath, FileMode.Create))
//    {
//      await file.CopyToAsync(stream);
//    }

//    // 3. Save Document entity
//    var document = new Document
//    {
//      Title = docTypeName,
//      Description = description,
//      FileName = uniqueFileName,
//      FileExtension = extension,
//      FileSize = file.Length,
//      FilePath = filePath,
//      UploadDate = DateTime.UtcNow,
//      UploadedByUserId = userId,
//      DocumentTypeId = docType.DocumentTypeId,
//      ReferenceEntityType = entityType,
//      ReferenceEntityId = entityId
//    };

//    await _repository.Document.AddAsync(document);
//    await _repository.SaveAsync();

//    // 4. Save DocumentVersion
//    var version = new DocumentVersion
//    {
//      DocumentId = document.DocumentId,
//      VersionNumber = 1,
//      FileName = uniqueFileName,
//      FilePath = filePath,
//      UploadedBy = userId,
//      UploadedDate = DateTime.UtcNow
//    };

//    await _repository.DocumentVersion.AddAsync(version);

//    // 5. Save DocumentAccessLog
//    var log = new DocumentAccessLog
//    {
//      DocumentId = document.DocumentId,
//      AccessedByUserId = userId,
//      AccessDateTime = DateTime.UtcNow,
//      Action = "Upload"
//    };

//    await _repository.DocumentAccessLog.AddAsync(log);

//    // 6. Optionally add tag (e.g., "Initial Upload")
//    var tag = await _repository.DocumentTag.GetByNameAsync("Initial Upload");
//    if (tag != null)
//    {
//      await _repository.DocumentTagMap.AddAsync(new DocumentTagMap
//      {
//        DocumentId = document.DocumentId,
//        TagId = tag.TagId
//      });
//    }

//    // 7. Optionally add to folder
//    var folder = await _repository.DocumentFolder.GetOrCreateFolderAsync("Institutes", entityType, entityId, userId);
//    // You can associate folderId with Document if needed.

//    await _repository.SaveAsync();

//    return document.DocumentId;
//  }


//}

