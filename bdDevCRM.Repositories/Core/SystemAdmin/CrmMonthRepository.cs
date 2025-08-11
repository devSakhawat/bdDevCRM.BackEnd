using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;
public class CrmMonthRepository : RepositoryBase<CrmMonth>, ICrmMonthRepository
{
  public CrmMonthRepository(CRMContext context) : base(context) { }

  // Get all Active Month
  public async Task<IEnumerable<CrmMonth>> GetActiveMonthAsync(bool trackChanges) =>
      await ListByConditionAsync(x => x.Status == true, c => c.MonthId, trackChanges, descending: false);

  ////// Get a single country by ID
  ////public async Task<Country> GetCountryAsync(int companyId, bool trackChanges) =>
  ////    await FirstOrDefaultAsync(x => x.CountryId.Equals(companyId), trackChanges);

  ////// Add a new country
  ////public void CreateCountry(Country country) => Create(country);

  ////// Update an existing country
  ////public void UpdateCountry(Country country) => UpdateByState(country);

  ////// Delete a country by ID
  ////public void DeleteCountry(Country country) => Delete(country);

  ////// Fix for CS0535: Implement GetCountriesAsync
  ////public async Task<IEnumerable<Country>> GetCountriesAsync(bool trackChanges) =>
  ////    await _context.Set<Country>().ToListAsync();

  ////// Fix for CS0535: Implement GetActiveCountriesAsync
  ////public async Task<IEnumerable<Country>> GetActiveCountriesAsync(bool trackChanges) =>
  ////    await _context.Set<Country>().Where(c => c.Status == true).ToListAsync();

  ////// Fix for CS8613: Adjust nullability in ExecuteSingleSql
  ////public override async Task<CRMMonth?> ExecuteSingleSql(string query) =>
  ////    await base.ExecuteSingleSql(query);
}
