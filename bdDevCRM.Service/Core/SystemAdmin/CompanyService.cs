using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Utilities.OthersLibrary;
using System.Linq.Expressions;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class CompanyService : ICompanyService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;

  public CompanyService(IRepositoryManager repository, ILoggerManager logger)
  {
    _repository = repository;
    _logger = logger;
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




  public async Task<CompanyDto> CreateCompanyAsync(CompanyDto entityForCreate)
  {
    Company Company = MyMapper.JsonClone<CompanyDto, Company>(entityForCreate);
    _repository.Companies.CreateCompany(Company);
    await _repository.SaveAsync();
    return entityForCreate;
  }

  public async Task UpdateCompanyAsync(int CompanyId, CompanyDto CompanyForUpdate, bool trackChanges)
  {
    Expression<Func<Company, bool>> expression = e => e.CompanyId == CompanyId;
    bool exists = await _repository.Companies.ExistsAsync(expression);
    if (!exists) throw new GenericNotFoundException("Company", "CompanyId", CompanyId.ToString());

    Company Company = MyMapper.JsonClone<CompanyDto, Company>(CompanyForUpdate);
    _repository.Companies.UpdateCompany(Company);
    await _repository.SaveAsync();
  }

  public async Task DeleteCompanyAsync(int CompanyId, bool trackChanges)
  {
    var Company = await _repository.Companies.GetByIdWithNotFoundException(CompanyId);
    _logger.LogWarn($"Company with Id: {CompanyId} is about to be deleted from the database.");
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

  public async Task<CompanyDto> GetCompanyAsync(int CompanyId, bool trackChanges)
  {
    if (CompanyId <= 0) throw new ArgumentOutOfRangeException(nameof(CompanyId), "Company ID must be be zero or non-negative integer.");

    Company Company = await _repository.Companies.GetCompanyAsync(CompanyId, trackChanges);
    if (Company == null) throw new GenericNotFoundException("Company", "CompanyId", CompanyId.ToString());

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
