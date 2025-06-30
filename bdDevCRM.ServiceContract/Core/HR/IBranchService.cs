using bdDevCRM.Entities.Entities;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Core.HR;

public interface IBranchService
{
  // param because it should be branchId
  Task<IEnumerable<BranchDto>> BranchesByCompanyIdForCombo(int companyId, UsersDto user);


}
