using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyLevelAndGradeMap
{
    public int Id { get; set; }

    public int? CompetencyLevelId { get; set; }

    public int? GradeId { get; set; }

    public virtual CompetencyLevel? CompetencyLevel { get; set; }

    public virtual GradeType? Grade { get; set; }
}
