using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Core.SystemAdmin;

public interface ISystemSettingsService
{
  Task<SystemSettings> GetSystemSettingsDataByCompanyId(int companyId);

  Task<AssemblyInfoDto> GetAssemblyInfoResult();

  //string SaveSystemSettings(SystemSettings objSystemSettings);

  //System.Data.DataTable GetSystemSettingsData();



  //bool CheckPaddingExistOrNot(int hrRecordId);

  //bool CheckPaddingExistOrNotByCompanyId(int companyId);

  //bool CheckPadding();

  //SystemSettings GetSystemSettingsDataByUserId(int userId);

  //SystemSettings GetSystemSettingsDataByHrRecordId(int hrRecordId);

  //SystemSettings GetSystemSettingsDataByEmployeeId(string employeeId);

  //SystemSettings GetSystemSettingsDataByCostCentreSalaryMappingCompanyId(int costCentreId);
}
