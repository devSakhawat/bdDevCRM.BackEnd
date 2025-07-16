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

internal sealed class ApplicantInfoService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IApplicantInfoService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantInfo.GetActiveApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<IEnumerable<ApplicantInfoDto>> GetActiveApplicantInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantInfo.GetActiveApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.ApplicantInfo.GetApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<ApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.ApplicantInfo.GetApplicantInfoAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantInfo", "ApplicantId", id.ToString());
    return MyMapper.JsonClone<ApplicantInfo, ApplicantInfoDto>(entity);
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoByApplicationIdAsync(int applicationId, bool trackChanges = false)
  {
    var entity = await _repository.ApplicantInfo.GetApplicantInfoByApplicationIdAsync(applicationId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantInfo", "ApplicationId", applicationId.ToString());
    return MyMapper.JsonClone<ApplicantInfo, ApplicantInfoDto>(entity);
  }

  public async Task<ApplicantInfoDto> CreateNewRecordAsync(ApplicantInfoDto dto, UsersDto currentUser)
  {
    if (dto.ApplicantId != 0)
      throw new InvalidCreateOperationException("ApplicantId must be 0.");

    // Check for duplicate email
    if (!string.IsNullOrEmpty(dto.EmailAddress))
    {
      bool emailExists = await _repository.ApplicantInfo.ExistsAsync(x => x.EmailAddress != null && x.EmailAddress.ToLower() == dto.EmailAddress.ToLower());
      if (emailExists) throw new DuplicateRecordException("ApplicantInfo", "EmailAddress");
    }

    // Check for duplicate application ID
    bool appExists = await _repository.ApplicantInfo.ExistsAsync(x => x.ApplicationId == dto.ApplicationId);
    if (appExists) throw new DuplicateRecordException("ApplicantInfo", "ApplicationId");

    var entity = MyMapper.JsonClone<ApplicantInfoDto, ApplicantInfo>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.ApplicantId = await _repository.ApplicantInfo.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, ApplicantInfoDto dto, bool trackChanges)
  {
    if (key != dto.ApplicantId) return "Key mismatch.";

    bool exists = await _repository.ApplicantInfo.ExistsAsync(x => x.ApplicantId == key);
    if (!exists) throw new GenericNotFoundException("ApplicantInfo", "ApplicantId", key.ToString());

    var entity = MyMapper.JsonClone<ApplicantInfoDto, ApplicantInfo>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.ApplicantInfo.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantInfo updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, ApplicantInfoDto dto)
  {
    if (key != dto.ApplicantId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(ApplicantInfoDto));

    await _repository.ApplicantInfo.DeleteAsync(x => x.ApplicantId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantInfo deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoByEmailAsync(string email, bool trackChanges = false)
  {
    var entity = await _repository.ApplicantInfo.GetApplicantInfoByEmailAsync(email, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantInfo", "EmailAddress", email);
    return MyMapper.JsonClone<ApplicantInfo, ApplicantInfoDto>(entity);
  }

  public async Task<GridEntity<ApplicantInfoDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ai.ApplicantId,
    ai.ApplicationId,
    ai.GenderId,
    ai.GenderName,
    ai.TitleValue,
    ai.TitleText,
    ai.FirstName,
    ai.LastName,
    ai.DateOfBirth,
    ai.MaritalStatusId,
    ai.MaritalStatusName,
    ai.Nationality,
    ai.HasValidPassport,
    ai.PassportNumber,
    ai.PassportIssueDate,
    ai.PassportExpiryDate,
    ai.PhoneCountryCode,
    ai.PhoneAreaCode,
    ai.PhoneNumber,
    ai.Mobile,
    ai.EmailAddress,
    ai.SkypeId,
    ai.ApplicantImagePath,
    ai.CreatedDate,
    ai.CreatedBy,
    ai.UpdatedDate,
    ai.UpdatedBy,
    app.ApplicationStatus,
    CONCAT(ai.TitleText, ' ', ai.FirstName, ' ', ai.LastName) as FullName
from ApplicantInfo ai
left join CrmApplication app on ai.ApplicationId = app.ApplicationId
";
    string orderBy = " ai.CreatedDate desc ";
    return await _repository.ApplicantInfo.GridData<ApplicantInfoDto>(sql, options, orderBy, "");
  }
}