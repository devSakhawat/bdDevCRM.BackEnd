using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalDetailsForFf
{
    public int DetailsId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CompetencyId { get; set; }

    public int? CompetencyAreaId { get; set; }

    public decimal? AchievingMarks { get; set; }

    public decimal? MarkWisePoint { get; set; }

    public int? MonthId { get; set; }
}
