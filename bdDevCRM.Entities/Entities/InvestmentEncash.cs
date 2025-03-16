using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentEncash
{
    public int InvestmentEncashId { get; set; }

    public decimal? PrincipleAmount { get; set; }

    public decimal? AcruedProfitAmount { get; set; }

    public decimal? AccProfitAmount { get; set; }

    public decimal? VarienceAmount { get; set; }

    public DateOnly? ClosedDate { get; set; }

    public bool? IsProfitAdjusted { get; set; }

    public int? IsLedgerPost { get; set; }

    public DateTime? PostingDate { get; set; }

    public int? PostedBy { get; set; }

    public string? VoucherNo { get; set; }

    public int? InvestmentId { get; set; }

    public bool? Status { get; set; }
}
