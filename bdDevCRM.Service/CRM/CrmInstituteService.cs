using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace bdDevCRM.Service.CRM;


internal sealed class CrmInstituteService : ICrmInstituteService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;
  private readonly IHttpContextAccessor _httpContextAccessor;


  public CrmInstituteService(IRepositoryManager repo, ILoggerManager logger, IConfiguration config, IHttpContextAccessor httpContextAccessor)
  {
    _repository = repo;
    _logger = logger;
    _config = config;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<CrmInstituteDLLDto>> GetInstitutesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmInstitutes.GetActiveInstitutesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmInstitute");
    return MyMapper.JsonCloneIEnumerableToList<CrmInstitute, CrmInstituteDLLDto>(list);
  }

  public async Task<IEnumerable<CrmInstituteDLLDto>> GetInstitutesByCountryIdDDLAsync(int countryId, bool trackChanges = false)
  {
    IEnumerable<CrmInstituteDLLDto> list = await _repository.CrmInstitutes.ListByWhereWithSelectAsync( 
      expression: x => x.CountryId == countryId, 
      selector: x => new CrmInstituteDLLDto
      { 
        InstituteId = x.InstituteId,
        InstituteName = x.InstituteName
      },
      orderBy: x => x.InstituteName,
      trackChanges: trackChanges
      );

    if (!list.Any()) return new List<CrmInstituteDLLDto>();
    //return MyMapper.JsonCloneIEnumerableToList<CrmInstituteDLLDto, CrmInstituteDto>(list);

    return list;
  }

  public async Task<GridEntity<CrmInstituteDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql =
            @"select InstituteId ,CRMInstitute.CountryId ,InstituteName ,Campus ,Website ,MonthlyLivingCost ,FundsRequirementforVisa ,ApplicationFee
,CRMInstitute.CurrencyId ,IsLanguageMandatory ,LanguagesRequirement ,InstitutionalBenefits ,PartTimeWorkDetails ,ScholarshipsPolicy ,InstitutionStatusNotes
,CRMInstitute.InstituteTypeId ,InstituteCode ,InstituteEmail ,InstituteAddress ,InstitutePhoneNO ,InstituteMobileNo,CRMInstitute.Status ,Country.CountryName 
,CrmCurrencyInfo.CurrencyName ,CRMInstituteType.InstituteTypeName ,docLogo.FilePath as InstitutionLogo , docProspectus.FilePath as InstitutionProspectus
from CRMInstitute
left join Country on CRMInstitute.CountryId = Country.CountryId
left join CrmCurrencyInfo on CRMInstitute.CountryId = CrmCurrencyInfo.CurrencyId
left join CRMInstituteType on CRMInstitute.InstituteTypeId = CRMInstituteType.InstituteTypeId

