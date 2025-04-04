using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class AccessControlRepository : RepositoryBase<Accesscontrol>, IAccessControlRepository
{
  public AccessControlRepository(CRMContext context) : base(context) { }




  //// Add a new Module
  //public void CreateModule(Module Module) => Create(Module);

  //// Update an existing Module
  //public void UpdateModule(Module Module) => UpdateByState(Module);

  //// Delete a Module by ID
  //public void DeleteModule(Module Module) => Delete(Module);


}
