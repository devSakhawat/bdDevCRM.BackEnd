using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICompanyService
{
  IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
  CompanyDto GetCompany(int companyId, bool trackChanges);
  CompanyDto CreateCompany(CompanyDto company);
  IEnumerable<CompanyDto> GetByIds(IEnumerable<int> ids, bool trackChanges);
  Task<IEnumerable<CompanyDto>> GetMotherCompanyForEditCompanyCombo(int companyId, int seastionCompnayId);
  Task<IEnumerable<CompanyDto>> GetMotherCompany(int companyId, UsersDto users);

  Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool trackChanges);
  Task<CompanyDto> GetCompanyAsync(int companyId, bool trackChanges);
  Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
  Task<(IEnumerable<CompanyDto> Companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyDto> CompanyCollection);
  Task<CompanyDto> CreateCompanyAsync(CompanyDto entityForCreate);
  Task DeleteCompanyAsync(int companyId, bool trackChanges);
  Task UpdateCompanyAsync(int companyId, CompanyDto companyForUpdate, bool trackChanges);
}
