using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraOnDevelopmentActivities
{
    public int OnDevActivityId { get; set; }

    public int? KraCompetenciesId { get; set; }

    public string? ActivityWhat { get; set; }

    public string? ActivityHow { get; set; }

    public DateTime? ActivityWhen { get; set; }

    public int? SerialNo { get; set; }

    public virtual KraCompetencies? KraCompetencies { get; set; }
}
