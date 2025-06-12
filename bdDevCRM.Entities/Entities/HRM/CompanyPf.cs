using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompanyPf
{
    public int ProvidentFundId { get; set; }

    public int CompanyId { get; set; }

    public string? TransectionId { get; set; }

    public DateOnly TransectionDate { get; set; }

    public int AccountHeadId { get; set; }

    public string? Particular { get; set; }

    public decimal AmountDr { get; set; }

    public decimal AmountCr { get; set; }

    public decimal Balance { get; set; }
}
