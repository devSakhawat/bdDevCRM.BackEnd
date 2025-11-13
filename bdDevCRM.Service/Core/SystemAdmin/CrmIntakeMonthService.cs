using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class CrmIntakeMonthService : ICrmIntakeMonthService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;

  public CrmIntakeMonthService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config)
  {
    _repository = repository;
    _logger = logger;
    _config = config;
  }

  public async Task<IEnumerable<CrmIntakeMonthDDL>> GetIntakeMonthsDDLAsync(bool trackChanges)
  {
    var intakeMonths = await _repository.CrmIntakeMonths.GetActiveIntakeMonthsAsync(trackChanges);
    var intakeMonthsDto = intakeMonths.Select(x => new CrmIntakeMonthDDL
    {
      IntakeMonthId = x.IntakeMonthId,
      MonthName = x.MonthName,
      MonthCode = x.MonthCode,
      MonthNumber = x.MonthNumber
    });
    return intakeMonthsDto;
  }

  public async Task<GridEntity<CrmIntakeMonthDto>> SummaryGrid(CRMGridOptions options)
  {
    string condition = string.Empty;
    string sql = @"
      SELECT IntakeMonthId, MonthName, MonthCode, MonthNumber, Description, 
             IsActive, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
      FROM CrmIntakeMonth";
    string orderBy = " MonthNumber asc ";
    return await _repository.CrmIntakeMonths.GridData<CrmIntakeMonthDto>(sql, options, orderBy, condition);
  }

  public async Task<string> CreateNewRecordAsync(CrmIntakeMonthDto modelDto)
  {
    var entityForDb = MyMapper.JsonClone<CrmIntakeMonthDto, CrmIntakeMonth>(modelDto);
    entityForDb.CreatedDate = DateTime.UtcNow;
    entityForDb.IsActive = true;

    _repository.CrmIntakeMonths.CreateIntakeMonth(entityForDb);
    await _repository.SaveAsync();
    return "Intake Month created successfully.";
  }

  public async Task<string> UpdateNewRecordAsync(int key, CrmIntakeMonthDto modelDto, bool trackChanges)
  {
    var entityForDb = await _repository.CrmIntakeMonths.GetIntakeMonthByIdAsync(key, trackChanges);
    if (entityForDb is null)
      throw new GenericNotFoundException($"Intake Month with id: {key} doesn't exist in the database."
        ,nameof(CrmIntakeMonthDto)
        ,key.ToString()
     );


    entityForDb.MonthName = modelDto.MonthName;
    entityForDb.MonthCode = modelDto.MonthCode;
    entityForDb.MonthNumber = modelDto.MonthNumber;
    entityForDb.Description = modelDto.Description;
    entityForDb.IsActive = modelDto.IsActive;
    entityForDb.UpdatedDate = DateTime.UtcNow;
    entityForDb.UpdatedBy = modelDto.UpdatedBy;

    await _repository.SaveAsync();
    return "Intake Month updated successfully.";
  }

  public async Task<string> DeleteRecordAsync(int key, CrmIntakeMonthDto modelDto)
  {
    var entityForDb = await _repository.CrmIntakeMonths.GetIntakeMonthByIdAsync(key, trackChanges: false);
    if (entityForDb is null)
      throw new GenericNotFoundException($"Intake Month with id: {key} doesn't exist in the database."
        ,nameof(CrmIntakeMonthDto)
        ,key.ToString()
       );

    _repository.CrmIntakeMonths.DeleteIntakeMonth(entityForDb);
    await _repository.SaveAsync();
    return "Intake Month deleted successfully.";
  }

  public async Task<string> SaveOrUpdate(int key, CrmIntakeMonthDto modelDto)
  {
    return key == 0 
      ? await CreateNewRecordAsync(modelDto) 
      : await UpdateNewRecordAsync(key, modelDto, trackChanges: false);
  }

  public async Task<IEnumerable<CrmIntakeMonthDto>> GetIntakeMonthsAsync(bool trackChanges)
  {
    var intakeMonths = await _repository.CrmIntakeMonths.GetActiveIntakeMonthsAsync(trackChanges);
    var intakeMonthsDto = MyMapper.JsonCloneIEnumerableToList<CrmIntakeMonth, CrmIntakeMonthDto>(intakeMonths);
    return intakeMonthsDto;
  }

  public async Task<CrmIntakeMonthDto> GetIntakeMonthAsync(int intakeMonthId, bool trackChanges)
  {
    var intakeMonth = await _repository.CrmIntakeMonths.GetIntakeMonthByIdAsync(intakeMonthId, trackChanges);
    if (intakeMonth is null)
      throw new GenericNotFoundException($"Intake Month with id: {intakeMonthId} doesn't exist in the database."
        ,nameof(CrmIntakeMonthDto)
        ,intakeMonthId.ToString()
        );
    
    var intakeMonthDto = MyMapper.JsonClone<CrmIntakeMonth, CrmIntakeMonthDto>(intakeMonth);
    return intakeMonthDto;
  }

  public async Task<CrmIntakeMonthDto> CreateIntakeMonthAsync(CrmIntakeMonthDto entityForCreate)
  {
    var intakeMonthEntity = MyMapper.JsonClone<CrmIntakeMonthDto, CrmIntakeMonth>(entityForCreate);
    intakeMonthEntity.CreatedDate = DateTime.UtcNow;
    intakeMonthEntity.IsActive = true;

    _repository.CrmIntakeMonths.CreateIntakeMonth(intakeMonthEntity);
    await _repository.SaveAsync();

    var intakeMonthToReturn = MyMapper.JsonClone<CrmIntakeMonth, CrmIntakeMonthDto>(intakeMonthEntity);
    return intakeMonthToReturn;
  }
}