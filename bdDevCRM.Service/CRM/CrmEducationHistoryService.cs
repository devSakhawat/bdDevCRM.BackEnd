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

internal sealed class CrmEducationHistoryService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmEducationHistoryService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmEducationHistories.GetActiveEducationHistoriesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmEducationHistory");
    return MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(list);
  }

  public async Task<IEnumerable<EducationHistoryDto>> GetActiveEducationHistoriesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmEducationHistories.GetActiveEducationHistoriesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmEducationHistory");
    return MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(list);
  }

  public async Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmEducationHistories.GetEducationHistoriesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmEducationHistory");
    return MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(list);
  }

  public async Task<EducationHistoryDto> GetEducationHistoryAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmEducationHistories.GetEducationHistoryAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmEducationHistory", "EducationHistoryId", id.ToString());
    return MyMapper.JsonClone<CrmEducationHistory, EducationHistoryDto>(entity);
  }

  public async Task<IEnumerable<EducationHistoryDto>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmEducationHistories.GetEducationHistoriesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<EducationHistoryDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmEducationHistory, EducationHistoryDto>(list);
  }

  public async Task<EducationHistoryDto> CreateNewRecordAsync(EducationHistoryDto dto, UsersDto currentUser)
  {
    if (dto.EducationHistoryId != 0)
      throw new InvalidCreateOperationException("EducationHistoryId must be 0.");

    var entity = MyMapper.JsonClone<EducationHistoryDto, CrmEducationHistory>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.EducationHistoryId = await _repository.CrmEducationHistories.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, EducationHistoryDto dto, bool trackChanges)
  {
    if (key != dto.EducationHistoryId) return "Key mismatch.";

    bool exists = await _repository.CrmEducationHistories.ExistsAsync(x => x.EducationHistoryId == key);
    if (!exists) throw new GenericNotFoundException("CrmEducationHistory", "EducationHistoryId", key.ToString());

    var entity = MyMapper.JsonClone<EducationHistoryDto, CrmEducationHistory>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmEducationHistories.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmEducationHistory updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, EducationHistoryDto dto)
  {
    if (key != dto.EducationHistoryId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(EducationHistoryDto));

    await _repository.CrmEducationHistories.DeleteAsync(x => x.EducationHistoryId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmEducationHistory deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<EducationHistoryDto> GetEducationHistoryByInstitutionAsync(string institution, bool trackChanges = false)
  {
    var entity = await _repository.CrmEducationHistories.GetEducationHistoryByInstitutionAsync(institution, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmEducationHistory", "Institution", institution);
    return MyMapper.JsonClone<CrmEducationHistory, EducationHistoryDto>(entity);
  }

  public async Task<GridEntity<EducationHistoryDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    eh.EducationHistoryId,
    eh.ApplicantId,
    eh.Institution,
    eh.Qualification,
    eh.PassingYear,
    eh.Grade,
    eh.AttachedDocument,
    eh.DocumentName,
    eh.PdfThumbnail,
    eh.CreatedDate,
    eh.CreatedBy,
    eh.UpdatedDate,
    eh.UpdatedBy,
    app.ApplicationStatus
from CrmEducationHistory eh
left join CrmApplication app on eh.ApplicantId = app.ApplicationId
";
    string orderBy = " eh.CreatedDate desc ";
    return await _repository.CrmEducationHistories.GridData<EducationHistoryDto>(sql, options, orderBy, "");
  }
}