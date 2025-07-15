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

internal sealed class PermanentAddressService(
    IRepositoryManager repository,
    ILoggerManager logger,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor) : IPermanentAddressService
{
  private readonly IRepositoryManager _repository = repository;
  private readonly ILoggerManager _logger = logger;
  private readonly IConfiguration _config = config;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public async Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.PermanentAddress.GetActivePermanentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PermanentAddress");
    return MyMapper.JsonCloneIEnumerableToList<PermanentAddress, PermanentAddressDto>(list);
  }

  public async Task<IEnumerable<PermanentAddressDto>> GetActivePermanentAddressesAsync(bool trackChanges = false)
  {
    var list = await _repository.PermanentAddress.GetActivePermanentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PermanentAddress");
    return MyMapper.JsonCloneIEnumerableToList<PermanentAddress, PermanentAddressDto>(list);
  }

  public async Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesAsync(bool trackChanges = false)
  {
    var list = await _repository.PermanentAddress.GetPermanentAddressesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("PermanentAddress");
    return MyMapper.JsonCloneIEnumerableToList<PermanentAddress, PermanentAddressDto>(list);
  }

  public async Task<PermanentAddressDto> GetPermanentAddressAsync(int id, bool trackChanges = false)
  {
    var entity = await _repository.PermanentAddress.GetPermanentAddressAsync(id, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PermanentAddress", "PermanentAddressId", id.ToString());
    return MyMapper.JsonClone<PermanentAddress, PermanentAddressDto>(entity);
  }

  public async Task<PermanentAddressDto> GetPermanentAddressByApplicantIdAsync(int applicantId, bool trackChanges = false)
  {
    var entity = await _repository.PermanentAddress.GetPermanentAddressByApplicantIdAsync(applicantId, trackChanges);
    if (entity == null) throw new GenericNotFoundException("PermanentAddress", "ApplicantId", applicantId.ToString());
    return MyMapper.JsonClone<PermanentAddress, PermanentAddressDto>(entity);
  }

  public async Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesByCountryIdAsync(int countryId, bool trackChanges = false)
  {
    var list = await _repository.PermanentAddress.GetPermanentAddressesByCountryIdAsync(countryId, trackChanges);
    if (!list.Any()) return new List<PermanentAddressDto>();
    return MyMapper.JsonCloneIEnumerableToList<PermanentAddress, PermanentAddressDto>(list);
  }

  public async Task<PermanentAddressDto> CreateNewRecordAsync(PermanentAddressDto dto, UsersDto currentUser)
  {
    if (dto.PermanentAddressId != 0)
      throw new InvalidCreateOperationException("PermanentAddressId must be 0.");

    // Check for duplicate applicant ID
    bool applicantExists = await _repository.PermanentAddress.ExistsAsync(x => x.ApplicantId == dto.ApplicantId);
    if (applicantExists) throw new DuplicateRecordException("PermanentAddress", "ApplicantId");

    var entity = MyMapper.JsonClone<PermanentAddressDto, PermanentAddress>(dto);
    entity.CreatedDate = DateTime.UtcNow;
    entity.CreatedBy = currentUser.UserId ?? 0;
    
    dto.PermanentAddressId = await _repository.PermanentAddress.CreateAndGetIdAsync(entity);
    dto.CreatedDate = entity.CreatedDate;
    dto.CreatedBy = entity.CreatedBy;

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, PermanentAddressDto dto, bool trackChanges)
  {
    if (key != dto.PermanentAddressId) return "Key mismatch.";

    bool exists = await _repository.PermanentAddress.ExistsAsync(x => x.PermanentAddressId == key);
    if (!exists) throw new GenericNotFoundException("PermanentAddress", "PermanentAddressId", key.ToString());

    var entity = MyMapper.JsonClone<PermanentAddressDto, PermanentAddress>(dto);
    entity.UpdatedDate = DateTime.UtcNow;
    
    _repository.PermanentAddress.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"PermanentAddress updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, PermanentAddressDto dto)
  {
    if (key != dto.PermanentAddressId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(PermanentAddressDto));

    await _repository.PermanentAddress.DeleteAsync(x => x.PermanentAddressId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"PermanentAddress deleted, id={key}");
    return OperationMessage.Success;
  }

  public async Task<GridEntity<PermanentAddressDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"
select 
    pa.PermanentAddressId,
    pa.ApplicantId,
    pa.Address,
    pa.City,
    pa.State,
    pa.CountryId,
    pa.CountryName,
    pa.PostalCode,
    pa.CreatedDate,
    pa.CreatedBy,
    pa.UpdatedDate,
    pa.UpdatedBy,
    app.ApplicationStatus,
    c.CountryName as CountryFullName
from PermanentAddress pa
left join CrmApplication app on pa.ApplicantId = app.ApplicationId
left join Country c on pa.CountryId = c.CountryId
";
    string orderBy = " pa.CreatedDate desc ";
    return await _repository.PermanentAddress.GridData<PermanentAddressDto>(sql, options, orderBy, "");
  }
}