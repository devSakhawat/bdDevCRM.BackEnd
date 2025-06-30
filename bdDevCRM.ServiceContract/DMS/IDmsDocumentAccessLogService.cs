using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsdocumentAccessLogService
{
  Task<IEnumerable<DmsdocumentAccessLogDDL>> GetAccessLogsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsdocumentAccessLogDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsdocumentAccessLogDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsdocumentAccessLogDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsdocumentAccessLogDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsdocumentAccessLogDto modelDto);
}