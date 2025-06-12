using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetAmortization
{
    public int AssetAmortizationId { get; set; }

    public int AssetIdentificationId { get; set; }

    public int? Status { get; set; }

    public decimal? SaleValue { get; set; }

    public DateTime? AssetAmortizationDate { get; set; }

    public int? AssetDisolved { get; set; }
}
