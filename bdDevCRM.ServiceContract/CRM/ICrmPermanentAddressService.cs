using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface ICrmPermanentAddressService
{
  Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<PermanentAddressDto>> GetActivePermanentAddressesAsync(bool trackChanges = false);
  Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesAsync(bool trackChanges = false);
  Task<PermanentAddressDto> GetPermanentAddressAsync(int id, bool trackChanges = false);
  Task<PermanentAddressDto> GetPermanentAddressByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<IEnumerable<PermanentAddressDto>> GetPermanentAddressesByCountryIdAsync(int countryId, bool trackChanges = false);
  Task<PermanentAddressDto> CreateNewRecordAsync(PermanentAddressDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, PermanentAddressDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, PermanentAddressDto dto);
  Task<GridEntity<PermanentAddressDto>> SummaryGrid(CRMGridOptions options);
}