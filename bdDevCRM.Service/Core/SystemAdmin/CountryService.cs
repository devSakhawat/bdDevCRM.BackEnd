using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

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

  public async Task<IEnumerable<CountryDDL>> GetCountriesDDLAsync(bool trackChanges = false)
  {
    IEnumerable<Country> countries = await _repository.Countries.GetActiveCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("Country");

    List<CountryDDL> countryDtos = MyMapper.JsonCloneIEnumerableToList<Country, CountryDDL>(countries);
    return countryDtos;
  }

  public async Task<GridEntity<CountryDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "Select * from Country";
    string orderBy = " CountryName asc ";
    var gridEntity = await _repository.Countries.GridData<CountryDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(CountryDto modelDto)
  {
    string res = string.Empty;

    #region New Record
    if (modelDto.CountryId != 0)
    {
      throw new InvalidCreateOperationException("CountryId must be 0 for creating a new country.");

    }
    else
    {
      bool isCountryExist = await _repository.Countries.ExistsAsync(x => x.CountryName.Trim().ToLower() == modelDto.CountryName.Trim().ToLower());
      if (isCountryExist) throw new DuplicateRecordException("Country", "CountryName");

      if (!isCountryExist)
      {
        Country country = MyMapper.JsonClone<CountryDto, Country>(modelDto);
        int lastCreatedId = await _repository.Countries.CreateAndGetIdAsync(country);
        if (string.IsNullOrEmpty(lastCreatedId.ToString()))
        {
          throw new InvalidCreateOperationException();
        }

        _logger.LogWarn($"New record create to Country. New id: {lastCreatedId} by .");
        return OperationMessage.Success;
      }
      else
      {
        res = "The country name already exist!";
        return res;
      }

    }
    #endregion New Record
  }

  public async Task<string> UpdateNewRecordAsync(int key, CountryDto modelDto, bool trackChanges)
  {
    string res = string.Empty;

    #region Update Country
    if (key > 0 && key == modelDto.CountryId)
    {
      Expression<Func<Country, bool>> expression = e => e.CountryId == key;
      bool exists = await _repository.Countries.ExistsAsync(expression);
      if (exists)
      {
        Country countryObj = MyMapper.JsonClone<CountryDto, Country>(modelDto);
        _repository.Countries.Update(countryObj);
        await _repository.SaveAsync();

        _logger.LogWarn($"Country with Id: {key} is about to be updated from the database by .");
        res = OperationMessage.Success;
      }
      else
      {
        res = "Update failed: country with the same name already exists.";
      }
    }
    else
    {
      res = "Invalid update attempt: key does not match the currency ID.";
    }
    #endregion Update Country

    return res;
  }

  public async Task<string> DeleteRecordAsync(int key, CountryDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new CountryDto().GetType().Name.ToString());
    if (key != modelDto.CountryId) throw new IdMismatchBadRequestException(key.ToString(), new CountryDto().GetType().Name.ToString());

    Country country = await _repository.Countries.FirstOrDefaultAsync(m => m.CountryId == key, trackChanges: false);
    if (country == null) throw new GenericNotFoundException("Country", "CountryId", key.ToString());

    await _repository.Countries.DeleteAsync(x => x.CountryId == modelDto.CountryId, trackChanges: true);
    await _repository.SaveAsync();
    _logger.LogWarn($"Country with Id: {key} is about to be deleted from the database.");
    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, CountryDto modelDto)
  {
    string res = string.Empty;
    bool isCountryExist = await _repository.Countries.ExistsAsync(x => x.CountryName.Trim().ToLower() == modelDto.CountryName.Trim().ToLower());

    #region New Recorod
    if (modelDto.CountryId == 0 && key == modelDto.CountryId)
    {
      if (isCountryExist) throw new DuplicateRecordException("Country", "CountryName");

      if (!isCountryExist)
      {
        Country country = MyMapper.JsonClone<CountryDto, Country>(modelDto);
        int lastCreatedId = await _repository.Countries.CreateAndGetIdAsync(country);
        if (string.IsNullOrEmpty(lastCreatedId.ToString()))
        {
          throw new InvalidCreateOperationException();
        }

        await _repository.SaveAsync();
        _logger.LogWarn($"New record create to Country. New id: {lastCreatedId} by .");
        return OperationMessage.Success;
      }
      else
      {
        res = "The country name already exist!";
        return res;
      }
    }
    #endregion New Currency

    #region Update Record
    else
    {
      if (key > 0 && key == modelDto.CountryId)
      {
        if (!isCountryExist)
        {
          Country country = MyMapper.JsonClone<CountryDto, Country>(modelDto);
          _repository.Countries.Update(country);
          await _repository.SaveAsync();

          _logger.LogWarn($"Country with Id: {key} is about to be updated from the database by .");
          res = OperationMessage.Success;
        }
        else
        {
          res = "Update failed: currency with the same name already exists.";
          return res;
        }
      }
      else
      {
        res = "Invalid update attempt: key does not match the currency ID.";
        return res;
      }
    }
    #endregion Update Record

    return res;
  }







  public async Task DeleteRecordAsync2(int countryId, bool trackChanges)
  {
    var country = await _repository.Countries.FirstOrDefaultAsync(c => c.CountryId.Equals(countryId));
    _logger.LogWarn($"Country with Id: {countryId} is about to be deleted from the database.");
    _repository.Countries.DeleteCountry(country);
    await _repository.SaveAsync();
  }

  public async Task<CountryDto> CreateCountryAsync(CountryDto entityForCreate)
  {
    Country country = MyMapper.JsonClone<CountryDto, Country>(entityForCreate);
    _repository.Countries.CreateCountry(country);
    await _repository.SaveAsync();
    return entityForCreate;
  }

  public async Task<IEnumerable<CountryDto>> GetCountriesAsync(bool trackChanges)
  {
    IEnumerable<Country> countries = await _repository.Countries.GetCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("Country");

    List<CountryDto> countryDtos = MyMapper.JsonCloneIEnumerableToList<Country, CountryDto>(countries);
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

}
