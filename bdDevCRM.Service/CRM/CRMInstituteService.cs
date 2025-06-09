using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
namespace bdDevCRM.Service.CRM;


internal sealed class CRMInstituteService : ICRMInstituteService
{
  private readonly IRepositoryManager _repo;
  private readonly ILoggerManager _log;
  private readonly IConfiguration _config;


  public CRMInstituteService(IRepositoryManager repo, ILoggerManager log , IConfiguration config)
  {
    _repo = repo;
    _log = log;
    _config = config;
  }

  public async Task<IEnumerable<CrmInstituteDto>> GetInstitutesDDLAsync(bool trackChanges = false)
  {
    var list = await _repo.CRMInstitutes.GetActiveInstitutesAsync(trackChanges);
    if (!list.Any()) throw new GenericListNotFoundException("CrmInstitute");
    return MyMapper.JsonCloneIEnumerableToList<Crminstitute, CrmInstituteDto>(list);
  }

  public async Task<GridEntity<CrmInstituteDto>> SummaryGrid(CRMGridOptions opt)
  {
    string sql = "select * from CRMInstitute";
    string orderBy = " InstituteName asc ";
    return await _repo.CRMInstitutes.GridData<CrmInstituteDto>(sql, opt, orderBy, "");
  }

  public async Task<string> CreateNewRecordAsync(CrmInstituteDto dto)
  {
    if (dto.InstituteId != 0)
      throw new InvalidCreateOperationException("InstituteId must be 0.");

    // Fix: Ensure InstituteName is a string before calling Trim and ToLower
    bool dup = await _repo.CRMInstitutes.ExistsAsync(x =>
        x.InstituteName != null &&
        x.InstituteName.Trim().ToLower().Equals(dto.InstituteName!.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase));
    if (dup) throw new DuplicateRecordException("CrmInstitute", "InstituteName");

    var entity = MyMapper.JsonClone<CrmInstituteDto, Crminstitute>(dto);
    int id = await _repo.CRMInstitutes.CreateAndGetIdAsync(entity);
    if (id <= 0) throw new InvalidCreateOperationException();

    _log.LogInfo($"CrmInstitute created, id={id}");
    return OperationMessage.Success;
  }

