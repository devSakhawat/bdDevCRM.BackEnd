using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EnhancementGuideLine
{
    public int EnhancementGuideLineId { get; set; }

    public int? RewardPenaltyGuideLineId { get; set; }

    public int? EnhancementUpperLimit { get; set; }

    public int? EnhancementLowerLimit { get; set; }

    public int? EntryLevelAmount { get; set; }

    public int? MidLevelAmount { get; set; }

    public int? HigherLevelAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? PaScoreConfigId { get; set; }
}
