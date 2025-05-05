using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Core.HR;

public interface IEmployeeService
{
  Task<EmploymentDto> GetEmploymentByHrRecordId(int hrRecordId);
  Task<WfstateDto> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId);
  Task<EmployeeDto> GetEmployeeByHrRecordId(int hrRecordId);

  // 
  Task<IEnumerable<EmployeeTypeDto>> EmployeeTypes(int param);

  Task<IEnumerable<EmployeesByCompanyBranchDepartmentDto>> GetEmployeeByCompanyIdAndBranchIdAndDepartmentId(int companyId, int branchId, int departmentId);

}
