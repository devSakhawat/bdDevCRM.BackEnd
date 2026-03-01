using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bdDevCRM.Service.CRM;

internal sealed class CrmPTEInformationService(
    IRepositoryManager repository,
    ILogger<CrmPTEInformationService> logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmPTEInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILogger<CrmPTEInformationService> _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<PTEInformationDto>> GetPTEInformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPTEInformations.GetActivePTEInformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmPTEInformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetActivePTEInformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPTEInformations.GetActivePTEInformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmPTEInformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetPTEInformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPTEInformations.GetPTEInformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<CrmPTEInformation, PTEInformationDto>(list);
  }

  public async Task<PTEInformationDto> GetPTEInformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmPTEInformations.GetPTEInformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmPTEInformation", "PTEInformationId", id.ToString());
    return MyMapper.JsonClone<CrmPTEInformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> GetPTEInformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmPTEInformations.GetPTEInformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmPTEInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmPTEInformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> CreateNewRecordAsync(PTEInformationDto dto, UsersDto currentUser)
  {
    if (dto.PTEInformationId != 0)
      throw new InvalidCreateOperationException("PTEInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmPTEInformations.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmPTEInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.PTEInformationId = await _repository.CrmPTEInformations.CreateAndGetIdAsync(entity);
    //dto.CreatedDate = entity.CreatedDate;
    //dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, PTEInformationDto dto, bool trackChanges)
  {
    if (key != dto.PTEInformationId) return "Key mismatch.";

    bool exists = await _repository.CrmPTEInformations.ExistsAsync(x => x.PTEInformationId == key);
    if (!exists) throw new GenericNotFoundException("CrmPTEInformation", "PTEInformationId", key.ToString());

    var entity = MyMapper.JsonClone<PTEInformationDto, CrmPTEInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmPTEInformations.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInformation("CrmPTEInformation updated, id={PTEInformationId}", key);
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, PTEInformationDto dto)
  {
    if (key != dto.PTEInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(PTEInformationDto));

    await _repository.CrmPTEInformations.DeleteAsync(x => x.PTEInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInformation("CrmPTEInformation deleted, id={PTEInformationId}", key);
    return OperationMessage.Success;
  }

  public async Task<GridEntity<PTEInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    pi.PTEInformationId,
    pi.ApplicantId,
    pi.Ptelistening,
    pi.Ptereading,
    pi.Ptewriting,
    pi.Ptespeaking,
    pi.PteoverallScore,
    pi.Ptedate,
    pi.PtescannedCopyPath,
    pi.PteadditionalInformation,
    pi.CreatedDate,
    pi.CreatedBy,
    pi.UpdatedDate,
    pi.UpdatedBy,
    app.ApplicationStatus
from CrmPTEInformation pi
left join CrmApplication app on pi.ApplicantId = app.ApplicationId
";
    string orderBy = " pi.CreatedDate desc ";
    return await _repository.CrmPTEInformations.GridData<PTEInformationDto>(sql, options, orderBy, "");
  }
}