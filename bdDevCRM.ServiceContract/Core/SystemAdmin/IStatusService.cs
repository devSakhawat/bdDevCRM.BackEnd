using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using System.Threading.Tasks;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IStatusService
{
  Task<IEnumerable<WfStateDto>> StatusByMenuId(int menuId, bool trackChanges);
  Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges);
  Task<GridEntity<WfStateDto>> WorkflowSummary(bool trackChanges, CRMGridOptions options);
  Task<WfStateDto> CreateNewRecordAsync(WfStateDto modelDto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, WfStateDto modelDto, bool trackChanges, UsersDto currentUser);
  Task<WfActionDto> CreateWfActionNewRecordAsync(WfActionDto modelDto, UsersDto currentUser, bool trackChanges);
  Task<string> UpdateWfActionRecordAsync(int key, WfActionDto modelDto, UsersDto currentUser, bool trackChanges = false);

  Task<string> SaveWorkflow(WfStateDto modelDto);
  Task<string> CreateActionAsync(WfActionDto modelDto);
  Task<string> DeleteAction(int key, WfActionDto modelDto);

  Task<IEnumerable<WfStateDto>> GetNextStatesByMenu(int menuId);

  Task<GridEntity<WfActionDto>> GetActionByStatusId(int stateId, CRMGridOptions options);
}
