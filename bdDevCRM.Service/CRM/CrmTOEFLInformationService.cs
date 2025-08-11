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

internal sealed class CrmTOEFLInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmTOEFLInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmTOEFLInformations.GetActiveToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmTOEFLInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmTOEFLInformation, TOEFLInformationDto>(list);
  }

  public async Task<IEnumerable<TOEFLInformationDto>> GetActiveToeflinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmTOEFLInformations.GetActiveToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmTOEFLInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmTOEFLInformation, TOEFLInformationDto>(list);
  }

  public async Task<IEnumerable<TOEFLInformationDto>> GetToeflinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmTOEFLInformations.GetToeflinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmTOEFLInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmTOEFLInformation, TOEFLInformationDto>(list);
  }

  public async Task<TOEFLInformationDto> GetToeflinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmTOEFLInformations.GetToeflinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmTOEFLInformation", "TOEFLInformationId", id.ToString());
    return MyMapper.JsonClone<CrmTOEFLInformation, TOEFLInformationDto>(entity);
  }

  public async Task<TOEFLInformationDto> GetToeflinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmTOEFLInformations.GetToeflinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmTOEFLInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmTOEFLInformation, TOEFLInformationDto>(entity);
  }

  public async Task<TOEFLInformationDto> CreateNewRecordAsync(TOEFLInformationDto dto, UsersDto currentUser)
  {
    if (dto.TOEFLInformationId != 0)
      throw new InvalidCreateOperationException("TOEFLInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmTOEFLInformations.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmTOEFLInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.TOEFLInformationId = await _repository.CrmTOEFLInformations.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, TOEFLInformationDto dto, bool trackChanges)
  {
    if (key != dto.TOEFLInformationId) return "Key mismatch.";

    bool exists = await _repository.CrmTOEFLInformations.ExistsAsync(x => x.TOEFLInformationId == key);
    if (!exists) throw new GenericNotFoundException("CrmTOEFLInformation", "TOEFLInformationId", key.ToString());

    var entity = MyMapper.JsonClone<TOEFLInformationDto, CrmTOEFLInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmTOEFLInformations.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmTOEFLInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, TOEFLInformationDto dto)
  {
    if (key != dto.TOEFLInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(TOEFLInformationDto));

    await _repository.CrmTOEFLInformations.DeleteAsync(x => x.TOEFLInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmTOEFLInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<TOEFLInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ti.TOEFLInformationId,
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
from CrmTOEFLInformation ti
left join CrmApplication app on ti.ApplicantId = app.ApplicationId
";
    string orderBy = " ti.CreatedDate desc ";
    return await _repository.CrmTOEFLInformations.GridData<TOEFLInformationDto>(sql, options, orderBy, "");
  }
}