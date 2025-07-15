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

internal sealed class ToeflinformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IToeflinformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.ToeflInformation.GetActiveToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ToeflInformation");
    return MyMapper.JsonCloneIEnumerableToList<Toeflinformation, TOEFLInformationDto>(list);
  }

  public async Task<IEnumerable<TOEFLInformationDto>> GetActiveToeflinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.ToeflInformation.GetActiveToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ToeflInformation");
    return MyMapper.JsonCloneIEnumerableToList<Toeflinformation, TOEFLInformationDto>(list);
  }

  public async Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.ToeflInformation.GetToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("ToeflInformation");
    return MyMapper.JsonCloneIEnumerableToList<Toeflinformation, TOEFLInformationDto>(list);
  }

  public async Task<TOEFLInformationDto> GetToeflinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.ToeflInformation.GetToeflinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ToeflInformation", "ToeflinformationId", id.ToString());
    return MyMapper.JsonClone<Toeflinformation, TOEFLInformationDto>(entity);
  }

  public async Task<TOEFLInformationDto> GetToeflinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.ToeflInformation.GetToeflinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("ToeflInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<Toeflinformation, TOEFLInformationDto>(entity);
  }

  public async Task<TOEFLInformationDto> CreateNewRecordAsync(TOEFLInformationDto dto, UsersDto currentUser)
  {
    if (dto.TOEFLInformationId != 0)
      throw new InvalidCreateOperationException("TOEFLInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.ToeflInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("ToeflInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<TOEFLInformationDto, Toeflinformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.TOEFLInformationId = await _repository.ToeflInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, TOEFLInformationDto dto, bool trackChanges)
  {
    if (key != dto.TOEFLInformationId) return "Key mismatch.";

    bool exists = await _repository.ToeflInformation.ExistsAsync(x => x.ToeflinformationId == key);
    if (!exists) throw new GenericNotFoundException("ToeflInformation", "ToeflinformationId", key.ToString());

    var entity = MyMapper.JsonClone<TOEFLInformationDto, Toeflinformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.ToeflInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"ToeflInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, TOEFLInformationDto dto)
  {
    if (key != dto.TOEFLInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(TOEFLInformationDto));

    await _repository.ToeflInformation.DeleteAsync(x => x.ToeflinformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"ToeflInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<TOEFLInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ti.ToeflinformationId,
    ti.ApplicantId,
    ti.Toefllistening,
    ti.Toeflreading,
    ti.Toeflwriting,
    ti.Toeflspeaking,
    ti.ToefloverallScore,
    ti.Toefldate,
    ti.ToeflscannedCopyPath,
    ti.ToefladditionalInformation,
    ti.CreatedDate,
    ti.CreatedBy,
    ti.UpdatedDate,
    ti.UpdatedBy,
    app.ApplicationStatus
from Toeflinformation ti
left join CrmApplication app on ti.ApplicantId = app.ApplicationId
";
    string orderBy = " ti.CreatedDate desc ";
    return await _repository.ToeflInformation.GridData<TOEFLInformationDto>(sql, options, orderBy, "");
  }
}