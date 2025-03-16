using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FiscalYear
{
    public int FiscalYearId { get; set; }

    public string FiscalYearName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? Status { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
