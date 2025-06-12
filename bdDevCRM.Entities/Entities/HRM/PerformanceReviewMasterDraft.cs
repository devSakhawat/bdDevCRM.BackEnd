using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformanceReviewMasterDraft
{
    public int PerformanceReviewId { get; set; }

    public int TotalRatingByEmployee { get; set; }

    public int? TotalRatingByRecommender { get; set; }

    public int? TotalRatingByApprover { get; set; }

    public int? TotalEvaluationMarkByLm { get; set; }

    public int? TotalEvaluationMarkByDh { get; set; }

    public int? EvaluationSummary { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public DateOnly? ReviewDateByRecommender { get; set; }

    public DateOnly? ReviewDateByApprover { get; set; }

    public DateOnly? TotalEvaluationMarkByLmdate { get; set; }

    public DateOnly? TotalEvaluationMarkByDhdate { get; set; }

    public DateOnly? HrHeadTargetApproveDate { get; set; }

    public DateOnly? HrHeadReviewApproveDate { get; set; }

    public int HrRecordId { get; set; }

    public int? RecommenderId { get; set; }

    public int? ApproverId { get; set; }

    public int? HrHeadRecordId { get; set; }

    public int? IsTargetApprovedByHrHead { get; set; }

    public int? HrGivenMarks { get; set; }

    public int? HrGivenPenalty { get; set; }

    public int? IsReviewApprovedByHrHead { get; set; }

    public int? YearConfigId { get; set; }

    public string? SelfComments { get; set; }

    public string? RecommenderComments { get; set; }

    public string? ApproverComments { get; set; }

    public string? HrHeadComments { get; set; }

    public int? RejectBy { get; set; }

    public string? RejectStatusRemarks { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? DocumentFilePath { get; set; }
}
