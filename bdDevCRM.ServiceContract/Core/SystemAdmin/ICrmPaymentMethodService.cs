using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface ICrmPaymentMethodService
{
  Task<IEnumerable<CrmPaymentMethodDDL>> GetPaymentMethodsDDLAsync(bool trackChanges);
  Task<IEnumerable<CrmPaymentMethodDDL>> GetOnlinePaymentMethodsDDLAsync(bool trackChanges);
  Task<GridEntity<CrmPaymentMethodDto>> SummaryGrid(CRMGridOptions options);
  Task<string> CreateNewRecordAsync(CrmPaymentMethodDto modelDto);
  Task<string> UpdateNewRecordAsync(int key, CrmPaymentMethodDto modelDto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, CrmPaymentMethodDto modelDto);
  Task<string> SaveOrUpdate(int key, CrmPaymentMethodDto modelDto);
  Task<IEnumerable<CrmPaymentMethodDto>> GetPaymentMethodsAsync(bool trackChanges);
  Task<CrmPaymentMethodDto> GetPaymentMethodAsync(int paymentMethodId, bool trackChanges);
  Task<CrmPaymentMethodDto> CreatePaymentMethodAsync(CrmPaymentMethodDto entityForCreate);
}