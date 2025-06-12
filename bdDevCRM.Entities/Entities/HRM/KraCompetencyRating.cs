using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraCompetencyRating
{
    public int KraCompetencyId { get; set; }

    public int? CompetencyId { get; set; }

    public int? CompetencyAreaId { get; set; }

    public int? SectionId { get; set; }

    public int? SectionLevelId { get; set; }

    public int? KraRating { get; set; }

    public int? KraCompetenciesId { get; set; }

    public virtual KraCompetencies? KraCompetencies { get; set; }
}
