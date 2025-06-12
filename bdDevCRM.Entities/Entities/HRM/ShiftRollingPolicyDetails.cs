using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ShiftRollingPolicyDetails
{
    public int RollingPolicyDetailsId { get; set; }

    public int ShiftRollingPolicyId { get; set; }

    public int? SortOrder { get; set; }

    public int? ShiftId { get; set; }

    public virtual ICollection<RollingGroupAndPolicyMap> RollingGroupAndPolicyMap { get; set; } = new List<RollingGroupAndPolicyMap>();

    public virtual ShiftRollingPolicy ShiftRollingPolicy { get; set; } = null!;
}
