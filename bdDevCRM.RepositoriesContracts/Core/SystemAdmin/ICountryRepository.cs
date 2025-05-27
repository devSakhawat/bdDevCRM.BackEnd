using bdDevCRM.Entities.Entities;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICountryRepository : IRepositoryBase<Country>
{
  Task<IEnumerable<Country>> GetCountriesAsync(bool trackChanges);
  Task<IEnumerable<Country>> GetActiveCountriesAsync(bool trackChanges);
  Task<Country> GetCountryAsync(int companyId, bool trackChanges);
  void CreateCountry(Country country);
  void UpdateCountry(Country country);
  void DeleteCountry(Country country);
}
