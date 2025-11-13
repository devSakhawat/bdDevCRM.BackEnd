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

internal sealed class CrmOthersInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmOthersInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmOthersInformations.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmOthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmOthersInformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetActiveOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmOthersInformations.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmOthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmOthersInformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmOthersInformations.GetOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmOthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmOthersInformation, OTHERSInformationDto>(list);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmOthersInformations.GetOthersinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmOthersInformation", "OthersInformationId", id.ToString());
    return MyMapper.JsonClone<CrmOthersInformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmOthersInformations.GetOthersinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmOthersInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmOthersInformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> CreateNewRecordAsync(OTHERSInformationDto dto, UsersDto currentUser)
  {
    if (dto.OthersInformationId != 0)
      throw new InvalidCreateOperationException("OthersInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmOthersInformations.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmOthersInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.OthersInformationId = await _repository.CrmOthersInformations.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, OTHERSInformationDto dto, bool trackChanges)
  {
    if (key != dto.OthersInformationId) return "Key mismatch.";

    bool exists = await _repository.CrmOthersInformations.ExistsAsync(x => x.OthersInformationId == key);
    if (!exists) throw new GenericNotFoundException("CrmOthersInformation", "OthersInformationId", key.ToString());

    var entity = MyMapper.JsonClone<OTHERSInformationDto, CrmOthersInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmOthersInformations.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmOthersInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, OTHERSInformationDto dto)
  {
    if (key != dto.OthersInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(OTHERSInformationDto));

    await _repository.CrmOthersInformations.DeleteAsync(x => x.OthersInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmOthersInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<OTHERSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    oi.OthersInformationId,
    oi.ApplicantId,
    oi.OthersadditionalInformation,
    oi.OthersscannedCopyPath,
    oi.CreatedDate,
    oi.CreatedBy,
    oi.UpdatedDate,
    oi.UpdatedBy,
    app.ApplicationStatus
from CrmOthersInformation oi
left join CrmApplication app on oi.ApplicantId = app.ApplicationId
";
    string orderBy = " oi.CreatedDate desc ";
    return await _repository.CrmOthersInformations.GridData<OTHERSInformationDto>(sql, options, orderBy, "");
  }
}