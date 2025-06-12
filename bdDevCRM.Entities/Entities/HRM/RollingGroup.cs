using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RollingGroup
{
    public int RollGroupId { get; set; }

    public string? RollGroupName { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public virtual ICollection<RollingGroupAndPolicyMap> RollingGroupAndPolicyMap { get; set; } = new List<RollingGroupAndPolicyMap>();

    public virtual ICollection<RollingGroupDetails> RollingGroupDetails { get; set; } = new List<RollingGroupDetails>();
}
