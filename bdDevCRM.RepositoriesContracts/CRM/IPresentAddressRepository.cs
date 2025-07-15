using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IPresentAddressRepository : IRepositoryBase<PresentAddress>
{
  Task<IEnumerable<PresentAddress>> GetActivePresentAddressesAsync(bool track);
  Task<IEnumerable<PresentAddress>> GetPresentAddressesAsync(bool track);
  Task<PresentAddress?> GetPresentAddressAsync(int id, bool track);
  Task<PresentAddress?> GetPresentAddressByApplicantIdAsync(int applicantId, bool track);
  Task<IEnumerable<PresentAddress>> GetPresentAddressesByCountryIdAsync(int countryId, bool track);
}