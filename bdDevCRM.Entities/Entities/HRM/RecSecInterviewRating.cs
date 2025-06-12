using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecSecInterviewRating
{
    public int RceSecondIntRatingId { get; set; }

    public string? SecondAssesmentComment { get; set; }

    public DateTime? SecInterviewRatingDateTime { get; set; }

    public int? InterviewId { get; set; }

    public int? JobVacancyId { get; set; }

    public int? ApplicantId { get; set; }

    public int? InterViewerId { get; set; }

    public int? IsRecommendFromSecndInterview { get; set; }

    public int? SaveBy { get; set; }

    public DateTime? SaveDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? RatingFrom { get; set; }

    public string? FileRef { get; set; }
}
