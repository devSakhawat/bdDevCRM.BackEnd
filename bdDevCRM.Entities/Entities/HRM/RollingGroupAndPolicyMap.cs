using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RollingGroupAndPolicyMap
{
    public int RollGroupMapId { get; set; }

    public int? ShiftRollingPolicyId { get; set; }

    public int? RollingPolicyDetailsId { get; set; }

    public int? RollGroupId { get; set; }

    public virtual RollingGroup? RollGroup { get; set; }

    public virtual ShiftRollingPolicyDetails? RollingPolicyDetails { get; set; }

    public virtual ShiftRollingPolicy? ShiftRollingPolicy { get; set; }
}
