using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsdocumentFolderService
{
  Task<IEnumerable<DmsdocumentFolderDDL>> GetFoldersDDLAsync(bool trackChanges);
  Task<GridEntity<DmsdocumentFolderDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsdocumentFolderDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsdocumentFolderDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsdocumentFolderDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsdocumentFolderDto modelDto);
}
