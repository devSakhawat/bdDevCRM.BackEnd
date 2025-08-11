using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class CrmPaymentMethodService : ICrmPaymentMethodService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _config;

  public CrmPaymentMethodService(IRepositoryManager repository, ILoggerManager logger, IConfiguration config)
  {
    _repository = repository;
    _logger = logger;
    _config = config;
  }

  public async Task<IEnumerable<CrmPaymentMethodDDL>> GetPaymentMethodsDDLAsync(bool trackChanges)
  {
    var paymentMethods = await _repository.CrmPaymentMethods.GetActivePaymentMethodsAsync(trackChanges);
    var paymentMethodsDto = paymentMethods.Select(x => new CrmPaymentMethodDDL
    {
      PaymentMethodId = x.PaymentMethodId,
      PaymentMethodName = x.PaymentMethodName,
      PaymentMethodCode = x.PaymentMethodCode,
      IsOnlinePayment = x.IsOnlinePayment,
      ProcessingFee = x.ProcessingFee,
      ProcessingFeeType = x.ProcessingFeeType
    });
    return paymentMethodsDto;
  }

  public async Task<IEnumerable<CrmPaymentMethodDDL>> GetOnlinePaymentMethodsDDLAsync(bool trackChanges)
  {
    var paymentMethods = await _repository.CrmPaymentMethods.GetOnlinePaymentMethodsAsync(trackChanges);
    var paymentMethodsDto = paymentMethods.Select(x => new CrmPaymentMethodDDL
    {
      PaymentMethodId = x.PaymentMethodId,
      PaymentMethodName = x.PaymentMethodName,
      PaymentMethodCode = x.PaymentMethodCode,
      IsOnlinePayment = x.IsOnlinePayment,
      ProcessingFee = x.ProcessingFee,
      ProcessingFeeType = x.ProcessingFeeType
    });
    return paymentMethodsDto;
  }

  public async Task<GridEntity<CrmPaymentMethodDto>> SummaryGrid(CRMGridOptions options)
  {
    string condition = string.Empty;
    string sql = @"
      SELECT PaymentMethodId, PaymentMethodName, PaymentMethodCode, Description, 
             ProcessingFee, ProcessingFeeType, IsOnlinePayment, IsActive, 
             CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
      FROM CrmPaymentMethod";
    string orderBy = " PaymentMethodName asc ";
    return await _repository.CrmPaymentMethods.GridData<CrmPaymentMethodDto>(sql, options, orderBy, condition);
  }

  public async Task<string> CreateNewRecordAsync(CrmPaymentMethodDto modelDto)
  {
    var entityForDb = MyMapper.JsonClone<CrmPaymentMethodDto, CrmPaymentMethod>(modelDto);
    entityForDb.CreatedDate = DateTime.UtcNow;
    entityForDb.IsActive = true;

    _repository.CrmPaymentMethods.CreatePaymentMethod(entityForDb);
    await _repository.SaveAsync();
    return "Payment Method created successfully.";
  }

  public async Task<string> UpdateNewRecordAsync(int key, CrmPaymentMethodDto modelDto, bool trackChanges)
  {
    var entityForDb = await _repository.CrmPaymentMethods.GetPaymentMethodByIdAsync(key, trackChanges);
    if (entityForDb is null)
      throw new GenericNotFoundException($"Payment Method with id: {key} doesn't exist in the database."
        ,nameof(CrmIntakeMonthDto)
        ,key.ToString()
        );

    entityForDb.PaymentMethodName = modelDto.PaymentMethodName;
    entityForDb.PaymentMethodCode = modelDto.PaymentMethodCode;
    entityForDb.Description = modelDto.Description;
    entityForDb.ProcessingFee = modelDto.ProcessingFee;
    entityForDb.ProcessingFeeType = modelDto.ProcessingFeeType;
    entityForDb.IsOnlinePayment = modelDto.IsOnlinePayment;
    entityForDb.IsActive = modelDto.IsActive;
    entityForDb.UpdatedDate = DateTime.UtcNow;
    entityForDb.UpdatedBy = modelDto.UpdatedBy;

    await _repository.SaveAsync();
    return "Payment Method updated successfully.";
  }

  public async Task<string> DeleteRecordAsync(int key, CrmPaymentMethodDto modelDto)
  {
    var entityForDb = await _repository.CrmPaymentMethods.GetPaymentMethodByIdAsync(key, trackChanges: false);
    if (entityForDb is null)
      throw new GenericNotFoundException($"Payment Method with id: {key} doesn't exist in the database."
        , nameof(CrmIntakeMonthDto)
        , key.ToString());

    _repository.CrmPaymentMethods.DeletePaymentMethod(entityForDb);
    await _repository.SaveAsync();
    return "Payment Method deleted successfully.";
  }

  public async Task<string> SaveOrUpdate(int key, CrmPaymentMethodDto modelDto)
  {
    return key == 0 
      ? await CreateNewRecordAsync(modelDto) 
      : await UpdateNewRecordAsync(key, modelDto, trackChanges: false);
  }

  public async Task<IEnumerable<CrmPaymentMethodDto>> GetPaymentMethodsAsync(bool trackChanges)
  {
    var paymentMethods = await _repository.CrmPaymentMethods.GetActivePaymentMethodsAsync(trackChanges);
    var paymentMethodsDto = MyMapper.JsonCloneIEnumerableToList<CrmPaymentMethod, CrmPaymentMethodDto>(paymentMethods);
    return paymentMethodsDto;
  }

  public async Task<CrmPaymentMethodDto> GetPaymentMethodAsync(int paymentMethodId, bool trackChanges)
  {
    var paymentMethod = await _repository.CrmPaymentMethods.GetPaymentMethodByIdAsync(paymentMethodId, trackChanges);
    if (paymentMethod is null)
      throw new GenericNotFoundException($"Payment Method with id: {paymentMethodId} doesn't exist in the database."
        , nameof(CrmIntakeMonthDto)
        , paymentMethodId.ToString()
        );
    
    var paymentMethodDto = MyMapper.JsonClone<CrmPaymentMethod, CrmPaymentMethodDto>(paymentMethod);
    return paymentMethodDto;
  }

  public async Task<CrmPaymentMethodDto> CreatePaymentMethodAsync(CrmPaymentMethodDto entityForCreate)
  {
    var paymentMethodEntity = MyMapper.JsonClone<CrmPaymentMethodDto, CrmPaymentMethod>(entityForCreate);
    paymentMethodEntity.CreatedDate = DateTime.UtcNow;
    paymentMethodEntity.IsActive = true;

    _repository.CrmPaymentMethods.CreatePaymentMethod(paymentMethodEntity);
    await _repository.SaveAsync();

    var paymentMethodToReturn = MyMapper.JsonClone<CrmPaymentMethod, CrmPaymentMethodDto>(paymentMethodEntity);
    return paymentMethodToReturn;
  }


}