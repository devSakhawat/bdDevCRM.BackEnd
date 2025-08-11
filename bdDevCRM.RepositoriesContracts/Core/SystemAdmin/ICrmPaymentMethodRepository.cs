using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface ICrmPaymentMethodRepository : IRepositoryBase<CrmPaymentMethod>
{
  Task<IEnumerable<CrmPaymentMethod>> GetActivePaymentMethodsAsync(bool trackChanges);
  Task<IEnumerable<CrmPaymentMethod>> GetOnlinePaymentMethodsAsync(bool trackChanges);
  Task<CrmPaymentMethod?> GetPaymentMethodByIdAsync(int paymentMethodId, bool trackChanges);
  void CreatePaymentMethod(CrmPaymentMethod paymentMethod);
  void UpdatePaymentMethod(CrmPaymentMethod paymentMethod);
  void DeletePaymentMethod(CrmPaymentMethod paymentMethod);
}