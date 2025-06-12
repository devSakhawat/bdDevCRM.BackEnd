using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GlsalaryItem
{
    public int GlsalaryItemId { get; set; }

    public int Adid { get; set; }

    public string Glcode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Side { get; set; } = null!;

    public short AdditionalInfo { get; set; }

    public int TranId { get; set; }

    public int? CompanyId { get; set; }

    public int? CostCenterId { get; set; }

    public int? LocationId { get; set; }

    public string? GlcostCentreCode { get; set; }
}
