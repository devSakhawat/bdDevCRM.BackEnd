using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface ICrmIntakeYearService
{
  Task<IEnumerable<CrmIntakeYearDDL>> GetIntakeYearsDDLAsync(bool trackChanges);
  Task<GridEntity<CrmIntakeYearDto>> SummaryGrid(bool trackChanges, CRMGridOptions options, UsersDto user);
  Task<string> CreateNewRecordAsync(CrmIntakeYearDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, CrmIntakeYearDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmIntakeYearDto modelDto);
  Task<string> SaveOrUpdate(int key, CrmIntakeYearDto modelDto);
  Task<IEnumerable<CrmIntakeYearDto>> GetIntakeYearsAsync(bool trackChanges);
  Task<CrmIntakeYearDto> GetIntakeYearAsync(int intakeYearId, bool trackChanges);
  Task<CrmIntakeYearDto> CreateIntakeYearAsync(CrmIntakeYearDto entityForCreate);
}