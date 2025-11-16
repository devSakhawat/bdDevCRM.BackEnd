using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;

namespace bdDevCRM.ServiceContract.DMS;


public interface IDmsDocumentTagService
{
  Task<IEnumerable<DmsDocumentTagDDL>> GetTagsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentTagDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentTagDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentTagDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentTagDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentTagDto modelDto);
}
