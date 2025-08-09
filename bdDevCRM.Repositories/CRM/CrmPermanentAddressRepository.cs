using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmPermanentAddressRepository : RepositoryBase<CrmPermanentAddress>, ICrmPermanentAddressRepository
{
  public CrmPermanentAddressRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmPermanentAddress>> GetActivePermanentAddressesAsync(bool track) =>
      await ListAsync(c => c.PermanentAddressId, track);

  public async Task<IEnumerable<CrmPermanentAddress>> GetPermanentAddressesAsync(bool track) =>
      await ListAsync(c => c.PermanentAddressId, track);

  public async Task<CrmPermanentAddress?> GetPermanentAddressAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PermanentAddressId == id, track);

  public async Task<CrmPermanentAddress?> GetPermanentAddressByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);

  public async Task<IEnumerable<CrmPermanentAddress>> GetPermanentAddressesByCountryIdAsync(int countryId, bool track) =>
      await ListByConditionAsync(x => x.CountryId == countryId, c => c.PermanentAddressId, track);
}