using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentAccessLogService
{
  Task<IEnumerable<DmsDocumentAccessLogDDL>> GetAccessLogsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentAccessLogDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentAccessLogDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentAccessLogDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentAccessLogDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentAccessLogDto modelDto);
}