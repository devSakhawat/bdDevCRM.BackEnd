using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ShifRollingPolicyMap
{
    public int ShiftRollingMapId { get; set; }

    public int ShiftRollingPolicyId { get; set; }

    public int? CompanyId { get; set; }

    public int? ReferenceId { get; set; }

    public int? ReferenceType { get; set; }

    public virtual ShiftRollingPolicy ShiftRollingPolicy { get; set; } = null!;
}
