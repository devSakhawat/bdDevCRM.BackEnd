using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SubsidaryAccountMapping
{
    public int SubsidaryAccountMappingId { get; set; }

    public int? SubLedgerAccountId { get; set; }

    public int? AccountHeadId { get; set; }

    public string? SubLedgerAccountCode { get; set; }

    public bool? IsActive { get; set; }
}
