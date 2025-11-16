using bdDevCRM.Utilities.CRMGrid.GRID;
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

internal sealed class CrmAdditionalInfoService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmAdditionalInfoService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalInfoes.GetActiveAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetActiveAdditionalInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalInfoes.GetActiveAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalInfoes.GetAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmAdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<AdditionalInfoDto> GetAdditionalInfoAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmAdditionalInfoes.GetAdditionalInfoAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmAdditionalInfo", "AdditionalInfoId", id.ToString());
    return MyMapper.JsonClone<CrmAdditionalInfo, AdditionalInfoDto>(entity);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmAdditionalInfoes.GetAdditionalInfosByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<AdditionalInfoDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmAdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<AdditionalInfoDto> CreateNewRecordAsync(AdditionalInfoDto dto, UsersDto currentUser)
  {
    if (dto.AdditionalInfoId != 0)
      throw new InvalidCreateOperationException("AdditionalInfoId must be 0.");

    var entity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.AdditionalInfoId = await _repository.CrmAdditionalInfoes.CreateAndGetIdAsync(entity);
    //dto.CreatedDate = entity.CreatedDate;
    //dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, AdditionalInfoDto dto, bool trackChanges)
  {
    if (key != dto.AdditionalInfoId) return "Key mismatch.";

    bool exists = await _repository.CrmAdditionalInfoes.ExistsAsync(x => x.AdditionalInfoId == key);
    if (!exists) throw new GenericNotFoundException("AdditionalInfo", "AdditionalInfoId", key.ToString());

    var entity = MyMapper.JsonClone<AdditionalInfoDto, CrmAdditionalInfo>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmAdditionalInfoes.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"AdditionalInfo updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, AdditionalInfoDto dto)
  {
    if (key != dto.AdditionalInfoId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(AdditionalInfoDto));

    await _repository.CrmAdditionalInfoes.DeleteAsync(x => x.AdditionalInfoId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"AdditionalInfo deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<AdditionalInfoDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ai.AdditionalInfoId,
    ai.ApplicantId,
    ai.RequireAccommodation,
    ai.HealthNmedicalNeeds,
    ai.HealthNmedicalNeedsRemarks,
    ai.AdditionalInformationRemarks,
    ai.DocumentTitle,
    ai.UploadFile,
    ai.DocumentName,
    ai.FileThumbnail,
    ai.RecordType,
    ai.CreatedDate,
    ai.CreatedBy,
    ai.UpdatedDate,
    ai.UpdatedBy,
    app.ApplicationStatus
from AdditionalInfo ai
left join CrmApplication app on ai.ApplicantId = app.ApplicationId
";
    string orderBy = " ai.CreatedDate desc ";
    return await _repository.CrmAdditionalInfoes.GridData<AdditionalInfoDto>(sql, options, orderBy, "");
  }
}