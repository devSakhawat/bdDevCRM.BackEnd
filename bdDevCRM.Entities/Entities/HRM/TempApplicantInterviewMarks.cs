using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempApplicantInterviewMarks
{
    public int AppInterviewMarksUploadId { get; set; }

    public int? ApplicantId { get; set; }

    public string? Name { get; set; }

    public decimal? InterviewMarks { get; set; }

    public DateTime? InterviewDate { get; set; }

    public int? AppliedPost { get; set; }

    public int? InterviewType { get; set; }

    public int? UserId { get; set; }
}
