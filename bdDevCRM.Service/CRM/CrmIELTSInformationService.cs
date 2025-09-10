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

internal sealed class CrmIELTSInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmIELTSInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<IELTSInformationDto>> GetIELTSinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmIELTSInformations.GetActiveIELTSinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmIELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmIELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetActiveIELTSinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmIELTSInformations.GetActiveIELTSinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmIELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmIELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IEnumerable<IELTSInformationDto>> GetIELTSinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmIELTSInformations.GetIELTSinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmIELTSInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmIELTSInformation, IELTSInformationDto>(list);
  }

  public async Task<IELTSInformationDto> GetIELTSinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmIELTSInformations.GetIELTSinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmIELTSInformation", "IELTSInformationId", id.ToString());
    return MyMapper.JsonClone<CrmIELTSInformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> GetIELTSinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmIELTSInformations.GetIELTSinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmIELTSInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmIELTSInformation, IELTSInformationDto>(entity);
  }

  public async Task<IELTSInformationDto> CreateNewRecordAsync(IELTSInformationDto dto, UsersDto currentUser)
  {
    if (dto.IELTSInformationId != 0)
      throw new InvalidCreateOperationException("IELTSInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmIELTSInformations.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmIELTSInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.IELTSInformationId = await _repository.CrmIELTSInformations.CreateAndGetIdAsync(entity);
    //dto.CreatedDate = entity.CreatedDate;
    //dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, IELTSInformationDto dto, bool trackChanges)
  {
    if (key != dto.IELTSInformationId) return "Key mismatch.";

    bool exists = await _repository.CrmIELTSInformations.ExistsAsync(x => x.IELTSInformationId == key);
    if (!exists) throw new GenericNotFoundException("CrmIELTSInformation", "IELTSInformationId", key.ToString());

    var entity = MyMapper.JsonClone<IELTSInformationDto, CrmIELTSInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmIELTSInformations.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmIELTSInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, IELTSInformationDto dto)
  {
    if (key != dto.IELTSInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(IELTSInformationDto));

    await _repository.CrmIELTSInformations.DeleteAsync(x => x.IELTSInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmIELTSInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<IELTSInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    ii.IELTSInformationId,
    ii.ApplicantId,
    ii.IELTSlistening,
    ii.IELTSreading,
    ii.IELTSwriting,
    ii.IELTSspeaking,
    ii.IELTSoverallScore,
    ii.IELTSdate,
    ii.IELTSscannedCopyPath,
    ii.IELTSadditionalInformation,
    ii.CreatedDate,
    ii.CreatedBy,
    ii.UpdatedDate,
    ii.UpdatedBy,
    app.ApplicationStatus
from CrmIELTSInformation ii
left join CrmApplication app on ii.ApplicantId = app.ApplicationId
";
    string orderBy = " ii.CreatedDate desc ";
    return await _repository.CrmIELTSInformations.GridData<IELTSInformationDto>(sql, options, orderBy, "");
  }
}