using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SubKpi
{
    public int SubKpiId { get; set; }

    public string? SubKpiName { get; set; }

    public int? ParentKpiId { get; set; }

    public int? Status { get; set; }

    public int? SortOrder { get; set; }

    public int SubKpiTotalMarksOfParrent { get; set; }
}
