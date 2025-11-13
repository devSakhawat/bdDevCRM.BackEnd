using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.CRM;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
namespace bdDevCRM.Service.CRM;

internal sealed class CrmYearService : ICrmYearService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;


  public CrmYearService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
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
    string query = "Select WfStates.WfStateId,WfStates.STATENAME,WfStates.MENUID,WfStates.ISDEFAULTSTART,WfStates.ISCLOSED,MENU.MODULEID,MENU.MENUNAME,MODULE.MODULENAME \r\nfrom WFSTATE ,MENU ,MODULE  \r\nwhere WfStates.MENUID = MENU.MENUID and MENU.MODULEID = MODULE.MODULEID";
    string orderBy = " MENUNAME,ISDEFAULTSTART,STATENAME asc ";
    var gridEntity = await _repository.Workflowes.GridData<WfStateDto>(query, options, orderBy, "");

    return gridEntity;
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
      selector: x => new WfState
      {
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


}
