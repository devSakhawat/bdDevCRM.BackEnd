using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Pfrecovery
{
    public int PfRecoveryId { get; set; }

    public DateOnly PostingMonth { get; set; }

    public int CompanyId { get; set; }

    public string? VoucherNo { get; set; }

    public DateTime? MakeDate { get; set; }

    public int? MakeBy { get; set; }

    public int RecoveryType { get; set; }
}
