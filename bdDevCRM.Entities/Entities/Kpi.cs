using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Kpi
{
    public int KpiId { get; set; }

    public string KpiName { get; set; } = null!;

    public int TotalMarks { get; set; }

    public int Status { get; set; }

    public int? SortOrder { get; set; }
}
