using Azure;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class StatusService : IStatusService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;


  public StatusService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<WfStateDto>> StatusByMenuId(int menuId, bool trackChanges)
  {
    IEnumerable<WfStateRepositoryDto> wfstates = await _repository.WfStates.StatusByMenuId(menuId, trackChanges);
    IEnumerable<WfStateDto> wfstatesDto = MyMapper.JsonCloneIEnumerableToList<WfStateRepositoryDto, WfStateDto>(wfstates);
    return wfstatesDto;
  }

  public async Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges)
  {
    IEnumerable<WfActionRepositoryDto> wfActions = await _repository.WfStates.ActionsByStatusIdForGroup(statusId, trackChanges);
    IEnumerable<WfActionDto> wfActionsDto = MyMapper.JsonCloneIEnumerableToList<WfActionRepositoryDto, WfActionDto>(wfActions);
    return wfActionsDto;
  }

  #region Workflow start
  public async Task<GridEntity<WfStateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options)
  {
    string query = "Select WFSTATE.WfStateId,WFSTATE.STATENAME,WFSTATE.MENUID,WFSTATE.ISDEFAULTSTART,WFSTATE.ISCLOSED,MENU.MODULEID,MENU.MENUNAME,MODULE.MODULENAME \r\nfrom WFSTATE ,MENU ,MODULE  \r\nwhere WFSTATE.MENUID = MENU.MENUID and MENU.MODULEID = MODULE.MODULEID";
    string orderBy = " MENUNAME,ISDEFAULTSTART,STATENAME asc ";
    var gridEntity = await _repository.Workflowes.GridData<WfStateDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<WfStateDto> CreateNewRecordAsync(WfStateDto modelDto ,UsersDto currentUser)
  {
    if (modelDto.WfStateId != 0)
      throw new InvalidCreateOperationException("CourseId must be 0.");

    bool dup = await _repository.WfStates.ExistsAsync(x => x.StateName != null && x.StateName.Trim().ToLower().Equals(modelDto.StateName!.Trim().ToLower()));
    if (dup) throw new DuplicateRecordException("Workflow", "StateName");

    var entity = MyMapper.JsonClone<WfStateDto, WfState>(modelDto);
    modelDto.WfStateId = await _repository.WfStates.CreateAndGetIdAsync(entity);

    return modelDto;
  }

  public async Task<string> UpdateRecordAsync(int key, WfStateDto modelDto, bool trackChanges, UsersDto currentUser)
  {
    if (key != modelDto.WfStateId) return "Key mismatch.";

    bool exists = await _repository.WfStates.ExistsAsync(x => x.WfStateId == key);
    if (!exists) throw new GenericNotFoundException("Status", "WfStateId", key.ToString());

    var entity = MyMapper.JsonClone<WfStateDto, WfState>(modelDto);
    _repository.WfStates.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"Status updated, id={key}");
    return OperationMessage.Success;
  }


  public async Task<WfActionDto> CreateWfActionNewRecordAsync(WfActionDto modelDto, UsersDto currentUser, bool trackChanges)
  {
    if (modelDto.WfActionId != 0)
      throw new InvalidCreateOperationException("CourseId must be 0.");

    bool dup = await _repository.WfActions.ExistsAsync(x => x.ActionName != null && x.ActionName.Trim().ToLower().Equals(modelDto.ActionName!.Trim().ToLower()));
    if (dup) throw new DuplicateRecordException("Workflow", "ActionName");

    var entity = MyMapper.JsonClone<WfActionDto, WfAction>(modelDto);
    modelDto.WfActionId = await _repository.WfActions.CreateAndGetIdAsync(entity);

    return modelDto;
  }

  public async Task<string> UpdateWfActionRecordAsync(int key, WfActionDto modelDto, UsersDto currentUser, bool trackChanges = false)
  {
    if (key != modelDto.WfActionId) return "Key mismatch.";

    bool exists = await _repository.WfActions.ExistsAsync(x => x.WfStateId == key);
    if (!exists) throw new GenericNotFoundException("Status", "ActionId", key.ToString());

    var entity = MyMapper.JsonClone<WfActionDto, WfAction>(modelDto);
    _repository.WfActions.Update(entity);
    await _repository.SaveAsync();
    _logger.LogInfo($"Action updated, id={key}");
    return OperationMessage.Success;
  }

  public async Task<string> SaveWorkflow(WfStateDto modelDto)
  {
    string res = string.Empty;

    #region New WfState
    var isDefaultStart = (bool)modelDto.IsDefaultStart ? 1 : 0;
    if (modelDto.WfStateId == 0)
    {
      bool isDefaultExist = await _repository.WfStates.ExistsAsync(x => x.IsDefaultStart == false && x.MenuId == modelDto.MenuId && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim());

      if (!isDefaultExist)
      {
        //if (!IsExistsUserByEmployee(usersDto.EmployeeId))
        if (!await _repository.WfStates.ExistsAsync(x => x.MenuId == modelDto.MenuId && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim()))
        {
          WfState wfstate = MyMapper.JsonClone<WfStateDto, WfState>(modelDto);
          int lastCreatedWfStateId = await _repository.WfStates.CreateAndGetIdAsync(wfstate);
          await _repository.SaveAsync();
          return OperationMessage.Success;
        }
        else
        {
          res = "The state name already exist!";
        }
      }
      else
      {
        res = "The state name already exist or Only one StatusId can be IsDefault!";
        return res;
      }
    }
    #endregion New WfState

    #region Update WfState
    else
    {
      bool isDefaultExist = await _repository.WfStates.ExistsAsync(x => x.IsDefaultStart == modelDto.IsDefaultStart && x.WfStateId == modelDto.WfStateId);

      if (!isDefaultExist)
      {


        WfState wfstate = MyMapper.JsonClone<WfStateDto, WfState>(modelDto);
        _repository.WfStates.Update(wfstate);
        await _repository.SaveAsync();

        return OperationMessage.Success;
      }
      else
      {
        res = "Only one StatusId can be IsDefault!";
        return res;
      }
    }
    #endregion Update WfState
    return res;
  }

  public async Task<string> CreateActionAsync(WfActionDto modelDto)
  {
    string res = string.Empty;

    #region New WfState
    if (modelDto.WfActionId == 0)
    {
      bool isActionExistByStateId = await _repository.WfActions.ExistsAsync(x => x.WfStateId == modelDto.WfStateId && x.ActionName.ToLower().Trim() == modelDto.ActionName.ToLower().Trim());

      if (!isActionExistByStateId)
      {

        WfAction wfAcation = MyMapper.JsonClone<WfActionDto, WfAction>(modelDto);
        int lastCreatedWfActionId = await _repository.WfActions.CreateAndGetIdAsync(wfAcation);
        await _repository.SaveAsync();
        return OperationMessage.Success;
      }
      else
      {
        res = "The action name already exist for the state!";
        return res;
      }
    }
    #endregion New WfState

    #region Update WfState
    else
    {
      bool isActionExistByStateId = await _repository.WfActions.ExistsAsync(x => x.WfStateId == modelDto.WfStateId && x.ActionName == modelDto.ActionName);

      if (!isActionExistByStateId)
      {
        WfAction wfAction = MyMapper.JsonClone<WfActionDto, WfAction>(modelDto);
        _repository.WfActions.Update(wfAction);
        await _repository.SaveAsync();

        return OperationMessage.Success;
      }
      else
      {
        res = "The action name already exist for the state!";
        return res;
      }
    }
    #endregion Update WfState
  }

  public async Task<string> DeleteAction(int key, WfActionDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new WfActionDto().GetType().Name.ToString());
    if (key != modelDto.WfActionId) throw new IdMismatchBadRequestException(key.ToString(), new ModuleDto().GetType().Name.ToString());

    WfAction wfactionData = await _repository.WfActions.FirstOrDefaultAsync(m => m.WfActionId == key, trackChanges: false);
    if (wfactionData == null) throw new GenericNotFoundException("WfAction", "ActionId", key.ToString());

    await _repository.WfActions.DeleteAsync(x => x.WfActionId == modelDto.WfActionId, trackChanges: true);
    await _repository.SaveAsync();
    return OperationMessage.Success;
  }

  public async Task<IEnumerable<WfStateDto>> GetNextStatesByMenu(int menuId)
  {
    IEnumerable<WfState> wfstates = await _repository.WfStates.ListByWhereWithSelectAsync(
      selector: x => new WfState { 
      WfStateId = x.WfStateId,
      StateName = x.StateName
      }, 
      expression: x => x.MenuId == menuId,
      orderBy: x => x.Sequence,
      trackChanges: false
      );

    IEnumerable<WfStateDto> wfstatesDto = MyMapper.JsonCloneIEnumerableToIEnumerable<WfState, WfStateDto>(wfstates);
    return wfstatesDto;
  }

  public async Task<GridEntity<WfActionDto>> GetActionByStatusId(int stateId, CRMGridOptions options)
  {
    const string SELECT_ACTION_BY_STATUSID =
        "Select *, (Select StateName from WFState where WfStateId = NextStateId) as NextStateName from WFAction where WfStateId = {0} ";
    string orderBy = " AcSortOrder asc ";

    string formattedQuery = string.Format(SELECT_ACTION_BY_STATUSID, stateId);
    var gridEntity = await _repository.WfActions.GridData<WfActionDto>(formattedQuery, options, orderBy, "");

    return gridEntity;
  }
  #endregion Workflow end


  public async Task<IEnumerable<WfStateDto>> GetWFStateByUserPermission(int menuId, int userId)
  {
    IEnumerable<WfStateDto> statusByMenuAndUser = MyMapper.JsonCloneIEnumerableToIEnumerable<WfStateRepositoryDto, WfStateDto>(await _repository.WfStates.GetWFStateByUserPermission(menuId, userId));
    return statusByMenuAndUser;
  }

  public async Task<IEnumerable<WfStateDto>> GetWFStateByMenuNUserPermission(string menuName, int userId)
  {
    IEnumerable<WfStateDto> statusByMenuAndUser = MyMapper.JsonCloneIEnumerableToIEnumerable<WfStateRepositoryDto, WfStateDto>(await _repository.WfStates.GetWFStateByMenuNUserPermission(menuName, userId));
    return statusByMenuAndUser;
  }

}
