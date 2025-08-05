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

internal sealed class CrmGMATInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmGmatinformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<GMATInformationDto>> GetGmatinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmGMATInformations.GetActiveGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmGMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmGMATInformation, GMATInformationDto>(list);
  }

  public async Task<IEnumerable<GMATInformationDto>> GetActiveGmatinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmGMATInformations.GetActiveGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmGMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmGMATInformation, GMATInformationDto>(list);
  }

  public async Task<IEnumerable<GMATInformationDto>> GetGmatinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmGMATInformations.GetGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmGMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmGMATInformation, GMATInformationDto>(list);
  }

  public async Task<GMATInformationDto> GetGmatinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmGMATInformations.GetGmatinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmGMATInformation", "GMATInformationId", id.ToString());
    return MyMapper.JsonClone<CrmGMATInformation, GMATInformationDto>(entity);
  }

  public async Task<GMATInformationDto> GetGmatinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmGMATInformations.GetGmatinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmGMATInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmGMATInformation, GMATInformationDto>(entity);
  }

  public async Task<GMATInformationDto> CreateNewRecordAsync(GMATInformationDto dto, UsersDto currentUser)
  {
    if (dto.GMATInformationId != 0)
      throw new InvalidCreateOperationException("GMATInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmGMATInformations.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmGMATInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<GMATInformationDto, CrmGMATInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;

    dto.GMATInformationId = await _repository.CrmGMATInformations.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, GMATInformationDto dto, bool trackChanges)
  {
    if (key != dto.GMATInformationId) return "Key mismatch.";

    bool exists = await _repository.CrmGMATInformations.ExistsAsync(x => x.GMATInformationId == key);
    if (!exists) throw new GenericNotFoundException("CrmGMATInformation", "GMATInformationId", key.ToString());

    var entity = MyMapper.JsonClone<GMATInformationDto, CrmGMATInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;

    _repository.CrmGMATInformations.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmGMATInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, GMATInformationDto dto)
  {
    if (key != dto.GMATInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(GMATInformationDto));

    await _repository.CrmGMATInformations.DeleteAsync(x => x.GMATInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmGMATInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<GMATInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    gi.GMATInformationId,
    gi.ApplicantId,
    gi.Gmatlistening,
    gi.Gmatreading,
    gi.Gmatwriting,
    gi.Gmatspeaking,
    gi.GmatoverallScore,
    gi.Gmatdate,
    gi.GmatscannedCopyPath,
    gi.GmatadditionalInformation,
    gi.CreatedDate,
    gi.CreatedBy,
    gi.UpdatedDate,
    gi.UpdatedBy,
    app.ApplicationStatus
from CrmGMATInformation gi
left join CrmApplication app on gi.ApplicantId = app.ApplicationId
";
    string orderBy = " gi.CreatedDate desc ";
    return await _repository.CrmGMATInformations.GridData<GMATInformationDto>(sql, options, orderBy, "");
  }
}