using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmailConfigLocationMapping
{
    public int EmailConfigLocationMapId { get; set; }

    public int? EmailNotificationConfigId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? ComapnyId { get; set; }
}
