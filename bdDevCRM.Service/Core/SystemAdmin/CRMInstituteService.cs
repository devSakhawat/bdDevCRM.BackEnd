using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace bdDevCRM.Service.Core.SystemAdmin;


internal sealed class CRMInstituteService : ICRMInstituteService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;
  private readonly IHttpContextAccessor _httpContextAccessor;


  public CRMInstituteService(IRepositoryManager repo, ILoggerManager logger, IConfiguration config, IHttpContextAccessor httpContextAccessor)
  {
    _repository = repo;
    _logger = logger;
    _config = config;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<CrmInstituteDto>> GetInstitutesDDLAsync(bool trackChanges = false)
  {
    var list = await _repository.CRMInstitutes.GetActiveInstitutesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmInstitute");
    return MyMapper.JsonCloneIEnumerableToList<Crminstitute, CrmInstituteDto>(list);
  }

  public async Task<GridEntity<CrmInstituteDto>> SummaryGrid(CRMGridOptions options)
  {
    string sql = @"select InstituteId ,CRMInstitute.CountryId ,InstituteName ,Campus ,Website ,MonthlyLivingCost ,FundsRequirementforVisa ,ApplicationFee
,CRMInstitute.CurrencyId ,IsLanguageMandatory ,LanguagesRequirement ,InstitutionalBenefits ,PartTimeWorkDetails ,ScholarshipsPolicy ,InstitutionStatusNotes
,CRMInstitute.InstituteTypeId ,InstituteCode ,InstituteEmail ,InstituteAddress ,InstitutePhoneNO ,InstituteMobileNo,CRMInstitute.Status ,Country.CountryName 
,CurrencyInfo.CurrencyName ,CRMInstituteType.InstituteTypeName ,docLogo.FilePath as InstitutionLogo , docProspectus.FilePath as InstitutionProspectus
from CRMInstitute
left join Country on CRMInstitute.CountryId = Country.CountryId
left join CurrencyInfo on CRMInstitute.CountryId = CurrencyInfo.CurrencyId
left join CRMInstituteType on CRMInstitute.InstituteTypeId = CRMInstituteType.InstituteTypeId

left join DMSDocument docLogo on CRMInstitute.InstituteId =  docLogo.ReferenceEntityId and docLogo.SystemTag = 'InstitutionLogo'
left join DMSDocument docProspectus on CRMInstitute.InstituteId = docProspectus.ReferenceEntityId and docProspectus.SystemTag = 'InstitutionProspectus' ";
    string orderBy = " InstituteName asc ";
    return await _repository.CRMInstitutes.GridData<CrmInstituteDto>(sql, options, orderBy, "");
  }

  public async Task<CrmInstituteDto> CreateNewRecordAsync(CrmInstituteDto dto, UsersDto currentUser)
  {
    if (dto.InstituteId != 0)
      throw new InvalidCreateOperationException("InstituteId must be 0.");

    //var instituteObj = await _repository.CRMInstitutes.FirstOrDefaultAsync(x => x.InstituteName.Trim().ToLower() == dto.InstituteName!.Trim().ToLower());
    bool dup = await _repository.CRMInstitutes.ExistsAsync(x => x.InstituteName != null && x.InstituteName.Trim().ToLower().Equals(dto.InstituteName!.Trim().ToLower()));

    if (dup) throw new DuplicateRecordException("CrmInstitute", "InstituteName");

    var entity = MyMapper.JsonClone<CrmInstituteDto, Crminstitute>(dto);
    dto.InstituteId = await _repository.CRMInstitutes.CreateAndGetIdAsync(entity);

    return dto;
  }

  public async Task<string> UpdateRecordAsync(int key, CrmInstituteDto dto, bool trackChanges)
  {
    if (key != dto.InstituteId) return "Key mismatch.";

    bool exists = await _repository.CRMInstitutes.ExistsAsync(x => x.InstituteId == key);
    if (!exists) throw new GenericNotFoundException("CrmInstitute", "InstituteId", key.ToString());

    var entity = MyMapper.JsonClone<CrmInstituteDto, Crminstitute>(dto);
    _repository.CRMInstitutes.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmInstitute updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, CrmInstituteDto dto)
  {
    if (key != dto.InstituteId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CrmInstituteDto));

    await _repository.CRMInstitutes.DeleteAsync(x => x.InstituteId == key, true);
    await _repository.SaveAsync();
    _logger.LogInfo($"CrmInstitute deleted, id={key}");
    return OperationMessage.Success;
  }

  // Implement this interface : Task<CrmInstituteDto> GetInstituteNameIdAsync(int key, bool trackChanges = false)
  public async Task<CrmInstituteDto> GetInstituteNameIdAsync(string name, bool trackChanges = false)
  {
    var entity = await _repository.CRMInstitutes.FirstOrDefaultAsync(x => x.InstituteName == name);
    if (entity == null) throw new GenericNotFoundException("CrmInstitute", "InstituteId", name.ToString());
    return MyMapper.JsonClone<Crminstitute, CrmInstituteDto>(entity);
  }

}



