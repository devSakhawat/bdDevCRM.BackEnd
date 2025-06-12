using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempRecommendation
{
    public int? PerformanceReviewId { get; set; }

    public int? Promotion { get; set; }

    public int? Enhancement { get; set; }

    public int? GeneralIncrement { get; set; }

    public int? Transfer { get; set; }

    public int? Termination { get; set; }

    public int? YearConfigId { get; set; }
}
