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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class StatusRepository : RepositoryBase<WfState>, IStatusRepository
{
  public StatusRepository(CRMContext context) : base(context) { }

  private const string SELECT_STATUS_BY_MENUID = "Select WFState.*, Menu.MenuName \r\nfrom WFState \r\ninner join Menu on Menu.MenuID = WFState.MenuId\r\nwhere WFState.MenuId = {0}";

  private const string SELECT_ACTION_BY_STATUSID =
            "Select *, (Select StateName from WFState where WfStateId = NextStateId) as NEXTSTATENAME from WFAction where WfStateId = {0} order by AcSortOrder";


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

  public async Task<IEnumerable<WfStateRepositoryDto>> GetWfStateByUserPermission(int menuId, int userId)
  {
    string query = string.Format(@"Select distinct * 
      from WFState 
      where WFStateId in (
        Select ReferenceId 
        from GroupPermission 
        where PermissionTableName = 'Status' 
          and GroupId in (Select GroupId from GroupMember where UserId ={0})
      ) and WFState.MenuId={1} order by Sequence", userId, menuId);

    IEnumerable<WfStateRepositoryDto> result = await ExecuteListQuery<WfStateRepositoryDto>(query);
    return result;
  }


  public async Task<IEnumerable<WfStateRepositoryDto>> GetWFStateByUserPermission(int menuId, int userId)
  {
    string query = @"
      SELECT DISTINCT ws.*
      FROM WFState ws
      WHERE ws.WFStateId IN
      (
        SELECT gp.ReferenceId
        FROM GroupPermission gp
        WHERE gp.PermissionTableName = 'Status'
          AND gp.GroupId IN (SELECT gm.GroupId FROM GroupMember gm WHERE gm.UserId = @UserId)
      ) AND ws.MenuId = @MenuId 
      ORDER BY ws.Sequence";

    var parameters = new SqlParameter[]
    {
      new SqlParameter("@MenuId", menuId),
      new SqlParameter("@UserId", userId),
    };

    IEnumerable<WfStateRepositoryDto> result = await ExecuteListQuery<WfStateRepositoryDto>(query, parameters);
    return result;
  }

  public async Task<IEnumerable<WfStateRepositoryDto>> GetWFStateByMenuNUserPermission(string menuName, int userId)
  {
    string query = string.Format(@"
      SELECT DISTINCT ws.*
      FROM WFState ws
		  inner join Menu on Menu.MenuID = ws.MenuId
      WHERE ws.WFStateId IN
      (
        SELECT gp.ReferenceId
        FROM GroupPermission gp
        WHERE gp.PermissionTableName = 'Status'
          AND gp.GroupId IN (SELECT gm.GroupId FROM GroupMember gm WHERE gm.UserId = {0})
      )
      AND Menu.MenuPath like '%{1}%'
      ORDER BY ws.Sequence", userId, menuName);

    IEnumerable<WfStateRepositoryDto> result = await ExecuteListQuery<WfStateRepositoryDto>(query);
    return result;
  }


}
