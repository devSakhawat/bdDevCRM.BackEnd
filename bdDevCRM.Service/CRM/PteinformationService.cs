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

internal sealed class PTEInformationService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IPTEInformationService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<PTEInformationDto>> GetPteinformationsDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.PTEInformation.GetActivePteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<PTEInformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetActivePteinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.PTEInformation.GetActivePteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<PTEInformation, PTEInformationDto>(list);
  }

  public async Task<IEnumerable<PTEInformationDto>> GetPteinformationsAsync(bool trackChanges = false)
  {
    var list = await _repository.PTEInformation.GetPteinformationsAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PTEInformation");
    return MyMapper.JsonCloneIEnumerableToList<PTEInformation, PTEInformationDto>(list);
  }

  public async Task<PTEInformationDto> GetPteinformationAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.PTEInformation.GetPteinformationAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PTEInformation", "PTEInformationId", id.ToString());
    return MyMapper.JsonClone<PTEInformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> GetPteinformationByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.PTEInformation.GetPteinformationByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PTEInformation", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<PTEInformation, PTEInformationDto>(entity);
  }

  public async Task<PTEInformationDto> CreateNewRecordAsync(PTEInformationDto dto, UsersDto currentUser)
  {
    if (dto.PTEInformationId != 0)
      throw new InvalidCreateOperationException("PTEInformationId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.PTEInformation.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("PTEInformation", "ApplicantId");

    var entity = MyMapper.JsonClone<PTEInformationDto, PTEInformation>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.PTEInformationId = await _repository.PTEInformation.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, PTEInformationDto dto, bool trackChanges)
  {
    if (key != dto.PTEInformationId) return "Key mismatch.";

    bool exists = await _repository.PTEInformation.ExistsAsync(x => x.PTEInformationId == key);
    if (!exists) throw new GenericNotFoundException("PTEInformation", "PTEInformationId", key.ToString());

    var entity = MyMapper.JsonClone<PTEInformationDto, PTEInformation>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.PTEInformation.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"PTEInformation updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, PTEInformationDto dto)
  {
    if (key != dto.PTEInformationId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(PTEInformationDto));

    await _repository.PTEInformation.DeleteAsync(x => x.PTEInformationId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"PTEInformation deleted, id={key}");
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
from PTEInformation pi
left join CrmApplication app on pi.ApplicantId = app.ApplicationId
";
    string orderBy = " pi.CreatedDate desc ";
    return await _repository.PTEInformation.GridData<PTEInformationDto>(sql, options, orderBy, "");
  }
}