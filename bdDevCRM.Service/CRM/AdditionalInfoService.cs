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

internal sealed class AdditionalInfoService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IAdditionalInfoService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.AdditionalInfo.GetActiveAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<AdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetActiveAdditionalInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.AdditionalInfo.GetActiveAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<AdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.AdditionalInfo.GetAdditionalInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("AdditionalInfo");
    return MyMapper.JsonCloneIEnumerableToList<AdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<AdditionalInfoDto> GetAdditionalInfoAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.AdditionalInfo.GetAdditionalInfoAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("AdditionalInfo", "AdditionalInfoId", id.ToString());
    return MyMapper.JsonClone<AdditionalInfo, AdditionalInfoDto>(entity);
  }

  public async Task<IEnumerable<AdditionalInfoDto>> GetAdditionalInfosByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.AdditionalInfo.GetAdditionalInfosByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<AdditionalInfoDto>();
    return MyMapper.JsonCloneIEnumerableToList<AdditionalInfo, AdditionalInfoDto>(list);
  }

  public async Task<AdditionalInfoDto> CreateNewRecordAsync(AdditionalInfoDto dto, UsersDto currentUser)
  {
    if (dto.AdditionalInfoId != 0)
      throw new InvalidCreateOperationException("AdditionalInfoId must be 0.");

    var entity = MyMapper.JsonClone<AdditionalInfoDto, AdditionalInfo>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.AdditionalInfoId = await _repository.AdditionalInfo.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, AdditionalInfoDto dto, bool trackChanges)
  {
    if (key != dto.AdditionalInfoId) return "Key mismatch.";

    bool exists = await _repository.AdditionalInfo.ExistsAsync(x => x.AdditionalInfoId == key);
    if (!exists) throw new GenericNotFoundException("AdditionalInfo", "AdditionalInfoId", key.ToString());

    var entity = MyMapper.JsonClone<AdditionalInfoDto, AdditionalInfo>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.AdditionalInfo.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"AdditionalInfo updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, AdditionalInfoDto dto)
  {
    if (key != dto.AdditionalInfoId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(AdditionalInfoDto));

    await _repository.AdditionalInfo.DeleteAsync(x => x.AdditionalInfoId == key, true);
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
    return await _repository.AdditionalInfo.GridData<AdditionalInfoDto>(sql, options, orderBy, "");
  }
}