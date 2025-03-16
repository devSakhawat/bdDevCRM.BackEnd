using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FinancerInfo
{
    public int FinancerId { get; set; }

    public string? FinancerCode { get; set; }

    public string FinancerName { get; set; } = null!;

    public int? FinancerType { get; set; }

    public int? IsFinancerActive { get; set; }
}
