using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveCostCentreMap
{
    public int LeaveCcmapId { get; set; }

    public int? LeavePolicyId { get; set; }

    public int? CompanyId { get; set; }

    public int? CostCentreId { get; set; }
}
