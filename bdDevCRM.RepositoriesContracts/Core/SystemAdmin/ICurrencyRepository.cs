using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRMM;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICurrencyRepository : IRepositoryBase<CurrencyInfo>
{
  
  //void UpdateCurrency(Currency currency);
  //void DeleteCurrency(Currency currency);
}
