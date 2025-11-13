using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.HR;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;

namespace bdDevCRM.Service.Core.HR;


internal sealed class EmployeeService : IEmployeeService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<EmploymentDto> GetEmploymentByHrRecordId(int hrRecordId)
  {
    EmploymentRepositoryDto employmentRepositoryDto = await _repository.Employees.GetEmploymentByHrRecordId(hrRecordId);
    //Check if the result is null
    if (employmentRepositoryDto == null) throw new GenericNotFoundException("EmploymentDto", "HrRecordId", hrRecordId.ToString());

    EmploymentDto employmentDto = MyMapper.JsonClone<EmploymentRepositoryDto, EmploymentDto>(employmentRepositoryDto);
    return employmentDto;
  }

  public async Task<WfStateDto> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId)
  {
    WfState wfstate = await _repository.Employees.GetEmployeeCurrentStatusByHrRecordId(hrRecordId);
    //Check if the result is null
    if (wfstate == null) throw new GenericNotFoundException("WfState", "Employee.StateId", hrRecordId.ToString());

    WfStateDto wfstateDto = MyMapper.JsonClone<WfState, WfStateDto>(wfstate);
    return wfstateDto;
  }

  public async Task<EmployeeDto> GetEmployeeByHrRecordId(int hrRecordId)
  {
    EmployeeRepositoryDto employee = await _repository.Employees.GetEmployeeByHrRecordId(hrRecordId);
    //Check if the result is null
    if (employee == null) throw new GenericNotFoundException("Employee", "Employee.HrrecordId", hrRecordId.ToString());

    EmployeeDto employeeDto = MyMapper.JsonClone<EmployeeRepositoryDto, EmployeeDto>(employee);
    return employeeDto;
  }

  // get employee types with id, name and code
  public async Task<IEnumerable<EmployeeTypeDto>> EmployeeTypes(int param)
  {
    // Initialize the employeeTypes collection properly
    IEnumerable<Employeetype> employeeTypes = Enumerable.Empty<Employeetype>();

    if (param == 0)
    {
      employeeTypes = await _repository.EmployeeTypes.ListByWhereWithSelectAsync(
        selector: x => new Employeetype
        {
          Employeetypeid = x.Employeetypeid,
          EmployeeTypeCode = x.EmployeeTypeCode,
          Employeetypename = x.Employeetypename
        }
        , x => x.IsActive == 0 && (x.IsNotAccess == null || x.IsNotAccess == false)
        , orderBy: x => x.Employeetypename
        , trackChanges: false
      );
    }
    else
    {
      employeeTypes = await _repository.EmployeeTypes.ListByWhereWithSelectAsync(
        selector: x => new Employeetype
        {
          Employeetypeid = x.Employeetypeid,
          EmployeeTypeCode = x.EmployeeTypeCode,
          Employeetypename = x.Employeetypename
        }
        , x => x.IsActive == 1, orderBy: x => x.Employeetypename, trackChanges: false);
    }

    // Check if the result is null or empty
    if (employeeTypes == null || !employeeTypes.Any())
      throw new GenericNotFoundException("EmployeeType", "EmployeeTypeId", "0");

    IEnumerable<EmployeeTypeDto> result = MyMapper.JsonCloneIEnumerableToList<Employeetype, EmployeeTypeDto>(employeeTypes);
    return result;
  }


  // get employees with id, name and code
  public async Task<IEnumerable<EmployeesByCompanyBranchDepartmentDto>> GetEmployeeByCompanyIdAndBranchIdAndDepartmentId(int companyId, int branchId, int departmentId)
  {
    string condition = "";
    if (companyId == 0)
    {
      condition = "";
    }
    else
    {
      condition = " where CompanyId = " + companyId;
    }

    if (departmentId == 0)
    {
      condition = condition;
    }
    else
    {
      if (condition == "")
      {
        condition = "where DEPARTMENTID=" + departmentId;
      }
      else
      {
        condition += " and DEPARTMENTID = " + departmentId;
      }
    }

    if (branchId != 0)
    {
      if (condition != "")
      {
        condition += " and BranchId=" + branchId;
      }
      else
      {
        condition = " where BranchId=" + branchId;
      }
    }

    IEnumerable<EmployeesByCompanyBranchDepartmentRepositoroyDto> employeeTypes = await _repository.Employees.GetEmployeeByCompanyIdAndBranchIdAndDepartmentId(condition);

    IEnumerable<EmployeesByCompanyBranchDepartmentDto> result = MyMapper.JsonCloneIEnumerableToList<EmployeesByCompanyBranchDepartmentRepositoroyDto, EmployeesByCompanyBranchDepartmentDto>(employeeTypes);
    
    return result;
  }


}
