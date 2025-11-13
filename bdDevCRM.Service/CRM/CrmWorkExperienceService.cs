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

internal sealed class CrmWorkExperienceService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmWorkExperienceService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmWorkExperiences.GetActiveWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmWorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetActiveWorkExperiencesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmWorkExperiences.GetActiveWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmWorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmWorkExperiences.GetWorkExperiencesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmWorkExperience");
    return MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<WorkExperienceHistoryDto> GetWorkExperienceAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmWorkExperiences.GetWorkExperienceAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmWorkExperience", "WorkExperienceId", id.ToString());
    return MyMapper.JsonClone<CrmWorkExperience, WorkExperienceHistoryDto>(entity);
  }

  public async Task<IEnumerable<WorkExperienceHistoryDto>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmWorkExperiences.GetWorkExperiencesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<WorkExperienceHistoryDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmWorkExperience, WorkExperienceHistoryDto>(list);
  }

  public async Task<WorkExperienceHistoryDto> CreateNewRecordAsync(WorkExperienceHistoryDto dto, UsersDto currentUser)
  {
    if (dto.WorkExperienceId != 0)
      throw new InvalidCreateOperationException("WorkExperienceId must be 0.");

    var entity = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;

    dto.WorkExperienceId = await _repository.CrmWorkExperiences.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    _logger.LogInfo($"New CrmWorkExperience created, id={dto.WorkExperienceId}");

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, WorkExperienceHistoryDto dto, bool trackChanges)
  {
    if (key != dto.WorkExperienceId) return "Key mismatch.";

    bool exists = await _repository.CrmWorkExperiences.ExistsAsync(x => x.WorkExperienceId == key);
    if (!exists) throw new GenericNotFoundException("CrmWorkExperience", "WorkExperienceId", key.ToString());

    var entity = MyMapper.JsonClone<WorkExperienceHistoryDto, CrmWorkExperience>(dto);
    entity.UpdatedDate = DateTime.UtcNow;

    _repository.CrmWorkExperiences.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmWorkExperience updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, WorkExperienceHistoryDto dto)
  {
    if (key != dto.WorkExperienceId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(WorkExperienceHistoryDto));

    await _repository.CrmWorkExperiences.DeleteAsync(x => x.WorkExperienceId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmWorkExperience deleted, id={key}");
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
from CrmWorkExperience we
left join CrmApplication app on we.ApplicantId = app.ApplicationId
";
    string orderBy = " we.CreatedDate desc ";
    return await _repository.CrmWorkExperiences.GridData<WorkExperienceHistoryDto>(sql, options, orderBy, "");
  }

}