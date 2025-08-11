using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.CRM;

internal sealed class CrmMonthService : ICrmMonthService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;

  public CrmMonthService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config)
  {
    _repository = repository;
    _logger = logger;
    _config = config;
  }

  public async Task<IEnumerable<CrmMonthDto>> GetMonthsDDLAsync(bool trackChanges = false)
  {
    var months = await _repository.CrmMonths.GetActiveMonthAsync(trackChanges);
    return MyMapper.JsonCloneIEnumerableToList<CrmMonth, CrmMonthDto>(months);
  }

  public async Task<IEnumerable<CrmMonthDto>> GetActiveMonthsAsync(bool trackChanges = false)
  {
    var months = await _repository.CrmMonths.GetActiveMonthAsync(trackChanges);
    return MyMapper.JsonCloneIEnumerableToList<CrmMonth, CrmMonthDto>(months);
  }

  public async Task<IEnumerable<CrmMonthDto>> GetMonthsAsync(bool trackChanges = false)
  {
    var months = await _repository.CrmMonths.ListAsync(x => x.MonthId, trackChanges);
    return MyMapper.JsonCloneIEnumerableToList<CrmMonth, CrmMonthDto>(months);
  }

  public async Task<CrmMonthDto> GetMonthAsync(int id, bool trackChanges = false)
  {
    var month = await _repository.CrmMonths.GetByIdAsync(x => x.MonthId == id, trackChanges);
    if (month == null)
      throw new GenericNotFoundException(
        $"Month with id: {id} doesn't exist in the database.",
        nameof(CrmMonthDto),
        id.ToString()
      );

    return MyMapper.JsonClone<CrmMonth, CrmMonthDto>(month);
  }

  public async Task<CrmMonthDto> CreateNewRecordAsync(CrmMonthDto dto, UsersDto currentUser)
  {
    var monthEntity = MyMapper.JsonClone<CrmMonthDto, CrmMonth>(dto);
    //monthEntity.CreatedDate = DateTime.UtcNow;
    //monthEntity.CreatedBy = currentUser.UserId ?? 0;
    //monthEntity.IsActive = true;

    await _repository.CrmMonths.CreateAsync(monthEntity);
    await _repository.SaveAsync();

    return MyMapper.JsonClone<CrmMonth, CrmMonthDto>(monthEntity);
  }

  public async Task<string> UpdateRecordAsync(int key, CrmMonthDto dto, bool trackChanges)
  {
    var monthEntity = await _repository.CrmMonths.GetByIdAsync(x => x.MonthId == key, trackChanges);

    if (monthEntity == null)
      throw new GenericNotFoundException(
        $"Month with id: {key} doesn't exist in the database.",
        nameof(key),
        key.ToString()
      );

    monthEntity.MonthName = dto.MonthName;
    monthEntity.MonthCode = dto.MonthCode;
    //monthEntity.MonthNumber = dto.MonthNumber;
    //monthEntity.Description = dto.Description;
    //monthEntity.IsActive = dto.IsActive;
    //monthEntity.UpdatedDate = DateTime.UtcNow;
    //monthEntity.UpdatedBy = dto.UpdatedBy;

    await _repository.SaveAsync();
    return "Month updated successfully.";
  }

  public async Task<string> DeleteRecordAsync(int key, CrmMonthDto dto)
  {
    var monthEntity = await _repository.CrmMonths.GetByIdAsync(x => x.MonthId == key, false);

    if (monthEntity == null)
      throw new GenericNotFoundException(
        $"Month with id: {key} doesn't exist in the database.",
        nameof(key),
        key.ToString()
      );

    _repository.CrmMonths.Delete(monthEntity);
    await _repository.SaveAsync();

    return "Month deleted successfully.";
  }

  public async Task<string> SaveOrUpdate(int key, CrmMonthDto modelDto, UsersDto currentUser)
  {
    return key == 0 
      ? (await CreateNewRecordAsync(modelDto, currentUser)).MonthId > 0 ? "Month created successfully." : "Failed to create month."
      : await UpdateRecordAsync(key, modelDto, trackChanges: false);
  }

  public async Task<GridEntity<CrmMonthDto>> SummaryGrid(CRMGridOptions options)
  {
    string condition = string.Empty;
    string sql = @"
      SELECT 
        MonthId,
        MonthName,
        MonthCode,
        MonthNumber,
        Description,
        IsActive,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy
      FROM CrmMonth";

    string orderBy = " MonthNumber asc ";
    return await _repository.CrmMonths.GridData<CrmMonthDto>(sql, options, orderBy, condition);
  }

  public async Task<CrmMonthDto> CreateMonthAsync(CrmMonthDto entityForCreate)
  {
    var monthEntity = MyMapper.JsonClone<CrmMonthDto, CrmMonth>(entityForCreate);
    //monthEntity.CreatedDate = DateTime.UtcNow;
    //monthEntity.IsActive = true;

    await _repository.CrmMonths.CreateAsync(monthEntity);
    await _repository.SaveAsync();

    return MyMapper.JsonClone<CrmMonth, CrmMonthDto>(monthEntity);
  }

  public async Task<IEnumerable<CrmMonthDto>> GetMonthsByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var applicantCourses = await _repository.CrmApplicantCourses.GetApplicantCoursesByApplicantIdAsync(applicantId, trackChanges);
    
    if (!applicantCourses.Any())
    {
      return new List<CrmMonthDto>(); 
    }

    var intakeMonthIds = applicantCourses
      .Where(ac => ac.IntakeMonthId > 0) 
      .Select(ac => ac.IntakeMonthId)
      .Distinct()
      .ToList();

    if (!intakeMonthIds.Any())
    {
      return new List<CrmMonthDto>();
    }

    var months = await _repository.CrmMonths.ListByConditionAsync(
      x => intakeMonthIds.Contains(x.MonthId),
      x => x.MonthName,
      trackChanges
    );

    return MyMapper.JsonCloneIEnumerableToList<CrmMonth, CrmMonthDto>(months);
  }
}