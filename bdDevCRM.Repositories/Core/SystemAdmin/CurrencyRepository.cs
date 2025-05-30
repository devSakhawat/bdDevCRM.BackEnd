using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRMM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class CurrencyRepository : RepositoryBase<CurrencyInfo>, ICurrencyRepository
{
  public CurrencyRepository(CRMContext context) : base(context) { }


  //// Add a new Currency
  //public void CreateCurrency(Currency currency) => Create(currency);

  //// Update an existing Currency
  //public void UpdateCurrency(Currency currency) => UpdateByState(currency);

  //// Delete a Currency by ID
  //public void DeleteCurrency(Currency currency) => Delete(currency);
}
