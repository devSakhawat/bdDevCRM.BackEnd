using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IPresentAddressService
{
  Task<IEnumerable<PresentAddressDto>> GetPresentAddressesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<PresentAddressDto>> GetActivePresentAddressesAsync(bool trackChanges = false);
  Task<IEnumerable<PresentAddressDto>> GetPresentAddressesAsync(bool trackChanges = false);
  Task<PresentAddressDto> GetPresentAddressAsync(int id, bool trackChanges = false);
  Task<PresentAddressDto> GetPresentAddressByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<IEnumerable<PresentAddressDto>> GetPresentAddressesByCountryIdAsync(int countryId, bool trackChanges = false);
  Task<PresentAddressDto> CreateNewRecordAsync(PresentAddressDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, PresentAddressDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, PresentAddressDto dto);
  Task<GridEntity<PresentAddressDto>> SummaryGrid(CRMGridOptions options);
}