using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencySectionLevel
{
    public int Id { get; set; }

    public int CompSectionId { get; set; }

    public string? Description { get; set; }

    public int? CompetencyLevelId { get; set; }

    public virtual CompetencyLevel? CompetencyLevel { get; set; }
}
