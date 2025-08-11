using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface ICrmIntakeMonthService
{
  Task<IEnumerable<CrmIntakeMonthDDL>> GetIntakeMonthsDDLAsync(bool trackChanges);
  Task<GridEntity<CrmIntakeMonthDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(CrmIntakeMonthDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, CrmIntakeMonthDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmIntakeMonthDto modelDto);
  Task<string> SaveOrUpdate(int key, CrmIntakeMonthDto modelDto);
  Task<IEnumerable<CrmIntakeMonthDto>> GetIntakeMonthsAsync(bool trackChanges);
  Task<CrmIntakeMonthDto> GetIntakeMonthAsync(int intakeMonthId, bool trackChanges);
  Task<CrmIntakeMonthDto> CreateIntakeMonthAsync(CrmIntakeMonthDto entityForCreate);
}