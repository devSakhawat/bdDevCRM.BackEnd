using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.RepositoriesContracts.DMS;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.DMS;


public class DmsdocumentTagMapRepository : RepositoryBase<DmsdocumentTagMap>, IDmsdocumentTagMapRepository
{
  public DmsdocumentTagMapRepository(CRMContext context) : base(context) { }


}
