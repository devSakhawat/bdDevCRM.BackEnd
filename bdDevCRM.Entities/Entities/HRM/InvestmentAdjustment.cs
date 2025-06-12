using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentAdjustment
{
    public int InvestAdustmentId { get; set; }

    public int? FiscalYearId { get; set; }

    public DateTime? PostingDate { get; set; }

    public string? VoucherNo { get; set; }
}
