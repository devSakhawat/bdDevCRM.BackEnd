using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ShiftRollingPolicy
{
    public int ShiftRollingPolicyId { get; set; }

    public string? RollingPolicyName { get; set; }

    public DateOnly? StartDate { get; set; }

    public string? RollingDuration { get; set; }

    /// <summary>
    /// 1=Weekly,2=Fortnightly,3=Monthly
    /// </summary>
    public int? DayOffType { get; set; }

    public int? DayOffDuration { get; set; }

    public bool? ShiftReplaceByDo { get; set; }

    public virtual ICollection<RollingGroupAndPolicyMap> RollingGroupAndPolicyMap { get; set; } = new List<RollingGroupAndPolicyMap>();

    public virtual ICollection<ShifRollingPolicyMap> ShifRollingPolicyMap { get; set; } = new List<ShifRollingPolicyMap>();

    public virtual ICollection<ShiftRollingPolicyDetails> ShiftRollingPolicyDetails { get; set; } = new List<ShiftRollingPolicyDetails>();
}
