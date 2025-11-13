using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentVersionService
{
  Task<IEnumerable<DmsDocumentVersionDDL>> GetVersionsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentVersionDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentVersionDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentVersionDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentVersionDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentVersionDto modelDto);
}
