using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveDeductionPolicy
{
    public int LeaveDedPolicyId { get; set; }

    public string? PolicyName { get; set; }

    public int? WithoutPayLeaveMap { get; set; }

    public int? IsActive { get; set; }

    public int? WithoutPayLeaveMapBasic { get; set; }

    public int? IsBasic { get; set; }
}
