using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
  public CompanyRepository(CRMContext context) : base(context) { }

  public IEnumerable<Company> GetAllCompanies(bool trackChanges) => List(x=> x.CompanyName, trackChanges);

  public Company GetCompany(int companyId, bool trackChanges) => FirstOrDefault(c => c.CompanyId.Equals(companyId), trackChanges);

  public IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges) => GetListByIds(x => ids.Contains(x.CompanyId), trackChanges);


  // Get all Companies
  public async Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges) => await ListAsync(x => x.CompanyId, trackChanges);

  // Get a single Company by ID
  public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges) => await FirstOrDefaultAsync(c => c.CompanyId.Equals(companyId), trackChanges); 

  public async Task<Company?> CompanyByCompanyIdWithAdditionalCondition(int companyId, string additionalCondition)
  {
    var quary = string.Format("Select * from Company where CompanyId = {0} {1}", companyId, additionalCondition);
    Company? objCompany = await ExecuteSingleData<Company>(quary);
    return objCompany;
  }


  // Add a new Company
  public void CreateCompany(Company Company) => Create(Company);

  // Update an existing Company
  public void UpdateCompany(Company Company) => UpdateByState(Company);

  // Delete a Company by ID
  public void DeleteCompany(Company Company) => Delete(Company);
}
