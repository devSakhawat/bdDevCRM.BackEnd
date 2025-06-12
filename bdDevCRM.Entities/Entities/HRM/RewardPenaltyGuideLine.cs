using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RewardPenaltyGuideLine
{
    public int RewardPenaltyGuideLineId { get; set; }

    public int? EligibilityId { get; set; }

    public int? FromAmount { get; set; }

    public int? ToAmount { get; set; }

    public int? YearConfigId { get; set; }

    public int? NumberOfBelowSatisfactory { get; set; }

    public int? IncrementPercentage { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
