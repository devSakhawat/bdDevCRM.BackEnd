using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class CountryService : ICountryService
{

  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public CountryService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<CountryDto> CreateCountryAsync(CountryDto entityForCreate)
  {
    Country country = MyMapper.JsonClone<CountryDto, Country>(entityForCreate);
    _repository.Countries.CreateCountry(country);
    await _repository.SaveAsync();
    return entityForCreate;
  }

  public async Task UpdateCountryAsync(int countryId, CountryDto countryForUpdate, bool trackChanges)
  {
    Expression<Func<Country, bool>> expression = e => e.CountryId == countryId;
    bool exists = await _repository.Countries.ExistsAsync(expression);
    if (!exists) throw new GenericNotFoundException("Country", "CountryId", countryId.ToString());

    Country country = MyMapper.JsonClone<CountryDto, Country>(countryForUpdate);
    _repository.Countries.UpdateCountry(country);
    await _repository.SaveAsync();
  }

  public async Task DeleteCountryAsync(int countryId, bool trackChanges)
  {
    var country = await _repository.Countries.FirstOrDefaultAsync(c => c.CountryId.Equals(countryId));
    _logger.LogWarn($"Country with Id: {countryId} is about to be deleted from the database.");
    _repository.Countries.DeleteCountry(country);
    await _repository.SaveAsync();
  }

  public async Task<IEnumerable<CountryDto>> GetCountriesAsync(bool trackChanges)
  {
    IEnumerable<Country> countries = await _repository.Countries.GetCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("Country");

    List<CountryDto> countryDtos = MyMapper.JsonCloneIEnumerableToList<Country, CountryDto>(countries);
    return countryDtos;
  }

  public async Task<IEnumerable<CountryDDL>> GetCountriesDDLAsync(bool trackChanges)
  {
    IEnumerable<Country> countries = await _repository.Countries.GetActiveCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("Country");

    List<CountryDDL> countryDtos = MyMapper.JsonCloneIEnumerableToList<Country, CountryDDL>(countries);
    return countryDtos;
  }

  public async Task<CountryDto> GetCountryAsync(int countryId, bool trackChanges)
  {
    if (countryId <= 0) throw new ArgumentOutOfRangeException(nameof(countryId), "Country ID must be be zero or non-negative integer.");

    Country country = await _repository.Countries.GetCountryAsync(countryId, trackChanges);
    if (country == null) throw new GenericNotFoundException("Country", "CountryId", countryId.ToString());

    CountryDto countryDto = MyMapper.JsonClone<Country, CountryDto>(country);
    return countryDto;
  }

  public Task<IEnumerable<CountryDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
  {
    throw new NotImplementedException();
  }

  public Task<(IEnumerable<CountryDto> countries, string ids)> CreateCountryCollectionAsync(IEnumerable<CountryDto> countryCollection)
  {
    throw new NotImplementedException();
  }
}
