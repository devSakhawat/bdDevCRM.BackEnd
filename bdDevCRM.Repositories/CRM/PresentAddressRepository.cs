using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmPresentAddressRepository : RepositoryBase<CrmPresentAddress>, ICrmPresentAddressRepository
{
  public CrmPresentAddressRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmPresentAddress>> GetActivePresentAddressesAsync(bool track) =>
      await ListAsync(c => c.PresentAddressId, track);

  public async Task<IEnumerable<CrmPresentAddress>> GetPresentAddressesAsync(bool track) =>
      await ListAsync(c => c.PresentAddressId, track);

  public async Task<CrmPresentAddress?> GetPresentAddressAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PresentAddressId == id, track);

  public async Task<CrmPresentAddress?> GetPresentAddressByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);

  public async Task<IEnumerable<CrmPresentAddress>> GetPresentAddressesByCountryIdAsync(int countryId, bool track) =>
      await ListByConditionAsync(x => x.CountryId == countryId, c => c.PresentAddressId, track);
}