  public async Task<string> UpdateRecordAsync(int key, CrmInstituteDto dto, bool trackChanges)
  {
    if (key != dto.InstituteId) return "Key mismatch.";

    bool exists = await _repo.CRMInstitutes.ExistsAsync(x => x.InstituteId == key);
    if (!exists) throw new GenericNotFoundException("CrmInstitute", "InstituteId", key.ToString());

    var entity = MyMapper.JsonClone<CrmInstituteDto, Crminstitute>(dto);
    _repo.CRMInstitutes.Update(entity);
    await _repo.SaveAsync();
    _log.LogInfo($"CrmInstitute updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> DeleteRecordAsync(int key, CrmInstituteDto dto)
  {
    if (key != dto.InstituteId)
      throw new IdMismatchBadRequestException(key.ToString(), nameof(CrmInstituteDto));

    await _repo.CRMInstitutes.DeleteAsync(x => x.InstituteId == key, true);
    await _repo.SaveAsync();
    _log.LogInfo($"CrmInstitute deleted, id={key}");
    return OperationMessage.Success;
  }

  public Task<string> SaveOrUpdateAsync(int key, CrmInstituteDto dto) => dto.InstituteId == 0 ? CreateNewRecordAsync(dto) : UpdateRecordAsync(key, dto, false);
}



//internal sealed class CRMInstituteService : ICRMInstituteService
//{
//  private readonly IRepositoryManager _repository;
//  private readonly ILoggerManager _logger;
//  private readonly IConfiguration _configuration;


//  public CRMInstituteService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
//  {
//    _repository = repository;
//    _logger = logger;
//    _configuration = configuration;
//  }

//  public async Task<IEnumerable<DDLInstituteDto>> CRMInstituteDLLByCountry(int countryId, bool trackChanges)
//  {
//    IEnumerable<DDLInstituteDto> institutesDDLByCountry = await _repository.CRMInstitute.ListByWhereWithSelectAsync(selector: x => new DDLInstituteDto { InstituteId = x.InstituteId, InstituteName = x.InstituteName }, expression: x => x.CountryId == countryId && x.Status == true, orderBy: x => x.InstituteId, trackChanges);
//    return institutesDDLByCountry;
//  }

//  //public async Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges)
//  //{
//  //  IEnumerable<WfActionRepositoryDto> wfActions = await _repository.WfState.ActionsByStatusIdForGroup(statusId, trackChanges);
//  //  IEnumerable<WfActionDto> wfActionsDto = MyMapper.JsonCloneIEnumerableToList<WfActionRepositoryDto, WfActionDto>(wfActions);
//  //  return wfActionsDto;
//  //}

//  //#region Workflow start
//  //public async Task<GridEntity<WfstateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options)
//  //{
//  //  string query = "Select WFSTATE.WFSTATEID,WFSTATE.STATENAME,WFSTATE.MENUID,WFSTATE.ISDEFAULTSTART,WFSTATE.ISCLOSED,MENU.MODULEID,MENU.MENUNAME,MODULE.MODULENAME \r\nfrom WFSTATE ,MENU ,MODULE  \r\nwhere WFSTATE.MENUID = MENU.MENUID and MENU.MODULEID = MODULE.MODULEID";
//  //  string orderBy = " MENUNAME,ISDEFAULTSTART,STATENAME asc ";
//  //  var gridEntity = await _repository.Workflow.GridData<WfstateDto>(query, options, orderBy, "");

//  //  return gridEntity;
//  //}

//  //public async Task<string> SaveWorkflow(WfstateDto modelDto)
//  //{
//  //  string res = string.Empty;

//  //  #region New WfState
//  //  var isDefaultStart = (bool)modelDto.IsDefaultStart ? 1 : 0;
//  //  if (modelDto.WfstateId == 0)
//  //  {
//  //    bool isDefaultExist = await _repository.WfState.ExistsAsync(x => x.IsDefaultStart == false && x.MenuId == modelDto.MenuId && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim());

//  //    if (!isDefaultExist)
//  //    {
//  //      //if (!IsExistsUserByEmployee(usersDto.EmployeeId))
//  //      if (!await _repository.WfState.ExistsAsync(x => x.MenuId == modelDto.MenuId && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim()))
//  //      {
//  //        Wfstate wfstate = MyMapper.JsonClone<WfstateDto, Wfstate>(modelDto);
//  //        int lastCreatedWfStateId = await _repository.WfState.CreateAndGetIdAsync(wfstate);
//  //        await _repository.SaveAsync();
//  //        return OperationMessage.Success;
//  //      }
//  //      else
//  //      {
//  //        res = "The state name already exist!";
//  //      }
//  //    }
//  //    else
//  //    {
//  //      res = "The state name already exist or Only one StatusId can be IsDefault!";
//  //      return res;
//  //    }
//  //  }
//  //  #endregion New WfState

//  //  #region Update WfState
//  //  else
//  //  {
//  //    bool isDefaultExist = await _repository.WfState.ExistsAsync(x => x.IsDefaultStart == modelDto.IsDefaultStart && x.WfstateId == modelDto.WfstateId);

//  //    if (!isDefaultExist)
//  //    {


//  //      Wfstate wfstate = MyMapper.JsonClone<WfstateDto, Wfstate>(modelDto);
//  //      _repository.WfState.Update(wfstate);
//  //      await _repository.SaveAsync();

//  //      return OperationMessage.Success;
//  //    }
//  //    else
//  //    {
//  //      res = "Only one StatusId can be IsDefault!";
//  //      return res;
//  //    }
//  //  }
//  //  #endregion Update WfState
//  //  return res;
//  //}

//  //public async Task<string> CreateActionAsync(WfActionDto modelDto)
//  //{
//  //  string res = string.Empty;

//  //  #region New WfState
//  //  if (modelDto.WfactionId == 0)
//  //  {
//  //    bool isActionExistByStateId = await _repository.WfAction.ExistsAsync(x => x.WfstateId == modelDto.WfstateId && x.ActionName.ToLower().Trim() == modelDto.ActionName.ToLower().Trim());

//  //    if (!isActionExistByStateId)
//  //    {

//  //      Wfaction wfAcation = MyMapper.JsonClone<WfActionDto, Wfaction>(modelDto);
//  //      int lastCreatedWfActionId = await _repository.WfAction.CreateAndGetIdAsync(wfAcation);
//  //      await _repository.SaveAsync();
//  //      return OperationMessage.Success;
//  //    }
//  //    else
//  //    {
//  //      res = "The action name already exist for the state!";
//  //      return res;
//  //    }
//  //  }
//  //  #endregion New WfState

//  //  #region Update WfState
//  //  else
//  //  {
//  //    bool isActionExistByStateId = await _repository.WfAction.ExistsAsync(x => x.WfstateId == modelDto.WfstateId && x.ActionName == modelDto.ActionName);

//  //    if (!isActionExistByStateId)
//  //    {
//  //      Wfaction wfAction = MyMapper.JsonClone<WfActionDto, Wfaction>(modelDto);
//  //      _repository.WfAction.Update(wfAction);
//  //      await _repository.SaveAsync();

//  //      return OperationMessage.Success;
//  //    }
//  //    else
//  //    {
//  //      res = "The action name already exist for the state!";
//  //      return res;
//  //    }
//  //  }
//  //  #endregion Update WfState
//  //}

//  //public async Task<string> DeleteAction(int key, WfActionDto modelDto)
//  //{
//  //  if (modelDto == null) throw new NullModelBadRequestException(new WfActionDto().GetType().Name.ToString());
//  //  if (key != modelDto.WfactionId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

//  //  Wfaction wfactionData = await _repository.WfAction.FirstOrDefaultAsync(m => m.WfactionId == key, trackChanges: false);
//  //  if (wfactionData == null) throw new GenericNotFoundException("Wfaction", "ActionId", key.ToString());

//  //  await _repository.WfAction.DeleteAsync(x => x.WfactionId == modelDto.WfactionId, trackChanges: true);
//  //  await _repository.SaveAsync();
//  //  return OperationMessage.Success;
//  //}

//  //public async Task<IEnumerable<WfstateDto>> GetNextStatesByMenu(int menuId)
//  //{
//  //  IEnumerable<Wfstate> wfstates = await _repository.WfState.ListByWhereWithSelectAsync(
//  //    selector: x => new Wfstate
//  //    {
//  //      WfstateId = x.WfstateId,
//  //      StateName = x.StateName
//  //    },
//  //    expression: x => x.MenuId == menuId,
//  //    orderBy: x => x.Sequence,
//  //    trackChanges: false
//  //    );

//  //  IEnumerable<WfstateDto> wfstatesDto = MyMapper.JsonCloneIEnumerableToIEnumerable<Wfstate, WfstateDto>(wfstates);
//  //  return wfstatesDto;
//  //}

//  //public async Task<GridEntity<WfActionDto>> GetActionByStatusId(int stateId, CRMGridOptions options)
//  //{
//  //  const string SELECT_ACTION_BY_STATUSID =
//  //      "Select *, (Select StateName from WFState where WFStateId = NextStateId) as NextStateName from WFAction where WFStateId = {0} ";
//  //  string orderBy = " AcSortOrder asc ";

//  //  string formattedQuery = string.Format(SELECT_ACTION_BY_STATUSID, stateId);
//  //  var gridEntity = await _repository.WfAction.GridData<WfActionDto>(formattedQuery, options, orderBy, "");

//  //  return gridEntity;
//  //}

//  //#endregion Workflow end


//}
