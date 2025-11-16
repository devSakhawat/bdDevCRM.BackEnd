using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICurrencyService
{
  Task<IEnumerable<CurrencyDDL>> GetCurrenciesDDLAsync();
  Task<GridEntity<CurrencyDto>> CurrecySummary(CRMGridOptions options);
  Task<string> SaveOrUpdate(int key,CurrencyDto modelDto);
  Task<string> DeleteCurrency(int key, CurrencyDto modelDto);


}
