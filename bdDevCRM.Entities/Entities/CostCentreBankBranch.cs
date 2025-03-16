using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CostCentreBankBranch
{
    public int CostCentreBankBranchId { get; set; }

    public int CostCentreId { get; set; }

    public int? BankId { get; set; }

    public int? BranchId { get; set; }

    public string? AccountName { get; set; }

    public string? AccountNo { get; set; }

    public bool? IsActive { get; set; }
}