left join DMSDocument docLogo on CRMInstitute.InstituteId =  docLogo.ReferenceEntityId and docLogo.SystemTag = 'InstitutionLogo'
left join DMSDocument docProspectus on CRMInstitute.InstituteId = docProspectus.ReferenceEntityId and docProspectus.SystemTag = 'InstitutionProspectus' ";
    string orderBy = " InstituteName asc ";
    return await _repository.CrmInstitutes.GridData<CrmInstituteDto>(sql, options, orderBy, "");
  }

  public async Task<CrmInstituteDto> CreateNewRecordAsync(CrmInstituteDto dto, UsersDto currentUser)
  {
    if (dto.InstituteId != 0)
      throw new InvalidCreateOperationException("InstituteId must be 0.");

    //var instituteObj = await _repository.CrmInstitutes.FirstOrDefaultAsync(x => x.InstituteName.Trim().ToLower() == dto.InstituteName!.Trim().ToLower());
    bool dup = await _repository.CrmInstitutes.ExistsAsync(x => x.InstituteName != null && x.InstituteName.Trim().ToLower().Equals(dto.InstituteName!.Trim().ToLower()));

    if (dup) throw new DuplicateRecordException("CrmInstitute", "InstituteName");

    var entity = MyMapper.JsonClone<CrmInstituteDto, CrmInstitute>(dto);
    dto.InstituteId = await _repository.CrmInstitutes.CreateAndGetIdAsync(entity);

    return dto;
  }

  public async Task<CrmInstituteDto> UpdateRecordAsync(int key, CrmInstituteDto updateDto, bool trackChanges)
  {
    if (key != updateDto.InstituteId) throw new IdMismatchBadRequestException(key.ToString(), "Institute Update");

    bool exists = await _repository.CrmInstitutes.ExistsAsync(x => x.InstituteId == key);
    if (!exists) throw new GenericNotFoundException("CrmInstitute", "InstituteId", key.ToString());

    var entity = MyMapper.JsonClone<CrmInstituteDto, CrmInstitute>(updateDto);
    _repository.CrmInstitutes.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmInstitute updated, id={key}");
    return MyMapper.JsonClone<CrmInstitute, CrmInstituteDto>(entity);
  }

  public async Task<string> DeleteRecordAsync(int key, CrmInstituteDto dto)
  {
    if (key != dto.InstituteId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CrmInstituteDto));

    await _repository.CrmInstitutes.DeleteAsync(x => x.InstituteId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmInstitute deleted, id={key}");
    return OperationMessage.Success;
  }

  // Implement this interface : Task<CrmInstituteDto> GetInstituteNameIdAsync(int key, bool trackChanges = false)
  public async Task<CrmInstituteDto> GetInstituteNameIdAsync(string name, bool trackChanges = false)
  {
    var entity = await _repository.CrmInstitutes.FirstOrDefaultAsync(x => x.InstituteName == name);
    if (entity == null) throw new GenericNotFoundException("CrmInstitute", "InstituteId", name.ToString());
    return MyMapper.JsonClone<CrmInstitute, CrmInstituteDto>(entity);
  }




  //public async Task<ApiResponse<CrmInstituteDto>> CreateNewRecordAsync(CrmInstituteDto dto, UsersDto currentUser)
  //{
  //  if (dto.InstituteId != 0)
  //    throw new InvalidCreateOperationException("InstituteId must be 0.");

  //  // Fix: Ensure InstituteName is a string before calling Trim and ToLower
  //  var instituteObj = await _repository.CrmInstitutes.FirstOrDefaultAsync(x => x.InstituteName.Trim().ToLower().Equals(dto.InstituteName!.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase));
  //  //bool dup = await _repository.CrmInstitutes.ExistsAsync(x => x.InstituteName != null && x.InstituteName.Trim().ToLower().Equals(dto.InstituteName!.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase));

  //  if (instituteObj != null) throw new DuplicateRecordException("CrmInstitute", "InstituteName");

  //  var entity = MyMapper.JsonClone<CrmInstituteDto, CrmInstitute>(dto);

  //  try
  //  {
  //    using var transaction = _repository.GroupPermission.TransactionBeginAsync();
  //    entity.InstituteId = await _repository.CrmInstitutes.CreateAndGetIdAsync(entity);
  //    //if (entity.InstituteId <= 0) throw new InvalidCreateOperationException();

  //    //// Create DMSDto object for save DMS data.
  //    //DMSDto dmsDto = new DMSDto();

  //    //01.Check DMSDocumentType by dmsDto.EntityType
  //    var dmsDocumentType = await _repository.DmsDocumentTypes.FirstOrDefaultAsync(f => f.DocumentType.ToLower().Trim() == "Logo".ToLower());
  //    if (dmsDocumentType == null)
  //    {
  //      dmsDocumentType = new DmsDocumentType
  //      {
  //        Name = "Logo",
  //        DocumentType = "CRMInstitute",
  //        IsMandatory = true,
  //        AcceptedExtensions = ".png",
  //        MaxFileSizeMb = 1
  //      };
  //      dmsDocumentType.DocumentTypeId = await _repository.DmsDocumentTypes.CreateAndGetIdAsync(dmsDocumentType);
  //    }
  //    _logger.LogInfo($"DMS Document Type created with Id: {dmsDocumentType.DocumentTypeId}, Name: {dmsDocumentType.Name}, DocumentType: {dmsDocumentType.DocumentType}");

  //    //02.Check parent forlder and child folder DmsDocumentFolders by dmsDto.FolderName and ParentFolderId
  //    var parentFolder = await _repository.DmsDocumentFolders.FirstOrDefaultAsync(f => f.FolderName.ToLower().Trim() == "CRMInstitute".ToLower() && (f.ParentFolderId == null || f.ParentFolderId == 0));
  //    if (parentFolder == null)
  //    {
  //      parentFolder = new DmsDocumentFolder
  //      {
  //        FolderName = "CRMInstitute",
  //        OwnerId = null, // OwnerId: Folder Owner.
  //        ReferenceEntityType = "CRMInstitute", // e.g., "CrmInstitute"
  //        ReferenceEntityId = null,
  //        ParentFolderId = null,
  //        DocumentId = null
  //      };
  //      //Parent folder has no ReferenceEntityId ,OwnerId, and ParentFolderId
  //      //because it is the top - level folder.
  //      //OwnerId,ReferenceEntityId,ParentFolderId is not set here, it will be set later when creating child folders.
  //      parentFolder.ParentFolderId = await _repository.DmsDocumentFolders.CreateAndGetIdAsync(parentFolder);
  //    }
  //    // loginfo with currentuserId
  //    if (parentFolder.ParentFolderId <= 0) throw new InvalidCreateOperationException("DMS Parent Folder creation failed.");
  //    _logger.LogInfo($"DMS Parent Folder created with Id: {parentFolder.ParentFolderId}, FolderName: {parentFolder.FolderName}, ReferenceEntityType: {parentFolder.ReferenceEntityType}");

  //    var dmsDocumentFolder = await _repository.DmsDocumentFolders.FirstOrDefaultAsync(f => f.FolderName.ToLower().Trim() == entity.InstituteName.ToLower().Trim()
  //    && f.ParentFolderId == parentFolder.ParentFolderId);
  //    if (dmsDocumentFolder == null)
  //    {
  //      dmsDocumentFolder = new DmsDocumentFolder
  //      {
  //        ParentFolderId = parentFolder.ParentFolderId,
  //        FolderName = entity.InstituteId.ToString().Replace(" ", "_"), // will be entitiy name column value
  //        OwnerId = null, // may be letter. now entity recordId
  //        ReferenceEntityType = "CRMInstitute", // e.g., "CrmInstitute"
  //        ReferenceEntityId = entity.InstituteId.ToString() // e.g., "1" (for CrmInstitute)
  //      };
  //      // casecade delete need because : I have no idea which table has the reference if there are not foreign key relationship. So it badly need.
  //      dmsDocumentFolder.FolderId = await _repository.DmsDocumentFolders.CreateAndGetIdAsync(dmsDocumentFolder);
  //    }
  //    _logger.LogInfo($"DMS Child Folder created with Id: {dmsDocumentFolder.FolderId}, FolderName: {dmsDocumentFolder.FolderName}, ParentFolderId: {dmsDocumentFolder.ParentFolderId}, ReferenceEntityType: {dmsDocumentFolder.ReferenceEntityType}, ReferenceEntityId: {dmsDocumentFolder.ReferenceEntityId}");

  //    //3.Create DmsDocument
  //    //03.Save file to disk and create DmsDocument
  //    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
  //    //folderPath = "wwwroot/Uploads/CRMInstitute/southwells/1/logo/"
  //    //wwwroot / Uploads / EntityName(CRMInstitute, CRMCourse, Student, Agent, like controller / entity) / 1(record or data: like studentIdentity, crnIntituteIdency, CrmCourseIdentity) / DocumentType(like: (student:passport, nid, accademic certificate || institute: logo, photo, procpectus etc))
  //    string folderPath = Path.Combine(rootPath, "Uploads", parentFolder.FolderName, dmsDocumentFolder.FolderName, dmsDocumentType.DocumentType.Trim());
  //    string fileName = $"{Path.GetFileNameWithoutExtension(dto.InstitutionLogoFile.FileName)}{DateTime.Now.Ticks}{Path.GetExtension(dto.InstitutionLogoFile.FileName)}";
  //    string fullPath = Path.Combine(folderPath, fileName);
  //    string relativeFilePath = $"Uploads/{parentFolder.FolderName}/{dmsDocumentFolder.FolderName}/{dmsDocumentType.DocumentType.Trim()}";

  //    // 
  //    var dmsDocument = await _repository.DmsDocuments.FirstOrDefaultAsync(f => f.FilePath.ToLower().Trim() == relativeFilePath.ToLower().Trim() && f.FileName.Trim().ToLower() == fileName.Trim().ToLower());
  //    if (dmsDocument == null)
  //    {
  //      if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

  //      //Save file to disk
  //      using (var stream = new FileStream(fullPath, FileMode.Create)) { await dto.InstitutionLogoFile.CopyToAsync(stream); }

  //      dmsDocument = new DmsDocument
  //      {
  //        Title = Path.GetFileNameWithoutExtension(fileName),
  //        Description = folderPath,
  //        FileName = fileName,
  //        FilePath = relativeFilePath,
  //        FileExtension = Path.GetExtension(fileName),
  //        FileSize = dto.InstitutionLogoFile.Length,
  //        FolderId = dmsDocumentFolder.FolderId,
  //        DocumentTypeId = dmsDocumentType.DocumentTypeId,
  //        ReferenceEntityType = dmsDocumentFolder.ReferenceEntityType,
  //        ReferenceEntityId = dmsDocumentFolder.ReferenceEntityId,
  //        UploadDate = DateTime.UtcNow,
  //        UploadedByUserId = currentUser.UserId.ToString(),
  //      };
  //      dmsDocument.DocumentId = await _repository.DmsDocuments.CreateAndGetIdAsync(dmsDocument);
  //    }
  //    //if (dmsDocument.DocumentId <= 0) throw new InvalidCreateOperationException("DMS Document creation failed.");
  //    _logger.LogInfo($"DMS Document created with Id: {dmsDocument.DocumentId}, Title: {dmsDocument.Title}, FilePath: {dmsDocument.FilePath}, UploadedByUserId: {dmsDocument.UploadedByUserId}");


  //    //4.Create DmsDocumentVersion
  //    var dmsDocumentVersions = await _repository.DmsDocumentVersions.FirstOrDefaultAsync(f => f.DocumentId == dmsDocument.DocumentId);
  //    if (dmsDocumentVersions == null)
  //    {
  //      dmsDocumentVersions = new DmsDocumentVersion
  //      {
  //        DocumentId = dmsDocument.DocumentId,
  //        VersionNumber = 1,
  //        FileName = dmsDocument.FileName,
  //        FilePath = dmsDocument.FilePath,
  //        UploadedBy = currentUser.UserId.ToString(),
  //        UploadedDate = DateTime.UtcNow,
  //      };
  //      dmsDocumentVersions.VersionId = await _repository.DmsDocumentVersions.CreateAndGetIdAsync(dmsDocumentVersions);
  //    }
  //    //if (dmsDocumentVersions.VersionId <= 0) throw new InvalidCreateOperationException("DMS Document Version creation failed.");
  //    _logger.LogInfo($"DMS Document Version created with Id: {dmsDocumentVersions.VersionId}, DocumentId: {dmsDocumentVersions.DocumentId}, VersionNumber: {dmsDocumentVersions.VersionNumber}, UploadedBy: {dmsDocumentVersions.UploadedBy}");


  //    //5.Create DmsDocumentAccessLog
  //    string ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
  //    string userAgent = _httpContextAccessor?.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";
  //    _logger.LogInfo($"File uploaded by UserId: {currentUser.UserId}, IP: {ipAddress}, User-Agent: {userAgent}");
  //    string macAddress = NetworkInterface.GetAllNetworkInterfaces()
  //        .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
  //        .Select(n => n.GetPhysicalAddress().ToString())
  //        .FirstOrDefault() ?? "Unavailable";
  //    var dmsdocumentAccessLog = await _repository.DmsDocumentAccessLogs.FirstOrDefaultAsync(f => f.DocumentId == dmsDocument.DocumentId);
  //    if (dmsdocumentAccessLog == null)
  //    {
  //      dmsdocumentAccessLog = new DmsDocumentAccessLog
  //      {
  //        DocumentId = dmsDocument.DocumentId,
  //        AccessedByUserId = currentUser.UserId.ToString(),
  //        AccessDateTime = DateTime.UtcNow,
  //        Action = "POST",
  //        IpAddress = ipAddress,
  //        DeviceInfo = userAgent, // User-Agent string
  //        MacAddress = macAddress,
  //        Notes = $"File uploaded by UserId: {currentUser.UserId.ToString()}, IP: {ipAddress}, User-Agent: {userAgent}"
  //      };
  //      dmsdocumentAccessLog.LogId = await _repository.DmsDocumentAccessLogs.CreateAndGetIdAsync(dmsdocumentAccessLog);
  //    }
  //    //if (dmsdocumentAccessLog.LogId <= 0) throw new InvalidCreateOperationException("DMS Document Access Log creation failed.");

  //    _logger.LogInfo($"DMS Document access logger created for DocumentId: {dmsdocumentAccessLog.DocumentId}, Action: {dmsdocumentAccessLog.Action}, AccessedByUserId: {dmsdocumentAccessLog.AccessedByUserId}");

  //    //7.Create DmsDocumentTag and Map
  //    var dmsDocumentTag = await _repository.DmsDocumentTags.FirstOrDefaultAsync(f => f.DocumentTagName.ToLower().Trim() == "Logo".ToLower());
  //    if (dmsDocumentTag == null)
  //    {
  //      dmsDocumentTag = new DmsDocumentTag
  //      {
  //        DocumentTagName = "Logo"
  //      };
  //      dmsDocumentTag.TagId = await _repository.DmsDocumentTags.CreateAndGetIdAsync(dmsDocumentTag);

  //      // Map the tag to the document
  //      var tagMap = new DmsDocumentTagMap
  //      {
  //        DocumentId = dmsDocument.DocumentId,
  //        TagId = dmsDocumentTag.TagId
  //      };

  //      await _repository.DmsDocumentTagMaps.CreateAsync(tagMap);
  //    }

  //    _logger.LogInfo($"CrmInstitute created, id={entity.InstituteId}");
  //    dto.InstituteId = entity.InstituteId; // Set the InstituteId in the DTO

  //    var result = new ApiResponse<CrmInstituteDto>(dto, 201, "Record created successfully")
  //    {
  //      IsSuccess = true,
  //      Timestamp = DateTime.UtcNow
  //    };
  //    return result;
  //  }
  //  catch (Exception)
  //  {
  //    await _repository.GroupPermission.TransactionRollbackAsync();
  //    throw;
  //  }
  //  finally
  //  {
  //    await _repository.GroupPermission.TransactionDisposeAsync();
  //  }


  //  //if (dmsDto.DocumentTagName != null && !string.IsNullOrWhiteSpace(dmsDto.DocumentTagName))
  //  //{
  //  //  var tags = dmsDto.DocumentTagName.Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).Distinct();
  //  //  foreach (var tagName in tags)
  //  //  {
  //  //    //Check if the tag already exists
  //  //   var tag = await _repository.DmsDocumentTags.FirstOrDefaultAsync(t => t.DocumentTagName == tagName);
  //  //    if (tag == null)
  //  //    {
  //  //      tag = new DmsDocumentTag { DocumentTagName = tagName };
  //  //      tag.TagId = await _repository.DmsDocumentTags.CreateAndGetIdAsync(tag);
  //  //      await _repository.SaveAsync();
  //  //    }
  //  //    // Map the tag to the document
  //  //    var tagMap = new DmsDocumentTagMap
  //  //    {
  //  //      DocumentId = document.DocumentId,
  //  //      TagId = tag.TagId
  //  //    };

  //  //    await _repository.DmsDocumentTagMaps.CreateAsync(tagMap);
  //  //  }
  //  //}


  //}


  //public Task<string> SaveOrUpdateAsync(int key, CrmInstituteDto dto) => dto.InstituteId == 0 ? CreateNewRecordAsync(dto) : UpdateRecordAsync(key, dto, false);
}





