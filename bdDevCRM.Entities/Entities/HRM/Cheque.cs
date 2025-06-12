using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Cheque
{
    public int ChequeId { get; set; }

    public int BankId { get; set; }

    public string ChequeImagePath { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? RptName { get; set; }
}
