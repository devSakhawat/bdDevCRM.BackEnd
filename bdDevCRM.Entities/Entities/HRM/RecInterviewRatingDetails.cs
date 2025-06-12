using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecInterviewRatingDetails
{
    public int InterviewRatingDetailsId { get; set; }

    public int? RecInterviewRatingMasterId { get; set; }

    public int? CompetencyId { get; set; }

    public string? Remarks { get; set; }

    public string? Rating { get; set; }
}
