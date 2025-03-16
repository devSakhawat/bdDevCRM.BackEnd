using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.HR;

namespace bdDevCRM.ServiceContract.HR;

public interface IEmployeeService
{
  Task<EmploymentDto> GetEmploymentByHrRecordId(int hrRecordId);
  Task<WfstateDto> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId);
  Task<EmployeeDto> GetEmployeeByHrRecordId(int hrRecordId);
}
