using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FinancialYear
{
    public int FinancialYearId { get; set; }

    public string? FinancialYearName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Status { get; set; }
}
