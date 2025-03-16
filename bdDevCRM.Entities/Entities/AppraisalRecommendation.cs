using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalRecommendation
{
    public int AppraisalRecommendationId { get; set; }

    public int? PerformanceReviewId { get; set; }

    public int? HrRecordId { get; set; }

    public int? EvaluationMarks { get; set; }

    public int? Promotion { get; set; }

    public int? Enhancement { get; set; }

    public int? GeneralIncrement { get; set; }

    public int? Transfer { get; set; }

    public int? Termination { get; set; }

    public int? Status { get; set; }

    public int? YearConfigId { get; set; }

    public int? EnhancementAmount { get; set; }

    public int? Recommender { get; set; }

    public DateOnly? RecommendationDate { get; set; }

    public int? Approver { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public int? NoSalaryIncrement { get; set; }

    public int? ProposedGross { get; set; }

    public int? EmailSentStatus { get; set; }

    public DateTime? EmailSentDate { get; set; }
}
