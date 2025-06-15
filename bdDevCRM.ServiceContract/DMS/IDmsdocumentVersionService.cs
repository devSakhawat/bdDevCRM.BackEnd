using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsdocumentVersionService
{
  Task<IEnumerable<DmsdocumentVersionDDL>> GetVersionsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsdocumentVersionDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsdocumentVersionDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsdocumentVersionDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsdocumentVersionDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsdocumentVersionDto modelDto);
}
