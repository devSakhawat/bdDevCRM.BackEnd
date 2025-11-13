using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CurrencyRepository : RepositoryBase<CrmCurrencyInfo>, ICurrencyRepository
{
  public CurrencyRepository(CRMContext context) : base(context) { }


  //// Add a new Currency
  //public void CreateCurrency(Currency currency) => Create(currency);

  //// Update an existing Currency
  //public void UpdateCurrency(Currency currency) => UpdateByState(currency);

  //// Delete a Currency by ID
  //public void DeleteCurrency(Currency currency) => Delete(currency);
}
