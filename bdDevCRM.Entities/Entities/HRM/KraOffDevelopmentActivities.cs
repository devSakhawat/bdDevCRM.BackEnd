using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraOffDevelopmentActivities
{
    public int OffDevActivityId { get; set; }

    public int? KraCompetenciesId { get; set; }

    public string? TrainingSource { get; set; }

    public string? TrainingName { get; set; }

    /// <summary>
    /// 1=High,2=Medium,3=Low
    /// </summary>
    public int? Priority { get; set; }

    public int? TrainingInfoId { get; set; }

    public int? TrainingSourceId { get; set; }

    public virtual KraCompetencies? KraCompetencies { get; set; }
}
