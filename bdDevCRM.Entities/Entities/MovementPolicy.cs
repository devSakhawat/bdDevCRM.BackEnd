using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MovementPolicy
{
    public int MovementPolicyId { get; set; }

    public string PolicyName { get; set; } = null!;

    public int DefaultPolicy { get; set; }

    public int IsActive { get; set; }

    public int? DefaultPolicyLogin { get; set; }
}
