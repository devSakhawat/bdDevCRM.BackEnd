using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class CurrencyService : ICurrencyService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public CurrencyService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<CurrencyDDL>> GetCurrenciesDDLAsync()
  {
    IEnumerable<CurrencyDDL> currenciesDDL = await _repository.Currency.ListByWhereWithSelectAsync(selector: x => new CurrencyDDL { CurrencyId = x.CurrencyId, CurrencyName = x.CurrencyName }, orderBy: x => x.CurrencyName, trackChanges: false);
    //if (currenciesDDL.Count() == 0) throw new GenericListNotFoundException("Currency");
    return currenciesDDL;
  }

  public async Task<GridEntity<CurrencyDto>> CurrecySummary(CRMGridOptions options)
  {
    string query = "Select * from CurrencyInfo";
    string orderBy = " CurrencyName asc ";
    var gridEntity = await _repository.Currency.GridData<CurrencyDto>(query, options, orderBy, "");

    return gridEntity;
  }

  public async Task<string> SaveOrUpdate(int key ,CurrencyDto modelDto)
  {
    string res = string.Empty;
    bool isDefaultExist = await _repository.Currency.ExistsAsync(x => x.IsDefault == modelDto.IsDefault && modelDto.IsDefault == 1);

    if (isDefaultExist)
    {
      res = "Only one currency can be marked as default.";
      return res;
    }
    else
    {

      bool isCurrencyExist = await _repository.Currency.ExistsAsync(x => x.CurrencyName.Trim().ToLower() == modelDto.CurrencyName.Trim().ToLower() && x.CurrencyName.Trim().ToLower() == modelDto.CurrencyName.Trim().ToLower());

      #region New Currency
      if (modelDto.CurrencyId == 0 && key == modelDto.CurrencyId)
      {

        if (!isCurrencyExist)
        {
          CurrencyInfo currency = MyMapper.JsonClone<CurrencyDto, CurrencyInfo>(modelDto);
          int lastCreatedWfStateId = await _repository.Currency.CreateAndGetIdAsync(currency);
          await _repository.SaveAsync();
          return OperationMessage.Success;
        }
        else
        {
          res = "The currency name and code already exist!";
          return res;
        }
      }
      #endregion New Currency

      #region Update Currency
      else
      {
        if (key > 0 && key == modelDto.CurrencyId)
        {
          if (!isCurrencyExist)
          {
            CurrencyInfo currency = MyMapper.JsonClone<CurrencyDto, CurrencyInfo>(modelDto);
            _repository.Currency.Update(currency);
            await _repository.SaveAsync();

            return OperationMessage.Success;
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
      #endregion Update Currency
    }
  }

  public async Task<string> DeleteCurrency(int key, CurrencyDto modelDto)
  {
    if (modelDto == null) throw new NullModelBadRequestException(new CurrencyDto().GetType().Name.ToString());
    if (key != modelDto.CurrencyId) throw new IdMismatchBadRequestException(key.ToString(), new CurrencyDto().GetType().Name.ToString());

    CurrencyInfo currencyData = await _repository.Currency.FirstOrDefaultAsync(m => m.CurrencyId == key, trackChanges: false);
    if (currencyData == null) throw new GenericNotFoundException("CurrencyInfo", "CurrencyId", key.ToString());

    await _repository.Currency.DeleteAsync(x => x.CurrencyId == modelDto.CurrencyId, trackChanges: true);
    await _repository.SaveAsync();
    return OperationMessage.Success;
  }



}
