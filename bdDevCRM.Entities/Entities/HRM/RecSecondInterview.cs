using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecSecondInterview
{
    public int? RecSecondInterviewRatingId { get; set; }

    public int? InterviewId { get; set; }

    public int? JobVacancyId { get; set; }

    public int? ApplicantId { get; set; }

    public int? InterViewerId { get; set; }

    public string? SecondInterviewComments { get; set; }

    public DateTime? SecondInterviewDate { get; set; }
}
