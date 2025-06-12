using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalGradeWiseAmountMaxRange
{
    public int AppraisalGradeWiseMaxAmountId { get; set; }

    public int? GradeId { get; set; }

    public int? ForCtcId { get; set; }

    public decimal? MaxAmount { get; set; }

    public int? NextCtcId { get; set; }
}
