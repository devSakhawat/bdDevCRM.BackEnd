using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveEncashmentPolicy
{
    public int EncashmentPolicyId { get; set; }

    public int? EncashmentHead { get; set; }

    public int? CalculatePercent { get; set; }
}
