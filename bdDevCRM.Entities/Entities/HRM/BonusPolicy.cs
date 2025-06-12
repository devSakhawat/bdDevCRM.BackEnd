using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BonusPolicy
{
    public int BonusPolicyId { get; set; }

    public string? BonusPolicyName { get; set; }

    public int? EmployeeTypeId { get; set; }

    public decimal? BonusPercentage { get; set; }

    public int? FromMonth { get; set; }

    public int? ToMonth { get; set; }

    public decimal? FixedValue { get; set; }
}
