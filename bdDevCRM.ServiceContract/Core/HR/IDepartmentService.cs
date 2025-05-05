using bdDevCRM.Entities.Entities;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Core.HR;

public interface IDepartmentService
{
  Task<IEnumerable<DepartmentDto>> DepartmentesByCompanyIdForCombo(int companyId, UsersDto user);


}
