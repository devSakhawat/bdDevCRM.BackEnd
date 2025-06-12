using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GeneralLedger
{
    public int LedgerId { get; set; }

    public int VoucherId { get; set; }

    public int? SubjectId { get; set; }

    public int? AccountHeadId { get; set; }

    public string? AccountHeadCode { get; set; }

    public string? SubLedgerCode { get; set; }

    public string? SubLedgerName { get; set; }

    public decimal? DrAmount { get; set; }

    public decimal? CrAmount { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? PostingDate { get; set; }

    public string? LedgerReference { get; set; }

    public decimal? MotherBalance { get; set; }

    public string? SubLedgerAccountCode { get; set; }
}
