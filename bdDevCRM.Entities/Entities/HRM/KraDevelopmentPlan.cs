using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraDevelopmentPlan
{
    public int DevelopmentPlanId { get; set; }

    public int? KraCompetenciesId { get; set; }

    public string? PlanInitiative { get; set; }

    public DateTime? PlanWhen { get; set; }

    public int? SerialNo { get; set; }

    public virtual KraCompetencies? KraCompetencies { get; set; }
}
