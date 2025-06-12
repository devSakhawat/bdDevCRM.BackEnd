using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VoucharDetails
{
    public int VoucharDetailsId { get; set; }

    public int? VoucharId { get; set; }

    public int? AccountHeadId { get; set; }

    public string? AccountHeadCode { get; set; }

    public string? SubLedgerCode { get; set; }

    public string? SubLedgerName { get; set; }

    public decimal? DrAmount { get; set; }

    public decimal? CrAmount { get; set; }

    public string? LedgerReference { get; set; }

    public int VoucherId { get; set; }

    public string? SubLedgerAccountCode { get; set; }
}
