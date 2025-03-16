using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobConfirmationMaster25032023
{
    public int JobConfirmationMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public string? JobSummary { get; set; }

    public int? TotalRatingPoint { get; set; }

    public int? EvaluateBy { get; set; }

    public DateTime? EvaluationDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? EvaluatorComment { get; set; }

    public int? Status { get; set; }

    public string? DocumentFilePath { get; set; }

    public int? IsletterGenerated { get; set; }

    public int? TransferPromotionId { get; set; }

    public int? JobRecommendationType { get; set; }

    public string? DocWithOutSalaryFilePath { get; set; }

    public int? IsLetterSent { get; set; }
}
