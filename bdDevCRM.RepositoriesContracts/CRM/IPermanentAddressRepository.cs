using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmPermanentAddressRepository : IRepositoryBase<CrmPermanentAddress>
{
  Task<IEnumerable<CrmPermanentAddress>> GetActivePermanentAddressesAsync(bool track);
  Task<IEnumerable<CrmPermanentAddress>> GetPermanentAddressesAsync(bool track);
  Task<CrmPermanentAddress?> GetPermanentAddressAsync(int id, bool track);
  Task<CrmPermanentAddress?> GetPermanentAddressByApplicantIdAsync(int applicantId, bool track);
  Task<IEnumerable<CrmPermanentAddress>> GetPermanentAddressesByCountryIdAsync(int countryId, bool track);
}