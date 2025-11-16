using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentTypeService
{
  Task<IEnumerable<DmsDocumentTypeDDL>> GetTypesDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentTypeDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentTypeDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentTypeDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentTypeDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentTypeDto modelDto);
}
