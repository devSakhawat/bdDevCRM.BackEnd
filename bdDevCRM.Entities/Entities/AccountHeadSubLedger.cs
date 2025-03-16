using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AccountHeadSubLedger
{
    public int SubLedgerAccountId { get; set; }

    public string? LastCode { get; set; }

    public int? LedgerHeadId { get; set; }

    public string? SubLedgerCode { get; set; }

    public string? SubLedgerName { get; set; }

    public bool IsActive { get; set; }

    public int? ReferenceId { get; set; }
}
