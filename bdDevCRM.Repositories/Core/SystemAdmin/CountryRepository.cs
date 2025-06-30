using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;
public class CountryRepository : RepositoryBase<Country>, ICountryRepository
{
  public CountryRepository(CRMContext context) : base(context) { }

  // Get all countries
  public async Task<IEnumerable<Country>> GetCountriesAsync(bool trackChanges) => await ListAsync(c => c.CountryId, trackChanges);

  // Get all Active countries
  public async Task<IEnumerable<Country>> GetActiveCountriesAsync(bool trackChanges) => await ListByConditionAsync(x => x.Status == 1,c => c.CountryId, trackChanges);
  // Get a single country by ID
  public async Task<Country> GetCountryAsync(int companyId, bool trackChanges) => await FirstOrDefaultAsync(x => x.CountryId.Equals(companyId), trackChanges);

  // Add a new country
  public void CreateCountry(Country country) => Create(country);

  // Update an existing country
  public void UpdateCountry(Country country) => UpdateByState(country);

  // Delete a country by ID
  public void DeleteCountry(Country country) => Delete(country);
}
