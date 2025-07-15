using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class PresentAddressRepository : RepositoryBase<PresentAddress>, IPresentAddressRepository
{
  public PresentAddressRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<PresentAddress>> GetActivePresentAddressesAsync(bool track) =>
      await ListAsync(c => c.PresentAddressId, track);

  public async Task<IEnumerable<PresentAddress>> GetPresentAddressesAsync(bool track) =>
      await ListAsync(c => c.PresentAddressId, track);

  public async Task<PresentAddress?> GetPresentAddressAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PresentAddressId == id, track);

  public async Task<PresentAddress?> GetPresentAddressByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);

  public async Task<IEnumerable<PresentAddress>> GetPresentAddressesByCountryIdAsync(int countryId, bool track) =>
      await ListByConditionAsync(x => x.CountryId == countryId, c => c.PresentAddressId, track);
}