using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;
public class CountryRepository : RepositoryBase<Country>, ICountryRepository
{
  public CountryRepository(CRMContext context) : base(context) { }

  // Get all countries
  public async Task<IEnumerable<Country>> GetCountriesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.CountryId).ToListAsync();

  // Get a single country by ID
  public async Task<Country> GetCountryAsync(int companyId, bool trackChanges) => await FindByCondition(c => c.CountryId.Equals(companyId), trackChanges).FirstOrDefaultAsync();

  // Add a new country
  public void CreateCountry(Country country) => Create(country);

  // Update an existing country
  public void UpdateCountry(Country country) => UpdateAsync(country);

  // Delete a country by ID
  public void DeleteCountry(Country country) => Delete(country);
}
