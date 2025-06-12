using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveReason
{
    public int LeaveReasonId { get; set; }

    public string? Reason { get; set; }

    public bool? IsActive { get; set; }
}
