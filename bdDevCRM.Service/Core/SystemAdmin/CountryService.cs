using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class CrmCountryService : ICrmCountryService
{

  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public CrmCountryService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<CrmCountryDDL>> GetCountriesDDLAsync(bool trackChanges = false)
  {
    IEnumerable<CrmCountry> countries = await _repository.Countries.GetActiveCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("CrmCountry");

    List<CrmCountryDDL> countryDtos = MyMapper.JsonCloneIEnumerableToList<CrmCountry, CrmCountryDDL>(countries);
    return countryDtos;
  }

  public async Task<GridEntity<CrmCountryDto>> SummaryGrid(CRMGridOptions options)
  {
    string query = "Select * from CrmCountry";
    string orderBy = " CountryName asc ";
    var gridEntity = await _repository.Countries.GridData<CrmCountryDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> CreateNewRecordAsync(CrmCountryDto modelDto)
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
      if (isCountryExist) throw new DuplicateRecordException("CrmCountry", "CountryName");

      if (!isCountryExist)
      {
        CrmCountry country = MyMapper.JsonClone<CrmCountryDto, CrmCountry>(modelDto);
        int lastCreatedId = await _repository.Countries.CreateAndGetIdAsync(country);
        if (string.IsNullOrEmpty(lastCreatedId.ToString()))
        {
          throw new InvalidCreateOperationException();
        }

        _logger.LogWarn($"New record create to CrmCountry. New id: {lastCreatedId} by .");
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

  public async Task<string> UpdateNewRecordAsync(int key, CrmCountryDto modelDto, bool trackChanges)
  {
    string res = string.Empty;

    #region Update CrmCountry
    if (key > 0 && key == modelDto.CountryId)
    {
      Expression<Func<CrmCountry, bool>> expression = e => e.CountryId == key;
      bool exists = await _repository.Countries.ExistsAsync(expression);
      if (exists)
      {
        CrmCountry countryObj = MyMapper.JsonClone<CrmCountryDto, CrmCountry>(modelDto);
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

  public async Task<string> DeleteRecordAsync(int key, CrmCountryDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new CrmCountryDto().GetType().Name.ToString());
    if (key != modelDto.CountryId) throw new IdMismatchBadRequestException(key.ToString(), new CrmCountryDto().GetType().Name.ToString());

    CrmCountry country = await _repository.Countries.FirstOrDefaultAsync(m => m.CountryId == key, trackChanges: false);
    if (country == null) throw new GenericNotFoundException("Country", "CountryId", key.ToString());

    await _repository.Countries.DeleteAsync(x => x.CountryId == modelDto.CountryId, trackChanges: true);
    await _repository.SaveAsync();
    _logger.LogWarn($"Country with Id: {key} is about to be deleted from the database.");
    return OperationMessage.Success;
  }

  public async Task<string> SaveOrUpdate(int key, CrmCountryDto modelDto)
  {
    string res = string.Empty;
    bool isCountryExist = await _repository.Countries.ExistsAsync(x => x.CountryName.Trim().ToLower() == modelDto.CountryName.Trim().ToLower());

    #region New Recorod
    if (modelDto.CountryId == 0 && key == modelDto.CountryId)
    {
      if (isCountryExist) throw new DuplicateRecordException("Country", "CountryName");

      if (!isCountryExist)
      {
        CrmCountry country = MyMapper.JsonClone<CrmCountryDto, CrmCountry>(modelDto);
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
          CrmCountry country = MyMapper.JsonClone<CrmCountryDto, CrmCountry>(modelDto);
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

  public async Task<CrmCountryDto> CreateCountryAsync(CrmCountryDto entityForCreate)
  {
    CrmCountry country = MyMapper.JsonClone<CrmCountryDto, CrmCountry>(entityForCreate);
    _repository.Countries.CreateCountry(country);
    await _repository.SaveAsync();
    return entityForCreate;
  }

  public async Task<IEnumerable<CrmCountryDto>> GetCountriesAsync(bool trackChanges)
  {
    IEnumerable<CrmCountry> countries = await _repository.Countries.GetCountriesAsync(trackChanges);
    if (countries.Count() == 0) throw new GenericListNotFoundException("CrmCountry");

    List<CrmCountryDto> countryDtos = MyMapper.JsonCloneIEnumerableToList<CrmCountry, CrmCountryDto>(countries);
    return countryDtos;
  }

  public async Task<CrmCountryDto> GetCountryAsync(int countryId, bool trackChanges)
  {
    if (countryId <= 0) throw new ArgumentOutOfRangeException(nameof(countryId), "Country ID must be be zero or non-negative integer.");

    CrmCountry country = await _repository.Countries.GetCountryAsync(countryId, trackChanges);
    if (country == null) throw new GenericNotFoundException("Country", "CountryId", countryId.ToString());

    CrmCountryDto countryDto = MyMapper.JsonClone<CrmCountry, CrmCountryDto>(country);
    return countryDto;
  }

}
