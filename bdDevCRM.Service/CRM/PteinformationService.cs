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

internal sealed class PteinformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IPteinformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<PTEInformationDto>> GetPteinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.PteInformation.GetActivePteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PteInformation");
    return MyMapper.JsonCloneIEnumerableToList<Pteinformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetActivePteinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.PteInformation.GetActivePteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PteInformation");
    return MyMapper.JsonCloneIEnumerableToList<Pteinformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetPteinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.PteInformation.GetPteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PteInformation");
    return MyMapper.JsonCloneIEnumerableToList<Pteinformation, PTEInformationDto>(list);
  }

  public async Task<PTEInformationDto> GetPteinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.PteInformation.GetPteinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PteInformation", "PteinformationId", id.ToString());
    return MyMapper.JsonClone<Pteinformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> GetPteinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.PteInformation.GetPteinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PteInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<Pteinformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> CreateNewRecordAsync(PTEInformationDto dto, UsersDto currentUser)
  {
    if (dto.PteinformationId != 0)
      throw new InvalidCreateOperationException("PteinformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.PteInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("PteInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<PTEInformationDto, Pteinformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.PteinformationId = await _repository.PteInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, PTEInformationDto dto, bool trackChanges)
  {
    if (key != dto.PteinformationId) return "Key mismatch.";

    bool exists = await _repository.PteInformation.ExistsAsync(x => x.PteinformationId == key);
    if (!exists) throw new GenericNotFoundException("PteInformation", "PteinformationId", key.ToString());

    var entity = MyMapper.JsonClone<PTEInformationDto, Pteinformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.PteInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"PteInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, PTEInformationDto dto)
  {
    if (key != dto.PteinformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(PTEInformationDto));

    await _repository.PteInformation.DeleteAsync(x => x.PteinformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"PteInformation deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<PTEInformationDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    pi.PteinformationId,
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
from Pteinformation pi
left join CrmApplication app on pi.ApplicantId = app.ApplicationId
";
    string orderBy = " pi.CreatedDate desc ";
    return await _repository.PteInformation.GridData<PTEInformationDto>(sql, options, orderBy, "");
  }
}