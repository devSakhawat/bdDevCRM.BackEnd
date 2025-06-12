using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobConfirmationReview
{
    public int JobConfirmationReviewId { get; set; }

    public int? JobConfirmationMasterId { get; set; }

    public int? PerformanceAttributeId { get; set; }

    public int? PerformanceRating { get; set; }

    public string? PerformanceComment { get; set; }

    public int? EvaluateBy { get; set; }

    public int? ApproverRecommenderType { get; set; }

    public int? Sequence { get; set; }

    public DateTime? EvaluationDate { get; set; }
}
