using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TaxSlabSettings
{
    public int SlabId { get; set; }

    public int? SlabType { get; set; }

    public int? SlabOrder { get; set; }

    public int? RangeAmountMale { get; set; }

    public int? RangeAmountFemale { get; set; }

    public int? RangeAmountOld { get; set; }

    public int? RangeAmountAutistic { get; set; }

    public int? RangePercentage { get; set; }

    public int? SalaryYearId { get; set; }

    public int? AssesmentYearId { get; set; }

    public string? SlabTypeName { get; set; }
}
