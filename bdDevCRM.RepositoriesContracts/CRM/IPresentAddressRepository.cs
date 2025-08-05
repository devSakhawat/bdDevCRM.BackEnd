using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmPresentAddressRepository : IRepositoryBase<CrmPresentAddress>
{
  Task<IEnumerable<CrmPresentAddress>> GetActivePresentAddressesAsync(bool track);
  Task<IEnumerable<CrmPresentAddress>> GetPresentAddressesAsync(bool track);
  Task<CrmPresentAddress?> GetPresentAddressAsync(int id, bool track);
  Task<CrmPresentAddress?> GetPresentAddressByApplicantIdAsync(int applicantId, bool track);
  Task<IEnumerable<CrmPresentAddress>> GetPresentAddressesByCountryIdAsync(int countryId, bool track);
}