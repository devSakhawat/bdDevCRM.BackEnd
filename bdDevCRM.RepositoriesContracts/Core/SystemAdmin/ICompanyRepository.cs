using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICompanyRepository : IRepositoryBase<Company>
{
  IEnumerable<Company> GetAllCompanies(bool trackChanges);
  Company GetCompany(int companyId, bool trackChanges);
  void CreateCompany(Company Company);

  IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges);
  Task<IEnumerable<CompanyRepositoryDto>> GetMotherCompanyForEditCompanyCombo(int companyId, int seastionCompanyId);
  string MotherCompanyQuary(int companyId, string additionalCondition);
  Task<IEnumerable<CompanyRepositoryDto>> GetMotherCompany(int companyId, string additionalCondition);
  Task<IEnumerable<CompanyRepositoryDto>> GetCompanyList(int companyId, string additionalCondition);


  Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges);
  Task<Company> GetCompanyAsync(int companyId, bool trackChanges);
  Task<Company?> CompanyByCompanyIdWithAdditionalCondition(int companyId, string additionalCondition);
  void UpdateCompany(Company Company);
  void DeleteCompany(Company Company);
}
