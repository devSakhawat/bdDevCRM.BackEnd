using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.ServiceContract.DMS;

public interface IDmsDocumentService
{
  Task<IEnumerable<DmsDocumentDDL>> GetDocumentsDDLAsync(bool trackChanges);
  Task<GridEntity<DmsDocumentDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(DmsDocumentDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, DmsDocumentDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, DmsDocumentDto modelDto);
  Task<string> SaveOrUpdate(int key, DmsDocumentDto modelDto);
  //// single record
  //Task<DmsDocument> GetExistingDocumentAsync(string entityId, string entityType, string documentType);

  Task<string> SaveFileAndDocumentWithAllDmsAsync(IFormFile file, string allAboutDMS);
}