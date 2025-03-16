using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformanceFactorsRating
{
    public int FactorsRatingId { get; set; }

    public int? PerformanceFactorsId { get; set; }

    public int? Rating { get; set; }

    public int? PerformanceReviewLogId { get; set; }
}
