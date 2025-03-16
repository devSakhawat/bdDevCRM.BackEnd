using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CtcSlabData
{
    public int CtcSlabId { get; set; }

    public int? CtcId { get; set; }

    public int? SlabTypeId { get; set; }

    public int? SlabOrder { get; set; }

    public int? RangeYear { get; set; }

    public decimal? RangePercentage { get; set; }

    public string? SlabTypeName { get; set; }
}
