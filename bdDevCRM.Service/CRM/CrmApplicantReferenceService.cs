using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.CRM;

internal sealed class CrmApplicantReferenceService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmApplicantReferenceService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantReferences.GetActiveApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetActiveApplicantReferencesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantReferences.GetActiveApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantReferences.GetApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<ApplicantReferenceDto> GetApplicantReferenceAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantReferences.GetApplicantReferenceAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmApplicantReference", "ApplicantReferenceId", id.ToString());
    return MyMapper.JsonClone<CrmApplicantReference, ApplicantReferenceDto>(entity);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantReferences.GetApplicantReferencesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<ApplicantReferenceDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<ApplicantReferenceDto> CreateNewRecordAsync(ApplicantReferenceDto dto, UsersDto currentUser)
  {
    if (dto.ApplicantReferenceId != 0)
      throw new InvalidCreateOperationException("ApplicantReferenceId must be 0.");

    var entity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.ApplicantReferenceId = await _repository.CrmApplicantReferences.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, ApplicantReferenceDto dto, bool trackChanges)
  {
    if (key != dto.ApplicantReferenceId) return "Key mismatch.";

    bool exists = await _repository.CrmApplicantReferences.ExistsAsync(x => x.ApplicantReferenceId == key);
    if (!exists) throw new GenericNotFoundException("CrmApplicantReference", "ApplicantReferenceId", key.ToString());

    var entity = MyMapper.JsonClone<ApplicantReferenceDto, CrmApplicantReference>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmApplicantReferences.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmApplicantReference updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, ApplicantReferenceDto dto)
  {
    if (key != dto.ApplicantReferenceId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(ApplicantReferenceDto));

    await _repository.CrmApplicantReferences.DeleteAsync(x => x.ApplicantReferenceId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmApplicantReference deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<ApplicantReferenceDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ar.ApplicantReferenceId,
    ar.ApplicantId,
    ar.Name,
    ar.Designation,
    ar.Institution,
    ar.EmailId,
    ar.PhoneNo,
    ar.FaxNo,
    ar.Address,
    ar.City,
    ar.State,
    ar.Country,
    ar.PostOrZipCode,
    ar.CreatedDate,
    ar.CreatedBy,
    ar.UpdatedDate,
    ar.UpdatedBy,
    app.ApplicationStatus
from CrmApplicantReference ar
left join CrmApplication app on ar.ApplicantId = app.ApplicationId
";
    string orderBy = " ar.CreatedDate desc ";
    return await _repository.CrmApplicantReferences.GridData<ApplicantReferenceDto>(sql, options, orderBy, "");
  }
}