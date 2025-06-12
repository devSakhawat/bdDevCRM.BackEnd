using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentSlabSettings
{
    public int InvestmentSlabId { get; set; }

    public int? SlabType { get; set; }

    public int? SlabOrder { get; set; }

    public decimal? RangeAmt { get; set; }

    public decimal? RangePercentage { get; set; }

    public int? SalaryYearId { get; set; }

    public int? AssesmentYearId { get; set; }

    public string? SlabTypeName { get; set; }

    public decimal? AllowableInvestment { get; set; }
}
