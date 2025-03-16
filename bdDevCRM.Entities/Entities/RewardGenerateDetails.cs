using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RewardGenerateDetails
{
    public int RewardGenDetailsId { get; set; }

    public int? RewardGenerateId { get; set; }

    public int? HrRecordId { get; set; }

    public int? IsRewardPrint { get; set; }

    public DateOnly? RewardPrintDate { get; set; }

    public int? RewardDistributionId { get; set; }

    public int? ServiceLength { get; set; }

    public string? ActualServiceLength { get; set; }

    public int? RewardEligibilityId { get; set; }
}
