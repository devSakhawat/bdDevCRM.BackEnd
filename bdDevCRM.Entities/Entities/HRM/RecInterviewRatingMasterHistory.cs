using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecInterviewRatingMasterHistory
{
    public int RecInterviewRatingMasterId { get; set; }

    public int? InterviewId { get; set; }

    public int? JobVacancyId { get; set; }

    public int? ApplicantId { get; set; }

    public int? InterViewerId { get; set; }

    public string? AssesmentComment { get; set; }

    public DateTime? InterviewRatingDateTime { get; set; }

    public int? IsRecommendFromFirstInterview { get; set; }
}
