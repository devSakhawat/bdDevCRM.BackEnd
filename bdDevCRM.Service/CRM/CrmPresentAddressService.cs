using bdDevCRM.Utilities.CRMGrid.GRID;
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

internal sealed class CrmPresentAddressService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : ICrmPresentAddressService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<PresentAddressDto>> GetPresentAddressesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPresentAddresses.GetActivePresentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPresentAddress");
    return MyMapper.JsonCloneIEnumerableToList<CrmPresentAddress, PresentAddressDto>(list);
  }

  public async Task<IEnumerable<PresentAddressDto>> GetActivePresentAddressesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPresentAddresses.GetActivePresentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPresentAddress");
    return MyMapper.JsonCloneIEnumerableToList<CrmPresentAddress, PresentAddressDto>(list);
  }

  public async Task<IEnumerable<PresentAddressDto>> GetPresentAddressesAsync(bool trackChanges = false)
  {
    var list = await _repository.CrmPresentAddresses.GetPresentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmPresentAddress");
    return MyMapper.JsonCloneIEnumerableToList<CrmPresentAddress, PresentAddressDto>(list);
  }

  public async Task<PresentAddressDto> GetPresentAddressAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.CrmPresentAddresses.GetPresentAddressAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmPresentAddress", "PresentAddressId", id.ToString());
    return MyMapper.JsonClone<CrmPresentAddress, PresentAddressDto>(entity);
  }

  public async Task<PresentAddressDto> GetPresentAddressByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.CrmPresentAddresses.GetPresentAddressByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("CrmPresentAddress", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<CrmPresentAddress, PresentAddressDto>(entity);
  }

  public async Task<IEnumerable<PresentAddressDto>> GetPresentAddressesByCountryIdAsync(int countryId, bool trackChanges = false)
  {
    var list = await _repository.CrmPresentAddresses.GetPresentAddressesByCountryIdAsync(countryId, trackChanges);
    if (!list.Any()) return new List<PresentAddressDto>();
    return MyMapper.JsonCloneIEnumerableToList<CrmPresentAddress, PresentAddressDto>(list);
  }

  public async Task<PresentAddressDto> CreateNewRecordAsync(PresentAddressDto dto, UsersDto currentUser)
  {
    if (dto.PresentAddressId != 0)
      throw new InvalidCreateOperationException("PresentAddressId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.CrmPresentAddresses.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("CrmPresentAddress", "ApplicantId");

    var entity = MyMapper.JsonClone<PresentAddressDto, CrmPresentAddress>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.PresentAddressId = await _repository.CrmPresentAddresses.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, PresentAddressDto dto, bool trackChanges)
  {
    if (key != dto.PresentAddressId) return "Key mismatch.";

    bool exists = await _repository.CrmPresentAddresses.ExistsAsync(x => x.PresentAddressId == key);
    if (!exists) throw new GenericNotFoundException("CrmPresentAddress", "PresentAddressId", key.ToString());

    var entity = MyMapper.JsonClone<PresentAddressDto, CrmPresentAddress>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.CrmPresentAddresses.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmPresentAddress updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, PresentAddressDto dto)
  {
    if (key != dto.PresentAddressId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(PresentAddressDto));

    await _repository.CrmPresentAddresses.DeleteAsync(x => x.PresentAddressId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmPresentAddress deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<PresentAddressDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    pra.PresentAddressId,
    pra.ApplicantId,
    pra.SameAsPermanentAddress,
    pra.Address,
    pra.City,
    pra.State,
    pra.CountryId,
    pra.CountryName,
    pra.PostalCode,
    pra.CreatedDate,
    pra.CreatedBy,
    pra.UpdatedDate,
    pra.UpdatedBy,
    app.ApplicationStatus,
    c.CountryName as CountryFullName
from CrmPresentAddress pra
left join CrmApplication app on pra.ApplicantId = app.ApplicationId
left join Country c on pra.CountryId = c.CountryId
";
    string orderBy = " pra.CreatedDate desc ";
    return await _repository.CrmPresentAddresses.GridData<PresentAddressDto>(sql, options, orderBy, "");
  }
}