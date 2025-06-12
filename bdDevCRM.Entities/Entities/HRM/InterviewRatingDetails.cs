using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InterviewRatingDetails
{
    public int InterviewRatingDetailsId { get; set; }

    public int? InterviewDetailsId { get; set; }

    public int? CompetencyId { get; set; }

    public int? CompetencyAreaId { get; set; }

    public decimal? Rating { get; set; }

    public int? MarkingId { get; set; }
}
