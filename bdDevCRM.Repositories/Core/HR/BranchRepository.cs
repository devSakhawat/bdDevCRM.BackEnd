using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.HR;


public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
{
  public BranchRepository(CRMContext context) : base(context) { }
}