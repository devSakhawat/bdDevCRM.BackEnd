using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentDetails
{
    public int InvestmentDetailsId { get; set; }

    public int? InvestmentId { get; set; }

    public DateOnly? InterestMonth { get; set; }

    public int? Duration { get; set; }

    public decimal? MonthlyInterest { get; set; }

    public int? Status { get; set; }

    public string? VoucherNo { get; set; }
}
