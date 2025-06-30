using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.HR;

namespace bdDevCRM.RepositoriesContracts.Core.HR;

public interface IEmployeeRepository : IRepositoryBase<Employee>
{
  Task<EmploymentRepositoryDto> GetEmploymentByHrRecordId(int hrRecordId);
  Task<Wfstate> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId);
  Task<EmployeeRepositoryDto> GetEmployeeByHrRecordId(int hrRecordId);

  Task<IEnumerable<EmployeesByCompanyBranchDepartmentRepositoroyDto>> GetEmployeeByCompanyIdAndBranchIdAndDepartmentId(string condition);
}
