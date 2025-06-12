using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentService
{
  Task<IEnumerable<KeyValuePair<int, string>>> GetDocumentDDLAsync();
  Task<GridEntity<DmsDocumentDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateAsync(DmsDocumentDto dto);
  Task<string> UpdateAsync(int key, DmsDocumentDto dto);
  Task<string> DeleteAsync(int key);
}

