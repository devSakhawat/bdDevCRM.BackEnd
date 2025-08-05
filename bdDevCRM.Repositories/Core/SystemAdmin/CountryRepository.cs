using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;
public class CrmCountryRepository : RepositoryBase<CrmCountry>, ICrmCountryRepository
{
  public CrmCountryRepository(CRMContext context) : base(context) { }

  // Get all countries
  public async Task<IEnumerable<CrmCountry>> GetCountriesAsync(bool trackChanges) => await ListAsync(c => c.CountryId, trackChanges);

  // Get all Active countries
  public async Task<IEnumerable<CrmCountry>> GetActiveCountriesAsync(bool trackChanges) => await ListByConditionAsync(x => x.Status == 1, c => c.CountryId, trackChanges);
  // Get a single country by ID
  public async Task<CrmCountry> GetCountryAsync(int companyId, bool trackChanges) => await FirstOrDefaultAsync(x => x.CountryId.Equals(companyId), trackChanges);

  // Add a new country
  public void CreateCountry(CrmCountry country) => Create(country);

  // Update an existing country
  public void UpdateCountry(CrmCountry country) => UpdateByState(country);

  // Delete a country by ID
  public void DeleteCountry(CrmCountry country) => Delete(country);
}
