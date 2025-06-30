using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;

namespace bdDevCRM.ServiceContract.DMS;


public interface IDmsdocumentTagService
{
  Task<IEnumerable<DmsdocumentTagDDL>> GetTagsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsdocumentTagDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsdocumentTagDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsdocumentTagDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsdocumentTagDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsdocumentTagDto modelDto);
}
