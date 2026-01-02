using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IAccessControlService
{
  Task<AccessControlDto> CreateAsync(AccessControlDto modelDto);
  Task<AccessControlDto> UpdateAsync(int key, AccessControlDto modelDto);

  Task<GridEntity<AccessControlDto>> AccessControlSummary(bool trackChanges, CRMGridOptions options);
  Task<string> DeleteRecordAsync(int key);
}
