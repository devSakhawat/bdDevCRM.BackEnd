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

internal sealed class IELTSInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IIELTSInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.IELTSInformation.GetActiveIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<IELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetActiveIeltsinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.IELTSInformation.GetActiveIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<IELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.IELTSInformation.GetIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<IELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IELTSInformationDto> GetIeltsinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.IELTSInformation.GetIeltsinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("IELTSInformation", "IELTSInformationId", id.ToString());
    return MyMapper.JsonClone<IELTSInformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> GetIeltsinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.IELTSInformation.GetIeltsinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("IELTSInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<IELTSInformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> CreateNewRecordAsync(IELTSInformationDto dto, UsersDto currentUser)
  {
    if (dto.IELTSInformationId != 0)
      throw new InvalidCreateOperationException("IELTSInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.IELTSInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("IELTSInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<IELTSInformationDto, IELTSInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.IELTSInformationId = await _repository.IELTSInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, IELTSInformationDto dto, bool trackChanges)
  {
    if (key != dto.IELTSInformationId) return "Key mismatch.";

    bool exists = await _repository.IELTSInformation.ExistsAsync(x => x.IELTSInformationId == key);
    if (!exists) throw new GenericNotFoundException("IELTSInformation", "IELTSInformationId", key.ToString());

    var entity = MyMapper.JsonClone<IELTSInformationDto, IELTSInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.IELTSInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"IELTSInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, IELTSInformationDto dto)
  {
    if (key != dto.IELTSInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(IELTSInformationDto));

    await _repository.IELTSInformation.DeleteAsync(x => x.IELTSInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"IELTSInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<IELTSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ii.IELTSInformationId,
    ii.ApplicantId,
    ii.Ieltslistening,
    ii.Ieltsreading,
    ii.Ieltswriting,
    ii.Ieltsspeaking,
    ii.IeltsoverallScore,
    ii.Ieltsdate,
    ii.IeltsscannedCopyPath,
    ii.IeltsadditionalInformation,
    ii.CreatedDate,
    ii.CreatedBy,
    ii.UpdatedDate,
    ii.UpdatedBy,
    app.ApplicationStatus
from IELTSInformation ii
left join CrmApplication app on ii.ApplicantId = app.ApplicationId
";
    string orderBy = " ii.CreatedDate desc ";
    return await _repository.IELTSInformation.GridData<IELTSInformationDto>(sql, options, orderBy, "");
  }
}