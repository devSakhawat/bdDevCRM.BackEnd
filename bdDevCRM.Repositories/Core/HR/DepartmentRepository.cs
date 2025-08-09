using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.HR;


public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
{
  public DepartmentRepository(CRMContext context) : base(context) { }
}