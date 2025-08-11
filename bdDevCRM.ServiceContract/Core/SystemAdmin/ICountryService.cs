using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICrmCountryService
{
  Task<IEnumerable<CrmCountryDDL>> GetCountriesDDLAsync(bool trackChanges);
  Task<GridEntity<CrmCountryDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(CrmCountryDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, CrmCountryDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmCountryDto modelDto);
  Task<string> SaveOrUpdate(int key, CrmCountryDto modelDto);


  Task<IEnumerable<CrmCountryDto>> GetCountriesAsync(bool trackChanges);
  Task<CrmCountryDto> GetCountryAsync(int countryId, bool trackChanges);
  Task<CrmCountryDto> CreateCountryAsync(CrmCountryDto entityForCreate);
}
