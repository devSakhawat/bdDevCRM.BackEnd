using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RewardEligibility
{
    public int RewardEligibilityId { get; set; }

    public string? EligibilityDescription { get; set; }

    public int? Duration { get; set; }
}
