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

internal sealed class GMATInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IGMATInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<GMATInformationDto>> GetGmatinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.GMATInformation.GetActiveGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("GMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<GMATInformation, GMATInformationDto>(list);
  }

  public async Task<IEnumerable<GMATInformationDto>> GetActiveGmatinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.GMATInformation.GetActiveGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("GMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<GMATInformation, GMATInformationDto>(list);
  }

  public async Task<IEnumerable<GMATInformationDto>> GetGmatinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.GMATInformation.GetGmatinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("GMATInformation");
    return MyMapper.JsonCloneIEnumerableToList<GMATInformation, GMATInformationDto>(list);
  }

  public async Task<GMATInformationDto> GetGmatinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.GMATInformation.GetGmatinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("GMATInformation", "GmatinformationId", id.ToString());
    return MyMapper.JsonClone<GMATInformation, GMATInformationDto>(entity);
  }

  public async Task<GMATInformationDto> GetGmatinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.GMATInformation.GetGmatinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("GMATInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<GMATInformation, GMATInformationDto>(entity);
  }

  public async Task<GMATInformationDto> CreateNewRecordAsync(GMATInformationDto dto, UsersDto currentUser)
  {
    if (dto.GmatinformationId != 0)
      throw new InvalidCreateOperationException("GmatinformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.GMATInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("GMATInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<GMATInformationDto, GMATInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;

    dto.GmatinformationId = await _repository.GMATInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, GMATInformationDto dto, bool trackChanges)
  {
    if (key != dto.GmatinformationId) return "Key mismatch.";

    bool exists = await _repository.GMATInformation.ExistsAsync(x => x.GMATInformationId == key);
    if (!exists) throw new GenericNotFoundException("GMATInformation", "GmatinformationId", key.ToString());

    var entity = MyMapper.JsonClone<GMATInformationDto, GMATInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;

    _repository.GMATInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"GMATInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, GMATInformationDto dto)
  {
    if (key != dto.GmatinformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(GMATInformationDto));

    await _repository.GMATInformation.DeleteAsync(x => x.GMATInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"GMATInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<GMATInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    gi.GmatinformationId,
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
from GMATInformation gi
left join CrmApplication app on gi.ApplicantId = app.ApplicationId
";
    string orderBy = " gi.CreatedDate desc ";
    return await _repository.GMATInformation.GridData<GMATInformationDto>(sql, options, orderBy, "");
  }
}