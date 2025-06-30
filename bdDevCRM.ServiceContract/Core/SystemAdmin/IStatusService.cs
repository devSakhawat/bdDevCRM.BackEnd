using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IStatusService
{
  Task<IEnumerable<WfstateDto>> StatusByMenuId(int menuId, bool trackChanges);
  Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges);
  Task<GridEntity<WfstateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options);
  Task<string> SaveWorkflow(WfstateDto modelDto);
  Task<string> CreateActionAsync(WfActionDto modelDto);
  Task<string> DeleteAction(int key, WfActionDto modelDto);

  Task<IEnumerable<WfstateDto>> GetNextStatesByMenu(int menuId);

  Task<GridEntity<WfActionDto>> GetActionByStatusId(int stateId, CRMGridOptions options);
}
