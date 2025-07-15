using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.CRM;

internal sealed class ApplicantReferenceService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IApplicantReferenceService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantReference.GetActiveApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetActiveApplicantReferencesAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantReference.GetActiveApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantReference.GetApplicantReferencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantReference");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<ApplicantReferenceDto> GetApplicantReferenceAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.ApplicantReference.GetApplicantReferenceAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantReference", "ApplicantReferenceId", id.ToString());
    return MyMapper.JsonClone<ApplicantReference, ApplicantReferenceDto>(entity);
  }

  public async Task<IEnumerable<ApplicantReferenceDto>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.ApplicantReference.GetApplicantReferencesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<ApplicantReferenceDto>();
    return MyMapper.JsonCloneIEnumerableToList<ApplicantReference, ApplicantReferenceDto>(list);
  }

  public async Task<ApplicantReferenceDto> CreateNewRecordAsync(ApplicantReferenceDto dto, UsersDto currentUser)
  {
    if (dto.ApplicantReferenceId != 0)
      throw new InvalidCreateOperationException("ApplicantReferenceId must be 0.");

    var entity = MyMapper.JsonClone<ApplicantReferenceDto, ApplicantReference>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.ApplicantReferenceId = await _repository.ApplicantReference.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, ApplicantReferenceDto dto, bool trackChanges)
  {
    if (key != dto.ApplicantReferenceId) return "Key mismatch.";

    bool exists = await _repository.ApplicantReference.ExistsAsync(x => x.ApplicantReferenceId == key);
    if (!exists) throw new GenericNotFoundException("ApplicantReference", "ApplicantReferenceId", key.ToString());

    var entity = MyMapper.JsonClone<ApplicantReferenceDto, ApplicantReference>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.ApplicantReference.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantReference updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, ApplicantReferenceDto dto)
  {
    if (key != dto.ApplicantReferenceId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(ApplicantReferenceDto));

    await _repository.ApplicantReference.DeleteAsync(x => x.ApplicantReferenceId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantReference deleted, id={key}");
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
from ApplicantReference ar
left join CrmApplication app on ar.ApplicantId = app.ApplicationId
";
    string orderBy = " ar.CreatedDate desc ";
    return await _repository.ApplicantReference.GridData<ApplicantReferenceDto>(sql, options, orderBy, "");
  }
}