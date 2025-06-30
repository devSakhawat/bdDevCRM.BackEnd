using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.HR;


public class EmployeeTypeRepository : RepositoryBase<Employeetype>, IEmployeeTypeRepository
{
  public EmployeeTypeRepository(CRMContext context) : base(context) { }
}