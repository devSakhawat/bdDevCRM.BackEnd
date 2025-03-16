using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollCycleMapping
{
    public int PayrollCycleMappingId { get; set; }

    public int PayrollCycleId { get; set; }

    public int CompanyId { get; set; }

    public int? BranchId { get; set; }
}
