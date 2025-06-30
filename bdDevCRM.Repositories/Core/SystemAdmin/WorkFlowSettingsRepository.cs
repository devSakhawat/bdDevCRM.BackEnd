using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;


public class WorkFlowSettingsRepository : RepositoryBase<Wfstate>, IWorkFlowSettingsRepository
{
  public WorkFlowSettingsRepository(CRMContext context) : base(context) { }


}
