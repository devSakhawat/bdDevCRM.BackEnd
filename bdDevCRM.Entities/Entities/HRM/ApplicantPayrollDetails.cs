using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantPayrollDetails
{
    public int ApplicantPayrollDetailsId { get; set; }

    public int? ApplicantPayrollId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? CurrencyId { get; set; }
}
