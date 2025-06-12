using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BgCost
{
    public string? EmployeeId { get; set; }

    public string? Costcenter { get; set; }

    public int? Ratio { get; set; }
}
