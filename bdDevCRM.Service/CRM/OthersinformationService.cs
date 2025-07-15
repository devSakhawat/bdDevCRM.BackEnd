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

internal sealed class OthersinformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IOthersinformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.OthersInformation.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<Othersinformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetActiveOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.OthersInformation.GetActiveOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<Othersinformation, OTHERSInformationDto>(list);
  }

  public async Task<IEnumerable<OTHERSInformationDto>> GetOthersinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.OthersInformation.GetOthersinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("OthersInformation");
    return MyMapper.JsonCloneIEnumerableToList<Othersinformation, OTHERSInformationDto>(list);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.OthersInformation.GetOthersinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("OthersInformation", "OthersinformationId", id.ToString());
    return MyMapper.JsonClone<Othersinformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> GetOthersinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.OthersInformation.GetOthersinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("OthersInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<Othersinformation, OTHERSInformationDto>(entity);
  }

  public async Task<OTHERSInformationDto> CreateNewRecordAsync(OTHERSInformationDto dto, UsersDto currentUser)
  {
    if (dto.OthersinformationId != 0)
      throw new InvalidCreateOperationException("OthersinformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.OthersInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("OthersInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<OTHERSInformationDto, Othersinformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.OthersinformationId = await _repository.OthersInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, OTHERSInformationDto dto, bool trackChanges)
  {
    if (key != dto.OthersinformationId) return "Key mismatch.";

    bool exists = await _repository.OthersInformation.ExistsAsync(x => x.OthersinformationId == key);
    if (!exists) throw new GenericNotFoundException("OthersInformation", "OthersinformationId", key.ToString());

    var entity = MyMapper.JsonClone<OTHERSInformationDto, Othersinformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.OthersInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"OthersInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, OTHERSInformationDto dto)
  {
    if (key != dto.OthersinformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(OTHERSInformationDto));

    await _repository.OthersInformation.DeleteAsync(x => x.OthersinformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"OthersInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<OTHERSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    oi.OthersinformationId,
    oi.ApplicantId,
    oi.OthersadditionalInformation,
    oi.OthersscannedCopyPath,
    oi.CreatedDate,
    oi.CreatedBy,
    oi.UpdatedDate,
    oi.UpdatedBy,
    app.ApplicationStatus
from Othersinformation oi
left join CrmApplication app on oi.ApplicantId = app.ApplicationId
";
    string orderBy = " oi.CreatedDate desc ";
    return await _repository.OthersInformation.GridData<OTHERSInformationDto>(sql, options, orderBy, "");
  }
}