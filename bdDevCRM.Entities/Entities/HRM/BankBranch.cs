using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BankBranch
{
    public int BranchId { get; set; }

    public string? BranchCode { get; set; }

    public int? BankId { get; set; }

    public string? BranchName { get; set; }

    public string? Address { get; set; }

    public int? IsActive { get; set; }
}
