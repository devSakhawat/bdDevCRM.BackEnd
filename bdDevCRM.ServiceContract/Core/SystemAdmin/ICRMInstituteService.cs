using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface ICRMInstituteService
{
  Task<IEnumerable<CrmInstituteDto>> GetInstitutesDDLAsync(bool trackChanges = false);
  Task<GridEntity<CrmInstituteDto>> SummaryGrid(CRMGridOptions options);
  Task<CrmInstituteDto> CreateNewRecordAsync(CrmInstituteDto dto ,UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, CrmInstituteDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmInstituteDto dto);

  Task<CrmInstituteDto> GetInstituteNameIdAsync(string name, bool trackChanges = false);

  //Task<string> SaveOrUpdateAsync(int key, CrmInstituteDto dto);
}


//public interface ICRMInstituteService
//{
//  Task<IEnumerable<DDLInstituteDto>> CRMInstituteDLLByCountry(int countryId, bool trackChanges);


//  //Task<IEnumerable<CountryDto>> GetCountriesAsync(bool trackChanges);
//  //Task<CountryDto> GetCountryAsync(int countryId, bool trackChanges);
//  //Task<IEnumerable<CountryDDL>> GetCountriesDDLAsync(bool trackChanges);
//  //Task<CountryDto> CreateCountryAsync(CountryDto entityForCreate);
//  //Task<IEnumerable<CountryDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
//  //Task<(IEnumerable<CountryDto> countries, string ids)> CreateCountryCollectionAsync
//  //  (IEnumerable<CountryDto> countryCollection);
//  //Task DeleteCountryAsync(int countryId, bool trackChanges);
//  //Task UpdateCountryAsync(int countryId, CountryDto countryForUpdate, bool trackChanges);
//}
