using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingCost
{
    public int TrainingCostId { get; set; }

    public int TrainingScheduleId { get; set; }

    public string ExpenditureHead { get; set; } = null!;

    public decimal CostAmount { get; set; }

    public int? TrainingId { get; set; }
}
