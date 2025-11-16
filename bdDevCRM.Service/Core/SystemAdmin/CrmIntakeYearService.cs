using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class CrmIntakeYearService : ICrmIntakeYearService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;

  public CrmIntakeYearService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config)
  {
    _repository = repository;
    _logger = logger;
    _config = config;
  }

  public async Task<IEnumerable<CrmIntakeYearDDL>> GetIntakeYearsDDLAsync(bool trackChanges)
  {
    var intakeYears = await _repository.CrmIntakeYears.GetActiveIntakeYearsAsync(trackChanges);
    var intakeYearsDto = intakeYears.Select(x => new CrmIntakeYearDDL
    {
      IntakeYearId = x.IntakeYearId,
      YearName = x.YearName,
      YearCode = x.YearCode,
      YearValue = x.YearValue
    });
    return intakeYearsDto;
  }

  public async Task<GridEntity<CrmIntakeYearDto>> SummaryGrid(bool trackChanges, CRMGridOptions options, UsersDto user)
  {
    string condition = string.Empty;
    string sql = @"
      SELECT IntakeYearId, YearName, YearCode, YearValue, Description, 
             IsActive, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
      FROM CrmIntakeYear";
    string orderBy = " YearValue desc ";
    return await _repository.CrmIntakeYears.GridData<CrmIntakeYearDto>(sql, options, orderBy, condition);
  }

  public async Task<string> CreateNewRecordAsync(CrmIntakeYearDto modelDto)
  {
    var entityForDb = MyMapper.JsonClone<CrmIntakeYearDto, CrmIntakeYear>(modelDto);
    entityForDb.CreatedDate = DateTime.UtcNow;
    entityForDb.IsActive = true;

    _repository.CrmIntakeYears.CreateIntakeYear(entityForDb);
    await _repository.SaveAsync();
    return "Intake Year created successfully.";
  }

  public async Task<string> UpdateNewRecordAsync(int key, CrmIntakeYearDto modelDto, bool trackChanges)
  {
    var entityForDb = await _repository.CrmIntakeYears.GetIntakeYearByIdAsync(key, trackChanges);
    if (entityForDb is null)
      throw new GenericNotFoundException(
          $"Intake Year with id: {key} doesn't exist in the database.",
          nameof(key),
          key.ToString()
      );

    entityForDb.YearName = modelDto.YearName;
    entityForDb.YearCode = modelDto.YearCode;
    entityForDb.YearValue = modelDto.YearValue;
    entityForDb.Description = modelDto.Description;
    entityForDb.IsActive = modelDto.IsActive;
    entityForDb.UpdatedDate = DateTime.UtcNow;
    entityForDb.UpdatedBy = modelDto.UpdatedBy;

    await _repository.SaveAsync();
    return "Intake Year updated successfully.";
  }

  public async Task<string> DeleteRecordAsync(int key, CrmIntakeYearDto modelDto)
  {
    var entityForDb = await _repository.CrmIntakeYears.GetIntakeYearByIdAsync(key, trackChanges: false);
    if (entityForDb is null)
      throw new GenericNotFoundException(
          $"Intake Year with id: {key} doesn't exist in the database.",
          nameof(key),
          key.ToString()
      );

    _repository.CrmIntakeYears.DeleteIntakeYear(entityForDb);
    await _repository.SaveAsync();
    return "Intake Year deleted successfully.";
  }

  public async Task<string> SaveOrUpdate(int key, CrmIntakeYearDto modelDto)
  {
    return key == 0 
      ? await CreateNewRecordAsync(modelDto) 
      : await UpdateNewRecordAsync(key, modelDto, trackChanges: false);
  }

  public async Task<IEnumerable<CrmIntakeYearDto>> GetIntakeYearsAsync(bool trackChanges)
  {
    var intakeYears = await _repository.CrmIntakeYears.GetActiveIntakeYearsAsync(trackChanges);
    var intakeYearsDto = MyMapper.JsonCloneIEnumerableToList<CrmIntakeYear, CrmIntakeYearDto>(intakeYears);
    return intakeYearsDto;
  }

  public async Task<CrmIntakeYearDto> GetIntakeYearAsync(int intakeYearId, bool trackChanges)
  {
    var intakeYear = await _repository.CrmIntakeYears.GetIntakeYearByIdAsync(intakeYearId, trackChanges);
    if (intakeYear is null)
      throw new GenericNotFoundException(
          $"Intake Year with id: {intakeYearId} doesn't exist in the database.",
          nameof(CrmIntakeYearDto),
          intakeYearId.ToString()
      );

    var intakeYearDto = MyMapper.JsonClone<CrmIntakeYear, CrmIntakeYearDto>(intakeYear);
    return intakeYearDto;
  }

  public async Task<CrmIntakeYearDto> CreateIntakeYearAsync(CrmIntakeYearDto entityForCreate)
  {
    var intakeYearEntity = MyMapper.JsonClone<CrmIntakeYearDto, CrmIntakeYear>(entityForCreate);
    intakeYearEntity.CreatedDate = DateTime.UtcNow;
    intakeYearEntity.IsActive = true;

    _repository.CrmIntakeYears.CreateIntakeYear(intakeYearEntity);
    await _repository.SaveAsync();

    var intakeYearToReturn = MyMapper.JsonClone<CrmIntakeYear, CrmIntakeYearDto>(intakeYearEntity);
    return intakeYearToReturn;
  }
}