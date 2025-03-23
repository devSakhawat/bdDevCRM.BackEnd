using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IGroupService
{
  Task<GridEntity<GroupDto>> GroupSummary(bool trackChanges, CRMGridOptions options);
}
