using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentRenewal
{
    public int InvestmentRenewalId { get; set; }

    public int InvestmentId { get; set; }

    public DateTime RenewDate { get; set; }

    public decimal? RenewInterest { get; set; }

    public DateOnly? RenewMaturityDate { get; set; }

    public int? RenewBy { get; set; }
}
