using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrFinancerAndPlanningMap
{
    public int FinancerAndPlanningMapId { get; set; }

    public int? TrainingPlanningId { get; set; }

    public int? FinancerId { get; set; }

    public decimal? Percentage { get; set; }
}
