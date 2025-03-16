using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TaxInvestmentLetterReferenceNo
{
    public DateOnly? IncomeYearFrom { get; set; }

    public DateOnly? IncomeYearTo { get; set; }

    public string? ReferenceNo { get; set; }

    public int? UniqueNumber { get; set; }
}
