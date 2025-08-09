using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IStatusService
{
  Task<IEnumerable<WfStateDto>> StatusByMenuId(int menuId, bool trackChanges);
  Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges);
  Task<GridEntity<WfStateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options);
  Task<string> SaveWorkflow(WfStateDto modelDto);
  Task<string> CreateActionAsync(WfActionDto modelDto);
  Task<string> DeleteAction(int key, WfActionDto modelDto);

  Task<IEnumerable<WfStateDto>> GetNextStatesByMenu(int menuId);

  Task<GridEntity<WfActionDto>> GetActionByStatusId(int stateId, CRMGridOptions options);
}
