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

internal sealed class OTHERSInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IOTHERSInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.OTHERSInformation.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OTHERSInformation");
    return MyMapper.JsonCloneIEnumerableToList<OTHERSInformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetActiveOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.OTHERSInformation.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OTHERSInformation");
    return MyMapper.JsonCloneIEnumerableToList<OTHERSInformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.OTHERSInformation.GetOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OTHERSInformation");
    return MyMapper.JsonCloneIEnumerableToList<OTHERSInformation, OTHERSInformationDto>(list);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.OTHERSInformation.GetOthersinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("OTHERSInformation", "OTHERSInformationId", id.ToString());
    return MyMapper.JsonClone<OTHERSInformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.OTHERSInformation.GetOthersinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("OTHERSInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<OTHERSInformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> CreateNewRecordAsync(OTHERSInformationDto dto, UsersDto currentUser)
  {
    if (dto.OTHERSInformationId != 0)
      throw new InvalidCreateOperationException("OTHERSInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.OTHERSInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("OTHERSInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<OTHERSInformationDto, OTHERSInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.OTHERSInformationId = await _repository.OTHERSInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, OTHERSInformationDto dto, bool trackChanges)
  {
    if (key != dto.OTHERSInformationId) return "Key mismatch.";

    bool exists = await _repository.OTHERSInformation.ExistsAsync(x => x.OTHERSInformationId == key);
    if (!exists) throw new GenericNotFoundException("OTHERSInformation", "OTHERSInformationId", key.ToString());

    var entity = MyMapper.JsonClone<OTHERSInformationDto, OTHERSInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.OTHERSInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"OTHERSInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, OTHERSInformationDto dto)
  {
    if (key != dto.OTHERSInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(OTHERSInformationDto));

    await _repository.OTHERSInformation.DeleteAsync(x => x.OTHERSInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"OTHERSInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<OTHERSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    oi.OTHERSInformationId,
    oi.ApplicantId,
    oi.OthersadditionalInformation,
    oi.OthersscannedCopyPath,
    oi.CreatedDate,
    oi.CreatedBy,
    oi.UpdatedDate,
    oi.UpdatedBy,
    app.ApplicationStatus
from OTHERSInformation oi
left join CrmApplication app on oi.ApplicantId = app.ApplicationId
";
    string orderBy = " oi.CreatedDate desc ";
    return await _repository.OTHERSInformation.GridData<OTHERSInformationDto>(sql, options, orderBy, "");
  }
}