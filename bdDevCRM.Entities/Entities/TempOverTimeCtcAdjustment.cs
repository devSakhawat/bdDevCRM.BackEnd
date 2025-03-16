using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempOverTimeCtcAdjustment
{
    public int OverTimeAdjustmentId { get; set; }

    public string? EmployeeId { get; set; }

    public DateOnly? OverTimeFromDate { get; set; }

    public DateOnly? OverTimeToDate { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcAmount { get; set; }

    public string? Remarks { get; set; }
}
