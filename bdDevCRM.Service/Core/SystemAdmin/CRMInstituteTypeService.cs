using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class CRMInstituteTypeService : ICRMInstituteTypeService
{
  private readonly IRepositoryManager _repo;
  private readonly ILoggerManager _log;
  private readonly IConfiguration _configuration;

  public CRMInstituteTypeService(IRepositoryManager repo, ILoggerManager log, IConfiguration configuration)
  {
    _repo = repo;
    _log = log;
    _configuration = configuration;
  }

  public async Task<IEnumerable<CRMInstituteTypeDto>> GetInstituteTypesDDLAsync(bool trackChanges = false)
  {
    var list = await _repo.CRMInstituteTypes.GetInstituteTypesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("InstituteType");

    return MyMapper.JsonCloneIEnumerableToList<CrminstituteType, CRMInstituteTypeDto>(list);
  }

  public async Task<GridEntity<CRMInstituteTypeDto>> SummaryGrid(CRMGridOptions opt)
  {
    string sql = "SELECT * FROM CrmInstituteType";
    string orderBy = " InstituteTypeName asc ";
    return await _repo.CRMInstituteTypes.GridData<CRMInstituteTypeDto>(sql, opt, orderBy, "");
  }

  public async Task<string> CreateNewRecordAsync(CRMInstituteTypeDto dto)
  {
    if (dto.InstituteTypeId != 0)
      throw new InvalidCreateOperationException("InstituteTypeId must be 0.");

    bool dup = await _repo.CRMInstituteTypes
                .ExistsAsync(x => x.InstituteTypeName.Trim().ToLower() ==
                                  dto.InstituteTypeName.Trim().ToLower());
    if (dup) throw new DuplicateRecordException("InstituteType", "InstituteTypeName");

    var entity = MyMapper.JsonClone<CRMInstituteTypeDto, CrminstituteType>(dto);
    int id = await _repo.CRMInstituteTypes.CreateAndGetIdAsync(entity);
    if (id <= 0) throw new InvalidCreateOperationException();

    _log.LogInfo($"InstituteType created, id={id}");
    return OperationMessage.Success;
  }

  public async Task<string> UpdateRecordAsync(int key, CRMInstituteTypeDto dto, bool trackChanges)
  {
    if (key != dto.InstituteTypeId) return "Key mismatch.";

    bool exists = await _repo.CRMInstituteTypes
                    .ExistsAsync(x => x.InstituteTypeId == key);
    if (!exists) throw new GenericNotFoundException("InstituteType", "InstituteTypeId", key.ToString());

    var entity = MyMapper.JsonClone<CRMInstituteTypeDto, CrminstituteType>(dto);
    _repo.CRMInstituteTypes.Update(entity);
    await _repo.SaveAsync();
    _log.LogInfo($"InstituteType updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, CRMInstituteTypeDto dto)
  {
    if (key != dto.InstituteTypeId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CRMInstituteTypeDto));

    await _repo.CRMInstituteTypes.DeleteAsync(x => x.InstituteTypeId == key, true);
    await _repo.SaveAsync();
    _log.LogInfo($"InstituteType deleted, id={key}");
    return OperationMessage.Success;
  }

  public Task<string> SaveOrUpdateAsync(int key, CRMInstituteTypeDto dto)
      => dto.InstituteTypeId == 0
         ? CreateNewRecordAsync(dto)
         : UpdateRecordAsync(key, dto, false);
}
