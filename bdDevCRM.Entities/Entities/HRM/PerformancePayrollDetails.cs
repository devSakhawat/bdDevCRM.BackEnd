using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformancePayrollDetails
{
    public int PerformancePayrollDetailId { get; set; }

    public int? PerformancePayrollId { get; set; }

    public int? CtcId { get; set; }

    public int? CtcValue { get; set; }

    public DateOnly? CtcEffectiveDate { get; set; }
}
