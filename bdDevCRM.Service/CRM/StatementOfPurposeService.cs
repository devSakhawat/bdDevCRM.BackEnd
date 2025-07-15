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

internal sealed class StatementOfPurposeService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IStatementOfPurposeService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<StatementOfPurposeDto>> GetStatementOfPurposesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.StatementOfPurpose.GetActiveStatementOfPurposesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("StatementOfPurpose");
    return MyMapper.JsonCloneIEnumerableToList<StatementOfPurpose, StatementOfPurposeDto>(list);
  }

  public async Task<IEnumerable<StatementOfPurposeDto>> GetActiveStatementOfPurposesAsync(bool trackChanges = false)
  {
    var list = await _repository.StatementOfPurpose.GetActiveStatementOfPurposesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("StatementOfPurpose");
    return MyMapper.JsonCloneIEnumerableToList<StatementOfPurpose, StatementOfPurposeDto>(list);
  }

  public async Task<IEnumerable<StatementOfPurposeDto>> GetStatementOfPurposesAsync(bool trackChanges = false)
  {
    var list = await _repository.StatementOfPurpose.GetStatementOfPurposesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("StatementOfPurpose");
    return MyMapper.JsonCloneIEnumerableToList<StatementOfPurpose, StatementOfPurposeDto>(list);
  }

  public async Task<StatementOfPurposeDto> GetStatementOfPurposeAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.StatementOfPurpose.GetStatementOfPurposeAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("StatementOfPurpose", "StatementOfPurposeId", id.ToString());
    return MyMapper.JsonClone<StatementOfPurpose, StatementOfPurposeDto>(entity);
  }

  public async Task<StatementOfPurposeDto> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.StatementOfPurpose.GetStatementOfPurposeByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("StatementOfPurpose", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<StatementOfPurpose, StatementOfPurposeDto>(entity);
  }

  public async Task<StatementOfPurposeDto> CreateNewRecordAsync(StatementOfPurposeDto dto, UsersDto currentUser)
  {
    if (dto.StatementOfPurposeId != 0)
      throw new InvalidCreateOperationException("StatementOfPurposeId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.StatementOfPurpose.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("StatementOfPurpose", "ApplicantId");

    var entity = MyMapper.JsonClone<StatementOfPurposeDto, StatementOfPurpose>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.StatementOfPurposeId = await _repository.StatementOfPurpose.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, StatementOfPurposeDto dto, bool trackChanges)
  {
    if (key != dto.StatementOfPurposeId) return "Key mismatch.";

    bool exists = await _repository.StatementOfPurpose.ExistsAsync(x => x.StatementOfPurposeId == key);
    if (!exists) throw new GenericNotFoundException("StatementOfPurpose", "StatementOfPurposeId", key.ToString());

    var entity = MyMapper.JsonClone<StatementOfPurposeDto, StatementOfPurpose>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.StatementOfPurpose.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"StatementOfPurpose updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, StatementOfPurposeDto dto)
  {
    if (key != dto.StatementOfPurposeId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(StatementOfPurposeDto));

    await _repository.StatementOfPurpose.DeleteAsync(x => x.StatementOfPurposeId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"StatementOfPurpose deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<StatementOfPurposeDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    sop.StatementOfPurposeId,
    sop.ApplicantId,
    sop.StatementOfPurposeRemarks,
    sop.StatementOfPurposeFilePath,
    sop.CreatedDate,
    sop.CreatedBy,
    sop.UpdatedDate,
    sop.UpdatedBy,
    app.ApplicationStatus
from StatementOfPurpose sop
left join CrmApplication app on sop.ApplicantId = app.ApplicationId
";
    string orderBy = " sop.CreatedDate desc ";
    return await _repository.StatementOfPurpose.GridData<StatementOfPurposeDto>(sql, options, orderBy, "");
  }
}