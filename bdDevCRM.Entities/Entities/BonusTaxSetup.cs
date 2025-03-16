using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BonusTaxSetup
{
    public int BonusTaxSetupId { get; set; }

    public int TaxYearId { get; set; }

    public int? TaxOrder { get; set; }

    public DateTime? CutOfDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
