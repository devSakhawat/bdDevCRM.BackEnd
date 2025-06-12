using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InterviewDetails
{
    public int InterviewDetailsId { get; set; }

    public int? InterviewId { get; set; }

    public int? InterviewerId { get; set; }

    public int? InterviewerType { get; set; }

    public int? SeqNo { get; set; }

    public int? IsSalaryAnalysis { get; set; }
}
