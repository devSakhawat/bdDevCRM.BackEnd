using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyLevel
{
    public int LevelId { get; set; }

    public string? LevelTitle { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<CompetencyLevelAndGradeMap> CompetencyLevelAndGradeMap { get; set; } = new List<CompetencyLevelAndGradeMap>();

    public virtual ICollection<CompetencySectionLevel> CompetencySectionLevel { get; set; } = new List<CompetencySectionLevel>();
}
