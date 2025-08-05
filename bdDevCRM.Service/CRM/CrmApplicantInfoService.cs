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

internal sealed class CrmApplicantInfoService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmApplicantInfoService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantInfoes.GetActiveApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<IEnumerable<ApplicantInfoDto>> GetActiveApplicantInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantInfoes.GetActiveApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<IEnumerable<ApplicantInfoDto>> GetApplicantInfosAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantInfoes.GetApplicantInfosAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmApplicantInfo");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantInfo, ApplicantInfoDto>(list);
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantInfoes.GetApplicantInfoAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmApplicantInfo", "ApplicantId", id.ToString());
    return MyMapper.JsonClone<CrmApplicantInfo, ApplicantInfoDto>(entity);
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoByApplicationIdAsync(int applicationId, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantInfoes.GetApplicantInfoByApplicationIdAsync(applicationId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmApplicantInfo", "ApplicationId", applicationId.ToString());
    return MyMapper.JsonClone<CrmApplicantInfo, ApplicantInfoDto>(entity);
  }

  public async Task<ApplicantInfoDto> CreateNewRecordAsync(ApplicantInfoDto dto, UsersDto currentUser)
  {
    if (dto.ApplicantId != 0)
      throw new InvalidCreateOperationException("ApplicantId must be 0.");

    // Check for duplicate email
    if (!string.IsNullOrEmpty(dto.EmailAddress))
    {
      bool emailExists = await _repository.CrmApplicantInfoes.ExistsAsync(x => x.EmailAddress != null && x.EmailAddress.ToLower() == dto.EmailAddress.ToLower());
      if (emailExists) throw new DuplicateRecordException("CrmApplicantInfo", "EmailAddress");
    }

    // Check for duplicate application ID
    bool appExists = await _repository.CrmApplicantInfoes.ExistsAsync(x => x.ApplicationId == dto.ApplicationId);
    if (appExists) throw new DuplicateRecordException("CrmApplicantInfo", "ApplicationId");

    var entity = MyMapper.JsonClone<ApplicantInfoDto, CrmApplicantInfo>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.ApplicantId = await _repository.CrmApplicantInfoes.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, ApplicantInfoDto dto, bool trackChanges)
  {
    if (key != dto.ApplicantId) return "Key mismatch.";

    bool exists = await _repository.CrmApplicantInfoes.ExistsAsync(x => x.ApplicantId == key);
    if (!exists) throw new GenericNotFoundException("CrmApplicantInfo", "ApplicantId", key.ToString());

    var entity = MyMapper.JsonClone<ApplicantInfoDto, CrmApplicantInfo>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmApplicantInfoes.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmApplicantInfo updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, ApplicantInfoDto dto)
  {
    if (key != dto.ApplicantId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(ApplicantInfoDto));

    await _repository.CrmApplicantInfoes.DeleteAsync(x => x.ApplicantId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmApplicantInfo deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<ApplicantInfoDto> GetApplicantInfoByEmailAsync(string email, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantInfoes.GetApplicantInfoByEmailAsync(email, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmApplicantInfo", "EmailAddress", email);
    return MyMapper.JsonClone<CrmApplicantInfo, ApplicantInfoDto>(entity);
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
from CrmApplicantInfo ai
left join CrmApplication app on ai.ApplicationId = app.ApplicationId
";
    string orderBy = " ai.CreatedDate desc ";
    return await _repository.CrmApplicantInfoes.GridData<ApplicantInfoDto>(sql, options, orderBy, "");
  }
}