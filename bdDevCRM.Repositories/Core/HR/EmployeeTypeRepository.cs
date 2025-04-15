using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.HR;


public class EmployeeTypeRepository : RepositoryBase<Employeetype>, IEmployeeTypeRepository
{
  public EmployeeTypeRepository(CRMContext context) : base(context) { }
}