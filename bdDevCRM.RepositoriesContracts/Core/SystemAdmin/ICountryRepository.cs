using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.System;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmCountryRepository : IRepositoryBase<CrmCountry>
{
  Task<IEnumerable<CrmCountry>> GetCountriesAsync(bool trackChanges);
  Task<IEnumerable<CrmCountry>> GetActiveCountriesAsync(bool trackChanges);
  Task<CrmCountry> GetCountryAsync(int companyId, bool trackChanges);
  void CreateCountry(CrmCountry country);
  void UpdateCountry(CrmCountry country);
  void DeleteCountry(CrmCountry country);
}
