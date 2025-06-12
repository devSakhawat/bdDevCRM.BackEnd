using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AmcExtension
{
    public int AmcExtId { get; set; }

    public int? AmcInfoId { get; set; }

    public DateTime? ExtDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? NewProjectValue { get; set; }

    public decimal? AmcPerentage { get; set; }

    public int? AmcTypeId { get; set; }

    public decimal? AmcAmount { get; set; }
}
