using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhUsageType
{
    public int UsageTypeId { get; set; }

    public string? UsageTypeName { get; set; }

    public int? IsCarUsageType { get; set; }

    public int? IsActive { get; set; }
}
