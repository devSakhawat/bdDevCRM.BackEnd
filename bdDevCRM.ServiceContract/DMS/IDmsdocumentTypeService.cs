using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsdocumentTypeService
{
  Task<IEnumerable<DmsdocumentTypeDDL>> GetTypesDDLAsync(bool trackChanges);
  Task<GridEntity<DmsdocumentTypeDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsdocumentTypeDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsdocumentTypeDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsdocumentTypeDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsdocumentTypeDto modelDto);
}
