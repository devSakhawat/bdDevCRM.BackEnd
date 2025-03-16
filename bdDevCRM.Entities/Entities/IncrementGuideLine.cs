using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IncrementGuideLine
{
    public int IncrementGuideLineId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? IncrementPercentage { get; set; }

    public int? RequiredYear { get; set; }

    public int? RewardPenaltyGuideLineId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
