using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class PermanentAddressRepository : RepositoryBase<PermanentAddress>, IPermanentAddressRepository
{
  public PermanentAddressRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<PermanentAddress>> GetActivePermanentAddressesAsync(bool track) =>
      await ListAsync(c => c.PermanentAddressId, track);

  public async Task<IEnumerable<PermanentAddress>> GetPermanentAddressesAsync(bool track) =>
      await ListAsync(c => c.PermanentAddressId, track);

  public async Task<PermanentAddress?> GetPermanentAddressAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.PermanentAddressId == id, track);

  public async Task<PermanentAddress?> GetPermanentAddressByApplicantIdAsync(int applicantId, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantId == applicantId, track);

  public async Task<IEnumerable<PermanentAddress>> GetPermanentAddressesByCountryIdAsync(int countryId, bool track) =>
      await ListByConditionAsync(x => x.CountryId == countryId, c => c.PermanentAddressId, track);
}