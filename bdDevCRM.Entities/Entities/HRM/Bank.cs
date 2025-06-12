using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Bank
{
    public int BankId { get; set; }

    public string BankCode { get; set; } = null!;

    public string? BankName { get; set; }

    public int? IsActive { get; set; }
}
