using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalRattingRange
{
    public int AppraisalRatingRangeId { get; set; }

    public int? CompetencyAreaId { get; set; }

    public decimal? FromRate { get; set; }

    public decimal? ToRate { get; set; }

    public decimal? RatingPoint { get; set; }
}
