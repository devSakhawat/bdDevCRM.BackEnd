using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryIncrementDetails
{
    public int SalaryIncrementDetailsId { get; set; }

    public int? SalaryIncrementId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? IncrementCtcId { get; set; }

    public decimal? IncrementCtcValue { get; set; }

    public int? CurrencyId { get; set; }

    public DateOnly? EffectiveDate { get; set; }
}
