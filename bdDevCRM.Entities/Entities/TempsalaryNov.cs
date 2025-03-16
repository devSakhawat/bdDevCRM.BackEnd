using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempsalaryNov
{
    public string? EmployeeId { get; set; }

    public decimal? OriginalAmount { get; set; }

    public decimal? GeneratedAmount { get; set; }

    public decimal? MismatchedAmount { get; set; }

    public DateTime? SalaryMonth { get; set; }
}
