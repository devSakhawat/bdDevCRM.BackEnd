using bdDevCRM.Shared.DataTransferObjects;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICountryService
{
  Task<IEnumerable<CountryDto>> GetCountriesAsync(bool trackChanges);
  Task<CountryDto> GetCountryAsync(int countryId, bool trackChanges);
  Task<CountryDto> CreateCountryAsync(CountryDto entityForCreate);
  Task<IEnumerable<CountryDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
  Task<(IEnumerable<CountryDto> countries, string ids)> CreateCountryCollectionAsync
    (IEnumerable<CountryDto> countryCollection);
  Task DeleteCountryAsync(int countryId, bool trackChanges);
  Task UpdateCountryAsync(int countryId, CountryDto countryForUpdate, bool trackChanges);
}
