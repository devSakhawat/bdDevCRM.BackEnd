using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class SystemSettingsService : ISystemSettingsService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;

  public SystemSettingsService(IRepositoryManager repository, ILoggerManager logger)
  {
    _repository = repository;
    _logger = logger;
  }

  public async Task<SystemSettings?> GetSystemSettingsDataByCompanyId(int companyId)
  {
    return await _repository.SystemSettings.GetSystemSettingsDataByCompanyId(companyId);
  }

  public async Task<AssemblyInfoDto> GetAssemblyInfoResult()
  {
    var assemblyInfo = await _repository.SystemSettings.GetAssemblyInfoResult();

    var assemblyInfoDto = new AssemblyInfoDto();
    if (assemblyInfo == null)
    {
      assemblyInfoDto.AssemblyInfoId = 1;
      assemblyInfoDto.AssemblyTitle = "bdDevsCRM";
      assemblyInfoDto.AssemblyCompany = "bdDevs Software Solution Ltd.";
      assemblyInfoDto.AssemblyProduct = "bdDevsCRM";
      assemblyInfoDto.AssemblyCopyright = "Copyright © bdDev 2025. All right reserved";
      assemblyInfoDto.AssemblyVersion = "Version: 1.0.0";
      assemblyInfoDto.AssemblyVersion = "Version: 1.0.0";
      assemblyInfoDto.PoweredBy = "bdDevs";
      assemblyInfoDto.PoweredByUrl = "http://www.bdDevs.com";
      assemblyInfoDto.ProductStyleSheet = "~/Assets/Css/Common_demo.css";
    }
    else
    {
      assemblyInfoDto = MyMapper.JsonClone<AssemblyInfo, AssemblyInfoDto>(assemblyInfo);
    }

    return assemblyInfoDto;
  }

  //public string SaveSystemSettings(SystemSettings objSystemSettings)
  //{
  //  return _systemSettingsDataService.SaveSystemSettings(objSystemSettings);
  //}

  //public DataTable GetSystemSettingsData()
  //{
  //  return _systemSettingsDataService.GetSystemSettingsData();
  //}
  //public bool CheckPaddingExistOrNot(int hrRecordId)
  //{
  //  var res = false;
  //  var objSystemSettings = _systemSettingsDataService.CheckPaddingExistOrNot(hrRecordId);
  //  if (objSystemSettings != null)
  //  {
  //    if (objSystemSettings.IsPaddingApplicable == 1)
  //    {
  //      res = true;
  //    }
  //  }
  //  return res;
  //}

  //public bool CheckPaddingExistOrNotByCompanyId(int companyId)
  //{
  //  var res = false;
  //  var objSystemSettings = _systemSettingsDataService.CheckPaddingExistOrNotByCompanyId(companyId);
  //  if (objSystemSettings != null)
  //  {
  //    if (objSystemSettings.IsPaddingApplicable == 1)
  //    {
  //      res = true;
  //    }
  //  }
  //  return res;
  //}

  //public bool CheckPadding()
  //{
  //  var res = false;
  //  var objSystemSettings = _systemSettingsDataService.CheckPadding();
  //  if (objSystemSettings != null)
  //  {
  //    if (objSystemSettings.IsPaddingApplicable == 1)
  //    {
  //      res = true;
  //    }
  //  }
  //  return res;
  //}

  //public SystemSettings GetSystemSettingsDataByUserId(int userId)
  //{
  //  return _systemSettingsDataService.GetSystemSettingsDataByUserId(userId);
  //}

  //public SystemSettings GetSystemSettingsDataByHrRecordId(int hrRecordId)
  //{
  //  return _systemSettingsDataService.GetSystemSettingsDataByHrRecordId(hrRecordId);
  //}

  //public SystemSettings GetSystemSettingsDataByEmployeeId(string employeeId)
  //{
  //  return _systemSettingsDataService.GetSystemSettingsDataByEmployeeId(employeeId);
  //}

  //public SystemSettings GetSystemSettingsDataByCostCentreSalaryMappingCompanyId(int costCentreId)
  //{
  //  return _systemSettingsDataService.GetSystemSettingsDataByCostCentreSalaryMappingCompanyId(costCentreId);
  //}
}

