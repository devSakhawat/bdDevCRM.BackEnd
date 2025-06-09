using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICountryService
{

  Task<IEnumerable<CountryDDL>> GetCountriesDDLAsync(bool trackChanges);
  Task<GridEntity<CountryDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(CountryDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, CountryDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CountryDto modelDto);
  Task<string> SaveOrUpdate(int key, CountryDto modelDto);


  Task<IEnumerable<CountryDto>> GetCountriesAsync(bool trackChanges);
  Task<CountryDto> GetCountryAsync(int countryId, bool trackChanges);
  Task<CountryDto> CreateCountryAsync(CountryDto entityForCreate);
}
