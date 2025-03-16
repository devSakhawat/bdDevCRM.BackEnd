using bdDevCRM.Shared.DataTransferObjects;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface ICompanyService
{
  IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
  CompanyDto GetCompany(int companyId, bool trackChanges);
  CompanyDto CreateCompany(CompanyDto company);
  IEnumerable<CompanyDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool trackChanges);
  Task<CompanyDto> GetCompanyAsync(int CompanyId, bool trackChanges);
  Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
  Task<(IEnumerable<CompanyDto> Companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyDto> CompanyCollection);
  Task<CompanyDto> CreateCompanyAsync(CompanyDto entityForCreate);
  Task DeleteCompanyAsync(int CompanyId, bool trackChanges);
  Task UpdateCompanyAsync(int CompanyId, CompanyDto CompanyForUpdate, bool trackChanges);
}
