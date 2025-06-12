using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FundYear
{
    public int TaxYearId { get; set; }

    public string? TaxYearName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Status { get; set; }

    public int? IsClosed { get; set; }
}
