using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class StatusRepository : RepositoryBase<WfState>, IStatusRepository
{
  public StatusRepository(CRMContext context) : base(context) { }

  private const string SELECT_STATUS_BY_MENUID = "Select WFState.*, Menu.MenuName \r\nfrom WFState \r\ninner join Menu on Menu.MenuID = WFState.MenuId\r\nwhere WFState.MenuId = {0}";

  private const string SELECT_ACTION_BY_STATUSID =
            "Select *, (Select StateName from WFState where WFStateId = NextStateId) as NEXTSTATENAME from WFAction where WFStateId = {0} order by AcSortOrder";


  public async Task<IEnumerable<WfStateRepositoryDto>> StatusByMenuId(int menuId, bool trackChanges)
  {
    string stateByMenuQuery = string.Format(SELECT_STATUS_BY_MENUID, menuId);
    IEnumerable<WfStateRepositoryDto> wfstates = await ExecuteListQuery<WfStateRepositoryDto>(stateByMenuQuery, null);
    return wfstates;
  }
  public async Task<IEnumerable<WfActionRepositoryDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges)
  {
    string wfActionByStatusIdQuery = string.Format(SELECT_ACTION_BY_STATUSID, statusId);
    IEnumerable<WfActionRepositoryDto> wfActionsByStatus = await ExecuteListQuery<WfActionRepositoryDto>(wfActionByStatusIdQuery, null);

    return wfActionsByStatus;
  }



}
