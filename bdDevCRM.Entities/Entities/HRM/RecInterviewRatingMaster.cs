using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecInterviewRatingMaster
{
    public int RecInterviewRatingMasterId { get; set; }

    public int? InterviewId { get; set; }

    public int? JobVacancyId { get; set; }

    public int? ApplicantId { get; set; }

    public int? InterViewerId { get; set; }

    public string? AssesmentComment { get; set; }

    public DateTime? InterviewRatingDateTime { get; set; }

    public int? IsRecommendFromFirstInterview { get; set; }

    public int? SavedBy { get; set; }

    public DateTime? SavedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? RatingFrom { get; set; }

    public string? FileRef { get; set; }

    public int? IsAcknowledge { get; set; }

    public int? IsDeny { get; set; }

    public string? Remarks { get; set; }
}
