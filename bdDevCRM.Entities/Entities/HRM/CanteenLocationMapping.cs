using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenLocationMapping
{
    public int CanteenLocationMappingId { get; set; }

    public int CanteenId { get; set; }

    public int CompanyId { get; set; }

    public bool IsActive { get; set; }

    public int BranchId { get; set; }
}
