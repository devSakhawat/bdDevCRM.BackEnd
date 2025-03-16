using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.ServiceContract.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.HR;
using bdDevCRM.Utilities.OthersLibrary;

namespace bdDevCRM.Service.HR;


internal sealed class EmployeeService : IEmployeeService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;

  public EmployeeService(IRepositoryManager repository, ILoggerManager logger)
  {
    _repository = repository;
    _logger = logger;
  }

  public async Task<EmploymentDto> GetEmploymentByHrRecordId(int hrRecordId)
  {
    EmploymentRepositoryDto employmentRepositoryDto = await _repository.Employees.GetEmploymentByHrRecordId(hrRecordId);
    //Check if the result is null
    if (employmentRepositoryDto == null) throw new GenericNotFoundException("EmploymentDto", "HrRecordId", hrRecordId.ToString());

    EmploymentDto employmentDto = MyMapper.JsonClone<EmploymentRepositoryDto, EmploymentDto>(employmentRepositoryDto);
    return employmentDto;
  }

  public async Task<WfstateDto> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId)
  {
    Wfstate wfstate = await _repository.Employees.GetEmployeeCurrentStatusByHrRecordId(hrRecordId);
    //Check if the result is null
    if (wfstate == null) throw new GenericNotFoundException("Wfstate", "Employee.StateId", hrRecordId.ToString());

    WfstateDto wfstateDto = MyMapper.JsonClone<Wfstate, WfstateDto>(wfstate);
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


}
