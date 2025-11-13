using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmInstituteTypeService
{
  Task<IEnumerable<CRMInstituteTypeDto>> GetInstituteTypesDDLAsync(bool trackChanges = false);
  Task<GridEntity<CRMInstituteTypeDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(CRMInstituteTypeDto dto);
  Task<string> UpdateRecordAsync(int key, CRMInstituteTypeDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CRMInstituteTypeDto dto);
  Task<string> SaveOrUpdateAsync(int key, CRMInstituteTypeDto dto);
}
