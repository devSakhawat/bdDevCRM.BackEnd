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

internal sealed class CrmApplicantCourseService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmApplicantCourseService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantCourses.GetActiveApplicantCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantCourse, ApplicantCourseDto>(list);
  }

  public async Task<IEnumerable<ApplicantCourseDto>> GetActiveApplicantCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantCourses.GetActiveApplicantCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantCourse, ApplicantCourseDto>(list);
  }

  public async Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantCourses.GetApplicantCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ApplicantCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantCourse, ApplicantCourseDto>(list);
  }

  public async Task<ApplicantCourseDto> GetApplicantCourseAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantCourses.GetApplicantCourseAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantCourse", "ApplicantCourseId", id.ToString());
    return MyMapper.JsonClone<CrmApplicantCourse, ApplicantCourseDto>(entity);
  }

  public async Task<IEnumerable<ApplicantCourseDto>> GetApplicantCoursesByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var list = await _repository.CrmApplicantCourses.GetApplicantCoursesByApplicantIdAsync(applicantId, trackChanges);
    if (!list.Any()) return new List<ApplicantCourseDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmApplicantCourse, ApplicantCourseDto>(list);
  }

  public async Task<ApplicantCourseDto> CreateNewRecordAsync(ApplicantCourseDto dto, UsersDto currentUser)
  {
    if (dto.ApplicantCourseId != 0)
      throw new InvalidCreateOperationException("ApplicantCourseId must be 0.");

    // Check for duplicate applicant-course combination
    bool dup = await _repository.CrmApplicantCourses.ExistsAsync(x => x.ApplicantId == dto.ApplicantId && x.CourseId == dto.CourseId);
    if (dup) throw new DuplicateRecordException("ApplicantCourse", "ApplicantId-CourseId combination");

    var entity = MyMapper.JsonClone<ApplicantCourseDto, CrmApplicantCourse>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.ApplicantCourseId = await _repository.CrmApplicantCourses.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, ApplicantCourseDto dto, bool trackChanges)
  {
    if (key != dto.ApplicantCourseId) return "Key mismatch.";

    bool exists = await _repository.CrmApplicantCourses.ExistsAsync(x => x.ApplicantCourseId == key);
    if (!exists) throw new GenericNotFoundException("ApplicantCourse", "ApplicantCourseId", key.ToString());

    var entity = MyMapper.JsonClone<ApplicantCourseDto, CrmApplicantCourse>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmApplicantCourses.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantCourse updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, ApplicantCourseDto dto)
  {
    if (key != dto.ApplicantCourseId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(ApplicantCourseDto));

    await _repository.CrmApplicantCourses.DeleteAsync(x => x.ApplicantCourseId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"ApplicantCourse deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<ApplicantCourseDto> GetApplicantCourseByApplicantAndCourseIdAsync(int applicantId, int courseId, bool trackChanges = false)
  {
    var entity = await _repository.CrmApplicantCourses.GetApplicantCourseByApplicantAndCourseIdAsync(applicantId, courseId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ApplicantCourse", "ApplicantId-CourseId", $"{applicantId}-{courseId}");
    return MyMapper.JsonClone<CrmApplicantCourse, ApplicantCourseDto>(entity);
  }

  public async Task<GridEntity<ApplicantCourseDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ac.ApplicantCourseId,
    ac.ApplicantId,
    ac.CourseId,
    ac.CountryId,
    ac.CountryName,
    ac.InstituteId,
    ac.InstituteName,
    ac.CourseTitle,
    ac.IntakeMonthId,
    ac.IntakeMonth,
    ac.IntakeYearId,
    ac.IntakeYear,
    ac.ApplicationFee,
    ac.CurrencyId,
    ac.CurrencyName,
    ac.PaymentMethodId,
    ac.PaymentMethod,
    ac.PaymentReferenceNumber,
    ac.PaymentDate,
    ac.Remarks,
    ac.CreatedDate,
    ac.CreatedBy,
    ac.UpdatedDate,
    ac.UpdatedBy,
    app.ApplicationStatus,
    c.CourseTitle as CourseFullTitle
from ApplicantCourse ac
left join CrmApplication app on ac.ApplicantId = app.ApplicationId
left join CrmCourse c on ac.CourseId = c.CourseId
";
    string orderBy = " ac.CreatedDate desc ";
    return await _repository.CrmApplicantCourses.GridData<ApplicantCourseDto>(sql, options, orderBy, "");
  }
}