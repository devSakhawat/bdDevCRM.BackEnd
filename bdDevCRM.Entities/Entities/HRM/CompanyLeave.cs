using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompanyLeave
{
    public int CompanyLeaveId { get; set; }

    public int LeavePolicyId { get; set; }

    public int CompanyId { get; set; }
}
