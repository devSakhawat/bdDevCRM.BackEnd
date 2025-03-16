using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Reward
{
    public int Rewardid { get; set; }

    public int? Hrrecordid { get; set; }

    public string? Natureofreward { get; set; }

    public DateTime? Rewarddate { get; set; }

    public string? Rewarddescription { get; set; }

    public string? Uploadfile { get; set; }
}
