using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IPermanentAddressRepository : IRepositoryBase<PermanentAddress>
{
  Task<IEnumerable<PermanentAddress>> GetActivePermanentAddressesAsync(bool track);
  Task<IEnumerable<PermanentAddress>> GetPermanentAddressesAsync(bool track);
  Task<PermanentAddress?> GetPermanentAddressAsync(int id, bool track);
  Task<PermanentAddress?> GetPermanentAddressByApplicantIdAsync(int applicantId, bool track);
  Task<IEnumerable<PermanentAddress>> GetPermanentAddressesByCountryIdAsync(int countryId, bool track);
}