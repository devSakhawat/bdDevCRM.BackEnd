using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformanceReviewDetail
{
    public int PerformanceReviewDetailId { get; set; }

    public int PerformanceReviewId { get; set; }

    public int PerformanceCriteriaId { get; set; }

    public int PerformanceCategoryId { get; set; }

    public int MarksAchieved { get; set; }

    public int? MarksAchievedFromRecommender { get; set; }

    public int? MarksAchievedFromApprover { get; set; }

    public int? MarksAchievedOnHrContent { get; set; }

    public int? ReviewMarksFromLm { get; set; }

    public int? ReviewMarksFromDh { get; set; }

    public int? SelfRatingOnHrContent { get; set; }
}
