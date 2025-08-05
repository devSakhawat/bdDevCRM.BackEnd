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

internal sealed class CrmCourseService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmCourseService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<CrmCourseDto>> GetCoursesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmCourses.GetActiveCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmCourse, CrmCourseDto>(list);
  }

  public async Task<IEnumerable<CRMCourseDDLDto>> GetCourseByInstituteIdDDLAsync(int instituteId, bool trackChanges = false)
  {

    IEnumerable<CRMCourseDDLDto> list = await _repository.CrmCourses.ListByWhereWithSelectAsync(
      expression: x => x.InstituteId == instituteId,
      selector: x => new CRMCourseDDLDto
      {
        CourseId = x.CourseId,
        CourseTitle = x.CourseTitle
      },
      orderBy: x => x.CourseTitle,
      trackChanges: trackChanges
      );

    if (!list.Any()) return new List<CRMCourseDDLDto>();
    return list;
  }

  public async Task<IEnumerable<CrmCourseDto>> GetActiveCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmCourses.GetActiveCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmCourse, CrmCourseDto>(list);
  }

  public async Task<IEnumerable<CrmCourseDto>> GetCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmCourses.GetCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<CrmCourse, CrmCourseDto>(list);
  }

  public async Task<CrmCourseDto> GetCourseAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmCourses.GetCourseAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmCourse", "CourseId", id.ToString());
    return MyMapper.JsonClone<CrmCourse, CrmCourseDto>(entity);
  }

  public async Task<CrmCourseDto> CreateNewRecordAsync(CrmCourseDto dto, UsersDto currentUser)
  {
    if (dto.CourseId != 0)
      throw new InvalidCreateOperationException("CourseId must be 0.");

    bool dup = await _repository.CrmCourses.ExistsAsync(x => x.CourseTitle != null && x.CourseTitle.Trim().ToLower().Equals(dto.CourseTitle!.Trim().ToLower()));
    if (dup) throw new DuplicateRecordException("CrmCourse", "CourseTitle");

    var entity = MyMapper.JsonClone<CrmCourseDto, CrmCourse>(dto);
    dto.CourseId = await _repository.CrmCourses.CreateAndGetIdAsync(entity);

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, CrmCourseDto dto, bool trackChanges)
  {
    if (key != dto.CourseId) return "Key mismatch.";

    bool exists = await _repository.CrmCourses.ExistsAsync(x => x.CourseId == key);
    if (!exists) throw new GenericNotFoundException("CrmCourse", "CourseId", key.ToString());

    var entity = MyMapper.JsonClone<CrmCourseDto, CrmCourse>(dto);
    _repository.CrmCourses.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmCourse updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, CrmCourseDto dto)
  {
    if (key != dto.CourseId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CrmCourseDto));

    await _repository.CrmCourses.DeleteAsync(x => x.CourseId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmCourse deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<CrmCourseDto> GetCourseByTitleAsync(string title, bool trackChanges = false)
  {
    var entity = await _repository.CrmCourses.FirstOrDefaultAsync(x => x.CourseTitle == title);
    if (entity == null) throw new GenericNotFoundException("CrmCourse", "CourseTitle", title);
    return MyMapper.JsonClone<CrmCourse, CrmCourseDto>(entity);
  }

  public async Task<GridEntity<CrmCourseDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    c.CourseId,
    c.InstituteId,
    c.CourseTitle,
    c.CourseLevel,
    c.CourseFee,
    c.ApplicationFee,
    c.CurrencyId,
    c.MonthlyLivingCost,
    c.PartTimeWorkDetails,
    c.StartDate,
    c.EndDate,
    c.CourseBenefits,
    c.LanguagesRequirement,
    c.CourseDuration,
    c.CourseCategory,
    c.AwardingBody,
    c.AdditionalInformationOfCourse,
    c.GeneralEligibility,
    c.FundsRequirementforVisa,
    c.InstitutionalBenefits,
    c.VisaRequirement,
    c.CountryBenefits,
    c.KeyModules,
    c.Status,
    c.After2YearsPswcompletingCourse,
    c.DocumentId,
    i.InstituteName,
    curr.CurrencyName
from CrmCourse c
left join CrmInstitute i on c.InstituteId = i.InstituteId
left join CrmCurrencyInfo curr on c.CurrencyId = curr.CurrencyId
";
    string orderBy = " CourseTitle asc ";
    return await _repository.CrmCourses.GridData<CrmCourseDto>(sql, options, orderBy, "");
  }
}