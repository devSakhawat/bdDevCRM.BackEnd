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

internal sealed class WorkExperienceService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IWorkExperienceService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.WorkExperience.GetActiveWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("WorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<WorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetActiveWorkExperiencesAsync(bool trackChanges = false)
  {
    var list = await _repository.WorkExperience.GetActiveWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("WorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<WorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesAsync(bool trackChanges = false)
  {
    var list = await _repository.WorkExperience.GetWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("WorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<WorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<WorkExperienceHistoryDto> GetWorkExperienceAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.WorkExperience.GetWorkExperienceAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("WorkExperience", "WorkExperienceId", id.ToString());
    return MyMapper.JsonClone<WorkExperience, WorkExperienceHistoryDto>(entity);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.WorkExperience.GetWorkExperiencesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<WorkExperienceHistoryDto>();
    return MyMapper.JsonCloneIEnumerableToList<WorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<WorkExperienceHistoryDto> CreateNewRecordAsync(WorkExperienceHistoryDto dto, UsersDto currentUser)
  {
    if (dto.WorkExperienceId != 0)
      throw new InvalidCreateOperationException("WorkExperienceId must be 0.");

    var entity = MyMapper.JsonClone<WorkExperienceHistoryDto, WorkExperience>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;

    dto.WorkExperienceId = await _repository.WorkExperience.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    _logger.LogInfo($"New WorkExperience created, id={dto.WorkExperienceId}");

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, WorkExperienceHistoryDto dto, bool trackChanges)
  {
    if (key != dto.WorkExperienceId) return "Key mismatch.";

    bool exists = await _repository.WorkExperience.ExistsAsync(x => x.WorkExperienceId == key);
    if (!exists) throw new GenericNotFoundException("WorkExperience", "WorkExperienceId", key.ToString());

    var entity = MyMapper.JsonClone<WorkExperienceHistoryDto, WorkExperience>(dto);
    entity.UpdatedDate = DateTime.UtcNow;

    _repository.WorkExperience.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"WorkExperience updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, WorkExperienceHistoryDto dto)
  {
    if (key != dto.WorkExperienceId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(WorkExperienceHistoryDto));

    await _repository.WorkExperience.DeleteAsync(x => x.WorkExperienceId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"WorkExperience deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<WorkExperienceHistoryDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    we.WorkExperienceId,
    we.ApplicantId,
    we.NameOfEmployer,
    we.Position,
    we.StartDate,
    we.EndDate,
    we.Period,
    we.MainResponsibility,
    we.ScannedCopy,
    we.DocumentName,
    we.FileThumbnail,
    we.CreatedDate,
    we.CreatedBy,
    we.UpdatedDate,
    we.UpdatedBy,
    app.ApplicationStatus
from WorkExperience we
left join CrmApplication app on we.ApplicantId = app.ApplicationId
";
    string orderBy = " we.CreatedDate desc ";
    return await _repository.WorkExperience.GridData<WorkExperienceHistoryDto>(sql, options, orderBy, "");
  }

}