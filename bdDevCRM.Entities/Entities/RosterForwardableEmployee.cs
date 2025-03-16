using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterForwardableEmployee
{
    public int RosterFwdEmpId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }
}
