using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace bdDevCRM.Service.CRM;

internal sealed class CrmAdditionalDocumentsService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmAdditionalDocumentService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalDocuments.GetActiveAdditionalDocumentsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalDocument");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalDocument, AdditionalDocumentDto>(list);
  }

  public async Task<IEnumerable<AdditionalDocumentDto>> GetActiveAdditionalDocumentsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalDocuments.GetActiveAdditionalDocumentsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalDocument");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalDocument, AdditionalDocumentDto>(list);
  }

  public async Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalDocuments.GetAdditionalDocumentsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmAdditionalDocument");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalDocument, AdditionalDocumentDto>(list);
  }

  public async Task<AdditionalDocumentDto> GetAdditionalDocumentAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmAdditionalDocuments.GetAdditionalDocumentAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmAdditionalDocument", "AdditionalDocumentId", id.ToString());
    return MyMapper.JsonClone<CrmAdditionalDocument, AdditionalDocumentDto>(entity);
  }

  public async Task<IEnumerable<AdditionalDocumentDto>> GetAdditionalDocumentsByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalDocuments.GetAdditionalDocumentsByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<AdditionalDocumentDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalDocument, AdditionalDocumentDto>(list);
  }

  public async Task<AdditionalDocumentDto> CreateNewRecordAsync(AdditionalDocumentDto dto, UsersDto currentUser)
  {
    if (dto.AdditionalDocumentId != 0)
      throw new InvalidCreateOperationException("AdditionalDocumentId must be 0.");

    var entity = MyMapper.JsonClone<AdditionalDocumentDto, CrmAdditionalDocument>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;

    dto.AdditionalDocumentId = await _repository.CrmAdditionalDocuments.CreateAndGetIdAsync(entity);
    //dto.CreatedDate = entity.CreatedDate;
    //dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, AdditionalDocumentDto dto, bool trackChanges)
  {
    if (key != dto.AdditionalDocumentId) return "Key mismatch.";

    bool exists = await _repository.CrmAdditionalDocuments.ExistsAsync(x => x.AdditionalDocumentId == key);
    if (!exists) throw new GenericNotFoundException("AdditionalDocument", "AdditionalDocumentId", key.ToString());

    var entity = MyMapper.JsonClone<AdditionalDocumentDto, CrmAdditionalDocument>(dto);
    entity.UpdatedDate = DateTime.UtcNow;

    _repository.CrmAdditionalDocuments.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"AdditionalDocument updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, AdditionalDocumentDto dto)
  {
    if (key != dto.AdditionalDocumentId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(AdditionalDocumentDto));

    await _repository.CrmAdditionalDocuments.DeleteAsync(x => x.AdditionalDocumentId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"AdditionalDocument deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<AdditionalDocumentDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
     SELECT [AdditionalDocumentId]
      ,[DocumentTitle]
      ,[DocumentPath]
      ,[DocumentName]
      ,[RecordType]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
      ,[ApplicantId]
      ,doc.FilePath as AttachedDocument
  FROM [dbDevCRM].[dbo].[CrmAdditionalDocument]
  LEFT JOIN DmsDocument doc on doc.ReferenceEntityId = CrmAdditionalDocument.ApplicantId 
    and TRIM(doc.ReferenceEntityType) = 'AdditionalDocument'";

    string orderBy = " AdditionalDocumentId ";
    return await _repository.CrmAdditionalDocuments.GridData<AdditionalDocumentDto>(sql, options, orderBy, string.Empty);
  }

}