using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterRunMonth
{
    public int? RollGroupId { get; set; }

    public int? ShiftId { get; set; }

    public int? RollPolicyId { get; set; }

    public DateTime? RosterRunMonth1 { get; set; }

    public int RosterRunMonthId { get; set; }
}
