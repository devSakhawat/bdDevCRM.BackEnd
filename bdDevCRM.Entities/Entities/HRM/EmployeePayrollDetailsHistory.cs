using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeePayrollDetailsHistory
{
    public int EmpPayrollDetailsId { get; set; }

    public int? EmpPayrollId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? CurrencyId { get; set; }

    public DateOnly? EffectiveDate { get; set; }
}
