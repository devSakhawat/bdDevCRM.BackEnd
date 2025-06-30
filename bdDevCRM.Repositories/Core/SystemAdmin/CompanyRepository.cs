using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
  private const string GETMOTHERCOMPANYQUARYFOREDITCOMPANYSQL = @"WITH hierarchy AS (
                            SELECT ROW_NUMBER() OVER (ORDER BY t.CompanyName) as RowIndex , t.CompanyId , t.CompanyName , t.PrimaryContact , t.Email , t.Fax , t.Phone , t.Address , t.FullLogoPath , t.FullLogoPathForReport , t.LetterHeader , t.LetterFooter , t.MotherId ,t.CompanyCode
                            FROM dbo.Company t
                            WHERE t.CompanyId={0}
                            UNION ALL
                            SELECT ROW_NUMBER() OVER (ORDER BY x.CompanyName) as RowIndex ,x.CompanyId , x.CompanyName , x.PrimaryContact , x.Email , x.Fax , x.Phone , x.Address , x.FullLogoPath , x.FullLogoPathForReport , x.LetterHeader , x.LetterFooter , x.MotherId ,x.CompanyCode
                            FROM dbo.Company x
                            JOIN hierarchy y ON x.CompanyId = y.MotherId)
                            SELECT * FROM hierarchy s where CompanyId <> {0} ORDER BY CompanyName";

  public CompanyRepository(CRMContext context) : base(context) { }

  public IEnumerable<Company> GetAllCompanies(bool trackChanges) => List(x=> x.CompanyName, trackChanges);

  public Company GetCompany(int companyId, bool trackChanges) => FirstOrDefault(c => c.CompanyId.Equals(companyId), trackChanges);

  public IEnumerable<Company> GetByIds(IEnumerable<int> ids, bool trackChanges) => GetListByIds(x => ids.Contains(x.CompanyId), trackChanges);

  public async Task<IEnumerable<CompanyRepositoryDto>> GetMotherCompanyForEditCompanyCombo(int companyId, int seastionCompnayId)
  {
    var sql = string.Format(GETMOTHERCOMPANYQUARYFOREDITCOMPANYSQL, companyId, seastionCompnayId);
    IEnumerable<CompanyRepositoryDto> companyRepositoriesDto = await ExecuteListQuery<CompanyRepositoryDto>(sql);
    return companyRepositoriesDto;
  }

  public async Task<IEnumerable<CompanyRepositoryDto>> GetMotherCompany(int companyId, string additionalCondition)
  {
    var sql = MotherCompanyQuary(companyId, additionalCondition);
    IEnumerable<CompanyRepositoryDto> companyRepositoriesDto = await ExecuteListQuery<CompanyRepositoryDto>(sql);
    return companyRepositoriesDto;
  }

  public async Task<IEnumerable<CompanyRepositoryDto>> GetCompanyList(int companyId, string additionalCondition)
  {
    var sql = string.Format("Select * from Company where CompanyId = {0} {1}", companyId, additionalCondition);
    IEnumerable<CompanyRepositoryDto> companyRepositoriesDto = await ExecuteListQuery<CompanyRepositoryDto>(sql);
    return companyRepositoriesDto;
  }


  public string MotherCompanyQuary(int companyId, string additionalCondition)
  {
    string sql = "";
    var companyOrder = "CompanyName";
    var assemblyQuery = string.Format(@"Select * from AssemblyInfo");
    var objAssembley = ExecuteSingleDataSyncronous<AssemblyInfo>(assemblyQuery);

    if (objAssembley.AssemblyInfoId == 1) companyOrder = "CompanyCode";

    if (companyId == 0)
    {
      sql = string.Format(@"Select [CompanyId],[CompanyCode],[CompanyName],[Address],[Phone],[Fax],[Email],[FullLogoPath],[PrimaryContact],[Flag],[FiscalYearStart],[MotherId],[IsCostCentre]
      ,[IsActive],[GratuityStartDate],[FullLogoPathForReport],[LetterHeader],[LetterFooter],[CompanyTin],ISNULL(IsPfApplicable,0) as IsPfApplicable,ISNULL(IsEwfApplicable,0) as IsEwfApplicable,ISNULL(IsPfApplicabe,0) as IsPfApplicabe,ISNULL(IsEwfApplicabe,0) as IsEwfApplicabe
      ,[CompanyAlias],[CompanyZone],[CompanyCircle],[IsCompanyContributionDisable] from Company where IsActive = 1 {0} order by {1}", additionalCondition, companyOrder);
    }
    else
    {
      sql =
          string.Format(@"with hierarchy (CompanyId, CompanyName, PrimaryContact, Email, Fax, Phone, Address, FullLogoPath, MotherId 
,Flag ,FiscalYearStart ,IsActive ,CompanyCode )
as ( select CompanyId, CompanyName, PrimaryContact, Email, Fax, Phone, Address, FullLogoPath, MotherId, Flag ,FiscalYearStart ,IsActive ,CompanyCode 
from Company  where  CompanyId={0} and IsActive=1 union all select x.CompanyId , x.CompanyName , x.PrimaryContact , x.Email , x.Fax , x.Phone , x.Address
, x.FullLogoPath , x.MotherId ,x.Flag ,x.FiscalYearStart , x.IsActive ,x.CompanyCode from   Company x join hierarchy y on (y.CompanyId = x.MotherId and x.IsActive=1) ) select * from   hierarchy where IsActive=1 {1} ORDER BY {2} ASC", companyId, additionalCondition, companyOrder);
    }
    return sql;
  }


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
