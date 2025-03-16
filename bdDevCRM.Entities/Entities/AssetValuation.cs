using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetValuation
{
    public int AssetValuationId { get; set; }

    public int AssetCategoryId { get; set; }

    public int? FinancialYearId { get; set; }

    public int? ValuationTypeId { get; set; }

    public int? ValuationMethodId { get; set; }

    public decimal? ValuationPercentage { get; set; }

    public int? CompanyId { get; set; }
}
