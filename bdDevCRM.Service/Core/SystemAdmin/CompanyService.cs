using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class CompanyService : ICompanyService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;


  public CompanyService(IRepositoryManager repository, ILoggerManager logger ,IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
  {
    var companies = _repository.Companies.GetAllCompanies(trackChanges).ToList();
    if (companies.Count() == 0) throw new GenericListNotFoundException("Company");
    var companyDtos = MyMapper.JsonCloneIEnumerableToList<Company, CompanyDto>(companies);
    return companyDtos;
  }

  public CompanyDto GetCompany(int id, bool trackChanges)
  {
    var company = _repository.Companies.GetCompany(id, trackChanges);
    //Check if the company is null
    if (company == null) throw new GenericNotFoundException("Company", "CompanyId", id.ToString());

    var companyDto = new CompanyDto();
    companyDto = MyMapper.JsonClone<Company, CompanyDto>(company);
    return companyDto;
  }

  public CompanyDto CreateCompany(CompanyDto company)
  {
    Company companyEntity = MyMapper.JsonClone<CompanyDto, Company>(company);
    _repository.Companies.CreateCompany(companyEntity);
    _repository.Save();

    var companyToReturn = MyMapper.JsonClone<Company, CompanyDto>(companyEntity);
    return companyToReturn;
  }

  public IEnumerable<CompanyDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
  {
    if (ids is null)
      throw new IdParametersBadRequestException();
    var companyEntities = _repository.Companies.GetByIds(ids, trackChanges);
    if (ids.Count() != companyEntities.Count())
      throw new CollectionByIdsBadRequestException("Companies");
    var companiesToReturn = MyMapper.JsonCloneIEnumerableToList<Company, CompanyDto>(companyEntities);
    return companiesToReturn;
  }

  public async Task<IEnumerable<CompanyDto>> GetMotherCompanyForEditCompanyCombo(int companyId, int seastionCompnayId)
  {
    IEnumerable<CompanyRepositoryDto> companyRepositoriesDto 
      = await _repository.Companies.GetMotherCompanyForEditCompanyCombo(companyId, seastionCompnayId);
    IEnumerable<CompanyDto> companyDtos = MyMapper.JsonCloneIEnumerableToList<CompanyRepositoryDto, CompanyDto>(companyRepositoriesDto);
    return companyDtos;
  }

  public async Task<IEnumerable<CompanyDto>> GetMotherCompany(int companyId, UsersDto users)
  {
    string additionalCondition = await _repository.AccessRestrictions.GenerateAccessRestrictionConditionForCompany((int)users.EmployeeId);
    if (additionalCondition != "") additionalCondition = " or " + additionalCondition;

    if (users.AccessParentCompany == 1 && additionalCondition == "")
    {
      companyId = 0;
      IEnumerable<CompanyRepositoryDto> companyRepositoriesDto = await _repository.Companies.GetMotherCompany(companyId, additionalCondition);
      return MyMapper.JsonCloneIEnumerableToList<CompanyRepositoryDto, CompanyDto>(companyRepositoriesDto);
    }
    else
    {
      int controlPanelModuleId = Convert.ToInt32(_configuration["AppSettings:controlPanelModuleId"]);

      var res = await _repository.GroupPermissiones.GetAccessPermisionForCurrentUser(controlPanelModuleId, (int)users.UserId);
      var isHr = res.Any(groupPermission => groupPermission.ReferenceID == 22);
      if (isHr && additionalCondition == "")
      {
        IEnumerable<CompanyRepositoryDto> motherCompaniesRepositoryDto = await _repository.Companies.GetMotherCompany(companyId, additionalCondition);
        return MyMapper.JsonCloneIEnumerableToList<CompanyRepositoryDto, CompanyDto>(motherCompaniesRepositoryDto);
      }
      else
      {
        IEnumerable<CompanyRepositoryDto> getCompanyList = await _repository.Companies.GetCompanyList(companyId, additionalCondition);
        return MyMapper.JsonCloneIEnumerableToList<CompanyRepositoryDto, CompanyDto>(getCompanyList);
      }
    }
  }




  public async Task<CompanyDto> CreateCompanyAsync(CompanyDto entityForCreate)
  {
    Company Company = MyMapper.JsonClone<CompanyDto, Company>(entityForCreate);
    _repository.Companies.CreateCompany(Company);
    await _repository.SaveAsync();
    return entityForCreate;
  }

  public async Task UpdateCompanyAsync(int companyId, CompanyDto companyForUpdate, bool trackChanges)
  {
    Expression<Func<Company, bool>> expression = e => e.CompanyId == companyId;
    bool exists = await _repository.Companies.ExistsAsync(expression);
    if (!exists) throw new GenericNotFoundException("Company", "CompanyId", companyId.ToString());

    Company Company = MyMapper.JsonClone<CompanyDto, Company>(companyForUpdate);
    _repository.Companies.UpdateCompany(Company);
    await _repository.SaveAsync();
  }

  public async Task DeleteCompanyAsync(int companyId, bool trackChanges)
  {
    var Company = await _repository.Companies.FirstOrDefaultAsync(x => x.CompanyId.Equals(companyId), trackChanges);
    _logger.LogWarn($"Company with Id: {companyId} is about to be deleted from the database.");
    _repository.Companies.DeleteCompany(Company);
    await _repository.SaveAsync();

    // i think the method should return int value.
  }

  public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool trackChanges)
  {
    IEnumerable<Company> Companies = await _repository.Companies.GetCompaniesAsync(trackChanges);
    List<CompanyDto> CompanyDtos = MyMapper.JsonCloneIEnumerableToList<Company, CompanyDto>(Companies);
    return CompanyDtos;
  }

  public async Task<CompanyDto> GetCompanyAsync(int companyId, bool trackChanges)
  {
    if (companyId <= 0) throw new ArgumentOutOfRangeException(nameof(companyId), "Company ID must be be zero or non-negative integer.");

    Company Company = await _repository.Companies.GetCompanyAsync(companyId, trackChanges);
    if (Company == null) throw new GenericNotFoundException("Company", "CompanyId", companyId.ToString());

    CompanyDto CompanyDto = MyMapper.JsonClone<Company, CompanyDto>(Company);
    return CompanyDto;
  }

  public Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
  {
    throw new NotImplementedException();
  }

  public Task<(IEnumerable<CompanyDto> Companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyDto> CompanyCollection)
  {
    throw new NotImplementedException();
  }
}
