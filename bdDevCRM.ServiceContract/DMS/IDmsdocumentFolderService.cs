using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentFolderService
{
  Task<IEnumerable<DmsDocumentFolderDDL>> GetFoldersDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentFolderDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentFolderDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentFolderDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentFolderDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentFolderDto modelDto);
}
