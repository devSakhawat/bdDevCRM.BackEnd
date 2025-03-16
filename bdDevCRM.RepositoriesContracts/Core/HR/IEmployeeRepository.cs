using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos;

namespace bdDevCRM.RepositoriesContracts.Core.HR;

public interface IEmployeeRepository
{
  Task<EmploymentRepositoryDto> GetEmploymentByHrRecordId(int hrRecordId);
  Task<Wfstate> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId);
  Task<EmployeeRepositoryDto> GetEmployeeByHrRecordId(int hrRecordId);
}
