using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
  public CompanyRepository(CRMContext context) : base(context) { }

  public IEnumerable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.CompanyName).ToList();

  public Company GetCompany(int companyId, bool trackChanges) => FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges).SingleOrDefault();

  public IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.CompanyId), trackChanges).ToList();


  // Get all Companies
  public async Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.CompanyId).ToListAsync();

  // Get a single Company by ID
  public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges) => await FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges).FirstOrDefaultAsync();

  public async Task<Company?> CompanyByCompanyIdWithAdditionalCondition(int companyId, string additionalCondition)
  {
    var quary = string.Format("Select * from Company where CompanyId = {0} {1}", companyId, additionalCondition);
    Company? objCompany = await GetSingleGenericResultByQuery<Company>(quary);
    return objCompany;
  }



  // Add a new Company
  public void CreateCompany(Company Company) => Create(Company);

  // Update an existing Company
  public void UpdateCompany(Company Company) => UpdateAsync(Company);

  // Delete a Company by ID
  public void DeleteCompany(Company Company) => Delete(Company);
}
