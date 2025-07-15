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

internal sealed class IeltsinformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IIeltsinformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.IeltsInformation.GetActiveIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IeltsInformation");
    return MyMapper.JsonCloneIEnumerableToList<Ieltsinformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetActiveIeltsinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.IeltsInformation.GetActiveIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IeltsInformation");
    return MyMapper.JsonCloneIEnumerableToList<Ieltsinformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetIeltsinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.IeltsInformation.GetIeltsinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("IeltsInformation");
    return MyMapper.JsonCloneIEnumerableToList<Ieltsinformation, IELTSInformationDto>(list);
  }

  public async Task<IELTSInformationDto> GetIeltsinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.IeltsInformation.GetIeltsinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("IeltsInformation", "IeltsinformationId", id.ToString());
    return MyMapper.JsonClone<Ieltsinformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> GetIeltsinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.IeltsInformation.GetIeltsinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("IeltsInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<Ieltsinformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> CreateNewRecordAsync(IELTSInformationDto dto, UsersDto currentUser)
  {
    if (dto.IELTSInformationId != 0)
      throw new InvalidCreateOperationException("IELTSInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.IeltsInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("IeltsInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<IELTSInformationDto, Ieltsinformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.IELTSInformationId = await _repository.IeltsInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, IELTSInformationDto dto, bool trackChanges)
  {
    if (key != dto.IELTSInformationId) return "Key mismatch.";

    bool exists = await _repository.IeltsInformation.ExistsAsync(x => x.IeltsinformationId == key);
    if (!exists) throw new GenericNotFoundException("IeltsInformation", "IeltsinformationId", key.ToString());

    var entity = MyMapper.JsonClone<IELTSInformationDto, Ieltsinformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.IeltsInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"IeltsInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, IELTSInformationDto dto)
  {
    if (key != dto.IELTSInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(IELTSInformationDto));

    await _repository.IeltsInformation.DeleteAsync(x => x.IeltsinformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"IeltsInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<IELTSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ii.IeltsinformationId,
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
from Ieltsinformation ii
left join CrmApplication app on ii.ApplicantId = app.ApplicationId
";
    string orderBy = " ii.CreatedDate desc ";
    return await _repository.IeltsInformation.GridData<IELTSInformationDto>(sql, options, orderBy, "");
  }
}