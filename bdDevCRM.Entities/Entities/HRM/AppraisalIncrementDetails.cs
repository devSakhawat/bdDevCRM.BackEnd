using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalIncrementDetails
{
    public int AppraisalIncrementHistoryId { get; set; }

    public int? SystemGeneratedIncrementNo { get; set; }

    public decimal? SystemGeneratedGradeWiseAmount { get; set; }

    public string? SystemGeneratedRecommandation { get; set; }

    public int? SystemGeneratedPromotedGradeId { get; set; }

    public int? IncrementNo { get; set; }

    public decimal? GradeWiseAmountIncrease { get; set; }

    public string? Recommandation { get; set; }

    public decimal? SpecialAllowance { get; set; }

    public int? PromotedGradeId { get; set; }

    public int? YearId { get; set; }

    public int? AppraisalYearEndProcessId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public int? ApproverId { get; set; }
}
