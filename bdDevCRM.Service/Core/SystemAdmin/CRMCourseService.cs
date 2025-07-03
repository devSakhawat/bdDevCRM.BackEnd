using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class CRMCourseService : ICRMCourseService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CRMCourseService(
      IRepositoryManager repository,
      ILoggerManager logger,
      IConfiguration config,
      IHttpContextAccessor httpContextAccessor)
  {
    _repository = repository;
    _logger = logger;
    _config = config;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<CrmCourseDto>> GetCoursesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CRMCourse.GetActiveCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<Crmcourse, CrmCourseDto>(list);
  }

  public async Task<IEnumerable<CRMCourseDDLDto>> GetCourseByInstituteIdDDLAsync(int instituteId, bool trackChanges = false)
  {
    var list = await _repository.CRMCourse.ListByWhereWithSelectAsync(
      selector: x => new CRMCourseDDLDto { CourseId = x.CourseId, CourseTitle = x.CourseTitle }
      ,expression: x => x.InstituteId == instituteId, trackChanges: false);

    return list.Any() ? MyMapper.JsonCloneIEnumerableToList<CRMCourseDDLDto, CRMCourseDDLDto>(list) 
      : new List<CRMCourseDDLDto>();
  }

  public async Task<IEnumerable<CrmCourseDto>> GetActiveCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CRMCourse.GetActiveCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<Crmcourse, CrmCourseDto>(list);
  }

  public async Task<IEnumerable<CrmCourseDto>> GetCoursesAsync(bool trackChanges = false)
  {
    var list = await _repository.CRMCourse.GetCoursesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmCourse");
    return MyMapper.JsonCloneIEnumerableToList<Crmcourse, CrmCourseDto>(list);
  }

  public async Task<CrmCourseDto> GetCourseAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CRMCourse.GetCourseAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmCourse", "CourseId", id.ToString());
    return MyMapper.JsonClone<Crmcourse, CrmCourseDto>(entity);
  }

  public async Task<CrmCourseDto> CreateNewRecordAsync(CrmCourseDto dto, UsersDto currentUser)
  {
    if (dto.CourseId != 0)
      throw new InvalidCreateOperationException("CourseId must be 0.");

    bool dup = await _repository.CRMCourse.ExistsAsync(x => x.CourseTitle != null && x.CourseTitle.Trim().ToLower().Equals(dto.CourseTitle!.Trim().ToLower()));
    if (dup) throw new DuplicateRecordException("CrmCourse", "CourseTitle");

    var entity = MyMapper.JsonClone<CrmCourseDto, Crmcourse>(dto);
    dto.CourseId = await _repository.CRMCourse.CreateAndGetIdAsync(entity);

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, CrmCourseDto dto, bool trackChanges)
  {
    if (key != dto.CourseId) return "Key mismatch.";

    bool exists = await _repository.CRMCourse.ExistsAsync(x => x.CourseId == key);
    if (!exists) throw new GenericNotFoundException("CrmCourse", "CourseId", key.ToString());

    var entity = MyMapper.JsonClone<CrmCourseDto, Crmcourse>(dto);
    _repository.CRMCourse.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmCourse updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, CrmCourseDto dto)
  {
    if (key != dto.CourseId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CrmCourseDto));

    await _repository.CRMCourse.DeleteAsync(x => x.CourseId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmCourse deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<CrmCourseDto> GetCourseByTitleAsync(string title, bool trackChanges = false)
  {
    var entity = await _repository.CRMCourse.FirstOrDefaultAsync(x => x.CourseTitle == title);
    if (entity == null) throw new GenericNotFoundException("CrmCourse", "CourseTitle", title);
    return MyMapper.JsonClone<Crmcourse, CrmCourseDto>(entity);
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
left join CurrencyInfo curr on c.CurrencyId = curr.CurrencyId
";
    string orderBy = " CourseTitle asc ";
    return await _repository.CRMCourse.GridData<CrmCourseDto>(sql, options, orderBy, "");
  }
}