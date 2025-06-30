using bdDevCRM.Entities.Entities.System;

namespace bdDevCRM.RepositoriesContracts.Core.Authentication;

public interface IAuthenticationRepository : IRepositoryBase<Users>
{
  Task<Users> AuthenticateByLoginId(string loginId);

  Task<Users> AuthenticateByPassword(string loginId, string password);




  //IEnumerable<Company> GetAllCompanies(bool trackChanges);
  //Company GetCompany(int companyId, bool trackChanges);
  //void CreateCompany(Company Company);

  //IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges);


  //Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges);
  //Task<Company> GetCompanyAsync(int companyId, bool trackChanges);
  //void UpdateCompany(Company Company);
  //void DeleteCompany(Company Company);
}