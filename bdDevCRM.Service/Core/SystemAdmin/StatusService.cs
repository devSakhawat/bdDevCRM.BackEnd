using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Common;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

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

  public async Task<IEnumerable<WfstateDto>> StatusByMenuId(int menuId, bool trackChanges)
  {
    IEnumerable<WfStateRepositoryDto> wfstates = await _repository.WfState.StatusByMenuId(menuId, trackChanges);
    IEnumerable<WfstateDto> wfstatesDto = MyMapper.JsonCloneIEnumerableToList<WfStateRepositoryDto, WfstateDto>(wfstates);
    return wfstatesDto;
  }

  public async Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges)
  {
    IEnumerable<WfActionRepositoryDto> wfActions = await _repository.WfState.ActionsByStatusIdForGroup(statusId, trackChanges);
    IEnumerable<WfActionDto> wfActionsDto = MyMapper.JsonCloneIEnumerableToList<WfActionRepositoryDto, WfActionDto>(wfActions);
    return wfActionsDto;
  }

  #region Workflow start
  public async Task<GridEntity<WfstateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options)
  {
    string query = "Select WFSTATE.WFSTATEID,WFSTATE.STATENAME,WFSTATE.MENUID,WFSTATE.ISDEFAULTSTART,WFSTATE.ISCLOSED,MENU.MODULEID,MENU.MENUNAME,MODULE.MODULENAME \r\nfrom WFSTATE ,MENU ,MODULE  \r\nwhere WFSTATE.MENUID = MENU.MENUID and MENU.MODULEID = MODULE.MODULEID";
    string orderBy = " MENUNAME,ISDEFAULTSTART,STATENAME asc ";
    var gridEntity = await _repository.Workflow.GridData<WfstateDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> SaveWorkflow(WfstateDto modelDto)
  {
    string res = string.Empty;

    #region New WfState
    int isDefaultStart = modelDto.IsDefaultStart ? 1 : 0;
    if (modelDto.WFStateId == 0)
    {
      bool isDefaultExist = await _repository.WfState.ExistsAsync(x => x.IsDefaultStart == false  && x.MenuId == modelDto.MenuID && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim());

      if (!isDefaultExist)
      {
        //if (!IsExistsUserByEmployee(usersDto.EmployeeId))
        if (!await _repository.WfState.ExistsAsync(x => x.MenuId == modelDto.MenuID && x.StateName.ToString().Trim() == modelDto.StateName.ToString().Trim()))
        {
          Wfstate wfstate = MyMapper.JsonClone<WfstateDto, Wfstate>(modelDto);
          int lastCreatedWfStateId = await _repository.WfState.CreateAndGetIdAsync(wfstate);
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
      bool isDefaultExist = await _repository.WfState.ExistsAsync(x => x.IsDefaultStart == modelDto.IsDefaultStart && x.WfstateId == modelDto.WFStateId);

      if (!isDefaultExist)
      {


        Wfstate wfstate = MyMapper.JsonClone<WfstateDto, Wfstate>(modelDto);
        _repository.WfState.Update(wfstate);
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


  #endregion Workflow end


}
