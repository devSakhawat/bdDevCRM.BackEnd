using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public class CrmPaymentMethodRepository : RepositoryBase<CrmPaymentMethod>, ICrmPaymentMethodRepository
{
  public CrmPaymentMethodRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmPaymentMethod>> GetActivePaymentMethodsAsync(bool trackChanges) =>
      await ListByConditionAsync(x => x.IsActive == true, c => c.PaymentMethodName, trackChanges, descending: false);

  public async Task<IEnumerable<CrmPaymentMethod>> GetOnlinePaymentMethodsAsync(bool trackChanges) =>
      await ListByConditionAsync(x => x.IsActive == true && x.IsOnlinePayment == true, c => c.PaymentMethodName, trackChanges, descending: false);

  public async Task<CrmPaymentMethod?> GetPaymentMethodByIdAsync(int paymentMethodId, bool trackChanges) =>
      await FirstOrDefaultAsync(x => x.PaymentMethodId.Equals(paymentMethodId), trackChanges);

  public void CreatePaymentMethod(CrmPaymentMethod paymentMethod) => Create(paymentMethod);

  public void UpdatePaymentMethod(CrmPaymentMethod paymentMethod) => UpdateByState(paymentMethod);

  public void DeletePaymentMethod(CrmPaymentMethod paymentMethod) => Delete(paymentMethod);
}